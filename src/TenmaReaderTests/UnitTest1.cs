using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TenmaReaderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_reading_resistance()
        {
            var reading = new TenmaReader.Reading("109973802");
            //Debug.WriteLine($"{reading.value} {reading.units}");
            Assert.AreEqual(reading.units, "kΩ");
            Assert.AreEqual(reading.value, 0.997);
        }

        [TestMethod]
        public void Test_reading_voltage()
        {
            var reading = new TenmaReader.Reading("04954;80:");
            //Debug.WriteLine($"{reading.value} {reading.units} ({reading.multiplier})");
            Assert.AreEqual(reading.units, "V");
            Assert.AreEqual(reading.value, 4.954);
        }
    }
}
