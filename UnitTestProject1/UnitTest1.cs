using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCB3;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Hole h1 = new Hole("X200000Y200000", 1, Units.METRIC);
            Hole h2 = new Hole("X400000Y200000", 1, Units.METRIC);
            h2.HoleZero = h1;
            h2.rotate(5.0);
            Assert.AreEqual(39.9238, h2.RotatedX, 0.001);

            Assert.AreEqual(21.7431, h2.RotatedY, 0.001); 

        }
    }
}
