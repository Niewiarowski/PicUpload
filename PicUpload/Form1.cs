using Interceptor;
using Interceptor.Communication;
using Ionic.Zlib;
using PicUpload.Camera;
using PicUpload.Camera.Layers;
using PicUpload.Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicUpload
{
    public partial class Form1 : Form
    {
        private readonly HabboInterceptor _interceptor;
        Bitmap image;

        List<int> Posters = new List<int>();

        bool IsScanning;
        bool IsWaitingForPosition;
        bool HasStartingPoint;
        bool Alternative;

        char rotation;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            _interceptor = new HabboInterceptor();
            _interceptor.Start();

            Enabled = false;

            _interceptor.DisassembleCompleted += () =>
            {
                Enabled = true;
                Text = "PicUpload";
                return Task.CompletedTask;
            };

            _interceptor.Disconnnected += () =>
            {
                Enabled = false;
                MessageBox.Show("Disconnected...");
                return Task.CompletedTask;
            };

            _interceptor.IncomingAttach(p => p.Hash == 15200078245527654, PreviewRecieved);
            _interceptor.IncomingAttach(p => p.Hash == 14073959295615033, UploadError);
            _interceptor.OutgoingAttach<MovePoster>(MovePicture);
            _interceptor.OutgoingAttach<PlacePoster>(PlacePicture);
        }

        private void SelectImage(object sender, EventArgs e)
        {
            image?.Dispose();

            AvailablePictures.Items.Clear();

            OpenFileDialog fileDialog = new OpenFileDialog { Title = "Select an image to upload" };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var tempBitmap = new Bitmap(fileDialog.FileName);

                if ((tempBitmap.Width == this.columnControl.Value * 15 && tempBitmap.Height == rowControl.Value * 15) || MessageBox.Show("Do you want the image to be automatically resized?", "Resize", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int width = (int)(this.columnControl.Value * 15);
                    int height = (int)(rowControl.Value * 15);

                    image = new Bitmap(width, height);
                    using (Graphics gr = Graphics.FromImage(image))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(tempBitmap, new Rectangle(0, 0, width, height));
                    }

                    tempBitmap.Dispose();
                }
                else
                {
                    image = tempBitmap;
                }

                int rows = (int)Math.Ceiling(image.Height / 15.0), columns = (int)Math.Ceiling(image.Width / 15.0);

                Text = string.Format("PicUpload | {0}x{1} | {2}c", columns, rows, (rows * columns) * 2);

                for (int x = 0; x < columns; x++)
                    for (int y = 0; y < rows; y++)
                        AvailablePictures.Items.Add($"{x + 1},{y + 1}");
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            Posters.Clear();

            IsScanning = false;
            HasStartingPoint = false;
            button5.Text = "Scan";
        }

        private async void StartAligningAsync(object sender, EventArgs e)
        {
            if (IsScanning || Posters.Count != columnControl.Value * rowControl.Value && button4.Text != "Aligning...")
                return;

            Posters = Posters.OrderBy(id => id).ToList();

            button4.Text = "Aligning...";

            bool isRight = rotation == 'r';

            int limit = Alternative ? 32 : 16;
            int height = Alternative ? 16 : 8;
            int width = Alternative ? 16 : 8;
            int heightAfter = (Alternative ? 8 : 4) * (isRight ? 1 : -1);

            for (int currentColumn = 0; currentColumn < columnControl.Value; currentColumn++)
                for (int currentRow = 0; currentRow < rowControl.Value; currentRow++)
                {
                    int tempL1 = (int)offsetX.Value + (width * currentColumn);
                    MovePoster movePoster = new MovePoster
                    {
                        Id = Posters[(currentColumn * (int)rowControl.Value) + currentRow],
                        Position = string.Format(":w={0},{1} l={2},{3} {4}",
                        (int)W1Offset.Value,
                        (int)W2Offset.Value,
                        tempL1,
                        (int)(offsetY.Value + ((currentRow * height) + (currentColumn * heightAfter))),
                        rotation)
                    };

                    await _interceptor.SendToServerAsync(movePoster);
                    await Task.Delay(50);
                }

            button4.Text = "Align";
        }

        private void StartScan(object sender, EventArgs e)
        {
            if (IsWaitingForPosition)
                return;

            Posters.Clear();
            IsScanning = true;
            button5.Text = "Scanning...";
            HasStartingPoint = false;
        }

        private void ToggleAlignment(object sender, EventArgs e)
        {
            Alternative = !Alternative;
            button6.Text = Alternative ? "Alternative" : "Regular";
        }

        public void HandlePoster(int id, string[] data)
        {
            if (!Posters.Contains(id))
                Posters.Add(id);

            if (!HasStartingPoint || IsWaitingForPosition)
            {
                rotation = data[2][0];

                string[] Ws = data[0].Split(',');

                bool temp = IsScanning;
                IsScanning = true;

                W1Offset.Value = int.Parse(Ws[0]);
                W2Offset.Value = int.Parse(Ws[1]);
                
                string[] Ls = data[1].Split(',');
                offsetX.Value = int.Parse(Ls[0]);
                offsetY.Value = int.Parse(Ls[1]);

                IsScanning = temp;

                HasStartingPoint = true;
                IsWaitingForPosition = false;

                button7.Text = "Move";
            }

            if (Posters.Count >= columnControl.Value * rowControl.Value)
            {
                button5.Text = "Scan";
                IsScanning = false;
            }
        }

        private void WaitForMove(object sender, EventArgs e)
        {
            if (IsScanning)
                return;

            IsWaitingForPosition = true;

            button7.Text = "Waiting...";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public async void UpdatePosition()
        {
            if (!IsScanning && Posters.Count == columnControl.Value * rowControl.Value)
            {
                MovePoster movePoster = new MovePoster
                {
                    Id = Posters[0],
                    Position = string.Format(":w={0},{1} l={2},{3} {4}",
                    (int)W1Offset.Value,
                    (int)W2Offset.Value,
                    (int)offsetX.Value,
                    (int)offsetY.Value,
                    rotation)
                };

                await _interceptor.SendToServerAsync(movePoster);

                if (Posters.Count >= 4)
                {
                    int height = Alternative ? 16 : 8;
                    int width = Alternative ? 16 : 8;
                    int heightAfter = (Alternative ? 8 : 4) * (rotation == 'r' ? 1 : -1);

                    int columns = (int)columnControl.Value - 1;
                    int rows = (int)rowControl.Value - 1;

                    await Task.Delay(50);

                    movePoster.Id = Posters[1];
                    movePoster.Position = string.Format(":w={0},{1} l={2},{3} {4}",
                       (int)W1Offset.Value,
                       (int)W2Offset.Value,
                       (int)offsetX.Value + (columns * width),
                       (int)offsetY.Value + (columns * heightAfter),
                       rotation);

                    await _interceptor.SendToServerAsync(movePoster);

                    await Task.Delay(50);

                    movePoster.Id = Posters[2];
                    movePoster.Position = string.Format(":w={0},{1} l={2},{3} {4}",
                       (int)W1Offset.Value,
                       (int)W2Offset.Value,
                       (int)offsetX.Value,
                       (int)(offsetY.Value + (rows * height)),
                       rotation);

                    await _interceptor.SendToServerAsync(movePoster);

                    await Task.Delay(50);

                    movePoster.Id = Posters[3];
                    movePoster.Position = string.Format(":w={0},{1} l={2},{3} {4}",
                       (int)W1Offset.Value,
                       (int)W2Offset.Value,
                       (int)offsetX.Value + (columns * width),
                       (int)(offsetY.Value + ((rows * height) + (columns * heightAfter))),
                       rotation);

                    await _interceptor.SendToServerAsync(movePoster);
                }
            }
        }

        private void positionValueChanged(object sender, EventArgs e)
            => UpdatePosition();

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog { Title = "Select a position file", Filter = "Image Position Files (*.ipf)|*.ipf" };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream file = File.Open(fileDialog.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(file);

                IsScanning = true;

                columnControl.Value = br.ReadInt32();
                rowControl.Value = br.ReadInt32();
                W1Offset.Value = br.ReadInt32();
                W2Offset.Value = br.ReadInt32();
                offsetX.Value = br.ReadInt32();
                offsetY.Value = br.ReadInt32();
                rotation = br.ReadChar();

                int posterCount = br.ReadInt32();

                Posters.Clear();

                for(int i = 0; i < posterCount; i++)
                    Posters.Add(br.ReadInt32());

                br.Close();
                file.Close();

                IsScanning = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "Save position file",
                Filter = "Image Position Files (*.ipf)|*.ipf"
            };

            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                Stream file = saveFile.OpenFile();

                if(file != null)
                {
                    BinaryWriter bw = new BinaryWriter(file);

                    bw.Write((int)columnControl.Value);
                    bw.Write((int)rowControl.Value);
                    bw.Write((int)W1Offset.Value);
                    bw.Write((int)W2Offset.Value);
                    bw.Write((int)offsetX.Value);
                    bw.Write((int)offsetY.Value);
                    bw.Write(rotation);

                    bw.Write(Posters.Count);

                    for(int i = 0; i < Posters.Count; i++)
                    {
                        bw.Write(Posters[i]);
                    }

                    bw.Close();
                    file.Close();
                }
            }
        }

        public Task<bool> MovePicture(MovePoster e)
        {

            if (IsScanning || IsWaitingForPosition)
                HandlePoster(e.Id, e.Position.Replace(":w=", string.Empty).Replace("l=", string.Empty).Split(' '));

            return Task.FromResult(true);
        }

        public Task<bool> PlacePicture(PlacePoster e)
        {
            if (IsScanning || IsWaitingForPosition)
            {
                string[] data = e.Position.Replace(":w=", string.Empty).Replace("l=", string.Empty).Split(' ');

                HandlePoster(int.Parse(data[0]), data.Skip(1).ToArray());
            }

            return Task.FromResult(true);
        }

        int IndexToUpload = 0;
        bool isPurchasingAll = false;
        public async Task PreviewRecieved(Packet e)
        {
            if (isPurchasingAll)
            {
                IndexToUpload++;
                if (IndexToUpload >= AvailablePictures.Items.Count)
                {
                    isPurchasingAll = false;
                    return;
                }

                Thread.Sleep(1000);
                AvailablePictures.SelectedIndex = IndexToUpload;
                await _interceptor.SendToServerAsync(new PurchasePoster());
                await UploadImage();
            }
        }

        public async Task UploadError(Packet e)
        {
            await Task.Delay(2000);
            await UploadImage();
        }

        private async void PurchaseAllImages(object sender, EventArgs e)
        {
            isPurchasingAll = true;
            AvailablePictures.SelectedIndex = 0;
            await UploadImage();
        }

        public async void UploadImage(object sender, EventArgs e) => await UploadImage();
        private async Task UploadImage()
        {
            if(AvailablePictures.SelectedIndex > -1)
            {
                string[] selection = ((string)AvailablePictures.SelectedItem).Split(',');

                int row = int.Parse(selection[0]);
                int column = int.Parse(selection[1]);

                HPhoto photo = new HPhoto { RoomId = 100 };

                int startingX = (row - 1) * 15;
                int startingY = (column - 1) * 15;
                
                for (int x = startingX; x < Math.Min(startingX + 15, image.Width); x++)
                {
                    for (int y = startingY; y < Math.Min(startingY + 15, image.Height); y++)
                    {
                        var rawColor = image.GetPixel(x, y);
                        if (rawColor.A == 0)
                            continue;

                        int color = int.Parse(ColorTranslator.ToHtml(rawColor).TrimStart('#'), NumberStyles.HexNumber);
                        photo.Sprites.Add(new Sprite
                        {
                            Name = "pirate_mast4grp_32_b_0_0",
                            X = (x - startingX) * 20,
                            Y = (y - startingY) * 20,
                            Z = 0,
                            Color = color == 0 ? color + 1 : color
                        });
                    }
                }

                photo.Planes.Add(new Plane
                {
                    Color = 0,
                    CornerPoints =
                    {
                        Point.Empty,
                        Point.Empty,
                        Point.Empty,
                        Point.Empty
                    },
                    Z = photo.Sprites.Count * 1.776104
                });

                byte[] compressedPhoto = photo.Compress();
                await _interceptor.SendToServerAsync(new PreviewPoster
                {
                    CompressedData = compressedPhoto
                });
            }
        }
    }
}
