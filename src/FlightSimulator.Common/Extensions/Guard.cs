using System;
using System.Collections.Generic;
using System.Text;

namespace EW.FlightSimulator.Common.Extensions
{
    internal static class Guard
    {
        internal static double EnsureValidNumber(double value, string paramName)
        {
            if (double.IsNaN(value)) throw new ArgumentException("NaN is not a valid number.", paramName);
            if (double.IsInfinity(value)) throw new ArgumentException("PositiveInfinity or NegativeInfinity is not a valid number.", paramName);
            return value;
        }
    }
}
