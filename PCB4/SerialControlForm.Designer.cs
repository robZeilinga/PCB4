namespace PCB4
{
    partial class SerialControlForm
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
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnScanPort = new System.Windows.Forms.Button();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckGRBLResult = new System.Windows.Forms.Button();
            this.btnCheckGRBL = new System.Windows.Forms.Button();
            this.cbStatus = new System.Windows.Forms.CheckBox();
            this.lblSrA = new System.Windows.Forms.Label();
            this.lblSrLn = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSrState = new System.Windows.Forms.Label();
            this.lblSrOv = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSrPn = new System.Windows.Forms.Label();
            this.lblSrPos = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSrFS = new System.Windows.Forms.Label();
            this.lblSrBf = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cBCommand = new System.Windows.Forms.ComboBox();
            this.btnGRBLReset = new System.Windows.Forms.Button();
            this.btnGRBLCommand4 = new System.Windows.Forms.Button();
            this.btnGRBLCommand3 = new System.Windows.Forms.Button();
            this.btnGRBLCommand2 = new System.Windows.Forms.Button();
            this.btnGRBLCommand1 = new System.Windows.Forms.Button();
            this.btnGRBLCommand0 = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.toolTipSerial = new System.Windows.Forms.ToolTip(this.components);
            this.timerSerial = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(-1, 122);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(276, 330);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // btnScanPort
            // 
            this.btnScanPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnScanPort.Location = new System.Drawing.Point(204, 1);
            this.btnScanPort.Name = "btnScanPort";
            this.btnScanPort.Size = new System.Drawing.Size(73, 23);
            this.btnScanPort.TabIndex = 7;
            this.btnScanPort.Text = "Scan Ports";
            this.btnScanPort.UseVisualStyleBackColor = true;
            this.btnScanPort.Click += new System.EventHandler(this.btnScanPort_Click_1);
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenPort.Location = new System.Drawing.Point(132, 1);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(73, 23);
            this.btnOpenPort.TabIndex = 6;
            this.btnOpenPort.Text = "Open";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click_1);
            // 
            // cbBaud
            // 
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaud.Location = new System.Drawing.Point(70, 1);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(62, 21);
            this.cbBaud.TabIndex = 5;
            this.cbBaud.Text = "115200";
            // 
            // cbPort
            // 
            this.cbPort.DropDownWidth = 270;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(0, 1);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(69, 21);
            this.cbPort.TabIndex = 4;
            this.cbPort.Text = "COM123";
            this.cbPort.TextChanged += new System.EventHandler(this.cbPort_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckGRBLResult);
            this.groupBox1.Controls.Add(this.btnCheckGRBL);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.lblSrA);
            this.groupBox1.Controls.Add(this.lblSrLn);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblSrState);
            this.groupBox1.Controls.Add(this.lblSrOv);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblSrPn);
            this.groupBox1.Controls.Add(this.lblSrPos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblSrFS);
            this.groupBox1.Controls.Add(this.lblSrBf);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(1, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 94);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Real-time Status Report";
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(174, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "A:";
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(2, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Ln:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(70, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Ov:";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(174, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Pn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(2, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Bf:";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(70, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "FS:";
            // 
            // cBCommand
            // 
            this.cBCommand.FormattingEnabled = true;
            this.cBCommand.Items.AddRange(new object[] {
            "$H (Homing)",
            "G90 G1 X1 F500 (absolute)",
            "G91 G1 X1 F500 (relarive)"});
            this.cBCommand.Location = new System.Drawing.Point(73, 457);
            this.cBCommand.Name = "cBCommand";
            this.cBCommand.Size = new System.Drawing.Size(156, 21);
            this.cBCommand.TabIndex = 41;
            // 
            // btnGRBLReset
            // 
            this.btnGRBLReset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLReset.Location = new System.Drawing.Point(189, 479);
            this.btnGRBLReset.Name = "btnGRBLReset";
            this.btnGRBLReset.Size = new System.Drawing.Size(89, 23);
            this.btnGRBLReset.TabIndex = 40;
            this.btnGRBLReset.Text = "RESET";
            this.btnGRBLReset.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand4
            // 
            this.btnGRBLCommand4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand4.Location = new System.Drawing.Point(151, 479);
            this.btnGRBLCommand4.Name = "btnGRBLCommand4";
            this.btnGRBLCommand4.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand4.TabIndex = 39;
            this.btnGRBLCommand4.Text = "$X";
            this.btnGRBLCommand4.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand3
            // 
            this.btnGRBLCommand3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand3.Location = new System.Drawing.Point(113, 479);
            this.btnGRBLCommand3.Name = "btnGRBLCommand3";
            this.btnGRBLCommand3.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand3.TabIndex = 38;
            this.btnGRBLCommand3.Text = "$N";
            this.btnGRBLCommand3.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand2
            // 
            this.btnGRBLCommand2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand2.Location = new System.Drawing.Point(75, 479);
            this.btnGRBLCommand2.Name = "btnGRBLCommand2";
            this.btnGRBLCommand2.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand2.TabIndex = 37;
            this.btnGRBLCommand2.Text = "$#";
            this.btnGRBLCommand2.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand1
            // 
            this.btnGRBLCommand1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand1.Location = new System.Drawing.Point(37, 479);
            this.btnGRBLCommand1.Name = "btnGRBLCommand1";
            this.btnGRBLCommand1.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand1.TabIndex = 36;
            this.btnGRBLCommand1.Text = "$$";
            this.btnGRBLCommand1.UseVisualStyleBackColor = true;
            // 
            // btnGRBLCommand0
            // 
            this.btnGRBLCommand0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGRBLCommand0.Location = new System.Drawing.Point(-1, 479);
            this.btnGRBLCommand0.Name = "btnGRBLCommand0";
            this.btnGRBLCommand0.Size = new System.Drawing.Size(32, 23);
            this.btnGRBLCommand0.TabIndex = 35;
            this.btnGRBLCommand0.Text = "$";
            this.btnGRBLCommand0.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSend.Location = new System.Drawing.Point(235, 455);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(43, 23);
            this.btnSend.TabIndex = 34;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(-1, 455);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 23);
            this.btnClear.TabIndex = 33;
            this.btnClear.Text = "Clear Log";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.ReadBufferSize = 2048;
            this.serialPort.ReadTimeout = 3000;
            this.serialPort.WriteTimeout = 3000;
            // 
            // timerSerial
            // 
            this.timerSerial.Interval = 500;
            // 
            // SerialControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 505);
            this.Controls.Add(this.cBCommand);
            this.Controls.Add(this.btnGRBLReset);
            this.Controls.Add(this.btnGRBLCommand4);
            this.Controls.Add(this.btnGRBLCommand3);
            this.Controls.Add(this.btnGRBLCommand2);
            this.Controls.Add(this.btnGRBLCommand1);
            this.Controls.Add(this.btnGRBLCommand0);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnScanPort);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.cbBaud);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.rtbLog);
            this.Name = "SerialControlForm";
            this.Text = "SerialControlForm";
            this.Load += new System.EventHandler(this.SerialForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnScanPort;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCheckGRBLResult;
        private System.Windows.Forms.Button btnCheckGRBL;
        private System.Windows.Forms.CheckBox cbStatus;
        private System.Windows.Forms.Label lblSrA;
        private System.Windows.Forms.Label lblSrLn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSrState;
        private System.Windows.Forms.Label lblSrOv;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSrPn;
        private System.Windows.Forms.Label lblSrPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSrFS;
        private System.Windows.Forms.Label lblSrBf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBCommand;
        private System.Windows.Forms.Button btnGRBLReset;
        private System.Windows.Forms.Button btnGRBLCommand4;
        private System.Windows.Forms.Button btnGRBLCommand3;
        private System.Windows.Forms.Button btnGRBLCommand2;
        private System.Windows.Forms.Button btnGRBLCommand1;
        private System.Windows.Forms.Button btnGRBLCommand0;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ToolTip toolTipSerial;
        private System.Windows.Forms.Timer timerSerial;
    }
}