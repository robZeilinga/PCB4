using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace PCB4
{
    class DrillJob2
    {
        public string DrillFileName { get; set; }
        public Dictionary<int, double> Drills { get; set; }
        public Dictionary<int, int> DrillCount { get; set; }

        public Dictionary<int, Pen> DrillColours { get; set; }

        public Units JobUnits { get; set; }


        public int iMIN_X { get; set; }
        public int iMIN_Y { get; set; }
        public int iMIN_FY { get; set; }
        public int iMAX_X { get; set; }
        public int iMAX_Y { get; set; }
        public int iMAX_FY { get; set; }
        public double dMIN_X { get; set; }
        public double dMIN_Y { get; set; }
        public double dMAX_X { get; set; }
        public double dMAX_Y { get; set; }


        public List<Hole> Holes { get; set; }

        public DrillJob2(string drillFileName)
        {
            Pen[] pens = new Pen[] { Pens.Black, Pens.Blue, Pens.Red, Pens.Green, Pens.Orange, Pens.Yellow,  Pens.Brown, Pens.BlueViolet, Pens.Chocolate, Pens.Cyan, Pens.DarkRed, Pens.DeepSkyBlue };
            DrillColours = new Dictionary<int, Pen>();


            iMAX_X = int.MinValue;
            iMAX_Y = int.MinValue;
            iMAX_FY = int.MinValue;

            iMIN_X = int.MaxValue;
            iMIN_Y = int.MaxValue;
            iMIN_FY = int.MaxValue;

            Holes = new List<Hole>();
            Drills = new Dictionary<int, double>();
            DrillCount = new Dictionary<int, int>();
            bool foundFirstPerc = false;
            bool foundSecondPerc = false;
            JobUnits = Units.METRIC;
            int currentToolNumber = 0;

            string[] lines = File.ReadAllLines(drillFileName);

            foreach (string lne in lines)
            {
                string thisLine = lne.Trim();

                if (thisLine == "%")
                {
                    if (!foundFirstPerc)
                    {
                        foundFirstPerc = true;
                        continue;
                    }

                    if (foundFirstPerc && !foundSecondPerc)
                    {
                        foundSecondPerc = true;
                        continue;
                    }
                }

                if (thisLine == "M71")
                {
                    JobUnits = Units.METRIC;
                }
                if (thisLine == "M72")
                {
                    JobUnits = Units.IMPERIAL;
                }

                if (thisLine == "M30")
                {
                    for (int x = 0; x < Drills.Count; x++)
                    {
                        DrillColours.Add(x+1, pens[x]);
                    }
                    // end

                }

                if (thisLine.StartsWith("T"))
                {
                    if (!foundSecondPerc)
                    {
                        // tool defintion
                        string[] toolparts = thisLine.Substring(1).Split(new char[] { 'C' });
                        int toolNum = int.Parse(toolparts[0]);
                        double toolSize = double.Parse(toolparts[1], CultureInfo.InvariantCulture);
                        Drills.Add(toolNum, toolSize);
                        DrillCount.Add(toolNum, 0);
                    }
                    else
                    {
                        // get tool number
                        int toolNum = int.Parse(thisLine.Substring(1));
                        currentToolNumber = toolNum;
                    }
                }
                if (thisLine.StartsWith("X"))
                {
                    // hole!
                    Hole thisHole = new Hole(thisLine, currentToolNumber, JobUnits);
                    // check extents 
                    if (thisHole.FilePoint.X > iMAX_X) iMAX_X = thisHole.FilePoint.X;
                    if (thisHole.FilePoint.Y > iMAX_Y) iMAX_Y = thisHole.FilePoint.Y;

                    if (thisHole.FilePoint.X < iMIN_X) iMIN_X = thisHole.FilePoint.X;
                    if (thisHole.FilePoint.Y < iMIN_Y) iMIN_Y = thisHole.FilePoint.Y;

                    /* double div = 10000.0; // metric 1/10,000
                     if (JobUnits == Units.IMPERIAL)
                     {
                         div = 100000.0; // Imperial 1/100,000
                     }
                     dMIN_X = Convert.ToDouble(iMIN_X) / div;
                     dMIN_Y = Convert.ToDouble(iMIN_Y) / div;
                     dMAX_X = Convert.ToDouble(iMAX_X) / div;
                     dMAX_Y = Convert.ToDouble(iMAX_Y) / div;
                     */
                    Holes.Add(thisHole);
                    DrillCount[currentToolNumber] = DrillCount[currentToolNumber] + 1;
                }
            }
            // got all holes
            /*
            foreach(Hole hle in Holes)
            {
                hle.FlippedFilePoint = new Point(hle.FilePoint.X, iMAX_Y - hle.FilePoint.Y);
            }
            */

            // ok we have extents so remove minX & min Y to move to 0,0 
            foreach (Hole hle in Holes)
            {
                //hle.ZeroPoint = new Point(hle.FilePoint.X - iMIN_X, hle.FilePoint.Y - iMIN_Y);
                hle.FlippedFilePoint = new Point(hle.FilePoint.X, iMAX_Y - hle.FilePoint.Y);
                if (hle.FlippedFilePoint.Y > iMAX_FY) iMAX_FY = hle.FlippedFilePoint.Y;
                if (hle.FlippedFilePoint.Y < iMIN_FY) iMIN_FY = hle.FlippedFilePoint.Y;
            }

            double div = 1000.0; // metric 1/10,000
            if (JobUnits == Units.IMPERIAL)
            {
                div = 10000.0; // Imperial 1/100,000
            }
            dMIN_X = int.MaxValue;
            dMIN_Y = int.MaxValue;
            dMAX_X = int.MinValue;
            dMAX_Y = int.MinValue;


            foreach (Hole hle in Holes)
            {
                hle.ZeroPoint = new Point(hle.FlippedFilePoint.X - iMIN_X, hle.FlippedFilePoint.Y - iMIN_FY);
                hle.ResolvedX = hle.ZeroPoint.X / div;
                hle.ResolvedY = hle.ZeroPoint.Y / div;
                if (hle.ResolvedX > dMAX_X) dMAX_X = hle.ResolvedX;
                if (hle.ResolvedX < dMIN_X) dMIN_X = hle.ResolvedX;

                if (hle.ResolvedY > dMAX_Y) dMAX_Y = hle.ResolvedY;
                if (hle.ResolvedY < dMIN_Y) dMIN_Y = hle.ResolvedY;

            }
        }
    }
}
