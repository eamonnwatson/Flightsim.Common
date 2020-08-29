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
        Undefined,
        Gram,
        Kilogram,
        LongTon,
        Milligram,
        Ounce,
        Pound,
        ShortTon,
        Stone,
        Tonne,
    }
    public struct Mass : IEquatable<Mass>, IComparable, IComparable<Mass>, IConvertible, IFormattable
    {
        #region Constructors
        public Mass(double value, MassUnit unit)
        {
            if (unit == MassUnit.Undefined)
                throw new ArgumentException("Invalid Unit has been specified", nameof(unit));

            Value = Guard.EnsureValidNumber(value, nameof(value));
            Unit = unit;
        }
        #endregion

        #region Static Properties
        public static MassUnit BaseUnit { get; } = MassUnit.Kilogram;
        public static Mass MaxValue { get; } = new Mass(double.MaxValue, BaseUnit);
        public static Mass MinValue { get; } = new Mass(double.MinValue, BaseUnit);
        public static MassUnit[] Units { get; } = Enum.GetValues(typeof(MassUnit))
                                                        .Cast<MassUnit>()
                                                        .Except(new MassUnit[] { MassUnit.Undefined })
                                                        .ToArray();
        public static Mass Zero { get; } = new Mass(0, BaseUnit);
        #endregion

        #region Static Factory
        public static Mass FromGrams(QuantityValue grams)
        {
            double value = (double)grams;
            return new Mass(value, MassUnit.Gram);
        }
        public static Mass FromKilograms(QuantityValue kilograms)
        {
            double value = (double)kilograms;
            return new Mass(value, MassUnit.Kilogram);
        }
        public static Mass FromLongTons(QuantityValue longtons)
        {
            double value = (double)longtons;
            return new Mass(value, MassUnit.LongTon);
        }
        public static Mass FromMilligrams(QuantityValue milligrams)
        {
            double value = (double)milligrams;
            return new Mass(value, MassUnit.Milligram);
        }
        public static Mass FromOunces(QuantityValue ounces)
        {
            double value = (double)ounces;
            return new Mass(value, MassUnit.Ounce);
        }
        public static Mass FromPounds(QuantityValue pounds)
        {
            double value = (double)pounds;
            return new Mass(value, MassUnit.Pound);
        }
        public static Mass FromShortTons(QuantityValue shorttons)
        {
            double value = (double)shorttons;
            return new Mass(value, MassUnit.ShortTon);
        }
        public static Mass FromStone(QuantityValue stone)
        {
            double value = (double)stone;
            return new Mass(value, MassUnit.Stone);
        }
        public static Mass FromTonnes(QuantityValue tonnes)
        {
            double value = (double)tonnes;
            return new Mass(value, MassUnit.Tonne);
        }
        public static Mass From(QuantityValue value, MassUnit fromUnit)
        {
            return new Mass((double)value, fromUnit);
        }
        #endregion

        #region Properties
        public double Value { get; }
        public MassUnit Unit { get; }
        public double Grams => As(MassUnit.Gram);
        public double Kilograms => As(MassUnit.Kilogram);
        public double LongTons => As(MassUnit.LongTon);
        public double Milligrams => As(MassUnit.Milligram);
        public double Ounces => As(MassUnit.Ounce);
        public double Pounds => As(MassUnit.Pound);
        public double ShortTons => As(MassUnit.ShortTon);
        public double Stone => As(MassUnit.Stone);
        public double Tonnes => As(MassUnit.Tonne);
        #endregion

        #region Conversion
        public double As(MassUnit unit)
        {
            if (Unit == unit)
                return Convert.ToDouble(Value);

            var converted = GetValueAs(unit);
            return Convert.ToDouble(converted);
        }
        public Mass ToUnit(MassUnit unit)
        {
            var convertedValue = GetValueAs(unit);
            return new Mass(convertedValue, unit);
        }
        private double GetValueAs(MassUnit unit)
        {
            if (Unit == unit)
                return Value;

            var baseUnitValue = GetValueInBaseUnit();

            switch (unit)
            {
                case MassUnit.Gram: return baseUnitValue * 1e3;
                case MassUnit.Kilogram: return (baseUnitValue * 1e3) / 1e3d;
                case MassUnit.LongTon: return baseUnitValue / 1.0160469088e3;
                case MassUnit.Milligram: return (baseUnitValue * 1e3) / 1e-3d;
                case MassUnit.Ounce: return baseUnitValue * 35.2739619;
                case MassUnit.Pound: return baseUnitValue / 0.45359237;
                case MassUnit.ShortTon: return baseUnitValue / 9.0718474e2;
                case MassUnit.Stone: return baseUnitValue * 0.1574731728702698;
                case MassUnit.Tonne: return baseUnitValue / 1e3;
                default:
                    throw new NotImplementedException($"Could not convert {Unit} to {unit}.");
            }
        }
        private double GetValueInBaseUnit()
        {
            switch (Unit)
            {
                case MassUnit.Gram: return Value / 1e3;
                case MassUnit.Kilogram: return (Value / 1e3) * 1e3d;
                case MassUnit.LongTon: return Value * 1.0160469088e3;
                case MassUnit.Milligram: return (Value / 1e3) * 1e-3d;
                case MassUnit.Ounce: return Value / 35.2739619;
                case MassUnit.Pound: return Value * 0.45359237;
                case MassUnit.ShortTon: return Value * 9.0718474e2;
                case MassUnit.Stone: return Value / 0.1574731728702698;
                case MassUnit.Tonne: return Value * 1e3;
                default:
                    throw new NotImplementedException($"Could not convert {Unit} to metres.");
            }
        }
        #endregion

        #region Operators
        public static Mass operator -(Mass right)
        {
            return new Mass(-right.Value, right.Unit);
        }
        public static Mass operator +(Mass left, Mass right)
        {
            return new Mass(left.Value + right.GetValueAs(left.Unit), left.Unit);
        }
        public static Mass operator -(Mass left, Mass right)
        {
            return new Mass(left.Value - right.GetValueAs(left.Unit), left.Unit);
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
            return left.Value <= right.GetValueAs(left.Unit);
        }
        public static bool operator >=(Mass left, Mass right)
        {
            return left.Value >= right.GetValueAs(left.Unit);
        }
        public static bool operator <(Mass left, Mass right)
        {
            return left.Value < right.GetValueAs(left.Unit);
        }
        public static bool operator >(Mass left, Mass right)
        {
            return left.Value > right.GetValueAs(left.Unit);
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
            return Value.Equals(other.GetValueAs(Unit));
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
            return ToString(format, new CultureInfo("en-US"));
        }
    }
}
