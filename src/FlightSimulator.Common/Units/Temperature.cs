using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EW.FlightSimulator.Common.Extensions;

namespace EW.FlightSimulator.Common.Units
{
    public enum TemperatureUnit
    {
        Celsius, Fahrenheit, Kelvin,
    }
    public struct Temperature : IUnit<TemperatureUnit>, IEquatable<Temperature>, IComparable, IComparable<Temperature>, IConvertible, IFormattable
    {
        #region Constructors
        public Temperature(double value, TemperatureUnit unit)
        {
            Value = GuardClauses.IsValidNumber(value, nameof(value));
            Unit = unit;
        }
        #endregion

        #region Static Properties
        public static TemperatureUnit BaseUnit { get; } = TemperatureUnit.Kelvin;
        public static Temperature MaxValue { get; } = new Temperature(double.MaxValue, BaseUnit);
        public static Temperature MinValue { get; } = new Temperature(double.MinValue, BaseUnit);
        public static TemperatureUnit[] Units { get; } = Enum.GetValues(typeof(TemperatureUnit))
                                                        .Cast<TemperatureUnit>()
                                                        .ToArray();
        public static Temperature Zero { get; } = new Temperature(0, BaseUnit);
        #endregion

        #region Static Factory
        public static Temperature FromCelsius(UnitValue celsius)
        {
            return From(celsius, TemperatureUnit.Celsius);
        }
        public static Temperature FromFahrenheit(UnitValue fahrenheit)
        {
            return From(fahrenheit, TemperatureUnit.Fahrenheit);
        }
        public static Temperature FromKelvin(UnitValue kelvin)
        {
            return From(kelvin, TemperatureUnit.Kelvin);
        }
        public static Temperature From(UnitValue value, TemperatureUnit fromUnit)
        {
            return new Temperature((double)value, fromUnit);
        }
        #endregion

        #region Static Method
        public static string GetAbbreviation(TemperatureUnit unit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public double Value { get; }
        public TemperatureUnit Unit { get; }
        public double Celsius => To(TemperatureUnit.Celsius);
        public double Fahrenheit => To(TemperatureUnit.Fahrenheit);
        public double Kelvin => To(TemperatureUnit.Kelvin);
        #endregion

        #region Conversion
        public double To(TemperatureUnit unit)
        {
            if (Unit == unit)
                return Convert.ToDouble(Value);

            var converted = ConvertTo(unit);
            return Convert.ToDouble(converted);
        }
        public Temperature ToUnit(TemperatureUnit unit)
        {
            var convertedValue = ConvertTo(unit);
            return new Temperature(convertedValue, unit);
        }
        private double ConvertTo(TemperatureUnit unit)
        {
            if (Unit == unit)
                return Value;

            var baseUnitValue = ConvertToBaseUnit();

            return unit switch
            {
                TemperatureUnit.Celsius => baseUnitValue - 273.15,
                TemperatureUnit.Fahrenheit => (baseUnitValue - 459.67 * 5 / 9) * 9 / 5,
                TemperatureUnit.Kelvin => baseUnitValue,
                _ => throw new NotImplementedException($"Could not convert {Unit} to {unit}."),
            };
        }
        private double ConvertToBaseUnit()
        {
            return Unit switch
            {
                TemperatureUnit.Celsius => Value + 273.15,
                TemperatureUnit.Fahrenheit => Value * 5 / 9 + 459.67 * 5 / 9,
                TemperatureUnit.Kelvin => Value,
                _ => throw new NotImplementedException($"Could not convert {Unit} to kelvin."),
            };
        }
        #endregion

        #region Operators
        public static Temperature operator -(Temperature right)
        {
            return new Temperature(-right.Value, right.Unit);
        }
        public static Temperature operator +(Temperature left, Temperature right)
        {
            return new Temperature(left.Value + right.ConvertTo(left.Unit), left.Unit);
        }
        public static Temperature operator -(Temperature left, Temperature right)
        {
            return new Temperature(left.Value - right.ConvertTo(left.Unit), left.Unit);
        }
        public static Temperature operator *(double left, Temperature right)
        {
            return new Temperature(left * right.Value, right.Unit);
        }
        public static Temperature operator *(Temperature left, double right)
        {
            return new Temperature(left.Value * right, left.Unit);
        }
        public static Temperature operator /(Temperature left, double right)
        {
            return new Temperature(left.Value / right, left.Unit);
        }
        public static double operator /(Temperature left, Temperature right)
        {
            return left.Kelvin / right.Kelvin;
        }
        public static bool operator <=(Temperature left, Temperature right)
        {
            return left.Value <= right.ConvertTo(left.Unit);
        }
        public static bool operator >=(Temperature left, Temperature right)
        {
            return left.Value >= right.ConvertTo(left.Unit);
        }
        public static bool operator <(Temperature left, Temperature right)
        {
            return left.Value < right.ConvertTo(left.Unit);
        }
        public static bool operator >(Temperature left, Temperature right)
        {
            return left.Value > right.ConvertTo(left.Unit);
        }
        public static bool operator ==(Temperature left, Temperature right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Temperature left, Temperature right)
        {
            return !(left == right);
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Temperature objTemperature))
                return false;

            return Equals(objTemperature);
        }
        public bool Equals(Temperature other)
        {
            return Value.Equals(other.ConvertTo(Unit));
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            if (!(obj is Temperature objTemperature)) throw new ArgumentException("Object is not a Temperature", nameof(obj));

            return CompareTo(objTemperature);
        }

        public int CompareTo(Temperature other)
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
            throw new InvalidCastException($"Converting {typeof(Temperature)} to bool is not supported.");
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Temperature)} to char is not supported.");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Temperature)} to DateTime is not supported.");
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
            if (conversionType == typeof(Temperature))
                return this;
            else if (conversionType == typeof(TemperatureUnit))
                return Unit;
            else
                throw new InvalidCastException($"Converting {typeof(Temperature)} to {conversionType} is not supported.");
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
            return UnitFormatter.Format<TemperatureUnit>(this, format);
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
