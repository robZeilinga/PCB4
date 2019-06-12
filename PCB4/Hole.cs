using System;
using System.Drawing;

namespace PCB_DR
{
    public class Hole
    {
        /// <summary>
        /// which tool - drill size for this hole
        /// </summary>
        public int ToolNum { get; set; }

        /// <summary>
        /// Metric or Emperial 
        /// </summary>
        public Units HoleUnit { get; set; }

        /// <summary>
        /// actual x & y points from drd file.
        /// </summary>
        public Point FilePoint  { get; set; }

        /// <summary>
        /// Zero Hole References  (which everything revolves around) 
        /// </summary>
        public Hole HoleZero{ get; set; }
        public Point ZeroPoint { get; set; }

        private Point flippedFilePoint;

        /// <summary>
        /// flipped Y ( turning PCB over to show Tracks)  
        /// </summary>
        public Point FlippedFilePoint
        {
            get
            {
                return this.flippedFilePoint;
            }
            set
            {
                this.flippedFilePoint = value;
                if (HoleUnit == Units.METRIC)
                {
                    // 10,000 th of  MM
                    FlippedYinmm =  Convert.ToDouble(value.Y) / 1000;
                }
                else
                {
                    // 100,000 th of INCH
                    FlippedYinmm = Convert.ToDouble(value.Y) / 100000;
                }
            }
        }

        /// <summary>
        /// resolved locations are those that have been converted back to base units from File units 10 000 mm or 100 000 inch
        /// </summary>
        
        public double Xinmm { get; set; }
        /// <summary>
        /// resolved locations are those that have been converted back to base units from File units 10 000 mm or 100 000 inch
        /// </summary>
        public double Yinmm { get; set; }
        /// <summary>
        /// resolved locations are those that have been converted back to base units from File units 10 000 mm or 100 000 inch
        /// </summary>
        public double FlippedYinmm { get; set; }

        /// <summary>
        /// placeholder for scale (should always be 1 )
        /// </summary>
        public double scale { get; set; }


        /// <summary>
        /// Rotation angle  
        /// </summary>
        public double rotationAngle { get; set; }

        /// <summary>
        /// Rotated X location 
        /// </summary>
        public double RotatedX { get; set; }
        /// <summary>
        /// Rotated Y location 
        /// </summary>
        public double RotatedY { get; set; }
        /// <summary>
        /// Rotated and Flipped Y location 
        /// </summary>
        public double FlippedRotatedY { get; set; }

        /// <summary>
        /// point where the gui has plotted this hole on the picture box, helps identify which hole was clicked on from the click event data.
        /// </summary>
        public Point plot_Point  { get; set; }

        // -----------------------------------------------------------------------------------------------------------------------
        public Hole(double X, double Y, int toolnum, Units fileUnits)
        {
            CreateHole(X, Y, toolnum, fileUnits);
        }

        public Hole(string line, int toolNum, Units fileUnits)
        {
            line = line.Trim().Substring(1);  // remove leading X;
            string[] parts = line.Split(new char[] { 'Y' });
            CreateHole(double.Parse(parts[0]), double.Parse(parts[1]), toolNum, fileUnits);
        }

        /// <summary>
        /// perform rotation of point araound the Zero hole - Throws Exception if Hole Zero is null
        /// </summary>
        /// <param name="rotAngle"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="scale"></param>
        public void rotate(double rotAngle, int xOffset = 0, int yOffset = 0, double scale = 1)
        {
            if(this.HoleZero == null)
            {
                throw new Exception("Cannot Rotate Hole, Hole xero not set");
            }

            this.rotationAngle = rotAngle;

            if (Xinmm == HoleZero.Xinmm && Yinmm == HoleZero.Yinmm)
            {
                // hole zero 
                this.RotatedX = Xinmm + xOffset;
                this.RotatedY = Yinmm + yOffset;
                this.FlippedRotatedY = FlippedYinmm + yOffset;
            }
            else
            {
                
                double xDiff = Xinmm - HoleZero.Xinmm;
                double yDiff = Yinmm - HoleZero.Yinmm;
                double flipped_yDiff = FlippedYinmm - HoleZero.FlippedYinmm;

                if(scale != 1)
                {
                    xDiff = xDiff * scale;
                    yDiff = yDiff * scale;
                    flipped_yDiff = flipped_yDiff * scale;
                }

                double original_hole_angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                double flipped_angle = Math.Atan2(flipped_yDiff, xDiff) * 180.0 / Math.PI;
                double dist1 = Math.Sqrt(Math.Pow(yDiff, 2) + Math.Pow(xDiff, 2));
                double flipped_dist1 = Math.Sqrt(Math.Pow(flipped_yDiff, 2) + Math.Pow(xDiff, 2));
                //if (scale != 1) dist1 = dist1 * scale;
                //if (scale != 1) flipped_dist1 = flipped_dist1 * scale;


                // Calculate Final Rotation Angle for This Hole
                double finalRotationAngleInRadians = AngleToRadians(original_hole_angle + this.rotationAngle);
                
                // Calculate offset of this hole === final position 
                double Xoffset = HoleZero.Xinmm + dist1 * Math.Cos(finalRotationAngleInRadians);
                double Yoffset = HoleZero.Yinmm + dist1 * Math.Sin(finalRotationAngleInRadians);
                double FlippedYoffset = HoleZero.FlippedYinmm + flipped_dist1 * Math.Sin(finalRotationAngleInRadians);

                this.RotatedX = Xoffset + xOffset;
                this.RotatedY = Yoffset + yOffset;
                this.FlippedRotatedY = FlippedYoffset + yOffset ;
            }
        }

        // ----------------------------------------------------------------------------------------

        private double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void CreateHole(double X, double Y, int toolnum, Units fileUnits)
        {
            this.ToolNum = toolnum;
            this.FilePoint = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
            this.HoleUnit = fileUnits;
            if (HoleUnit == Units.METRIC)
            {
                // 10,000 th of  MM
                this.Xinmm = X / 10000;
                this.Yinmm = Y / 10000;
            }
            else
            {
                // 100,000 th of INCH
                this.Xinmm = X / 100000;
                this.Yinmm = Y / 100000;
            }
        }



    }
}
