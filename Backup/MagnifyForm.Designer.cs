namespace USB_Scope
{
    partial class MagnifyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagnifyForm));
            this.btnResetThreshold = new System.Windows.Forms.Button();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.cmbChannel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numThreshold = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblYScaleMinus = new System.Windows.Forms.Label();
            this.lblYScalePlus = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearScope = new System.Windows.Forms.Button();
            this.lblUV = new System.Windows.Forms.Label();
            this.cmbNumShow = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trkVolume = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkAudioEnable = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnResetThreshold
            // 
            this.btnResetThreshold.Location = new System.Drawing.Point(325, 127);
            this.btnResetThreshold.Name = "btnResetThreshold";
            this.btnResetThreshold.Size = new System.Drawing.Size(84, 23);
            this.btnResetThreshold.TabIndex = 4;
            this.btnResetThreshold.Text = "Reset to Zero";
            this.btnResetThreshold.UseVisualStyleBackColor = true;
            this.btnResetThreshold.Click += new System.EventHandler(this.btnResetThreshold_Click);
            // 
            // lblThreshold
            // 
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Location = new System.Drawing.Point(327, 85);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(50, 13);
            this.lblThreshold.TabIndex = 1;
            this.lblThreshold.Text = "threshold";
            // 
            // cmbChannel
            // 
            this.cmbChannel.FormattingEnabled = true;
            this.cmbChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cmbChannel.Location = new System.Drawing.Point(330, 26);
            this.cmbChannel.Name = "cmbChannel";
            this.cmbChannel.Size = new System.Drawing.Size(55, 21);
            this.cmbChannel.TabIndex = 1;
            this.cmbChannel.SelectedIndexChanged += new System.EventHandler(this.cmbChannel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(327, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "channel";
            // 
            // numThreshold
            // 
            this.numThreshold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.numThreshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numThreshold.Location = new System.Drawing.Point(330, 101);
            this.numThreshold.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numThreshold.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            -2147483648});
            this.numThreshold.Name = "numThreshold";
            this.numThreshold.Size = new System.Drawing.Size(53, 20);
            this.numThreshold.TabIndex = 3;
            this.numThreshold.ValueChanged += new System.EventHandler(this.numThreshold_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "-0.5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "0.5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "1.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "1.5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "2.0 msec";
            // 
            // lblYScaleMinus
            // 
            this.lblYScaleMinus.AutoSize = true;
            this.lblYScaleMinus.Location = new System.Drawing.Point(271, 299);
            this.lblYScaleMinus.Name = "lblYScaleMinus";
            this.lblYScaleMinus.Size = new System.Drawing.Size(44, 13);
            this.lblYScaleMinus.TabIndex = 12;
            this.lblYScaleMinus.Text = "-100 uV";
            // 
            // lblYScalePlus
            // 
            this.lblYScalePlus.AutoSize = true;
            this.lblYScalePlus.Location = new System.Drawing.Point(271, 12);
            this.lblYScalePlus.Name = "lblYScalePlus";
            this.lblYScalePlus.Size = new System.Drawing.Size(47, 13);
            this.lblYScalePlus.TabIndex = 13;
            this.lblYScalePlus.Text = "+100 uV";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(271, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(19, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 300);
            this.panel1.TabIndex = 15;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // btnClearScope
            // 
            this.btnClearScope.Location = new System.Drawing.Point(325, 319);
            this.btnClearScope.Name = "btnClearScope";
            this.btnClearScope.Size = new System.Drawing.Size(84, 23);
            this.btnClearScope.TabIndex = 0;
            this.btnClearScope.Text = "Clear Scope";
            this.btnClearScope.UseVisualStyleBackColor = true;
            this.btnClearScope.Click += new System.EventHandler(this.btnClearScope_Click);
            // 
            // lblUV
            // 
            this.lblUV.AutoSize = true;
            this.lblUV.Location = new System.Drawing.Point(386, 103);
            this.lblUV.Name = "lblUV";
            this.lblUV.Size = new System.Drawing.Size(20, 13);
            this.lblUV.TabIndex = 17;
            this.lblUV.Text = "uV";
            // 
            // cmbNumShow
            // 
            this.cmbNumShow.FormattingEnabled = true;
            this.cmbNumShow.Items.AddRange(new object[] {
            "10",
            "20",
            "30"});
            this.cmbNumShow.Location = new System.Drawing.Point(330, 53);
            this.cmbNumShow.Name = "cmbNumShow";
            this.cmbNumShow.Size = new System.Drawing.Size(41, 21);
            this.cmbNumShow.TabIndex = 2;
            this.cmbNumShow.SelectedIndexChanged += new System.EventHandler(this.cmbNumShow_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(278, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "show last";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(371, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "spikes";
            // 
            // trkVolume
            // 
            this.trkVolume.LargeChange = 1;
            this.trkVolume.Location = new System.Drawing.Point(17, 26);
            this.trkVolume.Maximum = 15;
            this.trkVolume.Minimum = 1;
            this.trkVolume.Name = "trkVolume";
            this.trkVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkVolume.Size = new System.Drawing.Size(45, 104);
            this.trkVolume.TabIndex = 21;
            this.trkVolume.Value = 5;
            this.trkVolume.Scroll += new System.EventHandler(this.trkVolume_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.chkAudioEnable);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.trkVolume);
            this.groupBox1.Location = new System.Drawing.Point(325, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(84, 157);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(50, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "min";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "max";
            // 
            // chkAudioEnable
            // 
            this.chkAudioEnable.AutoSize = true;
            this.chkAudioEnable.Location = new System.Drawing.Point(14, 132);
            this.chkAudioEnable.Name = "chkAudioEnable";
            this.chkAudioEnable.Size = new System.Drawing.Size(59, 17);
            this.chkAudioEnable.TabIndex = 24;
            this.chkAudioEnable.Text = "Enable";
            this.chkAudioEnable.UseVisualStyleBackColor = true;
            this.chkAudioEnable.CheckedChanged += new System.EventHandler(this.chkAudioEnable_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "volume";
            // 
            // MagnifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 374);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbNumShow);
            this.Controls.Add(this.lblUV);
            this.Controls.Add(this.btnClearScope);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblYScalePlus);
            this.Controls.Add(this.lblYScaleMinus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numThreshold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbChannel);
            this.Controls.Add(this.lblThreshold);
            this.Controls.Add(this.btnResetThreshold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MagnifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Spike Scope";
            this.Load += new System.EventHandler(this.MagnifyForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MagnifyForm_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MagnifyForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnResetThreshold;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.ComboBox cmbChannel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numThreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblYScaleMinus;
        private System.Windows.Forms.Label lblYScalePlus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearScope;
        private System.Windows.Forms.Label lblUV;
        private System.Windows.Forms.ComboBox cmbNumShow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trkVolume;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkAudioEnable;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}