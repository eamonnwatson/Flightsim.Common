using EW.FlightSimulator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EW.FlightSimulator.Common.Units
{
    public enum MassUnit
    {
        Gram, Kilogram, LongTon, Milligram, Ounce, Pound, ShortTon, Stone, Tonne,
    }

    public struct Mass : IUnit<MassUnit>, IEquatable<Mass>, IComparable, IComparable<Mass>, IConvertible, IFormattable
    {
        #region Constructors
        public Mass(double value, MassUnit unit)
        {
            Value = GuardClauses.IsValidNumber(value, nameof(value));
            Unit = unit;
        }
        #endregion

        #region Static Properties
        public static MassUnit BaseUnit { get; } = MassUnit.Kilogram;
        public static Mass MaxValue { get; } = new Mass(double.MaxValue, BaseUnit);
        public static Mass MinValue { get; } = new Mass(double.MinValue, BaseUnit);
        public static MassUnit[] Units { get; } = Enum.GetValues(typeof(MassUnit))
                                                        .Cast<MassUnit>()
                                                        .ToArray();
        public static Mass Zero { get; } = new Mass(0, BaseUnit);
        #endregion

        #region Static Factory
        public static Mass FromGrams(UnitValue grams)
        {
            return From(grams, MassUnit.Gram);
        }
        public static Mass FromKilograms(UnitValue kilograms)
        {
            return From(kilograms, MassUnit.Kilogram);
        }
        public static Mass FromLongTons(UnitValue longtons)
        {
            return From(longtons, MassUnit.LongTon);
        }
        public static Mass FromMilligrams(UnitValue milligrams)
        {
            return From(milligrams, MassUnit.Milligram);
        }
        public static Mass FromOunces(UnitValue ounces)
        {
            return From(ounces, MassUnit.Ounce);
        }
        public static Mass FromPounds(UnitValue pounds)
        {
            return From(pounds, MassUnit.Pound);
        }
        public static Mass FromShortTons(UnitValue shorttons)
        {
            return From(shorttons, MassUnit.ShortTon);
        }
        public static Mass FromStone(UnitValue stone)
        {
            return From(stone, MassUnit.Stone);
        }
        public static Mass FromTonnes(UnitValue tonnes)
        {
            return From(tonnes, MassUnit.Tonne);
        }
        public static Mass From(UnitValue value, MassUnit fromUnit)
        {
            return new Mass((double)value, fromUnit);
        }
        #endregion

        #region Static Method
        public static string GetAbbreviation(MassUnit unit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public double Value { get; }
        public MassUnit Unit { get; }
        public double Grams => To(MassUnit.Gram);
        public double Kilograms => To(MassUnit.Kilogram);
        public double LongTons => To(MassUnit.LongTon);
        public double Milligrams => To(MassUnit.Milligram);
        public double Ounces => To(MassUnit.Ounce);
        public double Pounds => To(MassUnit.Pound);
        public double ShortTons => To(MassUnit.ShortTon);
        public double Stone => To(MassUnit.Stone);
        public double Tonnes => To(MassUnit.Tonne);
        #endregion

        #region Conversion
        public double To(MassUnit unit)
        {
            if (Unit == unit)
                return Convert.ToDouble(Value);

            var converted = ConvertTo(unit);
            return Convert.ToDouble(converted);
        }
        public Mass ToUnit(MassUnit unit)
        {
            var convertedValue = ConvertTo(unit);
            return new Mass(convertedValue, unit);
        }
        private double ConvertTo(MassUnit unit)
        {
            if (Unit == unit)
                return Value;

            var baseUnitValue = ConvertToBaseUnit();

            return unit switch
            {
                 MassUnit.Gram =>       baseUnitValue * 1000,
                 MassUnit.Kilogram =>   (baseUnitValue * 1000) / 1000,
                 MassUnit.LongTon =>    baseUnitValue / 1.0160469088e3,
                 MassUnit.Milligram =>  (baseUnitValue * 1000) / 0.001,
                 MassUnit.Ounce =>      baseUnitValue * 35.2739619,
                 MassUnit.Pound =>      baseUnitValue / 0.45359237,
                 MassUnit.ShortTon =>   baseUnitValue / 9.0718474e2,
                 MassUnit.Stone =>      baseUnitValue * 0.1574731728702698,
                 MassUnit.Tonne =>      baseUnitValue / 1000, 
                _ => throw new NotImplementedException($"Could not convert {Unit} to {unit}."),
            };
        }
        private double ConvertToBaseUnit()
        {
            return Unit switch
            {
                MassUnit.Gram =>        Value / 1000,
                MassUnit.Kilogram =>    (Value / 1000) * 1000,
                MassUnit.LongTon =>     Value * 1.0160469088e3,
                MassUnit.Milligram =>   (Value / 1000) * 0.001,
                MassUnit.Ounce =>       Value / 35.2739619,
                MassUnit.Pound =>       Value * 0.45359237,
                MassUnit.ShortTon =>    Value * 9.0718474e2,
                MassUnit.Stone =>       Value / 0.1574731728702698,
                MassUnit.Tonne =>       Value * 1000,
                _ => throw new NotImplementedException($"Could not convert {Unit} to kilograms."),
            };
        }
        #endregion

        #region Operators
        public static Mass operator -(Mass right)
        {
            return new Mass(-right.Value, right.Unit);
        }
        public static Mass operator +(Mass left, Mass right)
        {
            return new Mass(left.Value + right.ConvertTo(left.Unit), left.Unit);
        }
        public static Mass operator -(Mass left, Mass right)
        {
            return new Mass(left.Value - right.ConvertTo(left.Unit), left.Unit);
        }
        public static Mass operator *(double left, Mass right)
        {
            return new Mass(left * right.Value, right.Unit);
        }
        public static Mass operator *(Mass left, double right)
        {
            return new Mass(left.Value * right, left.Unit);
        }
        public static Mass operator /(Mass left, double right)
        {
            return new Mass(left.Value / right, left.Unit);
        }
        public static double operator /(Mass left, Mass right)
        {
            return left.Kilograms / right.Kilograms;
        }
        public static bool operator <=(Mass left, Mass right)
        {
            return left.Value <= right.ConvertTo(left.Unit);
        }
        public static bool operator >=(Mass left, Mass right)
        {
            return left.Value >= right.ConvertTo(left.Unit);
        }
        public static bool operator <(Mass left, Mass right)
        {
            return left.Value < right.ConvertTo(left.Unit);
        }
        public static bool operator >(Mass left, Mass right)
        {
            return left.Value > right.ConvertTo(left.Unit);
        }
        public static bool operator ==(Mass left, Mass right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Mass left, Mass right)
        {
            return !(left == right);
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Mass objMass))
                return false;

            return Equals(objMass);
        }
        public bool Equals(Mass other)
        {
            return Value.Equals(other.ConvertTo(Unit));
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            if (!(obj is Mass objMass)) throw new ArgumentException("Object is not a Mass", nameof(obj));

            return CompareTo(objMass);
        }

        public int CompareTo(Mass other)
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
            throw new InvalidCastException($"Converting {typeof(Mass)} to bool is not supported.");
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Mass)} to char is not supported.");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Mass)} to DateTime is not supported.");
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
            if (conversionType == typeof(Mass))
                return this;
            else if (conversionType == typeof(MassUnit))
                return Unit;
            else
                throw new InvalidCastException($"Converting {typeof(Mass)} to {conversionType} is not supported.");
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
            return UnitFormatter.Format<MassUnit>(this, format);
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
