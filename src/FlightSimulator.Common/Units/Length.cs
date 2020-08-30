using EW.FlightSimulator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EW.FlightSimulator.Common.Units
{
    public enum LengthUnit
    {
        Millimetre, Centimetre, Metre, Kilometre, Inch, Foot, Yard, Mile, NauticalMile,
    }

    public struct Length : IUnit<LengthUnit>, IEquatable<Length>, IComparable, IComparable<Length>, IConvertible, IFormattable
    {
        #region Constructors
        public Length(double value, LengthUnit unit)
        {
            Value = GuardClauses.IsValidNumber(value, nameof(value));
            Unit = unit;
        }
        #endregion

        #region Static Properties
        public static LengthUnit BaseUnit { get; } = LengthUnit.Metre;
        public static Length MaxValue { get; } = new Length(double.MaxValue, BaseUnit);
        public static Length MinValue { get; } = new Length(double.MinValue, BaseUnit);
        public static LengthUnit[] Units { get; } = Enum.GetValues(typeof(LengthUnit))
                                                        .Cast<LengthUnit>()
                                                        .ToArray();
        public static Length Zero { get; } = new Length(0, BaseUnit);
        #endregion

        #region Static Factory
        public static Length FromCentimetres(UnitValue centimetres)
        {
            return From(centimetres, LengthUnit.Centimetre);
        }
        public static Length FromFeet(UnitValue feet)
        {
            return From(feet, LengthUnit.Foot);
        }
        public static Length FromInches(UnitValue inches)
        {
            return From(inches, LengthUnit.Inch);
        }
        public static Length FromKilometers(UnitValue kilometres)
        {
            return From(kilometres, LengthUnit.Kilometre);
        }
        public static Length FromMeters(UnitValue metres)
        {
            return From(metres, LengthUnit.Metre);
        }
        public static Length FromMiles(UnitValue miles)
        {
            return From(miles, LengthUnit.Mile);
        }
        public static Length FromMillimetres(UnitValue millimetres)
        {
            return From(millimetres, LengthUnit.Millimetre);
        }
        public static Length FromNauticalMiles(UnitValue nauticalmiles)
        {
            return From(nauticalmiles, LengthUnit.NauticalMile);
        }
        public static Length FromYards(UnitValue yards)
        {
            return From(yards, LengthUnit.Yard);
        }
        public static Length From(UnitValue value, LengthUnit fromUnit)
        {
            return new Length((double)value, fromUnit);
        }
        #endregion

        #region Static Method
        public static string GetAbbreviation(LengthUnit unit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public double Value { get; }
        public LengthUnit Unit { get; }
        public double Centimetres => To(LengthUnit.Centimetre);
        public double Feet => To(LengthUnit.Foot);
        public double Inches => To(LengthUnit.Inch);
        public double Kilometres => To(LengthUnit.Kilometre);
        public double Miles => To(LengthUnit.Mile);
        public double Millimetres => To(LengthUnit.Millimetre);
        public double Metres => To(LengthUnit.Metre);
        public double NauticalMiles => To(LengthUnit.NauticalMile);
        public double Yards => To(LengthUnit.Yard);
        #endregion

        #region Conversion
        public double To(LengthUnit unit)
        {
            if (Unit == unit)
                return Convert.ToDouble(Value);

            var converted = ConvertTo(unit);
            return Convert.ToDouble(converted);
        }
        public Length ToUnit(LengthUnit unit)
        {
            var convertedValue = ConvertTo(unit);
            return new Length(convertedValue, unit);
        }
        private double ConvertTo(LengthUnit unit)
        {
            if (Unit == unit)
                return Value;

            var baseUnitValue = ConvertToBaseUnit();

            return unit switch
            {
                LengthUnit.Millimetre   => baseUnitValue / 0.001,
                LengthUnit.Centimetre   => baseUnitValue / 0.01,
                LengthUnit.Inch         => baseUnitValue / 0.0254,
                LengthUnit.Foot         => baseUnitValue / 0.3048,
                LengthUnit.Yard         => baseUnitValue / 0.9144,
                LengthUnit.Metre        => baseUnitValue,
                LengthUnit.Kilometre    => baseUnitValue / 1000,
                LengthUnit.Mile         => baseUnitValue / 1609.34,
                LengthUnit.NauticalMile => baseUnitValue / 1852,
                _ => throw new NotImplementedException($"Could not convert {Unit} to {unit}."),
            };
        }
        private double ConvertToBaseUnit()
        {
            return Unit switch
            {
                LengthUnit.Millimetre   => Value * 0.001,
                LengthUnit.Centimetre   => Value * 0.01,
                LengthUnit.Inch         => Value * 0.0254,
                LengthUnit.Foot         => Value * 0.3048,
                LengthUnit.Yard         => Value * 0.9144,
                LengthUnit.Metre        => Value,
                LengthUnit.Kilometre    => Value * 1000,
                LengthUnit.Mile         => Value * 1609.34,
                LengthUnit.NauticalMile => Value * 1852,
                _ => throw new NotImplementedException($"Could not convert {Unit} to metres."),
            };
        }
        #endregion

        #region Operators
        public static Length operator -(Length right)
        {
            return new Length(-right.Value, right.Unit);
        }
        public static Length operator +(Length left, Length right)
        {
            return new Length(left.Value + right.ConvertTo(left.Unit), left.Unit);
        }
        public static Length operator -(Length left, Length right)
        {
            return new Length(left.Value - right.ConvertTo(left.Unit), left.Unit);
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
            return left.Value <= right.ConvertTo(left.Unit);
        }
        public static bool operator >=(Length left, Length right)
        {
            return left.Value >= right.ConvertTo(left.Unit);
        }
        public static bool operator <(Length left, Length right)
        {
            return left.Value < right.ConvertTo(left.Unit);
        }
        public static bool operator >(Length left, Length right)
        {
            return left.Value > right.ConvertTo(left.Unit);
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
            return Value.Equals(other.ConvertTo(Unit));
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
            return Value.CompareTo(other.ConvertTo(Unit));
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
            return ToString("G");
        }

        public string ToString(string format)
        {
            return ToString(format, new CultureInfo("en-US"));
        }
    }
}
