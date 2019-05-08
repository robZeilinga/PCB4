using DirectShowLib;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/*
    To do : a)  read 2 holes from CNC 
            to get angle
            Scale
            Offset

            Rotate & offset Holes 

            Generate CNC file 

            Wahooo !!!!!




*/

namespace PCB3
{
    public partial class Form1 : Form
    {
        //==================== camera stuff ==========================
        // Open CV stuff =======================
        Emgu.CV.VideoCapture _capture = null;
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
        int frame = 30;
        int spacing = 0; // pixel spacing between integer points on picBoxOrig
        Hole currentHole;
        Hole holeZero;
        Hole hole2;
        DrillJob2 dj2;

        Graphics g1;
        Graphics g2;


        static Color defaultPenColor = Color.AliceBlue;
        public Form1()
        {
            InitializeComponent();
            Graphics g1 = Graphics.FromHwnd(pic_Box_Orig.Handle);
            Graphics g2 = Graphics.FromHwnd(pic_Box_Res.Handle);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
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

            lbl_NumHoles.Text = drillSum.ToString();
            lbl_Extents_X_Max.Text = dj2.dMAX_X.ToString(fmt);
            lbl_Extents_X_Min.Text = dj2.dMIN_X.ToString(fmt);

            lbl_Extents_Y_Max.Text = dj2.dMAX_Y.ToString(fmt);
            lbl_Extents_Y_Min.Text = dj2.dMIN_Y.ToString(fmt);


            lbl_Extents_dX.Text = (dj2.dMAX_X - dj2.dMIN_X).ToString(fmt);
            lbl_Extents_dY.Text = (dj2.dMAX_Y - dj2.dMIN_Y).ToString(fmt);

            // calc display Scale 
            lbl_PicX.Text = "/  " + (pic_Box_Orig.Width - (frame * 2)).ToString();
            lbl_PicY.Text = "/  " + (pic_Box_Orig.Height - (frame * 2)).ToString();

            int maxRulerY = Convert.ToInt16(dj2.dMAX_Y) + 5;
            int maxRulerX = Convert.ToInt16(dj2.dMAX_X) + 5;

            double xScale = Convert.ToDouble(maxRulerX) / (pic_Box_Orig.Width - (frame * 2));
            double yScale = Convert.ToDouble(maxRulerY) / (pic_Box_Orig.Height - (frame * 2));

            lbl_Scale_X.Text = xScale.ToString(fmt);
            lbl_Scale_Y.Text = yScale.ToString(fmt);




            if (g1 == null) g1 = Graphics.FromHwnd(pic_Box_Orig.Handle);
            if (g2 == null) g2 = Graphics.FromHwnd(pic_Box_Res.Handle);

            draw_Rulers(g1, maxRulerX, maxRulerY, pic_Box_Orig);
            draw_Rulers(g2, maxRulerX, maxRulerY, pic_Box_Res);



            // draw holes 



            drawHoles(dj2, spacing, frame);
        }

        private Point DrawHole(Graphics g, PictureBox picBox, double holeX, double holeY, double holeSize, Pen pen)
        {
            Point retVal = new Point();

            double tx = (holeX * spacing);
            double ty = (holeY * spacing);

            int plotx = (int)tx + frame - 1;
            int ploty = picBox.Height - frame - (int)ty - 1;
            holeSize = holeSize * spacing;

            retVal = new Point(plotx, ploty);

            g.DrawEllipse(pen, plotx - (int)(holeSize / 2), ploty - (int)(holeSize / 2), (float)holeSize, (float)holeSize);

            return retVal;
        }

