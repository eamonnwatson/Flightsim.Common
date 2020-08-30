using System;
using System.Collections.Generic;
using System.Text;

namespace EW.FlightSimulator.Common.Units
{
    internal interface IUnit<TUnitType> where TUnitType : Enum
    {
        double Value { get; }
        TUnitType Unit { get; }
        double To(TUnitType unit);
    }
}
