using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using PicUpload.Camera.Layers;
using Ionic.Zlib;

namespace PicUpload.Camera
{
    [DataContract]
    public class HPhoto
    {
        private long _timestamp;

        private static readonly DateTime _unixEpoch;
        private static readonly DataContractJsonSerializer _serializer;

        private List<Plane> _planes;
        [DataMember(Name = "planes", Order = 0)]
        public List<Plane> Planes
        {
            get { return _planes ?? (_planes = new List<Plane>()); }
            set { _planes = value; }
        }

        private List<Sprite> _sprites;
        [DataMember(Name = "sprites", Order = 1)]
        public List<Sprite> Sprites
        {
            get { return _sprites ?? (_sprites = new List<Sprite>()); }
            set { _sprites = value; }
        }

        [DataMember(Name = "modifiers", Order = 2)]
        public object Modifiers = new object();

        [DataMember(Name = "filters", Order = 3)]
        public List<object> Filters = new List<object>();

        [DataMember(Name = "roomid", Order = 4)]
        public int RoomId { get; set; }

        [DataMember(Name = "zoom", Order = 5, EmitDefaultValue = false)]
        public int Zoom { get; set; }

        [DataMember(Name = "status", Order = 6, EmitDefaultValue = false)]
        public long Status { get; set; }

        [DataMember(Name = "timestamp", Order = 7, EmitDefaultValue = false)]
        public long Timestamp { get; set; }

        [DataMember(Name = "checksum", Order = 8, EmitDefaultValue = false)]
        public long Checksum { get; set; }

        static HPhoto()
        {
            _serializer = new DataContractJsonSerializer(typeof(HPhoto));
            _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        private long GetStatus(ref long mod)
        {
            _timestamp -= (mod = _timestamp % 100);
            return _timestamp / 100 % 23 + 0;
        }
        private long GetChecksum(long mod, long key)
        {
            return (mod + 13) * (key + 29);
        }
        private long GetTimestamp(string blob, long key)
        {
            byte[] data = Encoding.Default.GetBytes(blob);
            return _timestamp + Calculate(data, key, RoomId);
        }
        private long Calculate(byte[] data, long key, int roomId)
        {
            long tKey = key, tRoomId = roomId;
            for (int i = 0; i < data.Length; i++)
            {
                tKey = (tKey + data[i]) % 255;
                tRoomId = (tKey + tRoomId) % 255;
            }
            return (tKey + tRoomId) % 100;
        }

        public string ToJson()
        {
            string json = string.Empty;
            using (var jsonStream = new MemoryStream())
            {
                _serializer.WriteObject(jsonStream, this);

                json = Encoding.UTF8.GetString(jsonStream.ToArray());
                json = json.Remove(json.Length - 1, 1);
            }

            return $"{json}}}";
        }
        public List<long> GetAll(bool genstatus = true, bool gentimestamp = true, bool genchecksum = true, bool customstatus = false, bool customtimestamp = false, bool customchecksum = false, long status = 0, long timestamp = 0, long checksum = 0)
        {
            List<long> values = new List<long>();

            HPhoto tmp = new HPhoto
            {
                Planes = Planes,
                RoomId = RoomId,
                Sprites = Sprites,
                Zoom = Zoom
            };

            string json = string.Empty;
            using (var jsonStream = new MemoryStream())
            {
                _serializer.WriteObject(jsonStream, tmp);

                json = Encoding.UTF8.GetString(jsonStream.ToArray());
                json = json.Remove(json.Length - 1, 1);
            }

            _timestamp = (long)(
                DateTime.UtcNow - _unixEpoch).TotalMilliseconds;

            long mod = 0;
            if (!customstatus)
            {
                if (genstatus)
                {
                    status = GetStatus(ref mod);
                }
                else
                {
                    status = this.Status;
                }
            }
            values.Add(status);
            json += ",\"status\":" + status;

            long key = json.Length;
            key = (key + _timestamp / 100 * 17) % 1493;

            if (!customstatus)
            {
                if (genstatus)
                {
                    timestamp = GetTimestamp(json, key);
                }
                else
                {
                    status = this.Status;
                }
            }
            values.Add(timestamp);
            if (!customstatus)
            {
                if (genstatus)
                {
                    checksum = GetChecksum(mod, key);
                }
                else
                {
                    status = this.Status;
                }
            }
            values.Add(checksum);

            return values;
        }

        public byte[] Compress()
        {
            var checksumValues = GetAll();

            Status = checksumValues[0];
            Timestamp = checksumValues[1];
            Checksum = checksumValues[2];

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(ToJson())))
            using (var compressStream = new MemoryStream())
            using (var compressor = new ZlibStream(compressStream, CompressionMode.Compress, CompressionLevel.BestCompression))
            {
                ms.CopyTo(compressor);
                compressor.Close();

                return compressStream.ToArray();
            }
        }

        public void Save(string path)
        {
            File.WriteAllText(path, ToJson());
        }

        public static HPhoto Load(string path)
        {
            using (var fileStream = File.Open(path, FileMode.Open))
                return (HPhoto)_serializer.ReadObject(fileStream);
        }
        public static HPhoto Create(string json)
        {
            byte[] data = Encoding.UTF8.GetBytes(json);
            using (var memoryStream = new MemoryStream(data))
                return (HPhoto)_serializer.ReadObject(memoryStream);
        }
    }
}