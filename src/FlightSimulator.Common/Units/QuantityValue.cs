using EW.FlightSimulator.Common.Extensions;

namespace EW.FlightSimulator.Common.Units
{
    public struct QuantityValue
    {
        private readonly double? value;
        private readonly decimal? valueDecimal;

        private QuantityValue(double val)
        {
            value = Guard.EnsureValidNumber(val, nameof(val));
            valueDecimal = null;
        }
        private QuantityValue(decimal val)
        {
            valueDecimal = val;
            value = null;
        }
        public static implicit operator QuantityValue(byte val) => new QuantityValue((double)val);
        public static implicit operator QuantityValue(short val) => new QuantityValue((double)val);
        public static implicit operator QuantityValue(int val) => new QuantityValue((double)val);
        public static implicit operator QuantityValue(long val) => new QuantityValue((double)val);
        public static implicit operator QuantityValue(float val) => new QuantityValue(val); 
        public static implicit operator QuantityValue(double val) => new QuantityValue(val);
        public static implicit operator QuantityValue(decimal val) => new QuantityValue(val);
        public static explicit operator double(QuantityValue number)
        {
            return number.value ?? (double)number.valueDecimal.GetValueOrDefault();
        }
        public static explicit operator decimal(QuantityValue number)
        {
            return number.valueDecimal ?? (decimal)number.value.GetValueOrDefault();
        }
        public override string ToString()
        {
            return value.HasValue ? value.ToString() : valueDecimal.ToString();
        }
    }
}