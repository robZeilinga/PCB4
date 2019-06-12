namespace PCB_DR
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pic_Box_Orig = new System.Windows.Forms.PictureBox();
            this.gb_DrillFile = new System.Windows.Forms.GroupBox();
            this.lbl_Angle = new System.Windows.Forms.Label();
            this.lbl_Dist = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lbl_Hole2_Y = new System.Windows.Forms.Label();
            this.lbl_Hole2_X = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.btn_Org_SetHole2 = new System.Windows.Forms.Button();
            this.cb_Flip = new System.Windows.Forms.CheckBox();
            this.lbl_Hole0_Y = new System.Windows.Forms.Label();
            this.lbl_Hole0_X = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Drill_File_Open = new System.Windows.Forms.Button();
            this.btn_Org_SetHole1 = new System.Windows.Forms.Button();
            this.lbl_DrillFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_PCB_Angle = new System.Windows.Forms.Label();
            this.lbl_PCB_Scale = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.timerSerial = new System.Windows.Forms.Timer(this.components);
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.btnGrabCamHole0 = new System.Windows.Forms.Button();
            this.btnGrabCamHole2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkMoveToClickedHole = new System.Windows.Forms.CheckBox();
            this.lblPCBRotationAngle = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPCBDist = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPCBHole2_Y = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lblPCBHole2_X = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPCBHole0_Y = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPCBHole0_X = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.cBCommand = new System.Windows.Forms.ComboBox();
            this.btnGRBLReset = new System.Windows.Forms.Button();
            this.btnGRBLCommand4 = new System.Windows.Forms.Button();
            this.btnGRBLCommand3 = new System.Windows.Forms.Button();
            this.btnGRBLCommand2 = new System.Windows.Forms.Button();
            this.btnGRBLCommand1 = new System.Windows.Forms.Button();
            this.btnGRBLCommand0 = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnCheckGRBLResult = new System.Windows.Forms.Button();
            this.btnCheckGRBL = new System.Windows.Forms.Button();
            this.cbStatus = new System.Windows.Forms.CheckBox();
            this.lblSrA = new System.Windows.Forms.Label();
            this.lblSrLn = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblSrState = new System.Windows.Forms.Label();
            this.lblSrOv = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lblSrPn = new System.Windows.Forms.Label();
            this.lblSrPos = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.lblSrFS = new System.Windows.Forms.Label();
            this.lblSrBf = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.btnScanPort = new System.Windows.Forms.Button();
            this.btn_OpenPort = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DrillNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumHoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnDrill = new System.Windows.Forms.Button();
            this.chk_IncludeHoleInfo = new System.Windows.Forms.CheckBox();
            this.chk_LeaveZHeight = new System.Windows.Forms.CheckBox();
            this.chk_IgnorePause = new System.Windows.Forms.CheckBox();
            this.cb_Drill = new System.Windows.Forms.CheckBox();
            this.txtDrillDepth = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtSafeZHeight = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.btn_GenFile = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.txtCameraOffsetY = new System.Windows.Forms.TextBox();
            this.txtCameraOffsetX = new System.Windows.Forms.TextBox();
            this.lbl_Offset_Y = new System.Windows.Forms.Label();
            this.lbl_Offset_X = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_Offset_Y_Cam = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btn_Offset_Save = new System.Windows.Forms.Button();
            this.txt_Offset_X_Cam = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_Offset_Y_Mark = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_Offset_X_Mark = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.btn_Offset_Z_Height_Save = new System.Windows.Forms.Button();
            this.txtBox_Offset_Z_Height = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnBounceHome2 = new System.Windows.Forms.Button();
            this.btnBounceHome0 = new System.Windows.Forms.Button();
            this.txtFeed = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtScanHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStreamCode = new System.Windows.Forms.Button();
            this.btn_camera_start = new System.Windows.Forms.Button();
            this.picBox_Video = new System.Windows.Forms.PictureBox();
            this.chk_Jog = new System.Windows.Forms.CheckBox();
            this.cmb_Camera = new System.Windows.Forms.ComboBox();
            this.lblMachinePosition = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Box_Orig)).BeginInit();
            this.gb_DrillFile.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Video)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pic_Box_Orig);
            this.groupBox1.Location = new System.Drawing.Point(12, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 505);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Original";
            // 
            // pic_Box_Orig
            // 
            this.pic_Box_Orig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_Box_Orig.Location = new System.Drawing.Point(3, 16);
            this.pic_Box_Orig.Name = "pic_Box_Orig";
            this.pic_Box_Orig.Size = new System.Drawing.Size(530, 486);
            this.pic_Box_Orig.TabIndex = 0;
            this.pic_Box_Orig.TabStop = false;
            this.pic_Box_Orig.Click += new System.EventHandler(this.pic_Box_Orig_Click);
            // 
            // gb_DrillFile
            // 
            this.gb_DrillFile.Controls.Add(this.lbl_Angle);
            this.gb_DrillFile.Controls.Add(this.lbl_Dist);
            this.gb_DrillFile.Controls.Add(this.label44);
            this.gb_DrillFile.Controls.Add(this.label45);
            this.gb_DrillFile.Controls.Add(this.lbl_Hole2_Y);
            this.gb_DrillFile.Controls.Add(this.lbl_Hole2_X);
            this.gb_DrillFile.Controls.Add(this.label40);
            this.gb_DrillFile.Controls.Add(this.label41);
            this.gb_DrillFile.Controls.Add(this.btn_Org_SetHole2);
            this.gb_DrillFile.Controls.Add(this.cb_Flip);
            this.gb_DrillFile.Controls.Add(this.lbl_Hole0_Y);
            this.gb_DrillFile.Controls.Add(this.lbl_Hole0_X);
            this.gb_DrillFile.Controls.Add(this.label8);
            this.gb_DrillFile.Controls.Add(this.label7);
            this.gb_DrillFile.Controls.Add(this.btn_Drill_File_Open);
            this.gb_DrillFile.Controls.Add(this.btn_Org_SetHole1);
            this.gb_DrillFile.Controls.Add(this.lbl_DrillFileName);
            this.gb_DrillFile.Controls.Add(this.label1);
            this.gb_DrillFile.Location = new System.Drawing.Point(11, 10);
            this.gb_DrillFile.Name = "gb_DrillFile";
            this.gb_DrillFile.Size = new System.Drawing.Size(281, 123);
            this.gb_DrillFile.TabIndex = 2;
            this.gb_DrillFile.TabStop = false;
            this.gb_DrillFile.Text = "Drill File";
            // 
            // lbl_Angle
            // 
            this.lbl_Angle.AutoSize = true;
            this.lbl_Angle.Location = new System.Drawing.Point(233, 74);
            this.lbl_Angle.Name = "lbl_Angle";
            this.lbl_Angle.Size = new System.Drawing.Size(20, 13);
            this.lbl_Angle.TabIndex = 21;
            this.lbl_Angle.Text = "Y: ";
            // 
            // lbl_Dist
            // 
            this.lbl_Dist.AutoSize = true;
            this.lbl_Dist.Location = new System.Drawing.Point(233, 55);
            this.lbl_Dist.Name = "lbl_Dist";
            this.lbl_Dist.Size = new System.Drawing.Size(20, 13);
            this.lbl_Dist.TabIndex = 20;
            this.lbl_Dist.Text = "X: ";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(198, 75);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(37, 13);
            this.label44.TabIndex = 19;
            this.label44.Text = "Angle:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(207, 55);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(28, 13);
            this.label45.TabIndex = 18;
            this.label45.Text = "Dist:";
            // 
            // lbl_Hole2_Y
            // 
            this.lbl_Hole2_Y.AutoSize = true;
            this.lbl_Hole2_Y.Location = new System.Drawing.Point(148, 99);
            this.lbl_Hole2_Y.Name = "lbl_Hole2_Y";
            this.lbl_Hole2_Y.Size = new System.Drawing.Size(20, 13);
            this.lbl_Hole2_Y.TabIndex = 17;
            this.lbl_Hole2_Y.Text = "Y: ";
            // 
            // lbl_Hole2_X
            // 
            this.lbl_Hole2_X.AutoSize = true;
            this.lbl_Hole2_X.Location = new System.Drawing.Point(148, 80);
            this.lbl_Hole2_X.Name = "lbl_Hole2_X";
            this.lbl_Hole2_X.Size = new System.Drawing.Size(20, 13);
            this.lbl_Hole2_X.TabIndex = 16;
            this.lbl_Hole2_X.Text = "X: ";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(122, 99);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(20, 13);
            this.label40.TabIndex = 15;
            this.label40.Text = "Y: ";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(122, 80);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(20, 13);
            this.label41.TabIndex = 14;
            this.label41.Text = "X: ";
            // 
            // btn_Org_SetHole2
            // 
            this.btn_Org_SetHole2.Location = new System.Drawing.Point(117, 50);
            this.btn_Org_SetHole2.Name = "btn_Org_SetHole2";
            this.btn_Org_SetHole2.Size = new System.Drawing.Size(75, 22);
            this.btn_Org_SetHole2.TabIndex = 13;
            this.btn_Org_SetHole2.Text = "Set Hole 2";
            this.btn_Org_SetHole2.UseVisualStyleBackColor = true;
            this.btn_Org_SetHole2.Click += new System.EventHandler(this.btn_Org_SetHole2_Click);
            // 
            // cb_Flip
            // 
            this.cb_Flip.AutoSize = true;
            this.cb_Flip.Checked = true;
            this.cb_Flip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Flip.Location = new System.Drawing.Point(201, 95);
            this.cb_Flip.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Flip.Name = "cb_Flip";
            this.cb_Flip.Size = new System.Drawing.Size(45, 17);
            this.cb_Flip.TabIndex = 12;
            this.cb_Flip.Text = "Flip ";
            this.cb_Flip.UseVisualStyleBackColor = true;
            // 
            // lbl_Hole0_Y
            // 
            this.lbl_Hole0_Y.AutoSize = true;
            this.lbl_Hole0_Y.Location = new System.Drawing.Point(49, 99);
            this.lbl_Hole0_Y.Name = "lbl_Hole0_Y";
            this.lbl_Hole0_Y.Size = new System.Drawing.Size(20, 13);
            this.lbl_Hole0_Y.TabIndex = 9;
            this.lbl_Hole0_Y.Text = "Y: ";
            // 
            // lbl_Hole0_X
            // 
            this.lbl_Hole0_X.AutoSize = true;
            this.lbl_Hole0_X.Location = new System.Drawing.Point(49, 80);
            this.lbl_Hole0_X.Name = "lbl_Hole0_X";
            this.lbl_Hole0_X.Size = new System.Drawing.Size(20, 13);
            this.lbl_Hole0_X.TabIndex = 8;
            this.lbl_Hole0_X.Text = "X: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Y: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "X: ";
            // 
            // btn_Drill_File_Open
            // 
            this.btn_Drill_File_Open.Location = new System.Drawing.Point(181, 17);
            this.btn_Drill_File_Open.Name = "btn_Drill_File_Open";
            this.btn_Drill_File_Open.Size = new System.Drawing.Size(30, 22);
            this.btn_Drill_File_Open.TabIndex = 4;
            this.btn_Drill_File_Open.Text = ". . .";
            this.btn_Drill_File_Open.UseVisualStyleBackColor = true;
            this.btn_Drill_File_Open.Click += new System.EventHandler(this.btn_Drill_File_Open_Click);
            // 
            // btn_Org_SetHole1
            // 
            this.btn_Org_SetHole1.Location = new System.Drawing.Point(19, 48);
            this.btn_Org_SetHole1.Name = "btn_Org_SetHole1";
            this.btn_Org_SetHole1.Size = new System.Drawing.Size(75, 22);
            this.btn_Org_SetHole1.TabIndex = 2;
            this.btn_Org_SetHole1.Text = "Set Hole 0";
            this.btn_Org_SetHole1.UseVisualStyleBackColor = true;
            this.btn_Org_SetHole1.Click += new System.EventHandler(this.btn_Org_SetHole1_Click);
            // 
            // lbl_DrillFileName
            // 
            this.lbl_DrillFileName.AutoSize = true;
            this.lbl_DrillFileName.Location = new System.Drawing.Point(71, 22);
            this.lbl_DrillFileName.Name = "lbl_DrillFileName";
            this.lbl_DrillFileName.Size = new System.Drawing.Size(74, 13);
            this.lbl_DrillFileName.TabIndex = 1;
            this.lbl_DrillFileName.Text = "Drill File Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drill File :";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Drill File|*.drd";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            // 
            // lbl_PCB_Angle
            // 
            this.lbl_PCB_Angle.AutoSize = true;
            this.lbl_PCB_Angle.Location = new System.Drawing.Point(199, 84);
            this.lbl_PCB_Angle.Name = "lbl_PCB_Angle";
            this.lbl_PCB_Angle.Size = new System.Drawing.Size(40, 13);
            this.lbl_PCB_Angle.TabIndex = 29;
            this.lbl_PCB_Angle.Text = "90.000";
            // 
            // lbl_PCB_Scale
            // 
            this.lbl_PCB_Scale.AutoSize = true;
            this.lbl_PCB_Scale.Location = new System.Drawing.Point(121, 84);
            this.lbl_PCB_Scale.Name = "lbl_PCB_Scale";
            this.lbl_PCB_Scale.Size = new System.Drawing.Size(34, 13);
            this.lbl_PCB_Scale.TabIndex = 28;
            this.lbl_PCB_Scale.Text = "9.000";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(163, 84);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(37, 13);
            this.label46.TabIndex = 27;
            this.label46.Text = "Angle:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(81, 84);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(40, 13);
            this.label47.TabIndex = 26;
            this.label47.Text = "Scale :";
            // 
            // timerSerial
            // 
            this.timerSerial.Enabled = true;
            this.timerSerial.Interval = 200;
            this.timerSerial.Tick += new System.EventHandler(this.timerSerial_Tick);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // btnGrabCamHole0
            // 
            this.btnGrabCamHole0.Location = new System.Drawing.Point(12, 47);
            this.btnGrabCamHole0.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrabCamHole0.Name = "btnGrabCamHole0";
            this.btnGrabCamHole0.Size = new System.Drawing.Size(66, 21);
            this.btnGrabCamHole0.TabIndex = 3;
            this.btnGrabCamHole0.Text = "Set Hole 0";
            this.btnGrabCamHole0.UseVisualStyleBackColor = true;
            this.btnGrabCamHole0.Click += new System.EventHandler(this.btnGrabCamHole0_Click);
            // 
            // btnGrabCamHole2
            // 
            this.btnGrabCamHole2.Location = new System.Drawing.Point(12, 74);
            this.btnGrabCamHole2.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrabCamHole2.Name = "btnGrabCamHole2";
            this.btnGrabCamHole2.Size = new System.Drawing.Size(66, 21);
            this.btnGrabCamHole2.TabIndex = 33;
            this.btnGrabCamHole2.Text = "Set Hole 2";
            this.btnGrabCamHole2.UseVisualStyleBackColor = true;
            this.btnGrabCamHole2.Click += new System.EventHandler(this.btnGrabCamHole2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkMoveToClickedHole);
            this.groupBox3.Controls.Add(this.lblPCBRotationAngle);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.lblPCBDist);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lbl_PCB_Angle);
            this.groupBox3.Controls.Add(this.label46);
            this.groupBox3.Controls.Add(this.lbl_PCB_Scale);
            this.groupBox3.Controls.Add(this.lblPCBHole2_Y);
            this.groupBox3.Controls.Add(this.label47);
            this.groupBox3.Controls.Add(this.label67);
            this.groupBox3.Controls.Add(this.lblPCBHole2_X);
            this.groupBox3.Controls.Add(this.label69);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.lblPCBHole0_Y);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblPCBHole0_X);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(298, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 123);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Camera Holes";
            // 
            // chkMoveToClickedHole
            // 
            this.chkMoveToClickedHole.AutoSize = true;
            this.chkMoveToClickedHole.Enabled = false;
            this.chkMoveToClickedHole.Location = new System.Drawing.Point(110, 17);
            this.chkMoveToClickedHole.Margin = new System.Windows.Forms.Padding(2);
            this.chkMoveToClickedHole.Name = "chkMoveToClickedHole";
            this.chkMoveToClickedHole.Size = new System.Drawing.Size(132, 17);
            this.chkMoveToClickedHole.TabIndex = 22;
            this.chkMoveToClickedHole.Text = "Move To Clicked Hole";
            this.chkMoveToClickedHole.UseVisualStyleBackColor = true;
            // 
            // lblPCBRotationAngle
            // 
            this.lblPCBRotationAngle.AutoSize = true;
            this.lblPCBRotationAngle.Location = new System.Drawing.Point(199, 100);
            this.lblPCBRotationAngle.Name = "lblPCBRotationAngle";
            this.lblPCBRotationAngle.Size = new System.Drawing.Size(40, 13);
            this.lblPCBRotationAngle.TabIndex = 37;
            this.lblPCBRotationAngle.Text = "90.000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(124, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "PCB Rotation :";
            // 
            // lblPCBDist
            // 
            this.lblPCBDist.AutoSize = true;
            this.lblPCBDist.Location = new System.Drawing.Point(28, 84);
            this.lblPCBDist.Name = "lblPCBDist";
            this.lblPCBDist.Size = new System.Drawing.Size(46, 13);
            this.lblPCBDist.TabIndex = 35;
            this.lblPCBDist.Text = "999.000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Dist:";
            // 
            // lblPCBHole2_Y
            // 
            this.lblPCBHole2_Y.AutoSize = true;
            this.lblPCBHole2_Y.Location = new System.Drawing.Point(160, 64);
            this.lblPCBHole2_Y.Name = "lblPCBHole2_Y";
            this.lblPCBHole2_Y.Size = new System.Drawing.Size(20, 13);
            this.lblPCBHole2_Y.TabIndex = 26;
            this.lblPCBHole2_Y.Text = "X: ";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(134, 64);
            this.label67.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(20, 13);
            this.label67.TabIndex = 25;
            this.label67.Text = "Y :";
            // 
            // lblPCBHole2_X
            // 
            this.lblPCBHole2_X.AutoSize = true;
            this.lblPCBHole2_X.Location = new System.Drawing.Point(89, 65);
            this.lblPCBHole2_X.Name = "lblPCBHole2_X";
            this.lblPCBHole2_X.Size = new System.Drawing.Size(20, 13);
            this.lblPCBHole2_X.TabIndex = 24;
            this.lblPCBHole2_X.Text = "X: ";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(63, 65);
            this.label69.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(20, 13);
            this.label69.TabIndex = 23;
            this.label69.Text = "X :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 65);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Hole #2";
            // 
            // lblPCBHole0_Y
            // 
            this.lblPCBHole0_Y.AutoSize = true;
            this.lblPCBHole0_Y.Location = new System.Drawing.Point(160, 45);
            this.lblPCBHole0_Y.Name = "lblPCBHole0_Y";
            this.lblPCBHole0_Y.Size = new System.Drawing.Size(20, 13);
            this.lblPCBHole0_Y.TabIndex = 21;
            this.lblPCBHole0_Y.Text = "X: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 45);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Y :";
            // 
            // lblPCBHole0_X
            // 
            this.lblPCBHole0_X.AutoSize = true;
            this.lblPCBHole0_X.Location = new System.Drawing.Point(89, 46);
            this.lblPCBHole0_X.Name = "lblPCBHole0_X";
            this.lblPCBHole0_X.Size = new System.Drawing.Size(20, 13);
            this.lblPCBHole0_X.TabIndex = 19;
            this.lblPCBHole0_X.Text = "X: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Hole Zero ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "X :";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.cBCommand);
            this.tabPage7.Controls.Add(this.btnGRBLReset);
            this.tabPage7.Controls.Add(this.btnGRBLCommand4);
            this.tabPage7.Controls.Add(this.btnGRBLCommand3);
            this.tabPage7.Controls.Add(this.btnGRBLCommand2);
            this.tabPage7.Controls.Add(this.btnGRBLCommand1);
            this.tabPage7.Controls.Add(this.btnGRBLCommand0);
            this.tabPage7.Controls.Add(this.btnSend);
            this.tabPage7.Controls.Add(this.btnClear);
            this.tabPage7.Controls.Add(this.groupBox7);
            this.tabPage7.Controls.Add(this.rtbLog);
            this.tabPage7.Controls.Add(this.cbPort);
            this.tabPage7.Controls.Add(this.cbBaud);
            this.tabPage7.Controls.Add(this.btnScanPort);
            this.tabPage7.Controls.Add(this.btn_OpenPort);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(711, 607);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "GRBL";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // cBCommand
            // 
            this.cBCommand.FormattingEnabled = true;
            this.cBCommand.Items.AddRange(new object[] {
            "$H (Homing)",
            "G90 G1 X1 F500 (absolute)",
            "G91 G1 X1 F500 (relarive)"});
            this.cBCommand.Location = new System.Drawing.Point(88, 336);
            this.cBCommand.Name = "cBCommand";
            this.cBCommand.Size = new System.Drawing.Size(156, 21);
            this.cBCommand.TabIndex = 41;
            this.cBCommand.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cBCommand_KeyUp);
            // 
            // btnGRBLReset
            // 
            this.btnGRBLReset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLReset.Location = new System.Drawing.Point(204, 358);
            this.btnGRBLReset.Name = "btnGRBLReset";
            this.btnGRBLReset.Size = new System.Drawing.Size(89, 23);
            this.btnGRBLReset.TabIndex = 40;
            this.btnGRBLReset.Text = "RESET";
            this.btnGRBLReset.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand4
            // 
            this.btnGRBLCommand4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand4.Location = new System.Drawing.Point(166, 358);
            this.btnGRBLCommand4.Name = "btnGRBLCommand4";
            this.btnGRBLCommand4.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand4.TabIndex = 39;
            this.btnGRBLCommand4.Text = "$X";
            this.btnGRBLCommand4.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand3
            // 
            this.btnGRBLCommand3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand3.Location = new System.Drawing.Point(128, 358);
            this.btnGRBLCommand3.Name = "btnGRBLCommand3";
            this.btnGRBLCommand3.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand3.TabIndex = 38;
            this.btnGRBLCommand3.Text = "$N";
            this.btnGRBLCommand3.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand2
            // 
            this.btnGRBLCommand2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand2.Location = new System.Drawing.Point(90, 358);
            this.btnGRBLCommand2.Name = "btnGRBLCommand2";
            this.btnGRBLCommand2.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand2.TabIndex = 37;
            this.btnGRBLCommand2.Text = "$#";
            this.btnGRBLCommand2.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand1
            // 
            this.btnGRBLCommand1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand1.Location = new System.Drawing.Point(52, 358);
            this.btnGRBLCommand1.Name = "btnGRBLCommand1";
            this.btnGRBLCommand1.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand1.TabIndex = 36;
            this.btnGRBLCommand1.Text = "$$";
            this.btnGRBLCommand1.UseVisualStyleBackColor = true;
            this.btnGRBLCommand1.Click += new System.EventHandler(this.btnGRBLCommand1_Click_1);
            // 
            // btnGRBLCommand0
            // 
            this.btnGRBLCommand0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand0.Location = new System.Drawing.Point(14, 358);
            this.btnGRBLCommand0.Name = "btnGRBLCommand0";
            this.btnGRBLCommand0.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand0.TabIndex = 35;
            this.btnGRBLCommand0.Text = "$";
            this.btnGRBLCommand0.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSend.Location = new System.Drawing.Point(250, 334);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(43, 23);
            this.btnSend.TabIndex = 34;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(14, 334);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 23);
            this.btnClear.TabIndex = 33;
            this.btnClear.Text = "Clear Log";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnCheckGRBLResult);
            this.groupBox7.Controls.Add(this.btnCheckGRBL);
            this.groupBox7.Controls.Add(this.cbStatus);
            this.groupBox7.Controls.Add(this.lblSrA);
            this.groupBox7.Controls.Add(this.lblSrLn);
            this.groupBox7.Controls.Add(this.label51);
            this.groupBox7.Controls.Add(this.lblSrState);
            this.groupBox7.Controls.Add(this.lblSrOv);
            this.groupBox7.Controls.Add(this.label61);
            this.groupBox7.Controls.Add(this.label62);
            this.groupBox7.Controls.Add(this.lblSrPn);
            this.groupBox7.Controls.Add(this.lblSrPos);
            this.groupBox7.Controls.Add(this.label63);
            this.groupBox7.Controls.Add(this.label64);
            this.groupBox7.Controls.Add(this.lblSrFS);
            this.groupBox7.Controls.Add(this.lblSrBf);
            this.groupBox7.Controls.Add(this.label65);
            this.groupBox7.Location = new System.Drawing.Point(14, 37);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(276, 94);
            this.groupBox7.TabIndex = 32;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Real-time Status Report";
            // 
            // btnCheckGRBLResult
            // 
            this.btnCheckGRBLResult.Enabled = false;
            this.btnCheckGRBLResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCheckGRBLResult.Location = new System.Drawing.Point(254, 71);
            this.btnCheckGRBLResult.Name = "btnCheckGRBLResult";
            this.btnCheckGRBLResult.Size = new System.Drawing.Size(20, 20);
            this.btnCheckGRBLResult.TabIndex = 32;
            this.btnCheckGRBLResult.Text = "?";
            this.btnCheckGRBLResult.UseVisualStyleBackColor = true;
            // 
            // btnCheckGRBL
            // 
            this.btnCheckGRBL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCheckGRBL.Location = new System.Drawing.Point(177, 71);
            this.btnCheckGRBL.Name = "btnCheckGRBL";
            this.btnCheckGRBL.Size = new System.Drawing.Size(78, 20);
            this.btnCheckGRBL.TabIndex = 31;
            this.btnCheckGRBL.Text = "Check GRBL";
            this.btnCheckGRBL.UseVisualStyleBackColor = true;
            // 
            // cbStatus
            // 
            this.cbStatus.AutoSize = true;
            this.cbStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbStatus.Location = new System.Drawing.Point(5, 73);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(168, 17);
            this.cbStatus.TabIndex = 17;
            this.cbStatus.Text = "Show Real-time Status Report";
            this.cbStatus.UseVisualStyleBackColor = true;
            // 
            // lblSrA
            // 
            this.lblSrA.AutoSize = true;
            this.lblSrA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrA.Location = new System.Drawing.Point(200, 56);
            this.lblSrA.Name = "lblSrA";
            this.lblSrA.Size = new System.Drawing.Size(29, 13);
            this.lblSrA.TabIndex = 28;
            this.lblSrA.Text = "SFM";
            // 
            // lblSrLn
            // 
            this.lblSrLn.AutoSize = true;
            this.lblSrLn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrLn.Location = new System.Drawing.Point(23, 56);
            this.lblSrLn.Name = "lblSrLn";
            this.lblSrLn.Size = new System.Drawing.Size(37, 13);
            this.lblSrLn.TabIndex = 30;
            this.lblSrLn.Text = "99999";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label51.Location = new System.Drawing.Point(174, 56);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(17, 13);
            this.label51.TabIndex = 27;
            this.label51.Text = "A:";
            // 
            // lblSrState
            // 
            this.lblSrState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSrState.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrState.Location = new System.Drawing.Point(2, 16);
            this.lblSrState.Name = "lblSrState";
            this.lblSrState.Size = new System.Drawing.Size(57, 21);
            this.lblSrState.TabIndex = 14;
            this.lblSrState.Text = "Status";
            this.lblSrState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSrOv
            // 
            this.lblSrOv.AutoSize = true;
            this.lblSrOv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrOv.Location = new System.Drawing.Point(94, 56);
            this.lblSrOv.Name = "lblSrOv";
            this.lblSrOv.Size = new System.Drawing.Size(67, 13);
            this.lblSrOv.TabIndex = 26;
            this.lblSrOv.Text = "100,100,100";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label61.Location = new System.Drawing.Point(2, 56);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(22, 13);
            this.label61.TabIndex = 29;
            this.label61.Text = "Ln:";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label62.Location = new System.Drawing.Point(70, 56);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(24, 13);
            this.label62.TabIndex = 25;
            this.label62.Text = "Ov:";
            // 
            // lblSrPn
            // 
            this.lblSrPn.AutoSize = true;
            this.lblSrPn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrPn.Location = new System.Drawing.Point(200, 43);
            this.lblSrPn.Name = "lblSrPn";
            this.lblSrPn.Size = new System.Drawing.Size(66, 13);
            this.lblSrPn.TabIndex = 24;
            this.lblSrPn.Text = "XYZPDHRS";
            // 
            // lblSrPos
            // 
            this.lblSrPos.AutoSize = true;
            this.lblSrPos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrPos.Location = new System.Drawing.Point(61, 16);
            this.lblSrPos.Name = "lblSrPos";
            this.lblSrPos.Size = new System.Drawing.Size(103, 26);
            this.lblSrPos.TabIndex = 15;
            this.lblSrPos.Text = "0.000,-10.000,5.000\r\n0.000";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label63.Location = new System.Drawing.Point(174, 43);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(23, 13);
            this.label63.TabIndex = 23;
            this.label63.Text = "Pn:";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label64.Location = new System.Drawing.Point(2, 43);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(20, 13);
            this.label64.TabIndex = 19;
            this.label64.Text = "Bf:";
            // 
            // lblSrFS
            // 
            this.lblSrFS.AutoSize = true;
            this.lblSrFS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrFS.Location = new System.Drawing.Point(94, 43);
            this.lblSrFS.Name = "lblSrFS";
            this.lblSrFS.Size = new System.Drawing.Size(52, 13);
            this.lblSrFS.TabIndex = 22;
            this.lblSrFS.Text = "500,8000";
            // 
            // lblSrBf
            // 
            this.lblSrBf.AutoSize = true;
            this.lblSrBf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSrBf.Location = new System.Drawing.Point(23, 43);
            this.lblSrBf.Name = "lblSrBf";
            this.lblSrBf.Size = new System.Drawing.Size(40, 13);
            this.lblSrBf.TabIndex = 20;
            this.lblSrBf.Text = "15,128";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label65.Location = new System.Drawing.Point(70, 43);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(23, 13);
            this.label65.TabIndex = 21;
            this.label65.Text = "FS:";
            // 
            // rtbLog
            // 
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.Location = new System.Drawing.Point(14, 134);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(403, 194);
            this.rtbLog.TabIndex = 11;
            this.rtbLog.Text = "";
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(14, 10);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(58, 21);
            this.cbPort.TabIndex = 6;
            this.cbPort.SelectedIndexChanged += new System.EventHandler(this.cbPort_SelectedIndexChanged_1);
            // 
            // cbBaud
            // 
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            "115200"});
            this.cbBaud.Location = new System.Drawing.Point(75, 10);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(66, 21);
            this.cbBaud.TabIndex = 7;
            this.cbBaud.Text = "115200";
            // 
            // btnScanPort
            // 
            this.btnScanPort.Location = new System.Drawing.Point(194, 9);
            this.btnScanPort.Name = "btnScanPort";
            this.btnScanPort.Size = new System.Drawing.Size(48, 21);
            this.btnScanPort.TabIndex = 9;
            this.btnScanPort.Text = "Scan";
            this.btnScanPort.UseVisualStyleBackColor = true;
            this.btnScanPort.Click += new System.EventHandler(this.btnScanPorts_Click);
            // 
            // btn_OpenPort
            // 
            this.btn_OpenPort.Location = new System.Drawing.Point(143, 9);
            this.btn_OpenPort.Name = "btn_OpenPort";
            this.btn_OpenPort.Size = new System.Drawing.Size(48, 21);
            this.btn_OpenPort.TabIndex = 8;
            this.btn_OpenPort.Text = "Open";
            this.btn_OpenPort.UseVisualStyleBackColor = true;
            this.btn_OpenPort.Click += new System.EventHandler(this.btn_OpenPort_Click_1);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dataGridView1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(711, 607);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Drills";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrillNum,
            this.Size,
            this.NumHoles,
            this.Colour});
            this.dataGridView1.Location = new System.Drawing.Point(18, 14);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Size = new System.Drawing.Size(353, 105);
            this.dataGridView1.TabIndex = 2;
            // 
            // DrillNum
            // 
            this.DrillNum.HeaderText = "#";
            this.DrillNum.Name = "DrillNum";
            this.DrillNum.Width = 50;
            // 
            // Size
            // 
            this.Size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.Width = 56;
            // 
            // NumHoles
            // 
            this.NumHoles.HeaderText = "Num Holes";
            this.NumHoles.Name = "NumHoles";
            // 
            // Colour
            // 
            this.Colour.HeaderText = "Colour";
            this.Colour.Name = "Colour";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.richTextBox1);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(711, 607);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Generate";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 180);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(473, 474);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnDrill);
            this.groupBox6.Controls.Add(this.chk_IncludeHoleInfo);
            this.groupBox6.Controls.Add(this.chk_LeaveZHeight);
            this.groupBox6.Controls.Add(this.chk_IgnorePause);
            this.groupBox6.Controls.Add(this.cb_Drill);
            this.groupBox6.Controls.Add(this.txtDrillDepth);
            this.groupBox6.Controls.Add(this.label48);
            this.groupBox6.Controls.Add(this.txtSafeZHeight);
            this.groupBox6.Controls.Add(this.label39);
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Controls.Add(this.label38);
            this.groupBox6.Controls.Add(this.btn_GenFile);
            this.groupBox6.Location = new System.Drawing.Point(10, 9);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(478, 157);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Generate Options";
            // 
            // btnDrill
            // 
            this.btnDrill.BackColor = System.Drawing.Color.White;
            this.btnDrill.Location = new System.Drawing.Point(79, 116);
            this.btnDrill.Name = "btnDrill";
            this.btnDrill.Size = new System.Drawing.Size(27, 25);
            this.btnDrill.TabIndex = 42;
            this.btnDrill.Text = "1";
            this.btnDrill.UseVisualStyleBackColor = false;
            this.btnDrill.Click += new System.EventHandler(this.btnDrill_Click);
            // 
            // chk_IncludeHoleInfo
            // 
            this.chk_IncludeHoleInfo.AutoSize = true;
            this.chk_IncludeHoleInfo.Location = new System.Drawing.Point(181, 72);
            this.chk_IncludeHoleInfo.Name = "chk_IncludeHoleInfo";
            this.chk_IncludeHoleInfo.Size = new System.Drawing.Size(145, 17);
            this.chk_IncludeHoleInfo.TabIndex = 41;
            this.chk_IncludeHoleInfo.Text = "Include Original Hole Info";
            this.chk_IncludeHoleInfo.UseVisualStyleBackColor = true;
            // 
            // chk_LeaveZHeight
            // 
            this.chk_LeaveZHeight.AutoSize = true;
            this.chk_LeaveZHeight.Checked = true;
            this.chk_LeaveZHeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_LeaveZHeight.Location = new System.Drawing.Point(181, 49);
            this.chk_LeaveZHeight.Name = "chk_LeaveZHeight";
            this.chk_LeaveZHeight.Size = new System.Drawing.Size(147, 17);
            this.chk_LeaveZHeight.TabIndex = 40;
            this.chk_LeaveZHeight.Text = "Leave Z at Safe Z Height";
            this.chk_LeaveZHeight.UseVisualStyleBackColor = true;
            // 
            // chk_IgnorePause
            // 
            this.chk_IgnorePause.AutoSize = true;
            this.chk_IgnorePause.Location = new System.Drawing.Point(181, 21);
            this.chk_IgnorePause.Name = "chk_IgnorePause";
            this.chk_IgnorePause.Size = new System.Drawing.Size(89, 17);
            this.chk_IgnorePause.TabIndex = 39;
            this.chk_IgnorePause.Text = "Ignore Pause";
            this.chk_IgnorePause.UseVisualStyleBackColor = true;
            // 
            // cb_Drill
            // 
            this.cb_Drill.AutoSize = true;
            this.cb_Drill.Checked = true;
            this.cb_Drill.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Drill.Location = new System.Drawing.Point(30, 121);
            this.cb_Drill.Name = "cb_Drill";
            this.cb_Drill.Size = new System.Drawing.Size(43, 17);
            this.cb_Drill.TabIndex = 38;
            this.cb_Drill.Text = "Drill";
            this.cb_Drill.UseVisualStyleBackColor = true;
            // 
            // txtDrillDepth
            // 
            this.txtDrillDepth.Location = new System.Drawing.Point(121, 68);
            this.txtDrillDepth.Name = "txtDrillDepth";
            this.txtDrillDepth.Size = new System.Drawing.Size(31, 20);
            this.txtDrillDepth.TabIndex = 37;
            this.txtDrillDepth.Text = "1";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(43, 71);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(72, 13);
            this.label48.TabIndex = 36;
            this.label48.Text = "Drill Z Depth :";
            // 
            // txtSafeZHeight
            // 
            this.txtSafeZHeight.Location = new System.Drawing.Point(121, 47);
            this.txtSafeZHeight.Name = "txtSafeZHeight";
            this.txtSafeZHeight.Size = new System.Drawing.Size(31, 20);
            this.txtSafeZHeight.TabIndex = 35;
            this.txtSafeZHeight.Text = "0";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(36, 50);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(79, 13);
            this.label39.TabIndex = 34;
            this.label39.Text = "Safe Z Height :";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(121, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(31, 20);
            this.textBox3.TabIndex = 33;
            this.textBox3.Text = "0.5";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(20, 21);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(95, 13);
            this.label38.TabIndex = 32;
            this.label38.Text = "Pause Time (sec) :";
            // 
            // btn_GenFile
            // 
            this.btn_GenFile.Location = new System.Drawing.Point(414, 11);
            this.btn_GenFile.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GenFile.Name = "btn_GenFile";
            this.btn_GenFile.Size = new System.Drawing.Size(59, 32);
            this.btn_GenFile.TabIndex = 31;
            this.btn_GenFile.Text = "Gen File";
            this.btn_GenFile.UseVisualStyleBackColor = true;
            this.btn_GenFile.Click += new System.EventHandler(this.btn_GenFile_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(711, 607);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Camera Offset";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label49);
            this.groupBox5.Controls.Add(this.label50);
            this.groupBox5.Controls.Add(this.txtCameraOffsetY);
            this.groupBox5.Controls.Add(this.txtCameraOffsetX);
            this.groupBox5.Controls.Add(this.lbl_Offset_Y);
            this.groupBox5.Controls.Add(this.lbl_Offset_X);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.label31);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.txt_Offset_Y_Cam);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.btn_Offset_Save);
            this.groupBox5.Controls.Add(this.txt_Offset_X_Cam);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.txt_Offset_Y_Mark);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txt_Offset_X_Mark);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.btn_Offset_Z_Height_Save);
            this.groupBox5.Controls.Add(this.txtBox_Offset_Z_Height);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Location = new System.Drawing.Point(10, 32);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(478, 214);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Camera Offset";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(395, 178);
            this.label49.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(14, 13);
            this.label49.TabIndex = 27;
            this.label49.Text = "Y";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(313, 178);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(14, 13);
            this.label50.TabIndex = 26;
            this.label50.Text = "X";
            // 
            // txtCameraOffsetY
            // 
            this.txtCameraOffsetY.Location = new System.Drawing.Point(415, 175);
            this.txtCameraOffsetY.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraOffsetY.Name = "txtCameraOffsetY";
            this.txtCameraOffsetY.Size = new System.Drawing.Size(53, 20);
            this.txtCameraOffsetY.TabIndex = 25;
            // 
            // txtCameraOffsetX
            // 
            this.txtCameraOffsetX.Location = new System.Drawing.Point(333, 175);
            this.txtCameraOffsetX.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraOffsetX.Name = "txtCameraOffsetX";
            this.txtCameraOffsetX.Size = new System.Drawing.Size(53, 20);
            this.txtCameraOffsetX.TabIndex = 24;
            // 
            // lbl_Offset_Y
            // 
            this.lbl_Offset_Y.AutoSize = true;
            this.lbl_Offset_Y.Location = new System.Drawing.Point(200, 178);
            this.lbl_Offset_Y.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Offset_Y.Name = "lbl_Offset_Y";
            this.lbl_Offset_Y.Size = new System.Drawing.Size(14, 13);
            this.lbl_Offset_Y.TabIndex = 23;
            this.lbl_Offset_Y.Text = "Y";
            // 
            // lbl_Offset_X
            // 
            this.lbl_Offset_X.AutoSize = true;
            this.lbl_Offset_X.Location = new System.Drawing.Point(118, 178);
            this.lbl_Offset_X.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Offset_X.Name = "lbl_Offset_X";
            this.lbl_Offset_X.Size = new System.Drawing.Size(14, 13);
            this.lbl_Offset_X.TabIndex = 22;
            this.lbl_Offset_X.Text = "X";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(183, 178);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(14, 13);
            this.label30.TabIndex = 21;
            this.label30.Text = "Y";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(101, 178);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 13);
            this.label31.TabIndex = 20;
            this.label31.Text = "X";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(53, 178);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(44, 13);
            this.label29.TabIndex = 19;
            this.label29.Text = "Offset : ";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(241, 140);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(14, 13);
            this.label26.TabIndex = 18;
            this.label26.Text = "Y";
            // 
            // txt_Offset_Y_Cam
            // 
            this.txt_Offset_Y_Cam.Location = new System.Drawing.Point(258, 137);
            this.txt_Offset_Y_Cam.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Offset_Y_Cam.Name = "txt_Offset_Y_Cam";
            this.txt_Offset_Y_Cam.Size = new System.Drawing.Size(53, 20);
            this.txt_Offset_Y_Cam.TabIndex = 17;
            this.txt_Offset_Y_Cam.TextChanged += new System.EventHandler(this.txt_Offset_X_Mark_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(159, 140);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 13);
            this.label27.TabIndex = 16;
            this.label27.Text = "X";
            // 
            // btn_Offset_Save
            // 
            this.btn_Offset_Save.Location = new System.Drawing.Point(258, 173);
            this.btn_Offset_Save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Offset_Save.Name = "btn_Offset_Save";
            this.btn_Offset_Save.Size = new System.Drawing.Size(45, 24);
            this.btn_Offset_Save.TabIndex = 15;
            this.btn_Offset_Save.Text = "Save";
            this.btn_Offset_Save.UseVisualStyleBackColor = true;
            this.btn_Offset_Save.Click += new System.EventHandler(this.btn_Offset_Save_Click);
            // 
            // txt_Offset_X_Cam
            // 
            this.txt_Offset_X_Cam.Location = new System.Drawing.Point(176, 137);
            this.txt_Offset_X_Cam.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Offset_X_Cam.Name = "txt_Offset_X_Cam";
            this.txt_Offset_X_Cam.Size = new System.Drawing.Size(53, 20);
            this.txt_Offset_X_Cam.TabIndex = 14;
            this.txt_Offset_X_Cam.TextChanged += new System.EventHandler(this.txt_Offset_X_Mark_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(155, 123);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(83, 13);
            this.label28.TabIndex = 13;
            this.label28.Text = "Record Drill Pos";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(15, 109);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(244, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "4. Move Drill so Camera Cross-Hairs are over Mark";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(241, 82);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(14, 13);
            this.label24.TabIndex = 11;
            this.label24.Text = "Y";
            // 
            // txt_Offset_Y_Mark
            // 
            this.txt_Offset_Y_Mark.Location = new System.Drawing.Point(258, 80);
            this.txt_Offset_Y_Mark.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Offset_Y_Mark.Name = "txt_Offset_Y_Mark";
            this.txt_Offset_Y_Mark.Size = new System.Drawing.Size(53, 20);
            this.txt_Offset_Y_Mark.TabIndex = 10;
            this.txt_Offset_Y_Mark.TextChanged += new System.EventHandler(this.txt_Offset_X_Mark_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(159, 82);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(14, 13);
            this.label23.TabIndex = 9;
            this.label23.Text = "X";
            // 
            // txt_Offset_X_Mark
            // 
            this.txt_Offset_X_Mark.Location = new System.Drawing.Point(176, 80);
            this.txt_Offset_X_Mark.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Offset_X_Mark.Name = "txt_Offset_X_Mark";
            this.txt_Offset_X_Mark.Size = new System.Drawing.Size(53, 20);
            this.txt_Offset_X_Mark.TabIndex = 7;
            this.txt_Offset_X_Mark.TextChanged += new System.EventHandler(this.txt_Offset_X_Mark_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(155, 66);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(83, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "Record Drill Pos";
            // 
            // btn_Offset_Z_Height_Save
            // 
            this.btn_Offset_Z_Height_Save.Location = new System.Drawing.Point(398, 32);
            this.btn_Offset_Z_Height_Save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Offset_Z_Height_Save.Name = "btn_Offset_Z_Height_Save";
            this.btn_Offset_Z_Height_Save.Size = new System.Drawing.Size(45, 24);
            this.btn_Offset_Z_Height_Save.TabIndex = 5;
            this.btn_Offset_Z_Height_Save.Text = "Save";
            this.btn_Offset_Z_Height_Save.UseVisualStyleBackColor = true;
            this.btn_Offset_Z_Height_Save.Click += new System.EventHandler(this.btn_Offset_Z_Height_Save_Click);
            // 
            // txtBox_Offset_Z_Height
            // 
            this.txtBox_Offset_Z_Height.Location = new System.Drawing.Point(333, 35);
            this.txtBox_Offset_Z_Height.Margin = new System.Windows.Forms.Padding(2);
            this.txtBox_Offset_Z_Height.Name = "txtBox_Offset_Z_Height";
            this.txtBox_Offset_Z_Height.Size = new System.Drawing.Size(62, 20);
            this.txtBox_Offset_Z_Height.TabIndex = 4;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(242, 37);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Record Z Height:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 66);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "3. Make a Mark with Drill";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 37);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(219, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "2. Move Drill Down to +/- 10mm above Work";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 20);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "1. Home Drill";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnBounceHome2);
            this.tabPage2.Controls.Add(this.btnBounceHome0);
            this.tabPage2.Controls.Add(this.txtFeed);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.txtScanHeight);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnGrabCamHole2);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnStreamCode);
            this.tabPage2.Controls.Add(this.btn_camera_start);
            this.tabPage2.Controls.Add(this.btnGrabCamHole0);
            this.tabPage2.Controls.Add(this.picBox_Video);
            this.tabPage2.Controls.Add(this.chk_Jog);
            this.tabPage2.Controls.Add(this.cmb_Camera);
            this.tabPage2.Controls.Add(this.lblMachinePosition);
            this.tabPage2.Controls.Add(this.label66);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(711, 607);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Camera";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnBounceHome2
            // 
            this.btnBounceHome2.Location = new System.Drawing.Point(82, 74);
            this.btnBounceHome2.Margin = new System.Windows.Forms.Padding(2);
            this.btnBounceHome2.Name = "btnBounceHome2";
            this.btnBounceHome2.Size = new System.Drawing.Size(66, 21);
            this.btnBounceHome2.TabIndex = 42;
            this.btnBounceHome2.Text = "Bounce2";
            this.btnBounceHome2.UseVisualStyleBackColor = true;
            this.btnBounceHome2.Click += new System.EventHandler(this.btnBounceHome2_Click);
            // 
            // btnBounceHome0
            // 
            this.btnBounceHome0.Location = new System.Drawing.Point(82, 47);
            this.btnBounceHome0.Margin = new System.Windows.Forms.Padding(2);
            this.btnBounceHome0.Name = "btnBounceHome0";
            this.btnBounceHome0.Size = new System.Drawing.Size(66, 21);
            this.btnBounceHome0.TabIndex = 41;
            this.btnBounceHome0.Text = "Bounce0";
            this.btnBounceHome0.UseVisualStyleBackColor = true;
            this.btnBounceHome0.Click += new System.EventHandler(this.btnBounceHome0_Click);
            // 
            // txtFeed
            // 
            this.txtFeed.Location = new System.Drawing.Point(406, 51);
            this.txtFeed.Name = "txtFeed";
            this.txtFeed.Size = new System.Drawing.Size(38, 20);
            this.txtFeed.TabIndex = 40;
            this.txtFeed.Text = "2000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(363, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Feed :";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(397, 72);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 21);
            this.button3.TabIndex = 38;
            this.button3.Text = "G0 Z Scan";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtScanHeight
            // 
            this.txtScanHeight.Location = new System.Drawing.Point(366, 72);
            this.txtScanHeight.Name = "txtScanHeight";
            this.txtScanHeight.Size = new System.Drawing.Size(26, 20);
            this.txtScanHeight.TabIndex = 37;
            this.txtScanHeight.Text = "40";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Scan Height:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(207, 73);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 21);
            this.button2.TabIndex = 35;
            this.button2.Text = "G92 X0 Y0 Z0 ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 73);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 21);
            this.button1.TabIndex = 34;
            this.button1.Text = "$H";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btnStreamCode
            // 
            this.btnStreamCode.Location = new System.Drawing.Point(152, 49);
            this.btnStreamCode.Margin = new System.Windows.Forms.Padding(2);
            this.btnStreamCode.Name = "btnStreamCode";
            this.btnStreamCode.Size = new System.Drawing.Size(180, 21);
            this.btnStreamCode.TabIndex = 33;
            this.btnStreamCode.Text = "Stream Generated Code";
            this.btnStreamCode.UseVisualStyleBackColor = true;
            this.btnStreamCode.Click += new System.EventHandler(this.btnStreamCode_Click);
            // 
            // btn_camera_start
            // 
            this.btn_camera_start.Location = new System.Drawing.Point(217, 24);
            this.btn_camera_start.Margin = new System.Windows.Forms.Padding(2);
            this.btn_camera_start.Name = "btn_camera_start";
            this.btn_camera_start.Size = new System.Drawing.Size(51, 21);
            this.btn_camera_start.TabIndex = 2;
            this.btn_camera_start.Text = "Start";
            this.btn_camera_start.UseVisualStyleBackColor = true;
            this.btn_camera_start.Click += new System.EventHandler(this.btn_camera_start_Click);
            // 
            // picBox_Video
            // 
            this.picBox_Video.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Video.Location = new System.Drawing.Point(12, 117);
            this.picBox_Video.Margin = new System.Windows.Forms.Padding(2);
            this.picBox_Video.Name = "picBox_Video";
            this.picBox_Video.Size = new System.Drawing.Size(693, 486);
            this.picBox_Video.TabIndex = 1;
            this.picBox_Video.TabStop = false;
            // 
            // chk_Jog
            // 
            this.chk_Jog.AutoSize = true;
            this.chk_Jog.Checked = true;
            this.chk_Jog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Jog.Location = new System.Drawing.Point(281, 26);
            this.chk_Jog.Name = "chk_Jog";
            this.chk_Jog.Size = new System.Drawing.Size(99, 17);
            this.chk_Jog.TabIndex = 32;
            this.chk_Jog.Text = "Enable Jogging";
            this.chk_Jog.UseVisualStyleBackColor = true;
            // 
            // cmb_Camera
            // 
            this.cmb_Camera.FormattingEnabled = true;
            this.cmb_Camera.Location = new System.Drawing.Point(88, 24);
            this.cmb_Camera.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_Camera.Name = "cmb_Camera";
            this.cmb_Camera.Size = new System.Drawing.Size(126, 21);
            this.cmb_Camera.TabIndex = 1;
            this.cmb_Camera.SelectedIndexChanged += new System.EventHandler(this.cmb_Camera_SelectedIndexChanged);
            // 
            // lblMachinePosition
            // 
            this.lblMachinePosition.AutoSize = true;
            this.lblMachinePosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachinePosition.Location = new System.Drawing.Point(505, 20);
            this.lblMachinePosition.Name = "lblMachinePosition";
            this.lblMachinePosition.Size = new System.Drawing.Size(74, 20);
            this.lblMachinePosition.TabIndex = 31;
            this.lblMachinePosition.Text = "X: Y: Z: ";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(402, 26);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(97, 13);
            this.label66.TabIndex = 30;
            this.label66.Text = "Machine Position : ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 28);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Select Camera";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(553, 11);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(719, 633);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 657);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_DrillFile);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "PCB DR (Drill Robot)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Box_Orig)).EndInit();
            this.gb_DrillFile.ResumeLayout(false);
            this.gb_DrillFile.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Video)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gb_DrillFile;
        private System.Windows.Forms.PictureBox pic_Box_Orig;
        private System.Windows.Forms.Button btn_Drill_File_Open;
        private System.Windows.Forms.Button btn_Org_SetHole1;
        private System.Windows.Forms.Label lbl_DrillFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lbl_Hole0_Y;
        private System.Windows.Forms.Label lbl_Hole0_X;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cb_Flip;
        private System.Windows.Forms.Label lbl_Angle;
        private System.Windows.Forms.Label lbl_Dist;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lbl_Hole2_Y;
        private System.Windows.Forms.Label lbl_Hole2_X;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Button btn_Org_SetHole2;
        private System.Windows.Forms.Label lbl_PCB_Angle;
        private System.Windows.Forms.Label lbl_PCB_Scale;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Timer timerSerial;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button btnGrabCamHole2;
        private System.Windows.Forms.Button btnGrabCamHole0;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPCBHole2_Y;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label lblPCBHole2_X;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPCBHole0_Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPCBHole0_X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.ComboBox cBCommand;
        private System.Windows.Forms.Button btnGRBLReset;
        private System.Windows.Forms.Button btnGRBLCommand4;
        private System.Windows.Forms.Button btnGRBLCommand3;
        private System.Windows.Forms.Button btnGRBLCommand2;
        private System.Windows.Forms.Button btnGRBLCommand1;
        private System.Windows.Forms.Button btnGRBLCommand0;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnCheckGRBLResult;
        private System.Windows.Forms.Button btnCheckGRBL;
        private System.Windows.Forms.CheckBox cbStatus;
        private System.Windows.Forms.Label lblSrA;
        private System.Windows.Forms.Label lblSrLn;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblSrState;
        private System.Windows.Forms.Label lblSrOv;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label lblSrPn;
        private System.Windows.Forms.Label lblSrPos;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label lblSrFS;
        private System.Windows.Forms.Label lblSrBf;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.Button btnScanPort;
        private System.Windows.Forms.Button btn_OpenPort;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrillNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumHoles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colour;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chk_IncludeHoleInfo;
        private System.Windows.Forms.CheckBox chk_LeaveZHeight;
        private System.Windows.Forms.CheckBox chk_IgnorePause;
        private System.Windows.Forms.CheckBox cb_Drill;
        private System.Windows.Forms.TextBox txtDrillDepth;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtSafeZHeight;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btn_GenFile;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txtCameraOffsetY;
        private System.Windows.Forms.TextBox txtCameraOffsetX;
        private System.Windows.Forms.Label lbl_Offset_Y;
        private System.Windows.Forms.Label lbl_Offset_X;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_Offset_Y_Cam;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btn_Offset_Save;
        private System.Windows.Forms.TextBox txt_Offset_X_Cam;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_Offset_Y_Mark;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_Offset_X_Mark;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btn_Offset_Z_Height_Save;
        private System.Windows.Forms.TextBox txtBox_Offset_Z_Height;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_camera_start;
        private System.Windows.Forms.PictureBox picBox_Video;
        private System.Windows.Forms.CheckBox chk_Jog;
        private System.Windows.Forms.ComboBox cmb_Camera;
        private System.Windows.Forms.Label lblMachinePosition;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lblPCBDist;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPCBRotationAngle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnStreamCode;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtScanHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFeed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnDrill;
        private System.Windows.Forms.CheckBox chkMoveToClickedHole;
        private System.Windows.Forms.Button btnBounceHome2;
        private System.Windows.Forms.Button btnBounceHome0;
    }
}

