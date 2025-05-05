namespace USB_Scope
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.tmrDraw = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblYScale = new System.Windows.Forms.Label();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.lblXScale = new System.Windows.Forms.Label();
            this.btnXZoomIn = new System.Windows.Forms.Button();
            this.btnXZoomOut = new System.Windows.Forms.Button();
            this.txtHPF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtSaveFilename = new System.Windows.Forms.TextBox();
            this.btnSelectFilename = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEnableHPF = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numMaxMinutes = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnChanSelectionWindow = new System.Windows.Forms.Button();
            this.tmrSynthData = new System.Windows.Forms.Timer(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.panel_lfp = new System.Windows.Forms.Panel();
            this.panel_psd = new System.Windows.Forms.Panel();
            this.btn_psd_amp_up = new System.Windows.Forms.Button();
            this.btn_psd_amp_down = new System.Windows.Forms.Button();
            this.panel_thr_lfp = new System.Windows.Forms.Panel();
            this.panel_thr_speed = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnZoomInAmp = new System.Windows.Forms.Button();
            this.btnZoomOutAmp = new System.Windows.Forms.Button();
            this.btnZoomInSpeed = new System.Windows.Forms.Button();
            this.btnZoomOutSpeed = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrMyStatusStrip = new System.Windows.Forms.StatusStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSettle = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel_psd_noise = new System.Windows.Forms.Panel();
            this.btn_psdhigh_amp_up = new System.Windows.Forms.Button();
            this.btn_psdhigh_amp_down = new System.Windows.Forms.Button();
            this.btnNotchFilterDisable = new System.Windows.Forms.RadioButton();
            this.btnNotchFilter50Hz = new System.Windows.Forms.RadioButton();
            this.btnNotchFilter60Hz = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.useAmp = new System.Windows.Forms.CheckBox();
            this.useSpeed = new System.Windows.Forms.CheckBox();
            this.checkBoxAmpThreshold = new System.Windows.Forms.CheckBox();
            this.checkBoxSpeedThreshold = new System.Windows.Forms.CheckBox();
            this.groupBoxAmpThreshold = new System.Windows.Forms.GroupBox();
            this.radioButton_Lwr_AmpThr = new System.Windows.Forms.RadioButton();
            this.radioButton_Higher_AmpThr = new System.Windows.Forms.RadioButton();
            this.groupBoxSpeedThreshold = new System.Windows.Forms.GroupBox();
            this.radioButton_Lwr_SpeedThr = new System.Windows.Forms.RadioButton();
            this.radioButton_Higher_SpeedThr = new System.Windows.Forms.RadioButton();
            this.groupBoxStimParam = new System.Windows.Forms.GroupBox();
            this.radioButtonEnableAllStim = new System.Windows.Forms.RadioButton();
            this.radioButtonEnableSpeedStim = new System.Windows.Forms.RadioButton();
            this.radioButtonEnableLFPStim = new System.Windows.Forms.RadioButton();
            this.groupBoxStimDesign = new System.Windows.Forms.GroupBox();
            this.radioButtonStimDesignNotSPEED = new System.Windows.Forms.RadioButton();
            this.radioButtonStimDesignNOTLFP = new System.Windows.Forms.RadioButton();
            this.radioButtonStimDesignOR = new System.Windows.Forms.RadioButton();
            this.radioButtonStimDesignAND = new System.Windows.Forms.RadioButton();
            this.lowerFilterNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.higherFilterDenUpDown = new System.Windows.Forms.NumericUpDown();
            this.lowerFilterDenUpDown = new System.Windows.Forms.NumericUpDown();
            this.higherFilterNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMinutes)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel_psd.SuspendLayout();
            this.sbrMyStatusStrip.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxAmpThreshold.SuspendLayout();
            this.groupBoxSpeedThreshold.SuspendLayout();
            this.groupBoxStimParam.SuspendLayout();
            this.groupBoxStimDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lowerFilterNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.higherFilterDenUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerFilterDenUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.higherFilterNumUpDown)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(693, 506);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(693, 535);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(693, 564);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // tmrDraw
            // 
            this.tmrDraw.Enabled = true;
            this.tmrDraw.Interval = 5;
            this.tmrDraw.Tick += new System.EventHandler(this.tmrDraw_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(102, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.aboutToolStripMenuItem1.Text = "&About Intan Demo";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // lblYScale
            // 
            this.lblYScale.AutoSize = true;
            this.lblYScale.Location = new System.Drawing.Point(823, 38);
            this.lblYScale.Name = "lblYScale";
            this.lblYScale.Size = new System.Drawing.Size(41, 13);
            this.lblYScale.TabIndex = 34;
            this.lblYScale.Text = "100 uV";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(812, 83);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(63, 23);
            this.btnZoomOut.TabIndex = 23;
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(812, 54);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(63, 23);
            this.btnZoomIn.TabIndex = 22;
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // lblXScale
            // 
            this.lblXScale.AutoSize = true;
            this.lblXScale.Location = new System.Drawing.Point(703, 441);
            this.lblXScale.Name = "lblXScale";
            this.lblXScale.Size = new System.Drawing.Size(47, 13);
            this.lblXScale.TabIndex = 37;
            this.lblXScale.Text = "10 msec";
            // 
            // btnXZoomIn
            // 
            this.btnXZoomIn.Location = new System.Drawing.Point(663, 457);
            this.btnXZoomIn.Name = "btnXZoomIn";
            this.btnXZoomIn.Size = new System.Drawing.Size(63, 23);
            this.btnXZoomIn.TabIndex = 29;
            this.btnXZoomIn.Text = "Zoom In";
            this.btnXZoomIn.UseVisualStyleBackColor = true;
            this.btnXZoomIn.Click += new System.EventHandler(this.btnXZoomIn_Click);
            // 
            // btnXZoomOut
            // 
            this.btnXZoomOut.Location = new System.Drawing.Point(732, 457);
            this.btnXZoomOut.Name = "btnXZoomOut";
            this.btnXZoomOut.Size = new System.Drawing.Size(63, 23);
            this.btnXZoomOut.TabIndex = 30;
            this.btnXZoomOut.Text = "Zoom Out";
            this.btnXZoomOut.UseVisualStyleBackColor = true;
            this.btnXZoomOut.Click += new System.EventHandler(this.btnXZoomOut_Click);
            // 
            // txtHPF
            // 
            this.txtHPF.AcceptsReturn = true;
            this.txtHPF.Location = new System.Drawing.Point(40, 69);
            this.txtHPF.Name = "txtHPF";
            this.txtHPF.Size = new System.Drawing.Size(50, 20);
            this.txtHPF.TabIndex = 1;
            this.txtHPF.Text = "1.0";
            this.txtHPF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHPF.TextChanged += new System.EventHandler(this.txtHPF_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Hz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Software High-Pass Filter Cutoff:";
            // 
            // txtSaveFilename
            // 
            this.txtSaveFilename.Location = new System.Drawing.Point(16, 77);
            this.txtSaveFilename.Name = "txtSaveFilename";
            this.txtSaveFilename.ReadOnly = true;
            this.txtSaveFilename.Size = new System.Drawing.Size(180, 20);
            this.txtSaveFilename.TabIndex = 1;
            this.txtSaveFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSelectFilename
            // 
            this.btnSelectFilename.Location = new System.Drawing.Point(76, 48);
            this.btnSelectFilename.Name = "btnSelectFilename";
            this.btnSelectFilename.Size = new System.Drawing.Size(120, 23);
            this.btnSelectFilename.TabIndex = 0;
            this.btnSelectFilename.Text = "Select Base Filename";
            this.btnSelectFilename.UseVisualStyleBackColor = true;
            this.btnSelectFilename.Click += new System.EventHandler(this.btnSelectFilename_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Enabled = false;
            this.btnRecord.Location = new System.Drawing.Point(121, 158);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 3;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEnableHPF);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtHPF);
            this.groupBox1.Location = new System.Drawing.Point(488, 457);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 97);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Software Offset Removal";
            // 
            // chkEnableHPF
            // 
            this.chkEnableHPF.AutoSize = true;
            this.chkEnableHPF.Checked = true;
            this.chkEnableHPF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableHPF.Location = new System.Drawing.Point(13, 22);
            this.chkEnableHPF.Name = "chkEnableHPF";
            this.chkEnableHPF.Size = new System.Drawing.Size(59, 17);
            this.chkEnableHPF.TabIndex = 0;
            this.chkEnableHPF.Text = "Enable";
            this.chkEnableHPF.UseVisualStyleBackColor = true;
            this.chkEnableHPF.CheckedChanged += new System.EventHandler(this.chkEnableHPF_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "(Date and time stamp will be added)";
            // 
            // numMaxMinutes
            // 
            this.numMaxMinutes.Location = new System.Drawing.Point(106, 124);
            this.numMaxMinutes.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numMaxMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxMinutes.Name = "numMaxMinutes";
            this.numMaxMinutes.Size = new System.Drawing.Size(43, 20);
            this.numMaxMinutes.TabIndex = 2;
            this.numMaxMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMaxMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxMinutes.ValueChanged += new System.EventHandler(this.numMaxMinutes_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Start new file every";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "minutes";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnChanSelectionWindow);
            this.groupBox4.Controls.Add(this.btnSelectFilename);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.numMaxMinutes);
            this.groupBox4.Controls.Add(this.txtSaveFilename);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.btnRecord);
            this.groupBox4.Location = new System.Drawing.Point(812, 479);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(202, 185);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Save to Disk";
            // 
            // btnChanSelectionWindow
            // 
            this.btnChanSelectionWindow.Location = new System.Drawing.Point(76, 18);
            this.btnChanSelectionWindow.Name = "btnChanSelectionWindow";
            this.btnChanSelectionWindow.Size = new System.Drawing.Size(118, 23);
            this.btnChanSelectionWindow.TabIndex = 55;
            this.btnChanSelectionWindow.Text = "Select Channels";
            this.btnChanSelectionWindow.UseVisualStyleBackColor = true;
            this.btnChanSelectionWindow.Click += new System.EventHandler(this.btnChanSelectionWindow_Click);
            // 
            // tmrSynthData
            // 
            this.tmrSynthData.Interval = 30;
            this.tmrSynthData.Tick += new System.EventHandler(this.tmrSynthData_Tick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
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
            this.comboBox1.Location = new System.Drawing.Point(5, 74);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(39, 21);
            this.comboBox1.TabIndex = 38;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // panel_lfp
            // 
            this.panel_lfp.Location = new System.Drawing.Point(53, 29);
            this.panel_lfp.Name = "panel_lfp";
            this.panel_lfp.Size = new System.Drawing.Size(750, 100);
            this.panel_lfp.TabIndex = 40;
            this.panel_lfp.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_lfp_Paint);
            this.panel_lfp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // panel_psd
            // 
            this.panel_psd.Controls.Add(this.btn_psd_amp_up);
            this.panel_psd.Controls.Add(this.btn_psd_amp_down);
            this.panel_psd.Location = new System.Drawing.Point(53, 439);
            this.panel_psd.Name = "panel_psd";
            this.panel_psd.Size = new System.Drawing.Size(300, 225);
            this.panel_psd.TabIndex = 41;
            this.panel_psd.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_psd_Paint);
            // 
            // btn_psd_amp_up
            // 
            this.btn_psd_amp_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_psd_amp_up.Location = new System.Drawing.Point(231, 96);
            this.btn_psd_amp_up.Name = "btn_psd_amp_up";
            this.btn_psd_amp_up.Size = new System.Drawing.Size(66, 29);
            this.btn_psd_amp_up.TabIndex = 43;
            this.btn_psd_amp_up.Text = "Zoom In";
            this.btn_psd_amp_up.UseVisualStyleBackColor = true;
            this.btn_psd_amp_up.Click += new System.EventHandler(this.btn_psd_amp_up_Click);
            // 
            // btn_psd_amp_down
            // 
            this.btn_psd_amp_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_psd_amp_down.Location = new System.Drawing.Point(231, 127);
            this.btn_psd_amp_down.Name = "btn_psd_amp_down";
            this.btn_psd_amp_down.Size = new System.Drawing.Size(66, 29);
            this.btn_psd_amp_down.TabIndex = 44;
            this.btn_psd_amp_down.Text = "Zoom Out";
            this.btn_psd_amp_down.UseVisualStyleBackColor = true;
            this.btn_psd_amp_down.Click += new System.EventHandler(this.btn_psd_amp_down_Click);
            // 
            // panel_thr_lfp
            // 
            this.panel_thr_lfp.Location = new System.Drawing.Point(53, 133);
            this.panel_thr_lfp.Name = "panel_thr_lfp";
            this.panel_thr_lfp.Size = new System.Drawing.Size(750, 150);
            this.panel_thr_lfp.TabIndex = 42;
            this.panel_thr_lfp.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_thr_lfp_Paint);
            this.panel_thr_lfp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_thr_lfp_MouseClick);
            // 
            // panel_thr_speed
            // 
            this.panel_thr_speed.Location = new System.Drawing.Point(53, 287);
            this.panel_thr_speed.Name = "panel_thr_speed";
            this.panel_thr_speed.Size = new System.Drawing.Size(750, 150);
            this.panel_thr_speed.TabIndex = 43;
            this.panel_thr_speed.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_thr_speed_Paint);
            this.panel_thr_speed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_thr_speed_MouseClick);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
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
            this.comboBox2.Location = new System.Drawing.Point(5, 336);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(39, 21);
            this.comboBox2.TabIndex = 45;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // btnZoomInAmp
            // 
            this.btnZoomInAmp.Location = new System.Drawing.Point(812, 231);
            this.btnZoomInAmp.Name = "btnZoomInAmp";
            this.btnZoomInAmp.Size = new System.Drawing.Size(63, 23);
            this.btnZoomInAmp.TabIndex = 46;
            this.btnZoomInAmp.Text = "Zoom In";
            this.btnZoomInAmp.UseVisualStyleBackColor = true;
            this.btnZoomInAmp.Click += new System.EventHandler(this.btnZoomInAmp_Click);
            // 
            // btnZoomOutAmp
            // 
            this.btnZoomOutAmp.Location = new System.Drawing.Point(812, 260);
            this.btnZoomOutAmp.Name = "btnZoomOutAmp";
            this.btnZoomOutAmp.Size = new System.Drawing.Size(63, 23);
            this.btnZoomOutAmp.TabIndex = 47;
            this.btnZoomOutAmp.Text = "Zoom Out";
            this.btnZoomOutAmp.UseVisualStyleBackColor = true;
            this.btnZoomOutAmp.Click += new System.EventHandler(this.btnZoomOutAmp_Click);
            // 
            // btnZoomInSpeed
            // 
            this.btnZoomInSpeed.Location = new System.Drawing.Point(812, 385);
            this.btnZoomInSpeed.Name = "btnZoomInSpeed";
            this.btnZoomInSpeed.Size = new System.Drawing.Size(63, 23);
            this.btnZoomInSpeed.TabIndex = 49;
            this.btnZoomInSpeed.Text = "Zoom In";
            this.btnZoomInSpeed.UseVisualStyleBackColor = true;
            this.btnZoomInSpeed.Click += new System.EventHandler(this.btnZoomInSpeed_Click);
            // 
            // btnZoomOutSpeed
            // 
            this.btnZoomOutSpeed.Location = new System.Drawing.Point(812, 414);
            this.btnZoomOutSpeed.Name = "btnZoomOutSpeed";
            this.btnZoomOutSpeed.Size = new System.Drawing.Size(63, 23);
            this.btnZoomOutSpeed.TabIndex = 50;
            this.btnZoomOutSpeed.Text = "Zoom Out";
            this.btnZoomOutSpeed.UseVisualStyleBackColor = true;
            this.btnZoomOutSpeed.Click += new System.EventHandler(this.btnZoomOutSpeed_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(82, 17);
            this.lblStatus.Text = "Ready to start.";
            // 
            // sbrMyStatusStrip
            // 
            this.sbrMyStatusStrip.AutoSize = false;
            this.sbrMyStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.sbrMyStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.sbrMyStatusStrip.Location = new System.Drawing.Point(102, 3);
            this.sbrMyStatusStrip.Name = "sbrMyStatusStrip";
            this.sbrMyStatusStrip.Size = new System.Drawing.Size(637, 22);
            this.sbrMyStatusStrip.SizingGrip = false;
            this.sbrMyStatusStrip.TabIndex = 3;
            this.sbrMyStatusStrip.Text = "statusStrip1";
            this.sbrMyStatusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.sbrMyStatusStrip_ItemClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSettle);
            this.groupBox3.Location = new System.Drawing.Point(665, 619);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(141, 45);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hardware Functions";
            // 
            // chkSettle
            // 
            this.chkSettle.AutoSize = true;
            this.chkSettle.Location = new System.Drawing.Point(13, 19);
            this.chkSettle.Name = "chkSettle";
            this.chkSettle.Size = new System.Drawing.Size(112, 17);
            this.chkSettle.TabIndex = 40;
            this.chkSettle.Text = "Amplifier Settle On";
            this.chkSettle.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.textBox1.Location = new System.Drawing.Point(5, 315);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(42, 16);
            this.textBox1.TabIndex = 53;
            this.textBox1.Text = "Speed";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Arial", 10F);
            this.textBox2.Location = new System.Drawing.Point(5, 57);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(42, 16);
            this.textBox2.TabIndex = 54;
            this.textBox2.Text = "LFP";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // panel_psd_noise
            // 
            this.panel_psd_noise.Location = new System.Drawing.Point(360, 439);
            this.panel_psd_noise.Name = "panel_psd_noise";
            this.panel_psd_noise.Size = new System.Drawing.Size(120, 163);
            this.panel_psd_noise.TabIndex = 55;
            // 
            // btn_psdhigh_amp_up
            // 
            this.btn_psdhigh_amp_up.Location = new System.Drawing.Point(360, 608);
            this.btn_psdhigh_amp_up.Name = "btn_psdhigh_amp_up";
            this.btn_psdhigh_amp_up.Size = new System.Drawing.Size(62, 28);
            this.btn_psdhigh_amp_up.TabIndex = 56;
            this.btn_psdhigh_amp_up.Text = "Zoom In";
            this.btn_psdhigh_amp_up.UseVisualStyleBackColor = true;
            this.btn_psdhigh_amp_up.Click += new System.EventHandler(this.btn_psdhigh_amp_up_Click);
            // 
            // btn_psdhigh_amp_down
            // 
            this.btn_psdhigh_amp_down.Location = new System.Drawing.Point(359, 636);
            this.btn_psdhigh_amp_down.Name = "btn_psdhigh_amp_down";
            this.btn_psdhigh_amp_down.Size = new System.Drawing.Size(62, 28);
            this.btn_psdhigh_amp_down.TabIndex = 57;
            this.btn_psdhigh_amp_down.Text = "Zoom Out";
            this.btn_psdhigh_amp_down.UseVisualStyleBackColor = true;
            this.btn_psdhigh_amp_down.Click += new System.EventHandler(this.btn_psdhigh_amp_down_Click);
            // 
            // btnNotchFilterDisable
            // 
            this.btnNotchFilterDisable.AutoSize = true;
            this.btnNotchFilterDisable.Checked = true;
            this.btnNotchFilterDisable.Location = new System.Drawing.Point(6, 19);
            this.btnNotchFilterDisable.Name = "btnNotchFilterDisable";
            this.btnNotchFilterDisable.Size = new System.Drawing.Size(58, 17);
            this.btnNotchFilterDisable.TabIndex = 0;
            this.btnNotchFilterDisable.TabStop = true;
            this.btnNotchFilterDisable.Text = "disable";
            this.btnNotchFilterDisable.UseVisualStyleBackColor = true;
            this.btnNotchFilterDisable.CheckedChanged += new System.EventHandler(this.btnNotchFilterDisable_CheckedChanged);
            // 
            // btnNotchFilter50Hz
            // 
            this.btnNotchFilter50Hz.AutoSize = true;
            this.btnNotchFilter50Hz.Location = new System.Drawing.Point(6, 43);
            this.btnNotchFilter50Hz.Name = "btnNotchFilter50Hz";
            this.btnNotchFilter50Hz.Size = new System.Drawing.Size(53, 17);
            this.btnNotchFilter50Hz.TabIndex = 1;
            this.btnNotchFilter50Hz.Text = "50 Hz";
            this.btnNotchFilter50Hz.UseVisualStyleBackColor = true;
            this.btnNotchFilter50Hz.CheckedChanged += new System.EventHandler(this.btnNotchFilter50Hz_CheckedChanged);
            // 
            // btnNotchFilter60Hz
            // 
            this.btnNotchFilter60Hz.AutoSize = true;
            this.btnNotchFilter60Hz.Location = new System.Drawing.Point(6, 66);
            this.btnNotchFilter60Hz.Name = "btnNotchFilter60Hz";
            this.btnNotchFilter60Hz.Size = new System.Drawing.Size(53, 17);
            this.btnNotchFilter60Hz.TabIndex = 2;
            this.btnNotchFilter60Hz.Text = "60 Hz";
            this.btnNotchFilter60Hz.UseVisualStyleBackColor = true;
            this.btnNotchFilter60Hz.CheckedChanged += new System.EventHandler(this.btnNotchFilter60Hz_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNotchFilter60Hz);
            this.groupBox2.Controls.Add(this.btnNotchFilter50Hz);
            this.groupBox2.Controls.Add(this.btnNotchFilterDisable);
            this.groupBox2.Location = new System.Drawing.Point(488, 567);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 97);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Software Notch Filter";
            // 
            // useAmp
            // 
            this.useAmp.AutoSize = true;
            this.useAmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.useAmp.Location = new System.Drawing.Point(2, 29);
            this.useAmp.Name = "useAmp";
            this.useAmp.Size = new System.Drawing.Size(50, 17);
            this.useAmp.TabIndex = 58;
            this.useAmp.Text = "Select";
            this.useAmp.UseCompatibleTextRendering = true;
            this.useAmp.UseVisualStyleBackColor = true;
            // 
            // useSpeed
            // 
            this.useSpeed.AutoSize = true;
            this.useSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.useSpeed.Location = new System.Drawing.Point(3, 287);
            this.useSpeed.Name = "useSpeed";
            this.useSpeed.Size = new System.Drawing.Size(50, 17);
            this.useSpeed.TabIndex = 59;
            this.useSpeed.Text = "Select";
            this.useSpeed.UseCompatibleTextRendering = true;
            this.useSpeed.UseVisualStyleBackColor = true;
            // 
            // checkBoxAmpThreshold
            // 
            this.checkBoxAmpThreshold.AutoSize = true;
            this.checkBoxAmpThreshold.Enabled = false;
            this.checkBoxAmpThreshold.Location = new System.Drawing.Point(812, 207);
            this.checkBoxAmpThreshold.Name = "checkBoxAmpThreshold";
            this.checkBoxAmpThreshold.Size = new System.Drawing.Size(59, 17);
            this.checkBoxAmpThreshold.TabIndex = 60;
            this.checkBoxAmpThreshold.Text = "Enable";
            this.checkBoxAmpThreshold.UseVisualStyleBackColor = true;
            this.checkBoxAmpThreshold.CheckStateChanged += new System.EventHandler(this.checkBoxAmpThreshold_CheckStateChanged);
            // 
            // checkBoxSpeedThreshold
            // 
            this.checkBoxSpeedThreshold.AutoSize = true;
            this.checkBoxSpeedThreshold.Enabled = false;
            this.checkBoxSpeedThreshold.Location = new System.Drawing.Point(812, 363);
            this.checkBoxSpeedThreshold.Name = "checkBoxSpeedThreshold";
            this.checkBoxSpeedThreshold.Size = new System.Drawing.Size(59, 17);
            this.checkBoxSpeedThreshold.TabIndex = 61;
            this.checkBoxSpeedThreshold.Text = "Enable";
            this.checkBoxSpeedThreshold.UseVisualStyleBackColor = true;
            this.checkBoxSpeedThreshold.CheckStateChanged += new System.EventHandler(this.checkBoxSpeedThreshold_CheckStateChanged);
            // 
            // groupBoxAmpThreshold
            // 
            this.groupBoxAmpThreshold.Controls.Add(this.radioButton_Lwr_AmpThr);
            this.groupBoxAmpThreshold.Controls.Add(this.radioButton_Higher_AmpThr);
            this.groupBoxAmpThreshold.Location = new System.Drawing.Point(897, 207);
            this.groupBoxAmpThreshold.Name = "groupBoxAmpThreshold";
            this.groupBoxAmpThreshold.Size = new System.Drawing.Size(117, 76);
            this.groupBoxAmpThreshold.TabIndex = 62;
            this.groupBoxAmpThreshold.TabStop = false;
            this.groupBoxAmpThreshold.Text = "Threshold Direction";
            // 
            // radioButton_Lwr_AmpThr
            // 
            this.radioButton_Lwr_AmpThr.AutoSize = true;
            this.radioButton_Lwr_AmpThr.Enabled = false;
            this.radioButton_Lwr_AmpThr.Location = new System.Drawing.Point(6, 53);
            this.radioButton_Lwr_AmpThr.Name = "radioButton_Lwr_AmpThr";
            this.radioButton_Lwr_AmpThr.Size = new System.Drawing.Size(54, 17);
            this.radioButton_Lwr_AmpThr.TabIndex = 1;
            this.radioButton_Lwr_AmpThr.TabStop = true;
            this.radioButton_Lwr_AmpThr.Text = "Lower";
            this.radioButton_Lwr_AmpThr.UseVisualStyleBackColor = true;
            // 
            // radioButton_Higher_AmpThr
            // 
            this.radioButton_Higher_AmpThr.AutoSize = true;
            this.radioButton_Higher_AmpThr.Enabled = false;
            this.radioButton_Higher_AmpThr.Location = new System.Drawing.Point(6, 30);
            this.radioButton_Higher_AmpThr.Name = "radioButton_Higher_AmpThr";
            this.radioButton_Higher_AmpThr.Size = new System.Drawing.Size(56, 17);
            this.radioButton_Higher_AmpThr.TabIndex = 0;
            this.radioButton_Higher_AmpThr.TabStop = true;
            this.radioButton_Higher_AmpThr.Text = "Higher";
            this.radioButton_Higher_AmpThr.UseVisualStyleBackColor = true;
            // 
            // groupBoxSpeedThreshold
            // 
            this.groupBoxSpeedThreshold.Controls.Add(this.radioButton_Lwr_SpeedThr);
            this.groupBoxSpeedThreshold.Controls.Add(this.radioButton_Higher_SpeedThr);
            this.groupBoxSpeedThreshold.Location = new System.Drawing.Point(900, 361);
            this.groupBoxSpeedThreshold.Name = "groupBoxSpeedThreshold";
            this.groupBoxSpeedThreshold.Size = new System.Drawing.Size(118, 76);
            this.groupBoxSpeedThreshold.TabIndex = 63;
            this.groupBoxSpeedThreshold.TabStop = false;
            this.groupBoxSpeedThreshold.Text = "Threshold Direction";
            // 
            // radioButton_Lwr_SpeedThr
            // 
            this.radioButton_Lwr_SpeedThr.AutoSize = true;
            this.radioButton_Lwr_SpeedThr.Enabled = false;
            this.radioButton_Lwr_SpeedThr.Location = new System.Drawing.Point(6, 53);
            this.radioButton_Lwr_SpeedThr.Name = "radioButton_Lwr_SpeedThr";
            this.radioButton_Lwr_SpeedThr.Size = new System.Drawing.Size(54, 17);
            this.radioButton_Lwr_SpeedThr.TabIndex = 1;
            this.radioButton_Lwr_SpeedThr.TabStop = true;
            this.radioButton_Lwr_SpeedThr.Text = "Lower";
            this.radioButton_Lwr_SpeedThr.UseVisualStyleBackColor = true;
            // 
            // radioButton_Higher_SpeedThr
            // 
            this.radioButton_Higher_SpeedThr.AutoSize = true;
            this.radioButton_Higher_SpeedThr.Enabled = false;
            this.radioButton_Higher_SpeedThr.Location = new System.Drawing.Point(6, 27);
            this.radioButton_Higher_SpeedThr.Name = "radioButton_Higher_SpeedThr";
            this.radioButton_Higher_SpeedThr.Size = new System.Drawing.Size(56, 17);
            this.radioButton_Higher_SpeedThr.TabIndex = 0;
            this.radioButton_Higher_SpeedThr.TabStop = true;
            this.radioButton_Higher_SpeedThr.Text = "Higher";
            this.radioButton_Higher_SpeedThr.UseVisualStyleBackColor = true;
            // 
            // groupBoxStimParam
            // 
            this.groupBoxStimParam.Controls.Add(this.radioButtonEnableAllStim);
            this.groupBoxStimParam.Controls.Add(this.radioButtonEnableSpeedStim);
            this.groupBoxStimParam.Controls.Add(this.radioButtonEnableLFPStim);
            this.groupBoxStimParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBoxStimParam.Location = new System.Drawing.Point(881, 12);
            this.groupBoxStimParam.Name = "groupBoxStimParam";
            this.groupBoxStimParam.Size = new System.Drawing.Size(137, 83);
            this.groupBoxStimParam.TabIndex = 64;
            this.groupBoxStimParam.TabStop = false;
            this.groupBoxStimParam.Text = "Stimulation Channels";
            this.groupBoxStimParam.UseCompatibleTextRendering = true;
            // 
            // radioButtonEnableAllStim
            // 
            this.radioButtonEnableAllStim.AutoSize = true;
            this.radioButtonEnableAllStim.Location = new System.Drawing.Point(8, 60);
            this.radioButtonEnableAllStim.Name = "radioButtonEnableAllStim";
            this.radioButtonEnableAllStim.Size = new System.Drawing.Size(47, 17);
            this.radioButtonEnableAllStim.TabIndex = 2;
            this.radioButtonEnableAllStim.TabStop = true;
            this.radioButtonEnableAllStim.Text = "Both";
            this.radioButtonEnableAllStim.UseVisualStyleBackColor = true;
            // 
            // radioButtonEnableSpeedStim
            // 
            this.radioButtonEnableSpeedStim.AutoSize = true;
            this.radioButtonEnableSpeedStim.Location = new System.Drawing.Point(7, 40);
            this.radioButtonEnableSpeedStim.Name = "radioButtonEnableSpeedStim";
            this.radioButtonEnableSpeedStim.Size = new System.Drawing.Size(56, 17);
            this.radioButtonEnableSpeedStim.TabIndex = 1;
            this.radioButtonEnableSpeedStim.TabStop = true;
            this.radioButtonEnableSpeedStim.Text = "Speed";
            this.radioButtonEnableSpeedStim.UseVisualStyleBackColor = true;
            // 
            // radioButtonEnableLFPStim
            // 
            this.radioButtonEnableLFPStim.AutoSize = true;
            this.radioButtonEnableLFPStim.Location = new System.Drawing.Point(7, 19);
            this.radioButtonEnableLFPStim.Name = "radioButtonEnableLFPStim";
            this.radioButtonEnableLFPStim.Size = new System.Drawing.Size(44, 17);
            this.radioButtonEnableLFPStim.TabIndex = 0;
            this.radioButtonEnableLFPStim.TabStop = true;
            this.radioButtonEnableLFPStim.Text = "LFP";
            this.radioButtonEnableLFPStim.UseVisualStyleBackColor = true;
            // 
            // groupBoxStimDesign
            // 
            this.groupBoxStimDesign.Controls.Add(this.radioButtonStimDesignNotSPEED);
            this.groupBoxStimDesign.Controls.Add(this.radioButtonStimDesignNOTLFP);
            this.groupBoxStimDesign.Controls.Add(this.radioButtonStimDesignOR);
            this.groupBoxStimDesign.Controls.Add(this.radioButtonStimDesignAND);
            this.groupBoxStimDesign.Location = new System.Drawing.Point(881, 101);
            this.groupBoxStimDesign.Name = "groupBoxStimDesign";
            this.groupBoxStimDesign.Size = new System.Drawing.Size(137, 89);
            this.groupBoxStimDesign.TabIndex = 65;
            this.groupBoxStimDesign.TabStop = false;
            this.groupBoxStimDesign.Text = "Stimulation Design";
            // 
            // radioButtonStimDesignNotSPEED
            // 
            this.radioButtonStimDesignNotSPEED.AutoSize = true;
            this.radioButtonStimDesignNotSPEED.Enabled = false;
            this.radioButtonStimDesignNotSPEED.Location = new System.Drawing.Point(58, 55);
            this.radioButtonStimDesignNotSPEED.Name = "radioButtonStimDesignNotSPEED";
            this.radioButtonStimDesignNotSPEED.Size = new System.Drawing.Size(76, 17);
            this.radioButtonStimDesignNotSPEED.TabIndex = 3;
            this.radioButtonStimDesignNotSPEED.TabStop = true;
            this.radioButtonStimDesignNotSPEED.Text = "Not Speed";
            this.radioButtonStimDesignNotSPEED.UseVisualStyleBackColor = true;
            // 
            // radioButtonStimDesignNOTLFP
            // 
            this.radioButtonStimDesignNOTLFP.AutoSize = true;
            this.radioButtonStimDesignNOTLFP.Enabled = false;
            this.radioButtonStimDesignNOTLFP.Location = new System.Drawing.Point(60, 32);
            this.radioButtonStimDesignNOTLFP.Name = "radioButtonStimDesignNOTLFP";
            this.radioButtonStimDesignNOTLFP.Size = new System.Drawing.Size(64, 17);
            this.radioButtonStimDesignNOTLFP.TabIndex = 2;
            this.radioButtonStimDesignNOTLFP.TabStop = true;
            this.radioButtonStimDesignNOTLFP.Text = "Not LFP";
            this.radioButtonStimDesignNOTLFP.UseVisualStyleBackColor = true;
            // 
            // radioButtonStimDesignOR
            // 
            this.radioButtonStimDesignOR.AutoSize = true;
            this.radioButtonStimDesignOR.Enabled = false;
            this.radioButtonStimDesignOR.Location = new System.Drawing.Point(8, 55);
            this.radioButtonStimDesignOR.Name = "radioButtonStimDesignOR";
            this.radioButtonStimDesignOR.Size = new System.Drawing.Size(41, 17);
            this.radioButtonStimDesignOR.TabIndex = 1;
            this.radioButtonStimDesignOR.TabStop = true;
            this.radioButtonStimDesignOR.Text = "OR";
            this.radioButtonStimDesignOR.UseVisualStyleBackColor = true;
            // 
            // radioButtonStimDesignAND
            // 
            this.radioButtonStimDesignAND.AutoSize = true;
            this.radioButtonStimDesignAND.Enabled = false;
            this.radioButtonStimDesignAND.Location = new System.Drawing.Point(7, 32);
            this.radioButtonStimDesignAND.Name = "radioButtonStimDesignAND";
            this.radioButtonStimDesignAND.Size = new System.Drawing.Size(48, 17);
            this.radioButtonStimDesignAND.TabIndex = 0;
            this.radioButtonStimDesignAND.TabStop = true;
            this.radioButtonStimDesignAND.Text = "AND";
            this.radioButtonStimDesignAND.UseVisualStyleBackColor = true;
            // 
            // lowerFilterNumUpDown
            // 
            this.lowerFilterNumUpDown.DecimalPlaces = 1;
            this.lowerFilterNumUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.lowerFilterNumUpDown.Location = new System.Drawing.Point(5, 38);
            this.lowerFilterNumUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.lowerFilterNumUpDown.Name = "lowerFilterNumUpDown";
            this.lowerFilterNumUpDown.Size = new System.Drawing.Size(42, 20);
            this.lowerFilterNumUpDown.TabIndex = 45;
            this.lowerFilterNumUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.lowerFilterNumUpDown.ValueChanged += new System.EventHandler(this.lowerFilterNumUpDown_ValueChanged);
            // 
            // higherFilterDenUpDown
            // 
            this.higherFilterDenUpDown.DecimalPlaces = 1;
            this.higherFilterDenUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.higherFilterDenUpDown.Location = new System.Drawing.Point(51, 83);
            this.higherFilterDenUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.higherFilterDenUpDown.Name = "higherFilterDenUpDown";
            this.higherFilterDenUpDown.Size = new System.Drawing.Size(42, 20);
            this.higherFilterDenUpDown.TabIndex = 46;
            this.higherFilterDenUpDown.Value = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.higherFilterDenUpDown.ValueChanged += new System.EventHandler(this.higherFilterDenUpDown_ValueChanged);
            // 
            // lowerFilterDenUpDown
            // 
            this.lowerFilterDenUpDown.DecimalPlaces = 1;
            this.lowerFilterDenUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.lowerFilterDenUpDown.Location = new System.Drawing.Point(5, 83);
            this.lowerFilterDenUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.lowerFilterDenUpDown.Name = "lowerFilterDenUpDown";
            this.lowerFilterDenUpDown.Size = new System.Drawing.Size(42, 20);
            this.lowerFilterDenUpDown.TabIndex = 47;
            this.lowerFilterDenUpDown.ValueChanged += new System.EventHandler(this.lowerFilterDenUpDown_ValueChanged);
            // 
            // higherFilterNumUpDown
            // 
            this.higherFilterNumUpDown.DecimalPlaces = 1;
            this.higherFilterNumUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.higherFilterNumUpDown.Location = new System.Drawing.Point(51, 38);
            this.higherFilterNumUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.higherFilterNumUpDown.Name = "higherFilterNumUpDown";
            this.higherFilterNumUpDown.Size = new System.Drawing.Size(42, 20);
            this.higherFilterNumUpDown.TabIndex = 48;
            this.higherFilterNumUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.higherFilterNumUpDown.ValueChanged += new System.EventHandler(this.higherFilterNumUpDown_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox4);
            this.groupBox5.Controls.Add(this.textBox3);
            this.groupBox5.Controls.Add(this.higherFilterNumUpDown);
            this.groupBox5.Controls.Add(this.higherFilterDenUpDown);
            this.groupBox5.Controls.Add(this.lowerFilterDenUpDown);
            this.groupBox5.Controls.Add(this.lowerFilterNumUpDown);
            this.groupBox5.Location = new System.Drawing.Point(0, 439);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(97, 111);
            this.groupBox5.TabIndex = 66;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Filter Settings";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox4.Location = new System.Drawing.Point(12, 62);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(72, 20);
            this.textBox4.TabIndex = 50;
            this.textBox4.Text = "Denominator";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox3.Location = new System.Drawing.Point(21, 17);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(55, 20);
            this.textBox3.TabIndex = 49;
            this.textBox3.Text = "Numerator";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCounter});
            this.statusStrip1.Location = new System.Drawing.Point(739, 3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(139, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 67;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCounter
            // 
            this.lblCounter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(38, 17);
            this.lblCounter.Text = "Timer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 678);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBoxStimDesign);
            this.Controls.Add(this.groupBoxStimParam);
            this.Controls.Add(this.groupBoxSpeedThreshold);
            this.Controls.Add(this.groupBoxAmpThreshold);
            this.Controls.Add(this.checkBoxSpeedThreshold);
            this.Controls.Add(this.checkBoxAmpThreshold);
            this.Controls.Add(this.useSpeed);
            this.Controls.Add(this.useAmp);
            this.Controls.Add(this.btn_psdhigh_amp_down);
            this.Controls.Add(this.btn_psdhigh_amp_up);
            this.Controls.Add(this.panel_psd_noise);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.sbrMyStatusStrip);
            this.Controls.Add(this.btnZoomInSpeed);
            this.Controls.Add(this.btnZoomOutSpeed);
            this.Controls.Add(this.btnZoomInAmp);
            this.Controls.Add(this.btnZoomOutAmp);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.panel_thr_speed);
            this.Controls.Add(this.panel_thr_lfp);
            this.Controls.Add(this.panel_lfp);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnXZoomOut);
            this.Controls.Add(this.btnXZoomIn);
            this.Controls.Add(this.lblXScale);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.lblYScale);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel_psd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Infinite Neural Loop";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMinutes)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel_psd.ResumeLayout(false);
            this.sbrMyStatusStrip.ResumeLayout(false);
            this.sbrMyStatusStrip.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxAmpThreshold.ResumeLayout(false);
            this.groupBoxAmpThreshold.PerformLayout();
            this.groupBoxSpeedThreshold.ResumeLayout(false);
            this.groupBoxSpeedThreshold.PerformLayout();
            this.groupBoxStimParam.ResumeLayout(false);
            this.groupBoxStimParam.PerformLayout();
            this.groupBoxStimDesign.ResumeLayout(false);
            this.groupBoxStimDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lowerFilterNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.higherFilterDenUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerFilterDenUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.higherFilterNumUpDown)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Timer tmrDraw;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label lblYScale;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Label lblXScale;
        private System.Windows.Forms.Button btnXZoomIn;
        private System.Windows.Forms.Button btnXZoomOut;
        private System.Windows.Forms.TextBox txtHPF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtSaveFilename;
        private System.Windows.Forms.Button btnSelectFilename;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEnableHPF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMaxMinutes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Timer tmrSynthData;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Panel panel_lfp;
        private System.Windows.Forms.Panel panel_psd;
        private System.Windows.Forms.Panel panel_thr_lfp;
        private System.Windows.Forms.Button btn_psd_amp_up;
        private System.Windows.Forms.Button btn_psd_amp_down;
        private System.Windows.Forms.Panel panel_thr_speed;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnZoomInAmp;
        private System.Windows.Forms.Button btnZoomOutAmp;
        private System.Windows.Forms.Button btnZoomInSpeed;
        private System.Windows.Forms.Button btnZoomOutSpeed;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.StatusStrip sbrMyStatusStrip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSettle;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel_psd_noise;
        private System.Windows.Forms.Button btn_psdhigh_amp_up;
        private System.Windows.Forms.Button btn_psdhigh_amp_down;
        private System.Windows.Forms.RadioButton btnNotchFilterDisable;
        private System.Windows.Forms.RadioButton btnNotchFilter50Hz;
        private System.Windows.Forms.RadioButton btnNotchFilter60Hz;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox useAmp;
        private System.Windows.Forms.CheckBox useSpeed;
        private System.Windows.Forms.CheckBox checkBoxAmpThreshold;
        private System.Windows.Forms.CheckBox checkBoxSpeedThreshold;
        private System.Windows.Forms.GroupBox groupBoxAmpThreshold;
        private System.Windows.Forms.RadioButton radioButton_Lwr_AmpThr;
        private System.Windows.Forms.RadioButton radioButton_Higher_AmpThr;
        private System.Windows.Forms.GroupBox groupBoxSpeedThreshold;
        private System.Windows.Forms.RadioButton radioButton_Lwr_SpeedThr;
        private System.Windows.Forms.RadioButton radioButton_Higher_SpeedThr;
        private System.Windows.Forms.GroupBox groupBoxStimParam;
        private System.Windows.Forms.RadioButton radioButtonEnableAllStim;
        private System.Windows.Forms.RadioButton radioButtonEnableSpeedStim;
        private System.Windows.Forms.RadioButton radioButtonEnableLFPStim;
        private System.Windows.Forms.GroupBox groupBoxStimDesign;
        private System.Windows.Forms.RadioButton radioButtonStimDesignOR;
        private System.Windows.Forms.RadioButton radioButtonStimDesignAND;
        private System.Windows.Forms.RadioButton radioButtonStimDesignNotSPEED;
        private System.Windows.Forms.RadioButton radioButtonStimDesignNOTLFP;
        private System.Windows.Forms.Button btnChanSelectionWindow;
        private System.Windows.Forms.NumericUpDown lowerFilterNumUpDown;
        private System.Windows.Forms.NumericUpDown higherFilterDenUpDown;
        private System.Windows.Forms.NumericUpDown lowerFilterDenUpDown;
        private System.Windows.Forms.NumericUpDown higherFilterNumUpDown;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCounter;
    }
}

