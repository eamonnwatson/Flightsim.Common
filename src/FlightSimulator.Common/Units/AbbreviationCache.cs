using System;
using System.Collections.Generic;
using System.Text;

namespace EW.FlightSimulator.Common.Units
{
    internal class AbbreviationCache
    {
        private static readonly (Type UnitType, int UnitValue, string[] Abbreviations)[] generalAbb
            = new []
            {
                (typeof(LengthUnit), (int)LengthUnit.Centimetre, new string[]{"cm"}),
                (typeof(LengthUnit), (int)LengthUnit.Foot, new string[]{"ft", "'", "′"}),
                (typeof(LengthUnit), (int)LengthUnit.Inch, new string[]{"in", "\"", "″"}),
                (typeof(LengthUnit), (int)LengthUnit.Kilometre, new string[]{"km"}),
                (typeof(LengthUnit), (int)LengthUnit.Metre, new string[]{"m"}),
                (typeof(LengthUnit), (int)LengthUnit.Mile, new string[]{"mi"}),
                (typeof(LengthUnit), (int)LengthUnit.Millimetre, new string[]{"mm"}),
                (typeof(LengthUnit), (int)LengthUnit.NauticalMile, new string[]{"NM"}),
                (typeof(LengthUnit), (int)LengthUnit.Yard, new string[]{"yd"}),
                (typeof(MassUnit), (int)MassUnit.Gram, new string[]{"g"}),
                (typeof(MassUnit), (int)MassUnit.Kilogram, new string[]{"kg"}),
                (typeof(MassUnit), (int)MassUnit.LongTon, new string[]{"long tn"}),
                (typeof(MassUnit), (int)MassUnit.Milligram, new string[]{"mg"}),
                (typeof(MassUnit), (int)MassUnit.Ounce, new string[]{"oz"}),
                (typeof(MassUnit), (int)MassUnit.Pound, new string[]{"lb", "lbs", "lbm"}),
                (typeof(MassUnit), (int)MassUnit.ShortTon, new string[]{"t (short)", "short tn", "ST"}),
                (typeof(MassUnit), (int)MassUnit.Stone, new string[]{"st"}),
                (typeof(MassUnit), (int)MassUnit.Tonne, new string[]{"t"}),
                (typeof(SpeedUnit), (int)SpeedUnit.FootPerHour, new string[]{"ft/h"}),
                (typeof(SpeedUnit), (int)SpeedUnit.FootPerMinute, new string[]{"ft/min"}),
                (typeof(SpeedUnit), (int)SpeedUnit.FootPerSecond, new string[]{"ft/s"}),
                (typeof(SpeedUnit), (int)SpeedUnit.KilometrePerHour, new string[]{"km/h"}),
                (typeof(SpeedUnit), (int)SpeedUnit.KilometrePerMinute, new string[]{"km/min"}),
                (typeof(SpeedUnit), (int)SpeedUnit.KilometrePerSecond, new string[]{"km/s"}),
                (typeof(SpeedUnit), (int)SpeedUnit.Knot, new string[]{"kn", "kt", "knot", "knots"}),
                (typeof(SpeedUnit), (int)SpeedUnit.MetrePerHour, new string[]{"m/h"}),
                (typeof(SpeedUnit), (int)SpeedUnit.MetrePerMinute, new string[]{"m/min"}),
                (typeof(SpeedUnit), (int)SpeedUnit.MetrePerSecond, new string[]{"m/s"}),
                (typeof(SpeedUnit), (int)SpeedUnit.MilePerHour, new string[]{"mph"}),
            };

        internal static AbbreviationCache Default { get; }
        static AbbreviationCache()
        {
            Default = new AbbreviationCache();
        }

        internal string GetDefaultAbbreviation(LengthUnit unit)
        {
            throw new NotImplementedException();
        }
    }
}
