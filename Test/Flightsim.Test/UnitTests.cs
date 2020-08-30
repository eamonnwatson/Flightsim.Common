using Microsoft.VisualStudio.TestTools.UnitTesting;
using EW.FlightSimulator.Common.Units;
using System;

namespace Flightsim.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestLength()
        {
            var feet = 5280;

            var length = Length.FromFeet(feet);

            Assert.AreEqual(1d, Math.Round(length.Miles, 2));
        }
    }
}
