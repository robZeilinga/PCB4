/*  GRBL-Plotter. Another GCode sender for GRBL.
    This file is part of the GRBL-Plotter application.
   
    Copyright (C) 2015-2016 Sven Hasemann contact: svenhb@web.de

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace PCB4
{
    partial class ControlSerialForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlSerialForm));
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnScanPort = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnGRBLCommand0 = new System.Windows.Forms.Button();
            this.btnGRBLCommand1 = new System.Windows.Forms.Button();
            this.btnGRBLCommand2 = new System.Windows.Forms.Button();
            this.btnGRBLCommand3 = new System.Windows.Forms.Button();
            this.toolTipSerial = new System.Windows.Forms.ToolTip(this.components);
            this.btnGRBLCommand4 = new System.Windows.Forms.Button();
            this.lblSrPos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSrBf = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSrFS = new System.Windows.Forms.Label();
            this.lblSrPn = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSrOv = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSrA = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSrLn = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCheckGRBL = new System.Windows.Forms.Button();
            this.btnGRBLReset = new System.Windows.Forms.Button();
            this.lblSrState = new System.Windows.Forms.Label();
            this.cBCommand = new System.Windows.Forms.ComboBox();
            this.cbStatus = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckGRBLResult = new System.Windows.Forms.Button();
            this.timerSerial = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_Jog = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblHoleZeroX = new System.Windows.Forms.Label();
            this.lblHoleZeroY = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblHole2Y = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblHole2X = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnGrabHole0 = new System.Windows.Forms.Button();
            this.btnGrabHole2 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            resources.ApplyResources(this.cbPort, "cbPort");
            this.cbPort.Name = "cbPort";
            this.toolTipSerial.SetToolTip(this.cbPort, resources.GetString("cbPort.ToolTip"));
            // 
            // cbBaud
            // 
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            resources.GetString("cbBaud.Items"),
            resources.GetString("cbBaud.Items1"),
            resources.GetString("cbBaud.Items2"),
            resources.GetString("cbBaud.Items3"),
            resources.GetString("cbBaud.Items4")});
            resources.ApplyResources(this.cbBaud, "cbBaud");
            this.cbBaud.Name = "cbBaud";
            this.toolTipSerial.SetToolTip(this.cbBaud, resources.GetString("cbBaud.ToolTip"));
            // 
            // btnOpenPort
            // 
            resources.ApplyResources(this.btnOpenPort, "btnOpenPort");
            this.btnOpenPort.Name = "btnOpenPort";
            this.toolTipSerial.SetToolTip(this.btnOpenPort, resources.GetString("btnOpenPort.ToolTip"));
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnScanPort
            // 
            resources.ApplyResources(this.btnScanPort, "btnScanPort");
            this.btnScanPort.Name = "btnScanPort";
            this.toolTipSerial.SetToolTip(this.btnScanPort, resources.GetString("btnScanPort.ToolTip"));
            this.btnScanPort.UseVisualStyleBackColor = true;
            this.btnScanPort.Click += new System.EventHandler(this.btnScanPort_Click);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.ReadBufferSize = 2048;
            this.serialPort.ReadTimeout = 3000;
            this.serialPort.WriteTimeout = 3000;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // rtbLog
            // 
            resources.ApplyResources(this.rtbLog, "rtbLog");
            this.rtbLog.Name = "rtbLog";
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.toolTipSerial.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.toolTipSerial.SetToolTip(this.btnSend, resources.GetString("btnSend.ToolTip"));
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnGRBLCommand0
            // 
            resources.ApplyResources(this.btnGRBLCommand0, "btnGRBLCommand0");
            this.btnGRBLCommand0.Name = "btnGRBLCommand0";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand0, resources.GetString("btnGRBLCommand0.ToolTip"));
            this.btnGRBLCommand0.UseVisualStyleBackColor = true;
            this.btnGRBLCommand0.Click += new System.EventHandler(this.btnGRBLCommand0_Click);
            // 
            // btnGRBLCommand1
            // 
            resources.ApplyResources(this.btnGRBLCommand1, "btnGRBLCommand1");
            this.btnGRBLCommand1.Name = "btnGRBLCommand1";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand1, resources.GetString("btnGRBLCommand1.ToolTip"));
            this.btnGRBLCommand1.UseVisualStyleBackColor = true;
            this.btnGRBLCommand1.Click += new System.EventHandler(this.btnGRBLCommand1_Click);
            // 
            // btnGRBLCommand2
            // 
            resources.ApplyResources(this.btnGRBLCommand2, "btnGRBLCommand2");
            this.btnGRBLCommand2.Name = "btnGRBLCommand2";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand2, resources.GetString("btnGRBLCommand2.ToolTip"));
            this.btnGRBLCommand2.UseVisualStyleBackColor = true;
            this.btnGRBLCommand2.Click += new System.EventHandler(this.btnGRBLCommand2_Click);
            // 
            // btnGRBLCommand3
            // 
            resources.ApplyResources(this.btnGRBLCommand3, "btnGRBLCommand3");
            this.btnGRBLCommand3.Name = "btnGRBLCommand3";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand3, resources.GetString("btnGRBLCommand3.ToolTip"));
            this.btnGRBLCommand3.UseVisualStyleBackColor = true;
            this.btnGRBLCommand3.Click += new System.EventHandler(this.btnGRBLCommand3_Click);
            // 
            // btnGRBLCommand4
            // 
            resources.ApplyResources(this.btnGRBLCommand4, "btnGRBLCommand4");
            this.btnGRBLCommand4.Name = "btnGRBLCommand4";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand4, resources.GetString("btnGRBLCommand4.ToolTip"));
            this.btnGRBLCommand4.UseVisualStyleBackColor = true;
            this.btnGRBLCommand4.Click += new System.EventHandler(this.btnGRBLCommand4_Click);
            // 
            // lblSrPos
            // 
            resources.ApplyResources(this.lblSrPos, "lblSrPos");
            this.lblSrPos.Name = "lblSrPos";
            this.toolTipSerial.SetToolTip(this.lblSrPos, resources.GetString("lblSrPos.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTipSerial.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // lblSrBf
            // 
            resources.ApplyResources(this.lblSrBf, "lblSrBf");
            this.lblSrBf.Name = "lblSrBf";
            this.toolTipSerial.SetToolTip(this.lblSrBf, resources.GetString("lblSrBf.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTipSerial.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // lblSrFS
            // 
            resources.ApplyResources(this.lblSrFS, "lblSrFS");
            this.lblSrFS.Name = "lblSrFS";
            this.toolTipSerial.SetToolTip(this.lblSrFS, resources.GetString("lblSrFS.ToolTip"));
            // 
            // lblSrPn
            // 
            resources.ApplyResources(this.lblSrPn, "lblSrPn");
            this.lblSrPn.Name = "lblSrPn";
            this.toolTipSerial.SetToolTip(this.lblSrPn, resources.GetString("lblSrPn.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTipSerial.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // lblSrOv
            // 
            resources.ApplyResources(this.lblSrOv, "lblSrOv");
            this.lblSrOv.Name = "lblSrOv";
            this.toolTipSerial.SetToolTip(this.lblSrOv, resources.GetString("lblSrOv.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTipSerial.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // lblSrA
            // 
            resources.ApplyResources(this.lblSrA, "lblSrA");
            this.lblSrA.Name = "lblSrA";
            this.toolTipSerial.SetToolTip(this.lblSrA, resources.GetString("lblSrA.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTipSerial.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // lblSrLn
            // 
            resources.ApplyResources(this.lblSrLn, "lblSrLn");
            this.lblSrLn.Name = "lblSrLn";
            this.toolTipSerial.SetToolTip(this.lblSrLn, resources.GetString("lblSrLn.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTipSerial.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // btnCheckGRBL
            // 
            resources.ApplyResources(this.btnCheckGRBL, "btnCheckGRBL");
            this.btnCheckGRBL.Name = "btnCheckGRBL";
            this.toolTipSerial.SetToolTip(this.btnCheckGRBL, resources.GetString("btnCheckGRBL.ToolTip"));
            this.btnCheckGRBL.UseVisualStyleBackColor = true;
            this.btnCheckGRBL.Click += new System.EventHandler(this.btnCheckGRBL_Click);
            // 
            // btnGRBLReset
            // 
            resources.ApplyResources(this.btnGRBLReset, "btnGRBLReset");
            this.btnGRBLReset.Name = "btnGRBLReset";
            this.btnGRBLReset.UseVisualStyleBackColor = true;
            this.btnGRBLReset.Click += new System.EventHandler(this.btnGRBLReset_Click);
            // 
            // lblSrState
            // 
            resources.ApplyResources(this.lblSrState, "lblSrState");
            this.lblSrState.Name = "lblSrState";
            // 
            // cBCommand
            // 
            this.cBCommand.FormattingEnabled = true;
            this.cBCommand.Items.AddRange(new object[] {
            resources.GetString("cBCommand.Items"),
            resources.GetString("cBCommand.Items1"),
            resources.GetString("cBCommand.Items2"),
            resources.GetString("cBCommand.Items3"),
            resources.GetString("cBCommand.Items4"),
            resources.GetString("cBCommand.Items5"),
            resources.GetString("cBCommand.Items6")});
            resources.ApplyResources(this.cBCommand, "cBCommand");
            this.cBCommand.Name = "cBCommand";
            this.cBCommand.SelectedIndexChanged += new System.EventHandler(this.cBCommand_SelectedIndexChanged);
            this.cBCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommand_KeyPress);
            // 
            // cbStatus
            // 
            resources.ApplyResources(this.cbStatus, "cbStatus");
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.UseVisualStyleBackColor = true;
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
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnCheckGRBLResult
            // 
            resources.ApplyResources(this.btnCheckGRBLResult, "btnCheckGRBLResult");
            this.btnCheckGRBLResult.Name = "btnCheckGRBLResult";
            this.btnCheckGRBLResult.UseVisualStyleBackColor = true;
            this.btnCheckGRBLResult.Click += new System.EventHandler(this.btnCheckGRBLResult_Click);
            // 
            // timerSerial
            // 
            this.timerSerial.Interval = 500;
            this.timerSerial.Tick += new System.EventHandler(this.timerSerial_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chk_Jog);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chk_Jog
            // 
            resources.ApplyResources(this.chk_Jog, "chk_Jog");
            this.chk_Jog.Name = "chk_Jog";
            this.chk_Jog.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // lblHoleZeroX
            // 
            resources.ApplyResources(this.lblHoleZeroX, "lblHoleZeroX");
            this.lblHoleZeroX.Name = "lblHoleZeroX";
            // 
            // lblHoleZeroY
            // 
            resources.ApplyResources(this.lblHoleZeroY, "lblHoleZeroY");
            this.lblHoleZeroY.Name = "lblHoleZeroY";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // lblHole2Y
            // 
            resources.ApplyResources(this.lblHole2Y, "lblHole2Y");
            this.lblHole2Y.Name = "lblHole2Y";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // lblHole2X
            // 
            resources.ApplyResources(this.lblHole2X, "lblHole2X");
            this.lblHole2X.Name = "lblHole2X";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // btnGrabHole0
            // 
            resources.ApplyResources(this.btnGrabHole0, "btnGrabHole0");
            this.btnGrabHole0.Name = "btnGrabHole0";
            this.btnGrabHole0.UseVisualStyleBackColor = true;
            this.btnGrabHole0.Click += new System.EventHandler(this.btnGrabHole0_Click);
            // 
            // btnGrabHole2
            // 
            resources.ApplyResources(this.btnGrabHole2, "btnGrabHole2");
            this.btnGrabHole2.Name = "btnGrabHole2";
            this.btnGrabHole2.UseVisualStyleBackColor = true;
            this.btnGrabHole2.Click += new System.EventHandler(this.btnGrabHole2_Click);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // ControlSerialForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGrabHole2);
            this.Controls.Add(this.btnGrabHole0);
            this.Controls.Add(this.lblHole2Y);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblHole2X);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lblHoleZeroY);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblHoleZeroX);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cBCommand);
            this.Controls.Add(this.btnGRBLReset);
            this.Controls.Add(this.btnGRBLCommand4);
            this.Controls.Add(this.btnGRBLCommand3);
            this.Controls.Add(this.btnGRBLCommand2);
            this.Controls.Add(this.btnGRBLCommand1);
            this.Controls.Add(this.btnGRBLCommand0);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnScanPort);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.cbBaud);
            this.Controls.Add(this.cbPort);
            this.MaximizeBox = false;
            this.Name = "ControlSerialForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialForm_FormClosing);
            this.Load += new System.EventHandler(this.SerialForm_Load);
            this.Resize += new System.EventHandler(this.SerialForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnScanPort;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnGRBLCommand0;
        private System.Windows.Forms.Button btnGRBLCommand1;
        private System.Windows.Forms.ToolTip toolTipSerial;
        private System.Windows.Forms.Button btnGRBLCommand2;
        private System.Windows.Forms.Button btnGRBLCommand3;
        private System.Windows.Forms.Timer timerSerial;
        private System.Windows.Forms.Button btnGRBLCommand4;
        private System.Windows.Forms.Button btnGRBLReset;
        private System.Windows.Forms.Label lblSrState;
        private System.Windows.Forms.Label lblSrPos;
        private System.Windows.Forms.ComboBox cBCommand;
        private System.Windows.Forms.CheckBox cbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSrBf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSrFS;
        private System.Windows.Forms.Label lblSrPn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSrOv;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSrA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSrLn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCheckGRBL;
        private System.Windows.Forms.Button btnCheckGRBLResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chk_Jog;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblHoleZeroX;
        private System.Windows.Forms.Label lblHoleZeroY;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblHole2Y;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblHole2X;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnGrabHole0;
        private System.Windows.Forms.Button btnGrabHole2;
        private System.Windows.Forms.Label label19;
    }
}

*/