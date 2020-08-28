using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EW.FlightSimulator.Common.Units
{
    public enum LengthUnit
    {
        Undefined,
        Metre,
        Kilometre,
        Centimetre,
        Millimetre,
        Inch,
        Foot,
        Yard,
        Mile,
        NauticalMile,
    }

    public struct Length : IEquatable<Length>, IComparable, IComparable<Length>, IConvertible, IFormattable
    {
        #region constructors
        public Length(double value, LengthUnit unit)
        {
            if (unit == LengthUnit.Undefined)
                throw new ArgumentException("Invalid Unit has been specified", nameof(unit));

            Value = value;
            Unit = unit;
        }
        #endregion

        #region Static Properties
        public static LengthUnit BaseUnit { get; } = LengthUnit.Metre;
        public static Length MaxValue { get; } = new Length(double.MaxValue, BaseUnit);
        public static Length MinValue { get; } = new Length(double.MinValue, BaseUnit);
        public static LengthUnit[] Units { get; } = Enum.GetValues(typeof(LengthUnit)).Cast<LengthUnit>().Except(new LengthUnit[] { LengthUnit.Undefined }).ToArray();
        public static Length Zero { get; } = new Length(0, BaseUnit);
        #endregion

        #region Static Factory
        public static Length FromCentimetres(QuantityValue centimetres)
        {
            return From(centimetres, LengthUnit.Centimetre);
        }
        public static Length FromFeet(QuantityValue feet)
        {
            return From(feet, LengthUnit.Foot);
        }
        public static Length FromInches(QuantityValue inches)
        {
            return From(inches, LengthUnit.Inch);
        }
        public static Length FromKilometers(QuantityValue kilometres)
        {
            return From(kilometres, LengthUnit.Kilometre);
        }
        public static Length FromMeters(QuantityValue metres)
        {
            return From(metres, LengthUnit.Metre);
        }
        public static Length FromMiles(QuantityValue miles)
        {
            return From(miles, LengthUnit.Mile);
        }
        public static Length FromMillimetres(QuantityValue millimetres)
        {
            return From(millimetres, LengthUnit.Millimetre);
        }
        public static Length FromNauticalMiles(QuantityValue nauticalmiles)
        {
            return From(nauticalmiles, LengthUnit.NauticalMile);
        }
        public static Length FromYards(QuantityValue yards)
        {
            return From(yards, LengthUnit.Yard);
        }
        public static Length From(QuantityValue value, LengthUnit fromUnit)
        {
            return new Length((double)value, fromUnit);
        }
        #endregion

        #region Properties
        public double Value { get; }
        public LengthUnit Unit { get; }
        public double Centimetres => As(LengthUnit.Centimetre);
        public double Feet => As(LengthUnit.Foot);
        public double Inches => As(LengthUnit.Inch);
        public double Kilometres => As(LengthUnit.Kilometre);
        public double Miles => As(LengthUnit.Mile);
        public double Millimetres => As(LengthUnit.Millimetre);
        public double Metres => As(LengthUnit.Metre);
        public double NauticalMiles => As(LengthUnit.NauticalMile);
        public double Yards => As(LengthUnit.Yard);
        #endregion

        #region Conversion
        public double As(LengthUnit unit)
        {
            if (Unit == unit)
                return Convert.ToDouble(Value);

            var converted = GetValueAs(unit);
            return Convert.ToDouble(converted);
        }
        public Length ToUnit(LengthUnit unit)
        {
            var convertedValue = GetValueAs(unit);
            return new Length(convertedValue, unit);
        }
        private double GetValueAs(LengthUnit unit)
        {
            if (Unit == unit)
                return Value;

            var baseUnitValue = GetValueInBaseUnit();

            switch (unit)
            {
                case LengthUnit.Foot: return baseUnitValue / 0.3048;
                case LengthUnit.Inch: return baseUnitValue / 2.54e-2;
                case LengthUnit.Kilometre: return (baseUnitValue) / 1e3d;
                case LengthUnit.Metre: return baseUnitValue;
                case LengthUnit.Mile: return baseUnitValue / 1609.34;
                case LengthUnit.Millimetre: return (baseUnitValue) / 1e-3d;
                case LengthUnit.NauticalMile: return baseUnitValue / 1852;
                case LengthUnit.Yard: return baseUnitValue / 0.9144;
                default:
                    throw new NotImplementedException($"Could not convert {Unit} to {unit}.");
            }
        }
        private double GetValueInBaseUnit()
        {
            switch (Unit)
            {
                case LengthUnit.Foot: return Value * 0.3048;
                case LengthUnit.Inch: return Value * 2.54e-2;
                case LengthUnit.Kilometre: return (Value) * 1e3d;
                case LengthUnit.Metre: return Value;
                case LengthUnit.Mile: return Value * 1609.34;
                case LengthUnit.Millimetre: return (Value) * 1e-3d;
                case LengthUnit.NauticalMile: return Value * 1852;
                case LengthUnit.Yard: return Value * 0.9144;
                default:
                    throw new NotImplementedException($"Could not convert {Unit} to base unit.");
            }
        }
        #endregion

        #region Operators
        public static Length operator -(Length right)
        {
            return new Length(-right.Value, right.Unit);
        }
        public static Length operator +(Length left, Length right)
        {
            return new Length(left.Value + right.GetValueAs(left.Unit), left.Unit);
        }
        public static Length operator -(Length left, Length right)
        {
            return new Length(left.Value - right.GetValueAs(left.Unit), left.Unit);
        }
        public static Length operator *(double left, Length right)
        {
            return new Length(left * right.Value, right.Unit);
        }
        public static Length operator *(Length left, double right)
        {
            return new Length(left.Value * right, left.Unit);
        }
        public static Length operator /(Length left, double right)
        {
            return new Length(left.Value / right, left.Unit);
        }
        public static double operator /(Length left, Length right)
        {
            return left.Metres / right.Metres;
        }
        public static bool operator <=(Length left, Length right)
        {
            return left.Value <= right.GetValueAs(left.Unit);
        }
        public static bool operator >=(Length left, Length right)
        {
            return left.Value >= right.GetValueAs(left.Unit);
        }
        public static bool operator <(Length left, Length right)
        {
            return left.Value < right.GetValueAs(left.Unit);
        }
        public static bool operator >(Length left, Length right)
        {
            return left.Value > right.GetValueAs(left.Unit);
        }
        public static bool operator ==(Length left, Length right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Length left, Length right)
        {
            return !(left == right);
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Length objLength))
                return false;

            return Equals(objLength);
        }
        public bool Equals(Length other)
        {
            return Value.Equals(other.GetValueAs(Unit));
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            if (!(obj is Length objLength)) throw new ArgumentException("Object is not a Length", nameof(obj));

            return CompareTo(objLength);
        }

        public int CompareTo(Length other)
        {
            return Value.CompareTo(other.GetValueAs(Unit));
        }
        #endregion

        #region IConvertible
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Length)} to bool is not supported.");
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Length)} to char is not supported.");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Length)} to DateTime is not supported.");
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString("g", provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(Length))
                return this;
            else if (conversionType == typeof(LengthUnit))
                return Unit;
            else
                throw new InvalidCastException($"Converting {typeof(Length)} to {conversionType} is not supported.");
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }
        #endregion

        #region IFormattable
        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override int GetHashCode()
        {
            return new { Value, Unit }.GetHashCode();
        }

        public override string ToString()
        {
            return ToString("g");
        }
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentUICulture);
        }
    }
}
