using Microsoft.VisualStudio.TestTools.UnitTesting;
using EW.FlightSimulator.Common.Units;

namespace Flightsim.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var feet = 5280;

            var length = Length.FromFeet(feet);

        }
    }
}
