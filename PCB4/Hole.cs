using System;
using System.Drawing;

namespace PCB3
{
    public class Hole
    {
        public int ToolNum { get; set; }
        public Units HoleUnit { get; set; }
        public Point FilePoint  { get; set; }

        public Point ZeroPoint { get; set; }

        private Point flippedFilePoint;
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
                    FlippedResolvedY = value.Y / 10000;
                }
                else
                {
                    // 100,000 th of INCH
                    FlippedResolvedY = value.Y / 100000;
                }
            }
        }


        public double ResolvedX { get; set; }
        public double ResolvedY { get; set; }
        public double FlippedResolvedY { get; set; }
        public Hole HoleZero{ get; set; }
        public double rotationAngle { get; set; }
        public double scale { get; set; }
        public double RotatedX { get; set; }
        public double RotatedY { get; set; }
        public double FlippedRotatedY { get; set; }



        public Point plot_Point  { get; set; }

      
        public Hole(string line, int toolNum, Units fileUnits)
        {

            //HoleZero = false;
            ToolNum = toolNum;
            line = line.Trim().Substring(1);  // remove leading X;
            string[] parts = line.Split(new char[] { 'Y' });
            FilePoint = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
            HoleUnit = fileUnits;
            // resolve from point.
            if(HoleUnit == Units.METRIC)
            {
                // 10,000 th of  MM
                ResolvedX = double.Parse(parts[0]) / 10000;
                ResolvedY = double.Parse(parts[1]) / 10000;
            }
            else
            {
                // 100,000 th of INCH
                ResolvedX = double.Parse(parts[0]) / 100000;
                ResolvedY = double.Parse(parts[1]) / 100000;
            }
        }

        public void rotate(double rotAngle, double scale = 1, int xOffset = 0, int yOffset = 0)
        {
            //scale = 1.0;
            rotationAngle = rotAngle;

            if (ResolvedX == HoleZero.ResolvedX && ResolvedY == HoleZero.ResolvedY)
            {
                // hole zero 
                RotatedX = ResolvedX + xOffset;
                RotatedY = ResolvedY + yOffset;
                FlippedRotatedY = FlippedResolvedY + yOffset;
                //if (flipped) RotatedY = flippedResolvedY + yOffset;

            }
            else
            {
                double xDiff = ResolvedX - HoleZero.ResolvedX;
                double yDiff = ResolvedY - HoleZero.ResolvedY;
                double f_yDiff = FlippedResolvedY - HoleZero.FlippedResolvedY;

                //if (flipped) yDiff = flippedResolvedY - HoleZero.flippedResolvedY;
                double angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                double f_angle = Math.Atan2(f_yDiff, xDiff) * 180.0 / Math.PI;
                double dist1 = Math.Sqrt(Math.Pow(yDiff, 2) + Math.Pow(xDiff, 2));
                double f_dist1 = Math.Sqrt(Math.Pow(f_yDiff, 2) + Math.Pow(xDiff, 2));
                if (scale != 1) dist1 = dist1 * scale;
                if (scale != 1) f_dist1 = f_dist1 * scale;


                // add angle
                angle += rotationAngle;
                // offset 

                double Xoffset = HoleZero.ResolvedX + dist1 * Math.Cos(AngleToRadians(angle));
                double Yoffset = HoleZero.ResolvedY + dist1 * Math.Sin(AngleToRadians(angle));
                double FlippedYoffset = HoleZero.FlippedResolvedY + f_dist1 * Math.Sin(AngleToRadians(angle));

                RotatedX = Xoffset + xOffset;
                RotatedY = Yoffset + yOffset;
                FlippedRotatedY = FlippedYoffset + yOffset ;
            }
        }

        private double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

    }
}
