/*  
 *  PCB DR (Drilling Robot)
 *  
 *  Inspired by Sven's Awesome work (GRBL-PLOTTER) 
 *  
 *  Serial Comms, GRBL Status Translation and Video Display almost entirely From GRBL PLOTTER
 *  
 *  Thanks to   https://github.com/svenhb/GRBL-Plotter
 *  
 * 
    Copyright (C) 2019 Rob Zeilinga contact: glock17ssp@gmail.com

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
*/


using DirectShowLib;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PCB_DR
{
    public partial class Form1 : Form
    {
        //==================== camera stuff ==========================
        // Open CV stuff =======================
        VideoCapture _capture = null;
        Image<Bgr, Byte> iFrame;

        double webcam_frm_cnt = 0;
        double FrameRate = 30;
        //double TotalFrames = 0;
        double Framesno = 0;
        //double codec_double = 0;

        private int _CameraIndex = 1;

        // ==========================OFFSET =========================
        double Offset_X = 0.0;
        double Offset_Y = 0.0;
        double Offset_zHeight = 0.0;

        // ==========================Angle Calcs =========================
        double dist1 = 0.0;
        double dist2 = 0.0;
        double angle1 = 0.0;
        double angle2 = 0.0;
        double result_Scale = 0.0;
        double result_angle = 0.0;


        // ==========================Angle Calcs =========================
        double angle = 1;
        // global Vars
        //Units FileUnits = Units.METRIC;
        int frame = 30; // pixels frame inside PicBox
        int spacing = 0; // pixel spacing between integer points on picBoxOrig
        // ==========================Display Scale =========================
        double Pic1_Scale = 0.0;
        Hole currentHole;
        Hole holeZero;
        Hole TargetHole;
        Hole hole2;
        DrillJob2 dj2;

        Graphics gDrillFileHolePlot;
        Graphics g2;
        Graphics gSimulatePicBox;

        static Color defaultPenColor = Color.AliceBlue;
        public Form1()
        {
            InitializeComponent();
            gDrillFileHolePlot = Graphics.FromHwnd(pic_Box_Orig.Handle);
            //ControlSerialForm frm = new ControlSerialForm(this, "First",1);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //this.Invalidate();

            // ==================================


            //txtDrillFileName.Text = drillFileFolder 
            //-> Create a List to store for ComboCameras
            List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();

            //-> Find systems cameras with DirectShow.Net dll 
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }

            //-> clear the combobox
            cmb_Camera.DataSource = null;
            cmb_Camera.Items.Clear();

            //-> bind the combobox
            cmb_Camera.DataSource = new BindingSource(ListCamerasData, null);
            cmb_Camera.DisplayMember = "Value";
            cmb_Camera.ValueMember = "Key";

            // load settings

            Offset_zHeight = Properties.Settings.Default.Offset_Z_Height;
            Offset_X = Properties.Settings.Default.Offset_Camera_X;
            Offset_Y = Properties.Settings.Default.Offset_Camera_Y;
            lbl_Offset_X.Text = Offset_X.ToString();
            lbl_Offset_Y.Text = Offset_Y.ToString();
            txtBox_Offset_Z_Height.Text = Offset_zHeight.ToString();

            grbl.init();

            SerialTab_load();

            KeyPreview = true;

            //this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            //InitializeComponent();

            //ControlSerialForm csf = new ControlSerialForm("NEW", 1);
            //csf.Show();

            //refreshPorts();

            //RaisePosEvent += OnRaisePosEvent;
            //RaiseStreamEvent += OnRaiseStreamEvent;

        }


        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            /*
            // resize panels 
            // check for min size 
            Form me = (Form)sender;
            if (me.Width < 1134) me.Width = 1134;
            if (me.Height < 759) me.Height = 759;
            // panel height calcs
            groupBox1.Height = me.Height - (206 + 48);
            groupBox2.Height = groupBox1.Height;

            groupBox1.Width = (me.Width - 45) / 2;
            groupBox2.Width = groupBox1.Width;
            gb_DrillFile.Width = groupBox1.Width;
            gb_Rotation.Width = groupBox1.Width;

            groupBox2.Location = new Point(groupBox1.Width + 15, 206);
            gb_Rotation.Location = new Point(groupBox1.Width + 15, 10);
            */
        }




        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //addToLog(keyData.ToString());

            string feed = "  F" + txtFeed.Text;
            if (chk_Jog.Checked)
            {
                // ESCAPE 
                if (keyData == Keys.Escape)
                {
                    btnStreamCode.Text = "Stream Generated Code";
                    realtimeCommand(133);
                    stopStream = true;
                    return true;
                }
                // SHIFTS ==============================================================

                if (keyData == (Keys.Left | Keys.Shift))
                {
                    requestSend("$J=G91 X-0.1" + feed);
                    return true;
                }
                else if (keyData == (Keys.Right | Keys.Shift))
                {
                    requestSend("$J=G91 X0.1" + feed);
                    return true;
                }
                else if (keyData == (Keys.Up | Keys.Shift))
                {
                    requestSend("$J=G91 Y0.1" + feed);
                    return true;
                }
                else if (keyData == (Keys.Down | Keys.Shift))
                {
                    requestSend("$J=G91 Y-0.1" + feed);
                    return true;
                }
                else if (keyData == (Keys.PageDown | Keys.Shift))
                {
                    requestSend("$J=G91 Z0.1" + feed);
                    return true;
                }
                else if (keyData == (Keys.PageUp | Keys.Shift))
                {
                    requestSend("$J=G91 Z-0.1" + feed);
                    return true;
                }
                //  ALT ==============================================================
                else if (keyData == (Keys.Right | Keys.Alt))
                {
                    requestSend("$J=G91 X2.54" + feed);
                    return true;
                }
                else if (keyData == (Keys.Left | Keys.Alt))
                {
                    requestSend("$J=G91 X-2.54" + feed);
                    return true;
                }
                else if (keyData == (Keys.Up | Keys.Alt))
                {
                    requestSend("$J=G91 Y2.54" + feed);
                    return true;
                }
                else if (keyData == (Keys.Down | Keys.Alt))
                {
                    requestSend("$J=G91 Y-2.54" + feed);
                    return true;
                }
                else if (keyData == (Keys.PageDown | Keys.Alt))
                {
                    requestSend("$J=G91 Z2.54" + feed);
                    return true;
                }
                else if (keyData == (Keys.PageUp | Keys.Alt))
                {
                    requestSend("$J=G91 Z-2.54" + feed);
                    return true;
                }
                // CTRL & ALT ==============================================================
                else if (keyData == (Keys.Right | Keys.Control | Keys.Alt))
                {
                    requestSend("$J=G91 X10" + feed);
                    return true;
                }
                else if (keyData == (Keys.Left | Keys.Control | Keys.Alt))
                {
                    requestSend("$J=G91 X-10" + feed);
                    return true;
                }
                else if (keyData == (Keys.Up | Keys.Control | Keys.Alt))
                {
                    requestSend("$J=G91 Y10" + feed);
                    return true;
                }
                else if (keyData == (Keys.Down | Keys.Control | Keys.Alt))
                {
                    requestSend("$J=G91 Y-10" + feed);
                    return true;
                }
                else if (keyData == (Keys.PageDown | Keys.Control | Keys.Alt))
                {
                    requestSend("$J=G91 Z10" + feed);
                    return true;
                }
                else if (keyData == (Keys.PageUp | Keys.Control | Keys.Alt))
                {
                    requestSend("$J=G91 Z-10" + feed);
                    return true;
                }
                // =============NORMAL ========================================
                else if (keyData == Keys.Right)
                {
                    requestSend("$J=G91 X1" + feed);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    requestSend("$J=G91 X-1" + feed);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    requestSend("$J=G91 Y1" + feed);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    requestSend("$J=G91 Y-1" + feed);
                    return true;
                }
                else if (keyData == Keys.PageDown)
                {
                    requestSend("$J=G91 Z1" + feed);
                    return true;
                }
                else if (keyData == Keys.PageUp)
                {
                    requestSend("$J=G91 Z-1" + feed);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void btn_Drill_File_Open_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK || dr == DialogResult.Yes)
            {
                loadDrillFile(openFileDialog1.FileName);
            }
        }

        private void loadDrillFile(string fileName)
        {
            dj2 = new DrillJob2(fileName);

            // create a grid in the drill info group box to display relevant details 
            displayDrillInfo(dj2);




            // get total number of holes
            int drillSum = 0;
            foreach (KeyValuePair<int, int> kvp in dj2.DrillCount)
            {
                drillSum += kvp.Value;
            }
            string fmt = "0.0000";  // metric format  1/10,000
            if (dj2.JobUnits == Units.IMPERIAL)
            {
                fmt = "0.000000";  // empieral format  1/100,000
            }

            //lbl_NumHoles.Text = drillSum.ToString();
            //lbl_Extents_X_Max.Text = dj2.dMAX_X.ToString(fmt);
            //lbl_Extents_X_Min.Text = dj2.dMIN_X.ToString(fmt);

            //lbl_Extents_Y_Max.Text = dj2.dMAX_Y.ToString(fmt);
            //lbl_Extents_Y_Min.Text = dj2.dMIN_Y.ToString(fmt);


            //lbl_Extents_dX.Text = (dj2.dMAX_X - dj2.dMIN_X).ToString(fmt);
            //lbl_Extents_dY.Text = (dj2.dMAX_Y - dj2.dMIN_Y).ToString(fmt);

            // calc display Scale 
            //lbl_PicX.Text = "/  " + (pic_Box_Orig.Width - (frame * 2)).ToString();
            //lbl_PicY.Text = "/  " + (pic_Box_Orig.Height - (frame * 2)).ToString();

            // add 1 mm to max X & Y to generate next ruler point (beyond PCB) 

            double xScale = (dj2.dMAX_X + 10.0) / (pic_Box_Orig.Width - (frame * 1.5));
            double yScale = (dj2.dMAX_Y + 10.0) / (pic_Box_Orig.Height - (frame * 1.5));

            //lbl_Scale_X.Text = xScale.ToString(fmt);
            //lbl_Scale_Y.Text = yScale.ToString(fmt);

            if (xScale > yScale)
            {
                //lblFinalScale.Text = xScale.ToString(fmt);
                Pic1_Scale = xScale;
            }
            else
            {
                //lblFinalScale.Text = yScale.ToString(fmt);
                Pic1_Scale = yScale;
            }



            if (gDrillFileHolePlot == null) gDrillFileHolePlot = Graphics.FromHwnd(pic_Box_Orig.Handle);

            draw_Rulers(gDrillFileHolePlot, Convert.ToInt16(dj2.dMAX_X + 10), Convert.ToInt16(dj2.dMAX_Y + 10.0), pic_Box_Orig);
            //draw_Rulers(g2, Convert.ToInt16(dj2.dMAX_X + 1), Convert.ToInt16(dj2.dMAX_Y + 1.0), pic_Box_Res);



            // draw holes 



            drawHoles(dj2, spacing, frame, Pic1_Scale);
        }

        private void displayDrillInfo(DrillJob2 dj2)
        {
            int startLocX = btnDrill.Location.X;
            int startLocY = btnDrill.Location.Y;
            Size sz = btnDrill.Size;
            int Xinc = 25;

            dataGridView1.Rows.Clear();
            int gridHeight = 25;
            foreach (KeyValuePair<int, double> kvp in dj2.Drills.OrderBy(q => q.Key))
            {
                //lblDrill_1
                // get number of holes for this drill
                int num = dj2.DrillCount[kvp.Key];
                if (kvp.Key > 1)
                {
                    Button nB = new Button();
                    nB.Size = sz;
                    nB.Parent = btnDrill.Parent;
                    nB.Location = new Point(startLocX + ((kvp.Key-1) * 25), startLocY);
                    nB.Text = kvp.Key.ToString();
                    nB.Click += btnDrill_Click;
                    nB.BackColor = Color.White;
                }

                Color color = dj2.DrillColours[kvp.Key].Color;
                // add row to layout
                DataGridViewRow dgvr = new DataGridViewRow();
                // Num
                DataGridViewTextBoxCell dgvtc = new DataGridViewTextBoxCell();
                dgvtc.Value = kvp.Key.ToString();
                dgvr.Cells.Add(dgvtc);
                // Size
                dgvtc = new DataGridViewTextBoxCell();
                dgvtc.Value = kvp.Value.ToString();
                dgvr.Cells.Add(dgvtc);
                // Num Holes
                dgvtc = new DataGridViewTextBoxCell();
                dgvtc.Value = num.ToString();
                dgvr.Cells.Add(dgvtc);
                // Colour
                dgvtc = new DataGridViewTextBoxCell();
                dgvtc.Value = color.ToString();
                dgvr.Cells.Add(dgvtc);
                dataGridView1.Rows.Add(dgvr);
                gridHeight += 22;
            }
            dataGridView1.Height = gridHeight;
        }

        private Point DrawHole(Graphics g, PictureBox picBox, double ResolvedX, double ResolvedY, double holeSize, Pen pen, double scale, bool FlipY = false)
        {
            Point retVal = new Point();

            double tx = (ResolvedX / scale);
            double ty = (ResolvedY / scale);


            int plotx = (int)tx + frame - 1;
            int ploty = (int)ty - 1;
            if (!FlipY)
            {
                ploty = picBox.Height - frame - (int)ty - 1;
            }
            holeSize = (holeSize / scale);

            retVal = new Point(plotx, ploty);

            g.DrawEllipse(pen, plotx - (int)(holeSize / 2), ploty - (int)(holeSize / 2), (float)holeSize, (float)holeSize);

            return retVal;
        }


        private void drawHoles(DrillJob2 dj2, int spacing, int frame, double scale)
        {
            if (gDrillFileHolePlot == null) gDrillFileHolePlot = Graphics.FromHwnd(pic_Box_Orig.Handle);
            foreach (Hole hle in dj2.Holes)
            {

                hle.plot_Point = DrawHole(gDrillFileHolePlot, pic_Box_Orig, hle.Xinmm, hle.Yinmm, dj2.Drills[hle.ToolNum], dj2.DrillColours[hle.ToolNum], scale);

            }
        }

        private void draw_Rulers(Graphics g, int maxX, int maxY, PictureBox picBox)
        {
            int tickLength = 5;
            /*
                Concept :   the Frame width is the clear space to the left and bottom of the plat area. 
                Margin  :   the 1/2 frame space to the top & right 

                so calcs as follows:
                               axis line
                0 <-- Frame---> | . . . . . . . . . . . < 1/2 frame > picboxwidth |

                x scaling = available plot space = (picboxwidth - 1.5 * frame)  

                tick spacing    : say we need to plot 80 points on X (0 . . . .80) (eagle values 80.1450) 
                                : assumption frame = 30;
                                : assumption picBoxwidth = 530
                                : so remaining plot area is 530 - 45 = 485;
                                : 485 / 80.145 =  6.05  == every point is 6 pixels 
            */

            Pen p = new Pen(Color.DarkGray, 1f);
            g.Clear(Color.LightGray);

            int plotarea_x = picBox.Width - (int)(frame * 1.5);
            int spacing_x = plotarea_x / maxX;

            int plotarea_y = picBox.Height - (int)(frame * 1.5);
            int spacing_y = plotarea_y / maxY;
            // take the smallest spacing
            spacing = spacing_y;
            if (spacing_x < spacing_y) spacing = spacing_x;

            g.DrawLine(p, new Point(frame, frame / 2), new Point(frame, picBox.Height - (frame)));  // vertical 
            g.DrawLine(p, new Point(frame, picBox.Height - (frame)), new Point(picBox.Width - (frame / 2), picBox.Height - (frame)));  // horizontal


            // draw ticks 
            // y ticks 
            for (int yt = 0; yt <= maxY + 10; yt = yt + 10)
            {
                // so line is from Frame-tickLength to Frame or picboxWidth-Frame(1.5)
                // height = picBoxHeight -frame - (spacing * yt)
                Point p1 = new Point(frame - tickLength, picBox.Height - frame - (spacing * yt));
                Point p2 = new Point(frame, picBox.Height - frame - (spacing * yt));

                g.DrawLine(p, p1, p2);
                g.DrawString(yt.ToString(), new Font(FontFamily.GenericMonospace, 9f), new SolidBrush(Color.DarkGray), new Point(0, picBox.Height - frame - (spacing * yt)));
            }
            // x ticks 
            for (int xt = 0; xt <= maxX + 10; xt = xt + 10)
            {
                // so line is from picBox.Height - Frame + tickLength to (Frame/2) or picboxHeight-Frame
                // X pos = frame + (spacing * xt)
                Point p1 = new Point(frame + (spacing * xt), picBox.Height - frame + tickLength);
                Point p2 = new Point(frame + (spacing * xt), picBox.Height - frame);

                g.DrawString(xt.ToString(), new Font(FontFamily.GenericMonospace, 9f), new SolidBrush(Color.DarkGray), new Point(frame + (spacing * xt) - 10, picBox.Height - 18));

                g.DrawLine(p, p1, p2);

            }

        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pic_Box_Orig_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            //lbl_Click_X.Text = me.X.ToString();
            //lbl_Click_Y.Text = me.Y.ToString();
            using (Pen p = new Pen(Color.DarkCyan, 4f))
            {
                foreach (Hole hle in dj2.Holes)
                {
                    if (me.X > (hle.plot_Point.X - 5) && me.X < (hle.plot_Point.X + 5) && me.Y > (hle.plot_Point.Y - 5) && me.Y < (hle.plot_Point.Y + 5))
                    {
                        DrawHole(gDrillFileHolePlot, pic_Box_Orig, hle.Xinmm, hle.Yinmm, dj2.Drills[hle.ToolNum] + 0.2, p, Pic1_Scale);
                        currentHole = hle;
                        break;
                    }
                }
            }
        }

        private void pic_Box_Res_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            //lbl_Click_X.Text = me.X.ToString();
            //lbl_Click_Y.Text = me.Y.ToString();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            /*
            int maxRulerY = Convert.ToInt16(dj2.dMAX_Y) + 5;
            int maxRulerX = Convert.ToInt16(dj2.dMAX_X) + 5;

            g2.Clear(Color.DarkGray);
            //draw_Rulers(g2, maxRulerX, maxRulerY, pic_Box_Res);
            // render 
            if (txtAngle.Text.Trim() != "")
            {
                angle = double.Parse(txtAngle.Text.Trim());
            }

            int xOffset = 0;
            int yOffset = 0;
            double scale = 1;

            if (txtScale.Text.Trim() != "") scale = double.Parse(txtScale.Text.Trim());

            if (txt_Offset_X.Text != "") xOffset = int.Parse(txt_Offset_X.Text.Trim());
            if (txt_Offset_Y.Text != "") yOffset = int.Parse(txt_Offset_Y.Text.Trim());

            foreach (Hole hle in dj2.Holes)
            {
                hle.HoleZero = currentHole;
                hle.rotationAngle = angle;
                hle.scale = 1.0;
                hle.rotate(angle, xOffset, yOffset, scale);
                DrawRotatedHole(hle);
            }
            */
        }


        private double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }


        private void btn_camera_start_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "Start")
            {
                try
                {
                    _capture = null;
                    _capture = new VideoCapture(_CameraIndex);

                    // todo = check incomming frame size and resize pictureBox2 to suite 
                    //                    double width = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.XiWidth);
                    //                    double height = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.XiHeight);

                    double width = _capture.Width;
                    double height = _capture.Height;

                    // work out ratio 


                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, FrameRate);
                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, picBox_Video.Height);
                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, picBox_Video.Width);

                    cmb_Camera.Enabled = false;
                    webcam_frm_cnt = 0;
                    Application.Idle += ProcessFrame;
                    btn.Text = "Stop";
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            else
            {
                _capture.Stop();
                Application.Idle -= ProcessFrame;
                ReleaseData();
                cmb_Camera.Enabled = true;
                Bitmap bmp = new Bitmap(100, 100);
                btn.Text = "Start";
                //picBoxCamera.Image ;
                picBox_Video.Image = new Bitmap(1, 1);

                _capture.Dispose();  // this was only for file render -- if funnies remove
            }
        }
        private void ReleaseData()
        {
            if (_capture != null)
                _capture.Dispose();
        }


        private void ProcessFrame(object sender, EventArgs arg)
        {
            try
            {
                Framesno = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames);
                iFrame = _capture.QueryFrame().ToImage<Bgr, Byte>();

                if (iFrame != null)
                {
                    // draw a yello line 
                    iFrame.Draw(new Cross2DF(new PointF(iFrame.Width / 2, iFrame.Height / 2), iFrame.Width - 2, iFrame.Height - 2), new Bgr(Color.Yellow), 1);

                    picBox_Video.Image = iFrame.ToBitmap();
                    if (_CameraIndex == 0)
                    {

                        //double time_index = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
                        //Time_Label.Text = "Time: " + TimeSpan.FromMilliseconds(time_index).ToString().Substring(0, 8);

                        //double framenumber = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames);
                        //Frame_lbl.Text = "Frame: " + framenumber.ToString();

                        Thread.Sleep((int)(1000.0 / FrameRate));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Offset_Z_Height_Save_Click(object sender, EventArgs e)
        {
            double zHeight = 0;
            if (double.TryParse(txtBox_Offset_Z_Height.Text.Trim(), out zHeight))
            {
                Properties.Settings.Default.Offset_Z_Height = zHeight;
                Properties.Settings.Default.Save();
            }

        }

        private void txt_Offset_X_Mark_TextChanged(object sender, EventArgs e)
        {
            checkOffsetFields();
        }

        private void checkOffsetFields()
        {
            if (txt_Offset_X_Cam.Text.Trim() != "" && txt_Offset_X_Mark.Text.Trim() != "" && txt_Offset_Y_Cam.Text.Trim() != "" && txt_Offset_Y_Mark.Text.Trim() != "")
            {
                btn_Offset_Save.Enabled = true;
                // calc offset 
                double Offset_Mark_X = double.Parse(txt_Offset_X_Mark.Text.Trim());
                double Offset_Mark_Y = double.Parse(txt_Offset_Y_Mark.Text.Trim());
                double Offset_Cam_X = double.Parse(txt_Offset_X_Cam.Text.Trim());
                double Offset_Cam_Y = double.Parse(txt_Offset_Y_Cam.Text.Trim());
                Offset_X = (Offset_Mark_X - Offset_Cam_X);
                Offset_Y = (Offset_Mark_Y - Offset_Cam_Y);
                lbl_Offset_X.Text = Offset_X.ToString();
                lbl_Offset_Y.Text = Offset_Y.ToString();
            }
            else
            {
                btn_Offset_Save.Enabled = false;
            }
        }

        private void btn_Offset_Save_Click(object sender, EventArgs e)
        {
            if (txtCameraOffsetX.Text.Trim() != "" && txtCameraOffsetY.Text.Trim() != "")
            {
                Offset_X = double.Parse(txtCameraOffsetX.Text.Trim());
                Offset_Y = double.Parse(txtCameraOffsetY.Text.Trim());
            }
            Properties.Settings.Default.Offset_Camera_X = Offset_X;
            Properties.Settings.Default.Offset_Camera_Y = Offset_Y;
            Properties.Settings.Default.Save();

        }

        private void btn_Org_SetHole2_Click(object sender, EventArgs e)
        {
            // accept this hole as hole 2!  
            if (currentHole != null && currentHole != holeZero)
            {
                hole2 = currentHole;
                lbl_Hole2_X.Text = currentHole.Xinmm.ToString("0.000");
                //                lbl_Hole2_Y.Text = currentHole.FlippedResolvedY.ToString();
                lbl_Hole2_Y.Text = currentHole.Yinmm.ToString("0.000");
                checkBothHolesSelected();
            }
        }
        private void btn_Org_SetHole1_Click(object sender, EventArgs e)
        {
            // accept this hole as hole Zero!  
            if (currentHole != null)
            {
                holeZero = currentHole;
                lbl_Hole0_X.Text = currentHole.Xinmm.ToString("0.000");
                //                lbl_Click_Y.Text = currentHole.FlippedResolvedY.ToString();
                lbl_Hole0_Y.Text = currentHole.Yinmm.ToString("0.000");
                checkBothHolesSelected();
            }
        }

        private void checkBothHolesSelected()
        {
            if (holeZero != null && hole2 != null)
            {
                double diffX = hole2.Xinmm - holeZero.Xinmm;
                double diffY = hole2.Yinmm - holeZero.Yinmm;
                dist1 = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

                lbl_Dist.Text = dist1.ToString("0.000");
                angle1 = Math.Atan2(diffY, diffX) / (Math.PI / 180);
                lbl_Angle.Text = angle1.ToString("0.000");

            }
        }

        private void checkBothPCBHolesSelected()
        {
            if(PCBHole0 && PCBHole1)
            {
                double hole2_Y = double.Parse(lblPCBHole2_Y.Text.Trim());
                double hole2_X = double.Parse(lblPCBHole2_X.Text.Trim());
                double hole0_X = double.Parse(lblPCBHole0_X.Text.Trim());
                double hole0_Y = double.Parse(lblPCBHole0_Y.Text.Trim());
                double xdiff = hole2_X - hole0_X;
                double ydiff = hole2_Y - hole0_Y;
                dist2 = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2));
                angle2 = Math.Atan2(ydiff, xdiff) / (Math.PI / 180);
                lbl_PCB_Angle.Text = angle2.ToString("0.000");

                result_Scale = dist2 / dist1;
                lblPCBDist.Text = dist2.ToString("0.000");

                lbl_PCB_Scale.Text = result_Scale.ToString("0.0000");
                double rotAngle = angle2 - angle1;
                result_angle = rotAngle;
                lblPCBRotationAngle.Text = rotAngle.ToString("0.000");
            }
        }


        private void btn_GenFile_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            // hole Zero from Camera
            double hole0_Cam_X = double.Parse(lblPCBHole0_X.Text.Trim());
            double hole0_Cam_Y = double.Parse(lblPCBHole0_Y.Text.Trim());

            // hole Zero from dRill File
            double hole0_Drill_X = holeZero.Xinmm;
            double hole0_Drill_Y = holeZero.FlippedYinmm;

            double Gcode_Offset_X = hole0_Cam_X - hole0_Drill_X;
            double Gcode_Offset_Y = hole0_Cam_Y - hole0_Drill_Y;

            double dSafeZHeight = 0.0;
            bool bSafeHeight = double.TryParse(txtSafeZHeight.Text.Trim(), out dSafeZHeight);
            double dDrillDepth = 0.0;
            bool bDrillDepth = double.TryParse(txtDrillDepth.Text.Trim(), out dDrillDepth);
            if (bSafeHeight && bDrillDepth)
            {

                string safeZHeight = dSafeZHeight.ToString("0.000");
                string drillDepth = dDrillDepth.ToString("0.000");
                double _offsetX = 0.0;
                double _offsetY = 0.0;

                if (cb_Drill.Checked)
                {
                    _offsetX = Offset_X;
                    _offsetY = Offset_Y;
                }


                richTextBox1.AppendText("; Generated File" + System.Environment.NewLine);
                richTextBox1.AppendText("G28   ; Home " + System.Environment.NewLine);
                richTextBox1.AppendText("G21   ; Units mm " + System.Environment.NewLine);
                richTextBox1.AppendText("G90   ; Absolute Positioning " + System.Environment.NewLine);
                richTextBox1.AppendText("; NO OFFSET FOR CAMERA ------------" + System.Environment.NewLine);
                richTextBox1.AppendText("; ---------------------------------" + System.Environment.NewLine);
                richTextBox1.AppendText("; Move to Safe Height " + System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z" + safeZHeight + System.Environment.NewLine);
                richTextBox1.AppendText("; ---------------------------------" + System.Environment.NewLine);
                richTextBox1.AppendText("; Hole Zero " + System.Environment.NewLine);
                richTextBox1.AppendText("G0 X" + (holeZero.Xinmm + Gcode_Offset_X + _offsetX).ToString("0.000") + " Y" + (holeZero.FlippedYinmm + Gcode_Offset_Y + _offsetY).ToString("0.000") + System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z" + drillDepth + System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z" + safeZHeight + System.Environment.NewLine);

                //richTextBox1.AppendText("G4 0.5" + System.Environment.NewLine);
                richTextBox1.AppendText("; ---------------------------------" + System.Environment.NewLine);

                foreach (Hole hle in dj2.Holes)
                {
                    if (ActiveDrills.Contains(hle.ToolNum) || hle == hole2) // hle == hole2
                    {
                    hle.HoleZero = holeZero;
                    hle.rotationAngle = result_angle;
                    hle.scale = result_Scale;
                    hle.rotate(result_angle, 0, 0, 1);
                    richTextBox1.AppendText("G0 X" + (hle.RotatedX + Gcode_Offset_X + _offsetX).ToString("0.000") + " Y" + (hle.RotatedY + Gcode_Offset_Y  + _offsetY).ToString("0.000") + System.Environment.NewLine);
                    //richTextBox1.AppendText("G4 0.5" + System.Environment.NewLine);
                    richTextBox1.AppendText("G0 Z" + drillDepth + System.Environment.NewLine);
                    richTextBox1.AppendText("G0 Z" + safeZHeight + System.Environment.NewLine);
                    richTextBox1.AppendText("; ---------------------------------" + System.Environment.NewLine);
                    }
                }
                richTextBox1.AppendText("; END " + System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z0 X0 Y0  ; Go Home" + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine);
            }
            btnStreamCode.Enabled = true;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void cmb_Camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            _CameraIndex = cmb.SelectedIndex;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }






        // SERIAL STUFF 
        private xyzPoint posWCO, posWork, posMachine;
        public xyzPoint posPause, posProbe, posProbeOld;
        private mState machineState = new mState();     // Keep info about Bf, Ln, FS, Pn, Ov, A from grbl status
        private pState mParserState = new pState();     // keep info about last M and G settings from GCode

        private bool PCBHole0 = false;
        private bool PCBHole1 = false;
        public bool serialPortOpen { get; private set; } = false;
        public bool isGrblVers0 { get; private set; } = true;
        public string grblVers { get; private set; } = "";
        public bool isLasermode { get; private set; } = false;
        public bool toolInSpindle { get; set; } = false;
        public bool isHeightProbing { get; set; } = false;      // automatic height probing -> less feedback
        public List<int> ActiveDrills = new List<int>();

        public List<string> GRBLSettings = new List<string>();          // keep $$ settings
        private Queue<string> lastSentToCOM = new Queue<string>();      // store last sent commands via COM

        private Dictionary<string, double> gcodeVariable = new Dictionary<string, double>();    // keep variables "PRBX" etc.
        public string parserStateGC = "";                  // keep parser state response [GC:G0 G54 G17 G21 G90 G94 M5 M9 T0 F0.0 S0]

        private const int timerReload = 200;
        private string rxString;
        private bool useSerial2 = false;
        private int iamSerial = 1;
        private string formTitle = "";


        // ==============================

        private void SerialTab_load()
        {
            refreshPorts();
            updateControls();
            loadSettings();
            //openPort();
            machineState.Clear();
            cbPort.Text = "";
            //throw new NotImplementedException();
        }


        private void loadSettings()
        {
            try
            {
                cbPort.Text = Properties.Settings.Default.serialPort1;
                cbBaud.Text = Properties.Settings.Default.serialBaud1;
            }
            catch (Exception e)
            {
                logError("Loading settings", e);
            }
        }
        private void saveSettings()
        {
            try
            {
                Properties.Settings.Default.locationSerForm1 = Location;
                Properties.Settings.Default.ctrlLaserMode = isLasermode;
                Properties.Settings.Default.serialPort1 = cbPort.Text;
                Properties.Settings.Default.serialBaud1 = cbBaud.Text;
                saveLastPos();
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                logError("Saving settings", e);
            }
        }


        public void setUp_Serial()
        {
            mParserState.reset();
            CultureInfo ci = new CultureInfo(Properties.Settings.Default.language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            formTitle = "NEW";
            iamSerial = 1;
            //            AppDomain currentDomain = AppDomain.CurrentDomain;
        }
        //Unhandled exception
        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //closePort();
            Exception ex = e.Exception;
            MessageBox.Show(ex.Message, "Thread exception");
            rtbLog.AppendText(ex.Message);
        }
        private void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                //closePort();
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show(ex.Message, "Application exception");
                rtbLog.AppendText(ex.Message);
            }
        }




        private bool isStreaming = false;        // true when steaming is in progress
                                                 //        private bool isStreamingRequestPause = false; // true when request pause (wait for idle to switch to pause)
                                                 //        private bool isStreamingPause = false;    // true when steaming-pause 
        private bool isStreamingCheck = false;    // true when steaming is in progress (check)
                                                  //        private bool isStreamingPause = false;    // true when steaming is in progress (check)
        private bool getParserState = false;      // true to send $G after status switched to idle
        private bool isDataProcessing = false;      // false when no data processing pending


        private void updateControls()
        {
            bool isConnected = serialPort.IsOpen;
            serialPortOpen = isConnected;
            bool isSensing = isStreaming;
            cbPort.Enabled = !isConnected;
            cbBaud.Enabled = !isConnected;
            btnScanPort.Enabled = !isConnected;
            btnClear.Enabled = isConnected;
            cBCommand.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnSend.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnGRBLCommand0.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnGRBLCommand1.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnGRBLCommand2.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnGRBLCommand3.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnGRBLCommand4.Enabled = isConnected && (!isStreaming || isStreamingPause);
            btnCheckGRBL.Enabled = isConnected && (!isStreaming || isStreamingPause);// && !isGrblVers0;
            //btnCheckGRBLResult.Enabled
            btnGRBLReset.Enabled = isConnected;// & !isSensing;
        }


        private void logError(string message, Exception error)
        {
            string textmsg = "\r\n[ERROR]: " + message + ". ";
            if (error != null) textmsg += error.Message;
            textmsg += "\r\n";
            rtbLog.AppendText(textmsg);
            rtbLog.ScrollToCaret();
        }


        public void logErrorThr(object sender, EventArgs e)
        {
            logError(mens, err);
            updateControls();
        }
        public void addToLog(string text)
        {
            rtbLog.AppendText(text + "\r");
            rtbLog.ScrollToCaret();
        }

        private void saveLastPos()
        {
            if (iamSerial == 1)
            {
                rtbLog.AppendText("\rSave last pos.: \r" + posWork.Print(true, true) + "\n");    // print in single lines
                Properties.Settings.Default.lastOffsetX = Math.Round(posWork.X, 3);
                Properties.Settings.Default.lastOffsetY = Math.Round(posWork.Y, 3);
                Properties.Settings.Default.lastOffsetZ = Math.Round(posWork.Z, 3);
                Properties.Settings.Default.lastOffsetA = Math.Round(posWork.A, 3);
                Properties.Settings.Default.lastOffsetB = Math.Round(posWork.B, 3);
                Properties.Settings.Default.lastOffsetC = Math.Round(posWork.C, 3);
                int gNr = mParserState.coord_select;
                gNr = ((gNr >= 54) && (gNr <= 59)) ? gNr : 54;
                Properties.Settings.Default.lastOffsetCoord = gNr;    //global.grblParserState.coord_select;
                Properties.Settings.Default.Save();
            }
        }

        private void btnScanPort_Click(object sender, EventArgs e)
        {
            //refreshPorts(); 
        }



        private void refreshPorts()
        {
            List<String> tList = new List<String>();
            cbPort.Items.Clear();
            cbPort.Text = "";
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames()) tList.Add(s);
            if (tList.Count < 1) { } //logError("No serial ports found", null);
            else
            {
                tList.Sort();
                cbPort.Items.AddRange(tList.ToArray());
                Console.WriteLine("tList = " + tList.Count);
                Console.WriteLine("combo " + cbPort.Items.Count + " tt " + cbPort.Text);
            }
            cbPort.Invalidate();
        }

        public bool closePort()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    rtbLog.AppendText("Close " + _port + " result : " + ((!serialPort.IsOpen) ? "Success" : "Failed") + " \r\n");

                }
                //rtbLog.AppendText("\rClose " + cbPort.Text + "\r");
                btn_OpenPort.Text = "Open";
                //saveSettings();
                updateControls();
                timerSerial.Interval = 1000;
                return (true);
            }
            catch (Exception err)
            {
                logError("Closing port", err);
                updateControls();
                timerSerial.Enabled = false;
                return (false);
            }
        }

        private void btn_OpenPort_Click_1(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                closePort();
                ((Button)sender).Text = "Open";
            }
            else
            {
                openPort();
                if (serialPort.IsOpen) ((Button)sender).Text = "Close";
            }

        }



        public string lastError = "";

        private int minimizeCount = 0;
        private bool openPort()
        {
            if (_port != "")
            {
                try
                {
                    serialPort.PortName = _port;
                    serialPort.BaudRate = Convert.ToInt32(cbBaud.Text);
                    serialPort.Open();
                    rtbLog.Clear();
                    rtbLog.AppendText("Open " + _port + " result : " + ((serialPort.IsOpen) ? "Success" : "Failed") + " \r\n");

                    rtbLog.Visible = true;
                    this.Show();

                    btn_OpenPort.Text = "Close";
                    //btn_OpenPort.Refresh();
                    isDataProcessing = true;
                    grbl.axisA = false; grbl.axisB = false; grbl.axisC = false; grbl.axisUpdate = false;
                    grblReset(false);
                    //updateControls();
                    if (Properties.Settings.Default.serialMinimize)
                        minimizeCount = 10;     // minimize window after 10 timer ticks
                    timerSerial.Interval = timerReload;
                    preventOutput = 0; preventEvent = 0;
                    isHeightProbing = false;
                    updateControls();

                    return (true);
                }
                catch (Exception err)
                {
                    minimizeCount = 0;
                    logError("Opening port", err);
                    updateControls();
                    return (false);
                }
            }
            else
            {
                MessageBox.Show("Select Com Port First");
                return false;
            }
        }




        private void resetVariables(bool resetToolCoord = false)
        {
            gcodeVariable.Clear();
            gcodeVariable.Add("PRBX", 0.0); // Probing coordinates
            gcodeVariable.Add("PRBY", 0.0);
            gcodeVariable.Add("PRBZ", 0.0);
            gcodeVariable.Add("PRDX", 0.0); // Probing delta coordinates
            gcodeVariable.Add("PRDY", 0.0); // delta = actual - last
            gcodeVariable.Add("PRDZ", 0.0);
            gcodeVariable.Add("MACX", 0.0); // actual Machine coordinates
            gcodeVariable.Add("MACY", 0.0);
            gcodeVariable.Add("MACZ", 0.0);
            gcodeVariable.Add("WACX", 0.0); // actual Work coordinates
            gcodeVariable.Add("WACY", 0.0);
            gcodeVariable.Add("WACZ", 0.0);
            gcodeVariable.Add("MLAX", 0.0); // last Machine coordinates (before break)
            gcodeVariable.Add("MLAY", 0.0);
            gcodeVariable.Add("MLAZ", 0.0);
            gcodeVariable.Add("WLAX", 0.0); // last Work coordinates (before break)
            gcodeVariable.Add("WLAY", 0.0);
            gcodeVariable.Add("WLAZ", 0.0);
            if (resetToolCoord)
            {
                gcodeVariable.Add("TOAN", 0.0); // TOol Actual Number
                gcodeVariable.Add("TOAX", 0.0); // Tool change position
                gcodeVariable.Add("TOAY", 0.0);
                gcodeVariable.Add("TOAZ", 0.0);
                gcodeVariable.Add("TOLN", 0.0); // TOol Last Number
                gcodeVariable.Add("TOLX", 0.0); // Tool change position
                gcodeVariable.Add("TOLY", 0.0);
                gcodeVariable.Add("TOLZ", 0.0);
            }
        }


        //private bool waitForIdle = false;
        //private bool externalProbe = false;
        private string[] eeprom1 = { "G54", "G55", "G56", "G57", "G58", "G59" };
        private string _port = "";


        private void cbPort_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _port = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void btnScanPorts_Click(object sender, EventArgs e)
        {
            refreshPorts();
        }



        private string[] eeprom2 = { "G10", "G28", "G30", "G28" };

        //Send reset sentence
        public void grblReset(bool savePos = true)//Stop/reset button
        {
            if (savePos)
            {
                //saveLastPos(); 
            }
            resetVariables();
            mParserState.reset();
            isStreaming = false;
            isStreamingPause = false;
            isHeightProbing = false;
            toolInSpindle = false;
            waitForIdle = false;
            externalProbe = false;
            var dataArray = new byte[] { 24 };//Ctrl-X
            if (serialPort.IsOpen)
                serialPort.Write(dataArray, 0, 1);
            rtbLog.AppendText("[CTRL-X]\r\n");
            preventOutput = 0; preventEvent = 0;
            grbl.axisA = false; grbl.axisB = false; grbl.axisC = false; grbl.axisUpdate = false;



        }


        #region serial receive handling
        //  RX Interupt
        //
        string mens;
        Exception err;
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            while ((serialPort.IsOpen) && (serialPort.BytesToRead > 0))
            {
                rxString = string.Empty;
                try
                {
                    rxString = serialPort.ReadTo("\r\n");              //read line from grbl, discard CR LF
                    isDataProcessing = true;
                    this.Invoke(new EventHandler(handleRxData));        //tigger rx process 
                    while ((serialPort.IsOpen) && (isDataProcessing))   //wait previous data line processed done
                    { }
                }
                catch (Exception errort)
                {
                    //MessageBox.Show(errort.ToString());
                    //serialPort.Close();
                    mens = "Error reading line from serial port";
                    err = errort;
                    this.Invoke(new EventHandler(logErrorThr));
                }
            }
        }

        //  Filter received message before further use
        //
        private static int preventOutput = 0;
        private static int preventEvent = 0;
        private void handleRxData(object sender, EventArgs e)
        {
            try
            {
                char[] charsToTrim = { '<', '>', '[', ']', ' ' };
                int tmp;
                //addToLog(string.Format("raw '{0}'", rxString));

                // reset message
                if (rxString.IndexOf("['$' for help]") >= 0)
                {
                    handleRX_Reset(rxString);
                    timerSerial.Enabled = true;
                    isDataProcessing = false;
                    lastError = "";
                    if (true)   // read grbl settings
                    {
                        addToLog("> Read grbl settings, hide response");
                        grbl.axisA = false; grbl.axisB = false; grbl.axisC = false; grbl.axisUpdate = false;
                        preventOutput = 10; preventEvent = 10;
                        requestSend("$$");  // get setup
                        requestSend("$#");  // get parameter
                    }
                    return;
                }

                else if (rxString.IndexOf("ok") >= 0)
                {
                    if (!isStreaming || isStreamingPause)
                    {
                        if (!isHeightProbing || cbStatus.Checked)
                            addToLog(string.Format("< {0}", rxString));          // < ok
                    }
#if (debuginfo)
          //  rtbLog.AppendText(string.Format("> ok {0} {1} {2}\r\n", sendLinesSent, sendLinesConfirmed, sendLinesCount));//if not in transfer log the txLine
                rtbLog.AppendText(string.Format("< {0} {1} {2}  \r\n", sendLinesSent, sendLinesConfirmed, grblBufferFree));//if not in transfer log the txLine
#endif
                    updateStreaming(rxString);                              // process all other messages
                    isDataProcessing = false;
                    return;
                }

                // Process status message with coordinates
                else if (((tmp = rxString.IndexOf('<')) >= 0) && (rxString.IndexOf('>') > tmp))
                {
                    if (cbStatus.Checked)
                        addToLog(rxString);
                    handleRX_Status(rxString.Trim(charsToTrim));// Process status message with coordinates
                    isDataProcessing = false;
                    return;
                }

                // Process feedback message with coordinates
                else if (((tmp = rxString.IndexOf('[')) >= 0) && (rxString.IndexOf(']') > tmp))
                {
                    handleRX_Feedback(rxString.Trim(charsToTrim).Split(':'));
                    if (!isHeightProbing || cbStatus.Checked)
                    {
                        if (preventOutput == 0)
                            addToLog(rxString);
                    }
                    isDataProcessing = false;
                    return;
                }

                else if (rxString.IndexOf("ALARM") >= 0)
                {
                    lastError = "";
                    addToLog("<\r\n< !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    addToLog(string.Format("< {0} \t{1}", rxString, grbl.getAlarm(rxString)));
                    resetStreaming();
                    isDataProcessing = false;
                    isHeightProbing = false;
                    grblStateNow = grblState.alarm;
                    OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));// lastCmd));
                    this.WindowState = FormWindowState.Minimized;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    return;
                }
                else if (rxString.IndexOf("error") >= 0)
                {
                    string tmpMsg = "";
                    if (rxString != lastError)
                    {
                        addToLog("<\r\n< !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        addToLog(string.Format("< {0} \t{1}", rxString, grbl.getError(rxString)));
                        lastError = rxString + " " + grbl.getError(rxString) + "\r\n";
                        this.WindowState = FormWindowState.Minimized;
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        addToLog(">>> Last sent commmands to grbl, oldest first:");
                        lastError += ">>> Last sent commmands to grbl, oldest first:";
                        foreach (string lastLine in lastSentToCOM)
                        {
                            tmpMsg = ">>> " + lastLine;
                            addToLog(tmpMsg);
                            lastError += tmpMsg + "\r\n";
                        }
                    }
                    grblStatus = grblStreaming.error;
                    if (isStreaming)
                    {
                        tmpMsg = string.Format("< Error before code line {0} \r\n", gCodeLineNr[gCodeLinesSent]);
                        addToLog(tmpMsg);
                        lastError += tmpMsg;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                        stopStreaming();
                    }
                    resetStreaming();
                    isHeightProbing = false;
                    isDataProcessing = false;
                    return;
                }

                // Show GRBL Settings Info if Version is >= 1.0
                else if ((rxString.IndexOf("$") >= 0) && (rxString.IndexOf("=") >= 0))
                {
                    handleRX_Setup(rxString);
                    isDataProcessing = false;
                    return;
                }
            }
            catch (Exception cc)
            {

            }
            isDataProcessing = false;
            return;
        }

        // process further RX messages (> ok)
        public void updateStreaming(string rxString)
        {
            int tmpIndex = gCodeLinesSent;
            //addToLog(string.Format("### {0} {1} {2}\r\n", sendLinesConfirmed, sendLinesSent, sendLinesCount));
            // 'ok' received, increment confirmend
            if (sendLinesConfirmed < sendLinesCount)
            {
                grbl.updateParserState(sendLines[sendLinesConfirmed], ref mParserState);
                grblBufferFree += (sendLines[sendLinesConfirmed].Length + 1);   //update bytes supose to be free on grbl rx bufer
                sendLinesConfirmed++;                   // line processed
                                                        // Remove already sent lines to release memory
                if ((sendLines.Count > 1) && (sendLinesConfirmed == sendLinesSent == sendLinesCount > 1))
                {
                    sendLines.RemoveAt(0);
                    sendLinesConfirmed--;
                    sendLinesSent--;
                    sendLinesCount--;
                }
            }
            // check if buffer is empty and system = IDLE 
            if ((sendLinesConfirmed == sendLinesCount) && (grblStateNow == grblState.idle))   // addToLog(">> Buffer empty\r");
            {
                if (isStreamingRequestPause)
                {
                    isStreamingPause = true;
                    isStreamingRequestPause = false;
                    grblStatus = grblStreaming.pause;
                    gcodeVariable["MLAX"] = posMachine.X; gcodeVariable["MLAY"] = posMachine.Y; gcodeVariable["MLAZ"] = posMachine.Z;
                    gcodeVariable["WLAX"] = posWork.X; gcodeVariable["WLAY"] = posWork.Y; gcodeVariable["WLAZ"] = posWork.Z;

                    if (getParserState)
                    { requestSend("$G"); }
                }
            }
            if (isStreaming)
            {
                if (!isStreamingPause)
                {
                    gCodeLinesConfirmed++;  //line processed
                                            // Remove already handled GCode lines to release memory
                    if ((gCodeLines.Count > 1) && (gCodeLinesSent > 1))
                    {
                        gCodeLines.RemoveAt(0);
                        gCodeLineNr.RemoveAt(0);
                        gCodeLinesConfirmed--;
                        gCodeLinesSent--;
                        gCodeLinesCount--;
                        tmpIndex = gCodeLinesSent;
                    }
                }
                else
                    grblStatus = grblStreaming.pause;   // update status
                                                        //Transfer finished and processed? Update status and controls
                if ((gCodeLinesConfirmed >= gCodeLinesCount) && (sendLinesConfirmed == sendLinesCount))
                {
                    isStreaming = false;
                    addToLog("\r\n[Streaming finish]");
                    grblStatus = grblStreaming.finish;
                    requestSend("$G");
                    if (isStreamingCheck)
                    { requestSend("$C"); isStreamingCheck = false; }
                    updateControls();
                    allowStreamingEvent = true;
                }
                else//not finished
                {
                    if (!(isStreamingPause || isStreamingRequestPause))
                        preProcessStreaming();//If more lines on file, send it  
                }
                if ((oldStatus != grblStatus) || allowStreamingEvent)
                {
                    sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                    oldStatus = grblStatus;     //grblStatus = oldStatus;
                    allowStreamingEvent = false;
                }
            }
            processSend();

        }

        // sendStreamEvent update main prog 
        //
        private void sendStreamEvent(int lineNr, grblStreaming status)
        {
            float codeFinish = (float)lineNr * 100 / (float)gCodeLinesTotal;
            float buffFinish = (float)(grblBufferSize - grblBufferFree) * 100 / (float)grblBufferSize;
            if (codeFinish > 100) { codeFinish = 100; }
            if (buffFinish > 100) { buffFinish = 100; }
            OnRaiseStreamEvent(new StreamEventArgs((int)lineNr, codeFinish, buffFinish, status));
        }

        private void handleRX_Reset(string rxString)
        {
            grblBufferSize = 127;  //rx bufer size of grbl on arduino 127
            resetStreaming();
            addToLog("> RESET\r\n" + rxString);
            if (rxString.ToLower().IndexOf("grbl 0") >= 0)
            { isGrblVers0 = true; isLasermode = false; }
            if (rxString.ToLower().IndexOf("grbl 1") >= 0)
            { isGrblVers0 = false; addToLog("> Version 1.x\r\n"); }
            grblVers = rxString.Substring(0, rxString.IndexOf('['));
            if (lastError.Length > 2)
            {
                addToLog("> last error: " + lastError);
                OnRaiseStreamEvent(new StreamEventArgs(0, -1, 0, grblStreaming.reset));
            }
            else
                OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.reset));

            lblSrBf.Text = "";
            lblSrFS.Text = "";
            lblSrPn.Text = "";
            lblSrLn.Text = "";
            lblSrOv.Text = "";
            lblSrA.Text = "";
            return;
        }

        private void handleRX_Feedback(string[] dataField)  // dataField = rxString.Trim(charsToTrim).Split(':')
        {
            if (dataField[0].IndexOf("GC") >= 0)            // handle G-Code parser state [GC:G0 G54 G17 G21 G90 G94 M5 M9 T0 F0.0 S0]
            {
                parserStateGC = dataField[1];
                grbl.updateParserState(dataField[1], ref mParserState);
                if (isGrblVers0)
                    parserStateGC = parserStateGC.Replace("M0 ", "");
                posPause = posWork;
                getParserState = false;
            }
            else if (dataField[0].IndexOf("PRB") >= 0)                // Probe message with coordinates // [PRB:-155.000,-160.000,-28.208:1]
            {
                grblStateNow = grblState.probe;
                posProbeOld = posProbe;
                grbl.getPosition("PRB:" + dataField[1], ref posProbe);  // get numbers from string
                gcodeVariable["PRBX"] = posProbe.X; gcodeVariable["PRBY"] = posProbe.Y; gcodeVariable["PRBZ"] = posProbe.Z;
                gcodeVariable["PRDX"] = posProbe.X - posProbeOld.X; gcodeVariable["PRDY"] = posProbe.Y - posProbeOld.Y; gcodeVariable["PRDZ"] = posProbe.Z - posProbeOld.Z;
                if (preventEvent == 0)
                    OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));// lastCmd));
            }
            else if (dataField[0].IndexOf("MSG") >= 0) //[MSG:Pgm End]
            {
                if (dataField[1].IndexOf("Pgm End") >= 0)
                {
                    if ((isStreaming) || (isHeightProbing))
                    {
                        isStreaming = false;
                        isHeightProbing = false;
                        preventEvent = 0; preventOutput = 0;
                        addToLog("\r[Streaming finish]");
                        grblStatus = grblStreaming.finish;
                        if (isStreamingCheck)
                        { requestSend("$C"); isStreamingCheck = false; }
                        updateControls();
                        allowStreamingEvent = true;
                        OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.finish));
                    }
                }
            }
            if (iamSerial == 1)
                grbl.setCoordinates(dataField[0], dataField[1]);
        }

        private void handleRX_Setup(string rxString)
        {
            string[] splt = rxString.Split('=');
            int id;
            if (int.TryParse(splt[0].Substring(1), out id))
            {
                if (!isGrblVers0)
                {
                    string msgNr = splt[0].Substring(1).Trim();
                    if (preventOutput == 0)
                    {
                        string txt = grbl.getSetting(msgNr).TrimEnd('\r');
                        addToLog(string.Format("< {0} ({1})", rxString.PadRight(14, ' '), txt));   // output $$ response
                    }
                    if (id == 32)
                    {
                        if (splt[1].IndexOf("1") >= 0)
                            isLasermode = true;
                        else
                            isLasermode = false;
                        OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.lasermode));
                    }
                }
                else
                    addToLog(string.Format("< {0}", rxString));
                GRBLSettings.Add(rxString);
                if (iamSerial == 1)
                    grbl.setSettings(id, splt[1]);
            }
            else
                addToLog(string.Format("< {0}", rxString));
        }

        private grblState grblStateNow = grblState.unknown;
        private grblState grblStateLast = grblState.unknown;

        // should occur with same frequent as timer interrupt -> each 200ms
        // old:         <Idle,MPos:0.000,0.000,0.000,WPos:0.000,0.000,0.000>
        // new in 1.1   < Idle | MPos:0.000,0.000,0.000 | FS:0,0 | WCO:0.000,0.000,0.000 >
        private bool allowStreamingEvent = true;
        private void handleRX_Status(string text)    // '<' and '>' already removed
        {
            char splitAt = '|';
            if (isGrblVers0)
                splitAt = ',';
            string[] dataField = text.Split(splitAt);
            string status = dataField[0].Trim(' ');
            //if (isGrblVers0)
            //{
            //    grbl.getPosition(dataField[1] + "," + dataField[2] + "," + dataField[3] + " ", ref posMachine);
            //    grbl.getPosition(dataField[4] + "," + dataField[5] + "," + dataField[6] + " ", ref posWork);
            //    posWCO = posMachine - posWork;
            // }
            // else
            // {
            machineState.Clear(); //lblSrPn.Text = ""; //lblSrA.Text = "";
            if (dataField.Length > 2)
            {
                for (int i = 2; i < dataField.Length; i++)
                {
                    if (dataField[i].IndexOf("WCO") >= 0)           // Work Coordinate Offset
                    {
                        grbl.getPosition(dataField[i], ref posWCO);
                        continue;
                    }
                    string[] data = dataField[i].Split(':');
                    if (dataField[i].IndexOf("Bf:") >= 0)            // Buffer state
                    { machineState.Bf = lblSrBf.Text = data[1]; continue; }
                    if (dataField[i].IndexOf("Ln:") >= 0)            // Line number
                    { machineState.Ln = lblSrLn.Text = data[1]; continue; }
                    if (dataField[i].IndexOf("FS:") >= 0)            // Current Feed and Speed
                    { machineState.FS = lblSrFS.Text = data[1]; continue; }
                    if (dataField[i].IndexOf("F:") >= 0)             // Current Feed 
                    { machineState.FS = lblSrFS.Text = data[1]; continue; }
                    if (dataField[i].IndexOf("Pn:") >= 0)            // Input Pin State
                    { machineState.Pn = lblSrPn.Text = data[1]; continue; }
                    if (dataField[i].IndexOf("Ov:") >= 0)            // Override Values
                    { machineState.Ov = lblSrOv.Text = data[1]; lblSrPn.Text = ""; lblSrA.Text = ""; continue; }
                    if (dataField[i].IndexOf("A:") >= 0)             // Accessory State
                    { machineState.A = lblSrA.Text = data[1]; continue; }
                }
                //}
                if (dataField[1].IndexOf("MPos") >= 0)
                {
                    grbl.getPosition(dataField[1], ref posMachine);
                    posWork = posMachine - posWCO;
                }
                else
                {
                    grbl.getPosition(dataField[1], ref posWork);
                    posMachine = posWork + posWCO;
                }
            }

            if (iamSerial == 1)
            {
                if (!grbl.posChanged)
                    grbl.posChanged = !(xyzPoint.AlmostEqual(grbl.posWCO, posWCO) && xyzPoint.AlmostEqual(grbl.posMachine, posMachine));
                if (!grbl.wcoChanged)
                    grbl.wcoChanged = !(xyzPoint.AlmostEqual(grbl.posWCO, posWCO));
                grbl.posWCO = posWCO; grbl.posWork = posWork; grbl.posMachine = posMachine;
            } // make it global

            gcodeVariable["MACX"] = posMachine.X; gcodeVariable["MACY"] = posMachine.Y; gcodeVariable["MACZ"] = posMachine.Z;
            gcodeVariable["WACX"] = posWork.X; gcodeVariable["WACY"] = posWork.Y; gcodeVariable["WACZ"] = posWork.Z;
            grblStateNow = grbl.parseStatus(status);
            lblSrState.BackColor = grbl.grblStateColor(grblStateNow);
            lblSrState.Text = status;

            lblSrPos.Text = posWork.Print(false, grbl.axisB || grbl.axisC); // show actual work position
            lblMachinePosition.Text = "X : " + posWork.X.ToString("0.000") + System.Environment.NewLine + "Y : " + posWork.Y.ToString("0.000") + System.Environment.NewLine + "Z : " + posWork.Z.ToString("0.000");
            if (grblStateNow != grblStateLast) { grblStateChanged(); }
            OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));

            if ((grblStateNow == grblState.idle) || (grblStateNow == grblState.check))
            {
                waitForIdle = false;
                if (externalProbe)
                {
                    posProbe = posMachine;
                    externalProbe = false;
                    OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblState.probe, machineState, mParserState, "($PROBE)"));
                }
                processSend();
            }
            grblStateLast = grblStateNow;
            //            OnRaisePosEvent(new PosEventArgs(posWork, posMachine, grblStateNow, machineState, mParserState, rxString));// lastCmd));
            allowStreamingEvent = true;
        }
        public event EventHandler<PosEventArgs> RaisePosEvent;
        protected virtual void OnRaisePosEvent(PosEventArgs e)
        {
            //addToLog("OnRaisePosEvent " + e.Status.ToString());
            RaisePosEvent?.Invoke(this, e);
            //EventHandler<PosEventArgs> handler = RaisePosEvent;
            //if (handler != null)
            //{
            //    handler(this, e);
            //}
        }

        #endregion
        // check free buffer before sending 
        // 1. requestSend(data) to add cleaned data to stack (sendLines) for sending / extract code for 2nd COM port
        // 2. processSend() check if grbl-buffer is free to take commands
        // 3. sendLine(data) if buffer can take commands
        // 4. updateStreaming(rxdata) check if command was sent
        private int grblBufferSize = 127;  //rx bufer size of grbl on arduino 127
        private int grblBufferFree = 127;    //actual suposed free bytes on grbl buffer
        private List<string> sendLines = new List<string>();
        private int sendLinesCount = 0;             // actual buffer size
        private int sendLinesSent = 0;              // actual sent line
        private int sendLinesConfirmed = 0;         // already received line

        //  requestSend fill up send buffer, called by main-prog for single commands
        //  or called by preProcessStreaming to stream GCode data
        //  requestSend -> processSend -> sendLine
        //
        public bool requestSend(string data)
        {
            if (isStreamingRequestPause)
            { addToLog("!!! Command blocked - wait for IDLE " + data); }
            else
            {
                var tmp = cleanUpCodeLine(data);
                if ((!string.IsNullOrEmpty(tmp)) && (tmp[0] != ';'))    // trim lines and remove all empty lines and comment lines
                {
                    if (tmp == "$#") preventEvent = 5;                  // no response echo for parser state
                    sendLines.Add(tmp);
                    sendLinesCount++;
                    processSend();
                    feedBackSettings(tmp);
                }
            }
            return serialPort.IsOpen;
        }

        /// <summary>
        /// Clear all streaming counters
        /// </summary>
        private void resetStreaming()
        {
            externalProbe = false;
            isStreaming = false;
            isStreamingRequestPause = false;
            isStreamingPause = false;
            gCodeLinesSent = 0;
            gCodeLinesCount = 0;
            gCodeLinesConfirmed = 0;
            gCodeLinesTotal = 0;
            gCodeLines.Clear();
            gCodeLineNr.Clear();
            sendLinesSent = 0;
            sendLinesCount = 0;
            sendLinesConfirmed = 0;
            sendLines.Clear();
            grblBufferFree = grblBufferSize;
        }

        // Streaming
        // 1. startStreaming() copy and filter gcode to list
        // 2. proceedStreaming() to copy data to stack for sending
        private List<string> gCodeLines = new List<string>();      // buffer with gcode commands
        private List<int> gCodeLineNr = new List<int>();         // corresponding line-nr from main-form
        private int gCodeLinesCount = 0;             // amount of lines to sent
        private int gCodeLinesSent = 0;              // actual sent line
        private int gCodeLinesConfirmed = 0;         // received line
        private int gCodeLinesTotal = 0;
        //private bool isStreaming = false;        // true when steaming is in progress
        private bool isStreamingRequestPause = false; // true when request pause (wait for idle to switch to pause)
        private bool isStreamingPause = false;    // true when steaming-pause 
        //private bool isStreamingCheck = false;    // true when steaming is in progress (check)
        //private bool getParserState = false;      // true to send $G after status switched to idle
        //private bool isDataProcessing = false;      // false when no data processing pending
        private grblStreaming grblStatus = grblStreaming.ok;
        private grblStreaming oldStatus = grblStreaming.ok;
        public void stopStreaming()
        {
            int line = 0;
            if ((gCodeLineNr != null) && (gCodeLinesSent < gCodeLineNr.Count))
            {
                line = gCodeLineNr[gCodeLinesSent];
                sendStreamEvent(line, grblStreaming.stop);
            }
            isHeightProbing = false;
            addToLog("[STOP Streaming (" + line.ToString() + ")]");
            resetStreaming();
            if (isStreamingCheck)
            {
                sendLine("$C");
                isStreamingCheck = false;
            }
            updateControls();
        }
        public void pauseStreaming()
        {
            if (!isStreamingPause)
            {
                isStreamingRequestPause = true;     // wait until buffer is empty before switch to pause
                addToLog("[Pause streaming]");
                addToLog("[Save Settings]");
                grblStatus = grblStreaming.waitidle;
                getParserState = true;
            }
            else
            {   //if ((posPause.X != posWork.X) || (posPause.Y != posWork.Y) || (posPause.Z != posWork.Z))
                addToLog("++++++++++++++++++++++++++++++++++++");
                if (!xyzPoint.AlmostEqual(posPause, posWork))
                {
                    addToLog("[Restore Position]");
                    requestSend(string.Format("G90 G0 X{0:0.000} Y{1:0.000}", posPause.X, posPause.Y).Replace(',', '.'));  // restore last position
                    string noG = parserStateGC.Substring(parserStateGC.IndexOf("M") - 1);
                    addToLog("[Restore Settings: " + noG + " ]");
                    requestSend(noG);           // restore actual GCode settings one by one
                    requestSend("G4 P2");       // wait 2 seconds
                    requestSend(string.Format("G1 Z{0:0.000}", posPause.Z).Replace(',', '.'));                      // restore last position
                }
                addToLog("[Start streaming - no echo]");
                addToLog("[Restore Settings: " + parserStateGC + " ]");
                isStreamingPause = false;
                isStreamingRequestPause = false;
                grblStatus = grblStreaming.ok;
                requestSend(parserStateGC);         // restore actual GCode settings one by one
                gCodeLinesConfirmed--;              // each restored setting will cause 'ok' and gCodeLinesConfirmed++

                preProcessStreaming();
            }
            updateControls();
        }

        private bool replaceFeedRate = false;
        private bool replaceSpindleSpeed = false;
        private string replaceFeedRateCmd = "";
        private string replaceSpindleSpeedCmd = "";
        private string replaceFeedRateCmdOld = "";
        private string replaceSpindleSpeedCmdOld = "";


        /*  preProcessStreaming copy line by line (requestSend(line)) to sendBuffer 
         *  if buffer free, to be able to track line-nr for feedback
         */
        //       int currentTool = -1;
        private void preProcessStreaming()
        {
            while ((gCodeLinesSent < gCodeLinesCount) && (grblBufferFree >= gCodeLines[gCodeLinesSent].Length + 1) && !waitForIdle)
            {
                string line = gCodeLines[gCodeLinesSent];
                int cmdMNr = gcode.getIntGCode('M', line);
                int cmdGNr = gcode.getIntGCode('G', line);
                int cmdTNr = gcode.getIntGCode('T', line);
                if (grbl.unknownG.Contains(cmdGNr))
                {
                    gCodeLines[gCodeLinesSent] = "(" + line + " - unknown)";  // don't pass unkown GCode to GRBL because is unknown
                    line = gCodeLines[gCodeLinesSent];
                    gCodeLinesConfirmed++;      // GCode is count as sent (but wasn't send) also count as received
                    addToLog(line);
                }
                if ((replaceFeedRate) && (gcode.getStringValue('F', line) != ""))
                {
                    string old_value = gcode.getStringValue('F', line);
                    replaceFeedRateCmdOld = old_value;
                    line = line.Replace(old_value, replaceFeedRateCmd);
                    gCodeLines[gCodeLinesSent] = line;
                    //                    addToLog("Replace feed in [" + line + "] old : " + old_value);
                }
                if ((replaceSpindleSpeed) && (gcode.getStringValue('S', line) != ""))
                {
                    string old_value = gcode.getStringValue('S', line);
                    line = line.Replace(old_value, replaceSpindleSpeedCmd);
                    replaceSpindleSpeedCmdOld = old_value;
                    gCodeLines[gCodeLinesSent] = line;
                    //                    addToLog("Replace spindle speed in [" + line + "] old : " + old_value);
                }
                // regular GCode expression 'T'
                if (cmdTNr >= 0) //&& (line.IndexOf("T") == 0) && (line.IndexOf("#T") < 0) && (line.IndexOf("$T") < 0))
                {   // T-word is allowed by grbl - no need to filter
                    setToolChangeCoordinates(cmdTNr, line);
                }
                if (cmdMNr == 6)
                {
                    /*
                    if (Properties.Settings.Default.ctrlToolChange)
                    {   // insert script code into GCODE
                        int index = gCodeLinesSent + 1;
                        int linenr = gCodeLineNr[gCodeLinesSent];
                        grblStatus = grblStreaming.toolchange;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                        index = insertComment(index, linenr, "($TOOL-START)");
                        addToLog("\r[TOOL change: T" + gcodeVariable["TOAN"].ToString() + " at " + gcodeVariable["TOAX"].ToString() + " , " + gcodeVariable["TOAY"].ToString() + " , " + gcodeVariable["TOAZ"].ToString() + "]");
                        if (toolInSpindle)
                        {   addToLog("[TOOL run script 1) " + Properties.Settings.Default.ctrlToolScriptPut + "  T" + gcodeVariable["TOLN"].ToString() + " at " + gcodeVariable["TOLX"].ToString() + " , " + gcodeVariable["TOLY"].ToString() + " , " + gcodeVariable["TOLZ"].ToString() + "]");
                            index = insertCode(Properties.Settings.Default.ctrlToolScriptPut, index, linenr, true);
                            index = insertComment(index, linenr, "($TOOL-OUT)");
                        }
                        addToLog("[TOOL run script 2) " + Properties.Settings.Default.ctrlToolScriptSelect + "]");
                        index = insertCode(Properties.Settings.Default.ctrlToolScriptSelect,index, linenr,true);
                        addToLog("[TOOL run script 3) " + Properties.Settings.Default.ctrlToolScriptGet + "]");
                        index = insertCode(Properties.Settings.Default.ctrlToolScriptGet,   index, linenr, true);
                        index = insertComment(index, linenr, "($TOOL-IN)");
                        addToLog("[TOOL run script 4) " + Properties.Settings.Default.ctrlToolScriptProbe + "]");
                        index = insertCode(Properties.Settings.Default.ctrlToolScriptProbe, index, linenr, true);
                        index = insertComment(index, linenr, "($END)");

                        // save actual tool info as last tool info
                        gcodeVariable["TOLN"] = gcodeVariable["TOAN"];
                        gcodeVariable["TOLX"] = gcodeVariable["TOAX"];
                        gcodeVariable["TOLY"] = gcodeVariable["TOAY"];
                        gcodeVariable["TOLZ"] = gcodeVariable["TOAZ"];

                        grblStatus = grblStreaming.toolchange;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                    }
                    gCodeLines[gCodeLinesSent] = "($" + line + ")";  // don't pass M6 to GRBL because is unknown
                    line = gCodeLines[gCodeLinesSent];
                    gCodeLinesConfirmed++;      // M6 is count as sent (but wasn't send) also count as received
                    */
                }
                if (cmdMNr == 30)
                {
                    /*
                    if (Properties.Settings.Default.ctrlToolChange)
                    {   // insert script code into GCODE
                        int index = gCodeLinesSent + 1;
                        int linenr = gCodeLineNr[gCodeLinesSent];
                        grblStatus = grblStreaming.toolchange;
                        sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);

                        if (toolInSpindle)
                        {
                            addToLog("[TOOL run script 1) " + Properties.Settings.Default.ctrlToolScriptPut + "  T" + gcodeVariable["TOLN"].ToString() + " at " + gcodeVariable["TOLX"].ToString() + " , " + gcodeVariable["TOLY"].ToString() + " , " + gcodeVariable["TOLZ"].ToString() + "]");
                            index = insertCode(Properties.Settings.Default.ctrlToolScriptPut, index, linenr, true);
                            index = insertComment(index, linenr, "($TOOL-OUT)");
                        }
                    }
                    */
                }
                if ((cmdMNr == 0) && !isStreamingCheck)
                {
                    isStreamingRequestPause = true;
                    addToLog("[Pause streaming]");
                    addToLog("[Save Settings]");
                    grblStatus = grblStreaming.waitidle;
                    getParserState = true;
                    sendStreamEvent(gCodeLineNr[gCodeLinesSent], grblStatus);
                    gCodeLinesSent++;
                    return;                 // abort while - don't fill up buffer
                }
                requestSend(line);
                gCodeLinesSent++;
            }
        }


        /*  startStreaming called by main-Prog
         *  get complete GCode list and copy to own list
         *  initialize streaming
         *  if startAtLine > 0 start with pause
         */

        public void startStreaming(IList<string> gCodeList, int startAtLine, bool check = false)
        {
            lastError = "";
            lastSentToCOM.Clear();
            //toolTable.init();       // fill structure
            rtbLog.Clear();
            if (!check)
                addToLog("[Start streaming - no echo]");
            else
                addToLog("[Start code check]");
            saveLastPos();
            if (replaceFeedRate)
                addToLog("!!! Override Feed Rate");
            if (replaceSpindleSpeed)
                addToLog("!!! Override Spindle Speed");
            isStreamingPause = false;
            isStreamingRequestPause = false;
            isStreamingCheck = check;
            grblStatus = grblStreaming.ok;
            string[] gCode = gCodeList.ToArray<string>();
            gCodeLines = new List<string>();
            gCodeLineNr = new List<int>();
            resetStreaming();
            if (isStreamingCheck)
            {
                sendLine("$C");
                grblBufferSize = 100;  //reduce size to avoid fake errors
            }

            string tmp;
            double pWord, lWord, oWord;
            string subline;
            for (int i = startAtLine; i < gCode.Length; i++)
            {
                tmp = cleanUpCodeLine(gCode[i]);
                if ((!string.IsNullOrEmpty(tmp)) && (tmp[0] != ';'))//trim lines and remove all empty lines and comment lines
                {
                    if (tmp.IndexOf("M98") >= 0)    // any subroutines?
                    {
                        pWord = findDouble("P", -1, tmp);
                        lWord = findDouble("L", 1, tmp);
                        int subStart = 0, subEnd = 0;
                        if (pWord > 0)
                        {
                            oWord = -1;
                            for (int si = i; si < gCode.Length; si++)   // find subroutine
                            {
                                subline = gCode[si];
                                if (subline.IndexOf("O") >= 0)          // find O-Word
                                {
                                    oWord = findDouble("O", -1, subline);
                                    if (oWord == pWord)
                                        subStart = si + 1;              // note start of sub
                                }
                                else                                    // find end of sub
                                {
                                    if (subStart > 0)                   // is match?
                                    {
                                        if (subline.IndexOf("M99") >= 0)
                                        { subEnd = si; break; }     // note end of sub
                                    }
                                }
                            }
                            //MessageBox.Show("start " + subStart.ToString()+" end "+ subEnd.ToString());
                            if (subStart < subEnd)
                            {
                                for (int repeat = 0; repeat < lWord; repeat++)
                                {
                                    for (int si = subStart; si < subEnd; si++)   // copy subroutine
                                    {
                                        gCodeLines.Add(gCode[si]);          // add gcode line to list to send
                                        gCodeLineNr.Add(si);                // add line nr
                                        gCodeLinesCount++;                  // Count total lines
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        gCodeLines.Add(tmp);        // add gcode line to list to send
                        gCodeLineNr.Add(i);         // add line nr
                        gCodeLinesCount++;          // Count total lines
                        if (tmp.IndexOf("M30") >= 0)
                            break;
                    }
                }
            }
            gCodeLines.Add("()");        // add gcode line to list to send
            gCodeLineNr.Add(gCode.Length - 1);         // add line nr
            gCodeLinesTotal = gCode.Length - 1;  // gCodeLinesCount will reduced after each 'confirmed' line
            isStreaming = true;
            updateControls();
            if (startAtLine > 0)
            {  // pauseStreaming();
                isStreamingPause = true;
            }
            else
                preProcessStreaming();
        }
        private static double findDouble(string start, double notfound, string txt)
        {
            int istart = txt.IndexOf(start);
            if (istart < 0)
                return notfound;
            string line = txt.Substring(istart + start.Length);
            string num = "";
            foreach (char c in line)
            {
                if (Char.IsLetter(c))
                    break;
                else if (Char.IsNumber(c) || c == '.' || c == '-')
                    num += c;
            }
            if (num.Length < 1)
                return notfound;
            return double.Parse(num, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        private void setToolChangeCoordinates(int cmdTNr, string line = "")
        {
            /*
            toolProp toolInfo = toolTable.getToolProperties(cmdTNr);
            if (toolInfo.toolnr != cmdTNr)
            {
                addToLog("\r[TOOL change: " + cmdTNr.ToString() + " no Information found! (" + line + ")]");
            }
            else
            {   // get new values
//                addToLog("\r[set tool coordinates "+ cmdTNr.ToString() + "]");
                gcodeVariable["TOAN"] = cmdTNr;
                gcodeVariable["TOAX"] = (double)toolInfo.X + (double)Properties.Settings.Default.toolOffX;
                gcodeVariable["TOAY"] = (double)toolInfo.Y + (double)Properties.Settings.Default.toolOffY;
                gcodeVariable["TOAZ"] = (double)toolInfo.Z + (double)Properties.Settings.Default.toolOffZ;
            }
            */
        }

        /*  processSend - send data if GRBL-buffer is ready to take new data
         *  called by timer and rx-interrupt
         *  take care of keywords
         */
        private bool waitForIdle = false;
        private bool externalProbe = false;
        //        private string[] eeprom1 = { "G54", "G55", "G56", "G57", "G58", "G59" };
        //        private string[] eeprom2 = { "G10", "G28", "G30", "G28" };
        public void processSend()
        {
            while ((sendLinesSent < sendLinesCount) && (grblBufferFree >= sendLines[sendLinesSent].Length + 1))
            {
                var line = sendLines[sendLinesSent];
                bool replaced = false;

                if (!isStreaming)       // check tool change coordinates
                {
                    int cmdTNr = gcode.getIntGCode('T', line);
                    if (cmdTNr >= 0)
                    {
                        //toolTable.init();       // fill structure
                        setToolChangeCoordinates(cmdTNr, line);
                        // save actual tool info as last tool info
                        gcodeVariable["TOLN"] = gcodeVariable["TOAN"];
                        gcodeVariable["TOLX"] = gcodeVariable["TOAX"];
                        gcodeVariable["TOLY"] = gcodeVariable["TOAY"];
                        gcodeVariable["TOLZ"] = gcodeVariable["TOAZ"];
                    }
                }
                if (line.IndexOf('#') > 0)                      // check if variable neededs to be replaced
                {
                    line = insertVariable(line);
                    replaced = true;
                    if (grblBufferFree < grblBufferSize)
                        waitForIdle = true;
                }
                if (line.IndexOf("(^2") >= 0)                   // forward cmd to 2nd GRBL
                    if (grblBufferFree < grblBufferSize)
                        waitForIdle = true;

                for (int i = 0; i < eeprom1.Length; i++)           // wait for IDLE beacuse of EEPROM access
                {
                    if (line.IndexOf(eeprom1[i]) >= 0)
                    {
                        if (grblBufferFree < grblBufferSize)
                            waitForIdle = true;
                        break;
                    }
                }
                for (int i = 0; i < eeprom2.Length; i++)        // wait for IDLE beacuse of EEPROM access
                {
                    if (line.IndexOf(eeprom2[i]) >= 0)
                    {
                        if (grblBufferFree < grblBufferSize)
                            waitForIdle = true;
                        break;
                    }
                }

                if ((!waitForIdle) || (grblStateNow == grblState.alarm))
                {
                    if (replaced)
                        sendLines[sendLinesSent] = line;    // needed to get correct length when receiving 'ok'
                                                            //  rtbLog.AppendText(string.Format("!!!> {0} {1}\r\n", line, sendLinesSent));
                    if (serialPort.IsOpen)
                    {
                        sendLine(line);                         // now really send data to Arduino
                        if (lastSentToCOM.Count > 10)
                            lastSentToCOM.Dequeue();            // store last sent commands via COM for error analysis
                        grblBufferFree -= (line.Length + 1);
                        sendLinesSent++;
                    }
                    else
                    {
                        addToLog("!!! Port is closed !!!");
                        resetStreaming();
                    }


                    if (line.IndexOf("(^2") >= 0)
                    {
                        int start = line.IndexOf('(');
                        int end = line.LastIndexOf(')');
                        if ((start >= 0) && (end > start))  // send data to 2nd COM-Port
                        {
                            var cmt = line.Substring(start, end - start + 1);
                        }
                    }
                    if (line.IndexOf("$TOOL") >= 0) { grblStatus = grblStreaming.toolchange; }
                    if (line == "($TOOL-IN)") { toolInSpindle = true; }
                    if (line == "($TOOL-OUT)") { toolInSpindle = false; }
                    if (line == "($END)") { grblStatus = grblStreaming.ok; }
                    if (line == "($PROBE)")
                    { waitForIdle = true; externalProbe = true; }
                }
                else
                    return;
            }
        }
        public event EventHandler<StreamEventArgs> RaiseStreamEvent;
        protected virtual void OnRaiseStreamEvent(StreamEventArgs e)
        {
            //addToLog("OnRaiseStreamEvent " + e.Status.ToString());
            //RaisePosEvent?.Invoke(this, e);
            EventHandler<StreamEventArgs> handler = RaiseStreamEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void grblStateChanged()
        {
            if ((sendLinesConfirmed == sendLinesCount) && (grblStateNow == grblState.idle))   // addToLog(">> Buffer empty\r");
            {
                if (isStreamingRequestPause)
                {
                    isStreamingPause = true;
                    isStreamingRequestPause = false;
                    grblStatus = grblStreaming.pause;
                    if (getParserState)
                    { requestSend("$G"); }
                    updateControls();
                }
            }
        }


        private void timerSerial_Tick(object sender, EventArgs e)
        {
            if (minimizeCount > 0)
            {
                minimizeCount--;
                if (minimizeCount == 0)
                    this.WindowState = FormWindowState.Minimized;
            }

            if (serialPort.IsOpen)
            {
                try
                {
                    var dataArray = new byte[] { Convert.ToByte('?') };
                    serialPort.Write(dataArray, 0, 1);
                }
                catch (Exception er)
                {
                    logError("Retrieving GRBL status", er);
                    serialPort.Close();
                }
            }
            if (waitForIdle)
            {
                processSend();
                //if (hideResponse == 0)
                //    rtbLog.AppendText(".");
            }
            if (isStreaming && !isStreamingRequestPause && !isStreamingPause)
                preProcessStreaming();
            if (callCheckGRBL > 0)
            {
                callCheckGRBL--;
                if (callCheckGRBL == 0)
                { btnCheckGRBL_Click(sender, e); }
            }
            if (preventOutput > 0)
                preventOutput--;
            if (preventEvent > 0)
                preventEvent--;
        }



        //*  ` remove unneccessary char but keep keywords
        //
        private string cleanUpCodeLine(string data)
        {
            var line = data.Replace("\r", "");  //remove CR
            line = line.Replace("\n", "");      //remove LF
            var orig = line;
            int start = orig.IndexOf('(');
            int end = orig.LastIndexOf(')');
            if (start >= 0) line = orig.Substring(0, start);
            if (end >= 0) line += orig.Substring(end + 1);

            // extract GCode for 2nd COM Port
            if ((start >= 0) && (end > start))  // send data to 2nd COM-Port
            {
                var cmt = orig.Substring(start, end - start + 1);
                if ((cmt.IndexOf("(^2") >= 0) || (cmt.IndexOf("($") == 0))
                {
                    line += cmt;                // keep 2nd COM port data for further use
                }
            }

            line = line.ToUpper();              //all uppercase
            line = line.Trim();
            return line;
        }

        private void feedBackSettings(string tmp)
        {
            if (!isStreaming || isStreamingPause)
            {
                tmp = tmp.Replace(" ", String.Empty);
                if (tmp.Contains("$32"))
                {
                    if (tmp.Contains("$32=1")) isLasermode = true;
                    if (tmp.Contains("$32=0")) isLasermode = false;
                    OnRaiseStreamEvent(new StreamEventArgs(0, 0, 0, grblStreaming.lasermode));
                }
                if (tmp.IndexOf("$") >= 0)
                { btnCheckGRBLResult.Enabled = false; btnCheckGRBLResult.BackColor = SystemColors.Control; }
            }
        }
        /// <summary>
        /// sendLine - now really send data to Arduino
        /// </summary>
        private void sendLine(string data)
        {
            try
            {
                serialPort.Write(data + "\r");
                lastSentToCOM.Enqueue(data);        // store last sent commands via COM for error analysis
#if (debuginfo)
                rtbLog.AppendText(string.Format("< {0} {1} {2} {3} \r\n", data, sendLinesSent, sendLinesConfirmed, grblBufferFree));//if not in transfer log the txLine
#endif
                if (!isHeightProbing && (!(isStreaming && !isStreamingPause)) || (cbStatus.Checked))
                {
                    rtbLog.AppendText(string.Format("> {0} \r\n", data));//if not in transfer log the txLine
                    rtbLog.ScrollToCaret();
                }
            }
            catch (Exception err)
            {
                logError("Sending line", err);
                updateControls();
            }
        }
        private string insertVariable(string line)
        {
            int pos = 0, posold = 0;
            string variable, mykey = "";
            double myvalue = 0;
            if (line.Length > 5)        // min length needed to be replaceable: x#TOLX
            {
                do
                {
                    pos = line.IndexOf('#', posold);
                    if (pos > 0)
                    {
                        myvalue = 0;
                        variable = line.Substring(pos, 5);
                        mykey = variable.Substring(1);
                        if (gcodeVariable.ContainsKey(mykey))
                        { myvalue = gcodeVariable[mykey]; }
                        else { line += " (" + mykey + " not found)"; }
                        line = line.Replace(variable, string.Format("{0:0.000}", myvalue));
                        //                  addToLog("replace "+ mykey+" by "+ myvalue.ToString());
                    }
                    posold = pos + 5;
                } while (pos > 0);
            }
            return line.Replace(',', '.');
        }

        private static int callCheckGRBL = 0;

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!isStreaming || isStreamingPause)
            {
                string cmd = cBCommand.Text;
                cBCommand.Items.Remove(cBCommand.SelectedItem);
                cBCommand.Items.Insert(0, cmd);
                requestSend(cmd);
                cBCommand.Text = "";
            }
        }
        private void btnGRBLCommand0_Click(object sender, EventArgs e)
        { requestSend("$"); }

        private void cBCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) { btnSend_Click(sender, e); }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnCheckGRBL_Click(object sender, EventArgs e)
        {
            float stepX = 0, stepY = 0, stepZ = 0;
            float speedX = 0, speedY = 0, speedZ = 0;
            float maxfX = 0, maxfY = 0, maxfZ = 0;
            string rx, ry, rz;
            int id;
            if ((GRBLSettings.Count > 0))
            {
                foreach (string setting in GRBLSettings)
                {
                    string[] splt = setting.Split('=');
                    if (splt.Length > 1)
                    {
                        if (int.TryParse(splt[0].Substring(1), out id))
                        {
                            if (id == 100) { float.TryParse(splt[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out stepX); }
                            else if (id == 101) { float.TryParse(splt[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out stepY); }
                            else if (id == 102) { float.TryParse(splt[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out stepZ); }
                            else if (id == 110) { float.TryParse(splt[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out speedX); }
                            else if (id == 111) { float.TryParse(splt[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out speedY); }
                            else if (id == 112) { float.TryParse(splt[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out speedZ); }
                        }
                    }
                }
                maxfX = stepX * speedX / 60000; rx = (maxfX < 30) ? "ok" : "problem!";
                maxfY = stepY * speedY / 60000; ry = (maxfY < 30) ? "ok" : "problem!";
                maxfZ = stepZ * speedZ / 60000; rz = (maxfZ < 30) ? "ok" : "problem!";
                if ((maxfX < 30) && (maxfY < 30) && (maxfZ < 30))
                    btnCheckGRBLResult.BackColor = Color.Lime;
                else
                    btnCheckGRBLResult.BackColor = Color.Fuchsia;
                btnCheckGRBLResult.Enabled = true;
                float minF = 1800 / Math.Max(stepX, Math.Max(stepY, stepZ));
                strCheckResult = "Maximum frequency at a 'STEP' pin (at Arduino UNO, Nano) must not exceed 30kHz.\r\nCalculation: steps/mm ($100) * speed-mm/min ($110) / 60 / 1000\r\n";
                strCheckResult += string.Format("Max frequency X = {0:.##}kHz - {1}\r\nMax frequency Y = {2:.##}kHz - {3}\r\nMax frequency Z = {4:.##}kHz - {5}\r\n\r\n", maxfX, rx, maxfY, ry, maxfZ, rz);
                strCheckResult += "Minimum feedrate (F) must not go below 30 steps/sec.\r\nCalculation: (lowest mm/min) = (30 steps/sec) * (60 sec/min) / (axis steps/mm setting)\r\n";
                strCheckResult += string.Format("Min Feedrate for X = {0:.#}mm/min\r\nMin Feedrate for Y = {1:.#}mm/min\r\nMin Feedrate for Z = {2:.#}mm/min\r\n\r\n", (1800 / stepX), (1800 / stepY), (1800 / stepZ));
                strCheckResult += string.Format("Avoid feedrates (F) below {0:.#}mm/min\r\n", minF);
                strCheckResult += "\r\nSettings are copied to clipboard for further use (e.g. save as text file)";
                System.Windows.Forms.Clipboard.SetText(string.Join("\r\n", GRBLSettings.ToArray()));

                //               MessageBox.Show(strCheckResult, "Information");
                GRBLSettings.Clear();
            }
            else
            {
                if (grblStateNow == grblState.idle)
                {
                    requestSend("$$"); //GRBLSettings.Clear();
                    callCheckGRBL = 4;                         // timer1 will recall this function after 2 seconds
                }
                else
                {
                    addToLog("Wait for IDLE, then try again!");
                }
            }
        }
        private static string strCheckResult = "";

        private void btnGrabCamHole0_Click(object sender, EventArgs e)
        {
            if (holeZero != null && hole2 != null)
            {
                lblPCBHole0_X.Text = posWork.X.ToString("0.000");
                lblPCBHole0_Y.Text = posWork.Y.ToString("0.000");
                PCBHole0 = true;
                checkBothPCBHolesSelected();
            }
            else
            {
                MessageBox.Show("Ensure Drill Holes are both selected!");
            }
        }

        private void btnGrabCamHole2_Click(object sender, EventArgs e)
        {
            if (holeZero != null && hole2 != null)
            {
                lblPCBHole2_X.Text = posWork.X.ToString("0.000");
                lblPCBHole2_Y.Text = posWork.Y.ToString("0.000");
                PCBHole1 = true;
                checkBothPCBHolesSelected();
            }
            else
            {
                MessageBox.Show("Ensure Drill Holes are both selected!");
            }

        }

        bool stopStream = false;
        private void btnStreamCode_Click(object sender, EventArgs e)
        {
            stopStream = false;
            if (btnStreamCode.Text != "Stop")
            {
                btnStreamCode.Text = "Stop";
                if (richTextBox1.Lines.Count() > 0)
                {
                    foreach (string lne in richTextBox1.Lines)
                    {
                        if (!stopStream)
                        {
                            if (!lne.StartsWith(";")) requestSend(lne);
                        }
                        else
                        {
                            realtimeCommand(133);
                            btnStreamCode.Text = "Stream Generated Code";
                            break;
                        }
                    }
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            requestSend("$H");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            requestSend("G92 X0 Y0 Z0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            requestSend("G0 Z" + txtScanHeight.Text);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            
        }

        private void label33_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDrill_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.BackColor == Color.White)
            {
                ActiveDrills.Add(int.Parse(btn.Text));
                btn.BackColor = Color.LightGreen;
            }
            else
            {
                ActiveDrills.Remove(int.Parse(btn.Text));
                btn.BackColor = Color.White;
            }
        }

        private void btnGRBLCommand1_Click_1(object sender, EventArgs e)
        {
            { requestSend("$$"); GRBLSettings.Clear(); }
        }

        private void btnCheckGRBLResult_Click(object sender, EventArgs e)
        {
            MessageBox.Show(strCheckResult, "Information");
        }


        public void realtimeCommand(int cmd)
        {
            realtimeCommand((byte)cmd);
        }


        public void realtimeCommand(byte cmd)
        {
            var dataArray = new byte[] { Convert.ToByte(cmd) };
            serialPort.Write(dataArray, 0, 1);
            addToLog("> '0x" + cmd.ToString("X") + "' " + grbl.getRealtime(cmd));
            if ((cmd == 0x85) && !(isStreaming && !isStreamingPause))                   //  Jog Cancel
            {
                sendLinesSent = 0;
                sendLinesCount = 0;
                sendLinesConfirmed = 0;
                sendLines.Clear();
                grblBufferFree = grblBufferSize;
            }
        }


    }
}
