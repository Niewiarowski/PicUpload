namespace PicUpload
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AvailablePictures = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.columnControl = new System.Windows.Forms.NumericUpDown();
            this.rowControl = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.offsetX = new System.Windows.Forms.NumericUpDown();
            this.offsetY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.W1Offset = new System.Windows.Forms.NumericUpDown();
            this.W2Offset = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.columnControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.W1Offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.W2Offset)).BeginInit();
            this.SuspendLayout();
            // 
            // AvailablePictures
            // 
            this.AvailablePictures.FormattingEnabled = true;
            this.AvailablePictures.Location = new System.Drawing.Point(12, 206);
            this.AvailablePictures.Name = "AvailablePictures";
            this.AvailablePictures.Size = new System.Drawing.Size(260, 108);
            this.AvailablePictures.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.UploadImage);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SelectImage);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 177);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Reset);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(147, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Align";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.StartAligningAsync);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 41);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Scan";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.StartScan);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 70);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(125, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Regular";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.ToggleAlignment);
            // 
            // columnControl
            // 
            this.columnControl.Location = new System.Drawing.Point(158, 112);
            this.columnControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.columnControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.columnControl.Name = "columnControl";
            this.columnControl.Size = new System.Drawing.Size(43, 20);
            this.columnControl.TabIndex = 7;
            this.columnControl.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            //this.columnControl.ValueChanged += new System.EventHandler(this.columnControl_ValueChanged);
            // 
            // rowControl
            // 
            this.rowControl.Location = new System.Drawing.Point(214, 112);
            this.rowControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.rowControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rowControl.Name = "rowControl";
            this.rowControl.Size = new System.Drawing.Size(43, 20);
            this.rowControl.TabIndex = 8;
            this.rowControl.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 10;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(147, 41);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(125, 23);
            this.button7.TabIndex = 11;
            this.button7.Text = "Move";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.WaitForMove);
            // 
            // offsetX
            // 
            this.offsetX.Location = new System.Drawing.Point(31, 112);
            this.offsetX.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.offsetX.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.offsetX.Name = "offsetX";
            this.offsetX.Size = new System.Drawing.Size(43, 20);
            this.offsetX.TabIndex = 13;
            this.offsetX.ValueChanged += new System.EventHandler(this.positionValueChanged);
            // 
            // offsetY
            // 
            this.offsetY.Location = new System.Drawing.Point(80, 112);
            this.offsetY.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.offsetY.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.offsetY.Name = "offsetY";
            this.offsetY.Size = new System.Drawing.Size(43, 20);
            this.offsetY.TabIndex = 14;
            this.offsetY.ValueChanged += new System.EventHandler(this.positionValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "X Offset";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Y Offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Height";
            // 
            // W1Offset
            // 
            this.W1Offset.Location = new System.Drawing.Point(32, 149);
            this.W1Offset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.W1Offset.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.W1Offset.Name = "W1Offset";
            this.W1Offset.Size = new System.Drawing.Size(43, 20);
            this.W1Offset.TabIndex = 19;
            this.W1Offset.ValueChanged += new System.EventHandler(this.positionValueChanged);
            // 
            // W2Offset
            // 
            this.W2Offset.Location = new System.Drawing.Point(81, 149);
            this.W2Offset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.W2Offset.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.W2Offset.Name = "W2Offset";
            this.W2Offset.Size = new System.Drawing.Size(43, 20);
            this.W2Offset.TabIndex = 20;
            this.W2Offset.ValueChanged += new System.EventHandler(this.positionValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "W1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "W2";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(147, 138);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(60, 32);
            this.button8.TabIndex = 23;
            this.button8.Text = "Open";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(207, 138);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(65, 32);
            this.button9.TabIndex = 24;
            this.button9.Text = "Save";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(12, 349);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(260, 23);
            this.button10.TabIndex = 25;
            this.button10.Text = "Purchase All";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.PurchaseAllImages);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 377);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.W2Offset);
            this.Controls.Add(this.W1Offset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.offsetY);
            this.Controls.Add(this.offsetX);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rowControl);
            this.Controls.Add(this.columnControl);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AvailablePictures);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "PicUpload";
            //this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.columnControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.W1Offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.W2Offset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AvailablePictures;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.NumericUpDown columnControl;
        private System.Windows.Forms.NumericUpDown rowControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.NumericUpDown offsetX;
        private System.Windows.Forms.NumericUpDown offsetY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown W1Offset;
        private System.Windows.Forms.NumericUpDown W2Offset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
    }
}

