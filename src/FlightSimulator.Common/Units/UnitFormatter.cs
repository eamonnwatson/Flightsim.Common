using System;

namespace EW.FlightSimulator.Common.Units
{
    internal class UnitFormatter
    {
        internal static string Format<TUnitType>(IUnit<TUnitType> unit, string format) where TUnitType : Enum
        {
            if (string.IsNullOrWhiteSpace(format))
                format = "g";

            throw new NotImplementedException();
        }
    }
}