        private void drawHoles(DrillJob2 dj2, int spacing, int frame)
        {
            if (g1 == null) g1 = Graphics.FromHwnd(pic_Box_Orig.Handle);
            foreach (Hole hle in dj2.Holes)
            {

                hle.plot_Point = DrawHole(g1, pic_Box_Orig, hle.ResolvedX, hle.ResolvedY, dj2.Drills[hle.ToolNum], dj2.DrillColours[hle.ToolNum - 1]);

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

            Pen p = Pens.LightGray;
            g.Clear(Color.DarkGray);

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
            for (int yt = 0; yt <= maxY; yt = yt + 10)
            {
                // so line is from Frame-tickLength to Frame or picboxWidth-Frame(1.5)
                // height = picBoxHeight -frame - (spacing * yt)
                Point p1 = new Point(frame - tickLength, picBox.Height - frame - (spacing * yt));
                Point p2 = new Point(frame, picBox.Height - frame - (spacing * yt));
                if (cb_Grid.Checked) p2 = new Point(picBox.Width - (frame / 2), picBox.Height - frame - (spacing * yt));
                g.DrawLine(p, p1, p2);
                g.DrawString(yt.ToString(), new Font(FontFamily.GenericMonospace, 9f), new SolidBrush(Color.Gray), new Point(0, picBox.Height - frame - (spacing * yt)));
            }
            // x ticks 
            for (int xt = 0; xt <= maxX; xt = xt + 10)
            {
                // so line is from picBox.Height - Frame + tickLength to (Frame/2) or picboxHeight-Frame
                // X pos = frame + (spacing * xt)
                Point p1 = new Point(frame + (spacing * xt), picBox.Height - frame + tickLength);
                Point p2 = new Point(frame + (spacing * xt), picBox.Height - frame);

                g.DrawString(xt.ToString(), new Font(FontFamily.GenericMonospace, 9f), new SolidBrush(Color.Gray), new Point(frame + (spacing * xt) - 10, picBox.Height - 18));
                if (cb_Grid.Checked) p2 = new Point(frame + (spacing * xt), frame);

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
            using (Pen p = new Pen(Color.DarkCyan, 3f))
            {
                foreach (Hole hle in dj2.Holes)
                {
                    if (me.X > (hle.plot_Point.X - 5) && me.X < (hle.plot_Point.X + 5) && me.Y > (hle.plot_Point.Y - 5) && me.Y < (hle.plot_Point.Y + 5))
                    {
                        // found
                        /*
                        if (cb_Flip.Checked)
                        {
                            lblHoleFound.Text = " Hole x : " + hle.ResolvedX.ToString() + "  y: " + hle.FlippedResolvedY.ToString() + ":" + hle.plot_Point.X + " | " + hle.plot_Point.Y;
                            DrawHole(g1, pic_Box_Orig, hle.ResolvedX, hle.FlippedResolvedY, 5, p);
                        }
                        else
                        {
                        */
                        lblHoleFound.Text = " Hole x : " + hle.ResolvedX.ToString() + "  y: " + hle.ResolvedY.ToString() + ":" + hle.plot_Point.X + " | " + hle.plot_Point.Y;
                        DrawHole(g1, pic_Box_Orig, hle.ResolvedX, hle.ResolvedY, 5, p);
                        /*
                        }
                        */
                        currentHole = hle;
                        break;
                    }
                }
                lblHoleFound.Text = "False";
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
                hle.rotate(angle, scale, xOffset, yOffset);
                DrawRotatedHole(hle);
            }
        }

        private void DrawRotatedHole(Hole hle)
        {
            DrawHole(g2, pic_Box_Res, hle.RotatedX, hle.RotatedY, dj2.Drills[hle.ToolNum], dj2.DrillColours[hle.ToolNum - 1]);
        }

        private double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            angle += 1;
            button1_Click(button1, new EventArgs());
            if (angle >= 360) angle = 0.1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

                    double width = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.XiWidth);
                    double height = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.XiHeight);

