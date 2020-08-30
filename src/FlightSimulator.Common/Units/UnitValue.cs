using EW.FlightSimulator.Common.Extensions;

namespace EW.FlightSimulator.Common.Units
{
    public struct UnitValue
    {
        private readonly double? value;
        private readonly decimal? valueDecimal;

        private UnitValue(double val)
        {
            value = GuardClauses.IsValidNumber(val, nameof(val));
            valueDecimal = null;
        }
        private UnitValue(decimal val)
        {
            valueDecimal = val;
            value = null;
        }
        public static implicit operator UnitValue(byte val) => new UnitValue((double)val);
        public static implicit operator UnitValue(short val) => new UnitValue((double)val);
        public static implicit operator UnitValue(int val) => new UnitValue((double)val);
        public static implicit operator UnitValue(long val) => new UnitValue((double)val);
        public static implicit operator UnitValue(float val) => new UnitValue(val); 
        public static implicit operator UnitValue(double val) => new UnitValue(val);
        public static implicit operator UnitValue(decimal val) => new UnitValue(val);
        public static explicit operator double(UnitValue number)
        {
            return number.value ?? (double)number.valueDecimal.GetValueOrDefault();
        }
        public static explicit operator decimal(UnitValue number)
        {
            return number.valueDecimal ?? (decimal)number.value.GetValueOrDefault();
        }
        public override string ToString()
        {
            return value.HasValue ? value.ToString() : valueDecimal.ToString();
        }
    }
}