                    // work out ratio 


                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, FrameRate);
                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 720 / 2);
                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 1280 / 2);
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
            if( txtCameraOffsetX.Text.Trim() != "" && txtCameraOffsetY.Text.Trim() != "")
            {
                Offset_X = double.Parse(txtCameraOffsetX.Text.Trim());
                Offset_Y = double.Parse(txtCameraOffsetY.Text.Trim());
            }
            Properties.Settings.Default.Offset_Camera_X = Offset_X;
            Properties.Settings.Default.Offset_Camera_Y = Offset_Y;
            Properties.Settings.Default.Save();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // accept this hole as hole 2!  
            if (currentHole != null && currentHole != holeZero)
            {
                hole2 = currentHole;
                lbl_Hole2_X.Text = currentHole.ResolvedX.ToString();
                lbl_Hole2_Y.Text = currentHole.FlippedResolvedY.ToString();
                checkBothHolesSelected();
            }
        }
        private void btn__DrillFileLoad_Click(object sender, EventArgs e)
        {
            // accept this hole as hole Zero!  
            if (currentHole != null)
            {
                holeZero = currentHole;
                lbl_Click_X.Text = currentHole.ResolvedX.ToString();
                lbl_Click_Y.Text = currentHole.FlippedResolvedY.ToString();
                checkBothHolesSelected();
            }
        }

        private void checkBothHolesSelected()
        {
            if (holeZero != null && hole2 != null)
            {
                double diffX = hole2.ResolvedX - holeZero.ResolvedX;
                double diffY = hole2.FlippedResolvedY - holeZero.FlippedResolvedY;
                dist1 = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

                lbl_Dist.Text = dist1.ToString("0.000");
                angle1 = Math.Atan2(diffY, diffX) / (Math.PI / 180);
                lbl_Angle.Text = angle1.ToString("0.000");

            }
        }

        private void txt_Hole_Zero_X_TextChanged(object sender, EventArgs e)
        {
            if (txt_Hole_Zero_X.Text.Trim() != "" && txt_Hole_Zero_Y.Text.Trim() != "" && txt_Hole_2_X.Text.Trim() != "" && txt_Hole_2_Y.Text.Trim() != "")
            {
                double hole2_Y = double.Parse(txt_Hole_2_Y.Text.Trim());
                double hole2_X = double.Parse(txt_Hole_2_X.Text.Trim());
                double hole0_X = double.Parse(txt_Hole_Zero_X.Text.Trim());
                double hole0_Y = double.Parse(txt_Hole_Zero_Y.Text.Trim());
                double xdiff = hole2_X - hole0_X;
                double ydiff = hole2_Y - hole0_Y;
                dist2 = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2));
                lblCamDist.Text = dist2.ToString("0.000");
                angle2 = Math.Atan2(ydiff, xdiff) / (Math.PI / 180);
                lblCamAngle.Text = angle2.ToString("0.000");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // calc scale & angle diff 
            result_Scale = dist2 / dist1;
            lbl_Result_Scale.Text = result_Scale.ToString("0.0000");
            result_angle = Math.Abs(angle1) - Math.Abs(angle2);
            lbl_Result_Angle.Text = result_angle.ToString("0.0000");

        }

        private void button5_Click(object sender, EventArgs e)
        {

            // hole Zero from Camera
            double hole0_Cam_X = double.Parse(txt_Hole_Zero_X.Text.Trim());
            double hole0_Cam_Y = double.Parse(txt_Hole_Zero_Y.Text.Trim());

            // hole Zero from dRill File
            double hole0_Drill_X = holeZero.ResolvedX;
            double hole0_Drill_Y = holeZero.FlippedResolvedY;

            double Gcode_Offset_X = hole0_Cam_X - hole0_Drill_X;
            double Gcode_Offset_Y = hole0_Cam_Y - hole0_Drill_Y;

            double dSafeZHeight = 0.0;
            bool bSafeHeight = double.TryParse(txtSafeZHeight.Text.Trim(), out dSafeZHeight);
            double dDrillDepth = 0.0;
            bool bDrillDepth = double.TryParse(txtDrillDepth.Text.Trim(), out dDrillDepth);
            if (bSafeHeight && bDrillDepth)
            {

                string safeZHeight = dSafeZHeight.ToString("0.000");
                string drillDepth =  dDrillDepth.ToString("0.000");
                double _offsetX = 0.0;
                double _offsetY = 0.0;

                if(cb_Drill.Checked)
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
                richTextBox1.AppendText("G0 X" + (holeZero.ResolvedX + Gcode_Offset_X + _offsetX).ToString("0.000") + " Y" + (holeZero.FlippedResolvedY + Gcode_Offset_Y-0.6+_offsetY).ToString("0.000") + System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z" + drillDepth +  System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z" + safeZHeight + System.Environment.NewLine);

                //richTextBox1.AppendText("G4 0.5" + System.Environment.NewLine);
                richTextBox1.AppendText("; ---------------------------------" + System.Environment.NewLine);

                foreach (Hole hle in dj2.Holes)
                {
                    hle.HoleZero = holeZero;
                    hle.rotationAngle = result_angle;
                    hle.scale = result_Scale;
                    hle.rotate(angle, result_Scale, 0, 0);
                    richTextBox1.AppendText("G0 X" + (hle.RotatedX + Gcode_Offset_X+_offsetX).ToString("0.000") + " Y" + (hle.RotatedY + Gcode_Offset_Y-0.6+_offsetY).ToString("0.000") + System.Environment.NewLine);
                    //richTextBox1.AppendText("G4 0.5" + System.Environment.NewLine);
                    richTextBox1.AppendText("G0 Z" + drillDepth + System.Environment.NewLine);
                    richTextBox1.AppendText("G0 Z" + safeZHeight + System.Environment.NewLine);
                    richTextBox1.AppendText("; ---------------------------------" + System.Environment.NewLine);
                }
                richTextBox1.AppendText("; END " + System.Environment.NewLine);
                richTextBox1.AppendText("G0 Z0 X0 Y0  ; Go Home" + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine);
            }
        }
    }
}
