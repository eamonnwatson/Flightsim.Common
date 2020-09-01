using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EW.FlightSimulator.Common.Extensions;

namespace EW.FlightSimulator.Common.Units
{
    public enum SpeedUnit
    {
        FootPerHour, FootPerMinute, FootPerSecond, KilometrePerHour, KilometrePerMinute, KilometrePerSecond, Knot, MetrePerHour, MetrePerMinute, MetrePerSecond, MilePerHour,
    }

    public struct Speed : IUnit<SpeedUnit>, IEquatable<Speed>, IComparable, IComparable<Speed>, IConvertible, IFormattable
    {
        #region Constructors
        public Speed(double value, SpeedUnit unit)
        {
            Value = GuardClauses.IsValidNumber(value, nameof(value));
            Unit = unit;
        }
        #endregion

        #region Static Properties
        public static SpeedUnit BaseUnit { get; } = SpeedUnit.MetrePerSecond;
        public static Speed MaxValue { get; } = new Speed(double.MaxValue, BaseUnit);
        public static Speed MinValue { get; } = new Speed(double.MinValue, BaseUnit);
        public static SpeedUnit[] Units { get; } = Enum.GetValues(typeof(SpeedUnit))
                                                        .Cast<SpeedUnit>()
                                                        .ToArray();
        public static Speed Zero { get; } = new Speed(0, BaseUnit);
        #endregion

        #region Static Factory
        public static Speed FromFeetPerHour(UnitValue feetperhour)
        {
            return From(feetperhour, SpeedUnit.FootPerHour);
        }
        public static Speed FromFeetPerMinute(UnitValue feetperminute)
        {
            return From(feetperminute, SpeedUnit.FootPerMinute);
        }
        public static Speed FromFeetPerSecond(UnitValue feetpersecond)
        {
            return From(feetpersecond, SpeedUnit.FootPerSecond);
        }
        public static Speed FromKilometresPerHour(UnitValue kilometresperhour)
        {
            return From(kilometresperhour, SpeedUnit.KilometrePerHour);
        }
        public static Speed FromKilometresPerMinutes(UnitValue kilometresperminutes)
        {
            return From(kilometresperminutes, SpeedUnit.KilometrePerMinute);
        }
        public static Speed FromKilometresPerSecond(UnitValue kilometrespersecond)
        {
            return From(kilometrespersecond, SpeedUnit.KilometrePerSecond);
        }
        public static Speed FromKnots(UnitValue knots)
        {
            return From(knots, SpeedUnit.Knot);
        }
        public static Speed FromMetresPerHour(UnitValue metresperhour)
        {
            return From(metresperhour, SpeedUnit.MetrePerHour);
        }
        public static Speed FromMetresPerMinutes(UnitValue metresperminutes)
        {
            return From(metresperminutes, SpeedUnit.MetrePerMinute);
        }
        public static Speed FromMetresPerSecond(UnitValue metrespersecond)
        {
            return From(metrespersecond, SpeedUnit.MetrePerSecond);
        }
        public static Speed FromMilesPerHour(UnitValue milesperhour)
        {
            return From(milesperhour, SpeedUnit.MilePerHour);
        }
        public static Speed From(UnitValue value, SpeedUnit fromUnit)
        {
            return new Speed((double)value, fromUnit);
        }
        #endregion

        #region Static Method
        public static string GetAbbreviation(SpeedUnit unit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public double Value { get; }
        public SpeedUnit Unit { get; }
        public double FeetPerHour => To(SpeedUnit.FootPerHour);
        public double FeetPerMinute => To(SpeedUnit.FootPerMinute);
        public double FeetPerSecond => To(SpeedUnit.FootPerSecond);
        public double KilometresPerHour => To(SpeedUnit.KilometrePerHour);
        public double KilometresPerMinutes => To(SpeedUnit.KilometrePerMinute);
        public double KilometresPerSecond => To(SpeedUnit.KilometrePerSecond);
        public double Knots => To(SpeedUnit.Knot);
        public double MetresPerHour => To(SpeedUnit.MetrePerHour);
        public double MetresPerMinutes => To(SpeedUnit.MetrePerMinute);
        public double MetresPerSecond => To(SpeedUnit.MetrePerSecond);
        public double MilesPerHour => To(SpeedUnit.MilePerHour);

        #endregion

        #region Conversion
        public double To(SpeedUnit unit)
        {
            if (Unit == unit)
                return Convert.ToDouble(Value);

            var converted = ConvertTo(unit);
            return Convert.ToDouble(converted);
        }
        public Speed ToUnit(SpeedUnit unit)
        {
            var convertedValue = ConvertTo(unit);
            return new Speed(convertedValue, unit);
        }
        private double ConvertTo(SpeedUnit unit)
        {
            if (Unit == unit)
                return Value;

            var baseUnitValue = ConvertToBaseUnit();

            return unit switch
            {
                SpeedUnit.FootPerHour           => baseUnitValue / 0.3048 * 3600,
                SpeedUnit.FootPerMinute         => baseUnitValue / 0.3048 * 60,
                SpeedUnit.FootPerSecond         => baseUnitValue / 0.3048,
                SpeedUnit.KilometrePerHour      => (baseUnitValue * 3600) / 1000,
                SpeedUnit.KilometrePerMinute    => (baseUnitValue * 60) / 1000,
                SpeedUnit.KilometrePerSecond    => (baseUnitValue) / 1000,
                SpeedUnit.Knot                  => baseUnitValue / 0.514444,
                SpeedUnit.MetrePerHour          => baseUnitValue * 3600,
                SpeedUnit.MetrePerMinute        => baseUnitValue * 60,
                SpeedUnit.MetrePerSecond        => baseUnitValue,
                SpeedUnit.MilePerHour           => baseUnitValue / 0.44704,
                _ => throw new NotImplementedException($"Can not convert {Unit} to {unit}."),
            };
        }
        private double ConvertToBaseUnit()
        {
            return Unit switch
            {
                SpeedUnit.FootPerHour           => Value * 0.3048 / 3600,
                SpeedUnit.FootPerMinute         => Value * 0.3048 / 60,
                SpeedUnit.FootPerSecond         => Value * 0.3048,
                SpeedUnit.KilometrePerHour      => (Value / 3600) * 1000,
                SpeedUnit.KilometrePerMinute    => (Value / 60) * 1000,
                SpeedUnit.KilometrePerSecond    => (Value) * 1000,
                SpeedUnit.Knot                  => Value * 0.514444,
                SpeedUnit.MetrePerHour          => Value / 3600,
                SpeedUnit.MetrePerMinute        => Value / 60,
                SpeedUnit.MetrePerSecond        => Value,
                _ => throw new NotImplementedException($"Can not convert {Unit} to metres per second."),
            };
        }
        #endregion

        #region Operators
        public static Speed operator -(Speed right)
        {
            return new Speed(-right.Value, right.Unit);
        }
        public static Speed operator +(Speed left, Speed right)
        {
            return new Speed(left.Value + right.ConvertTo(left.Unit), left.Unit);
        }
        public static Speed operator -(Speed left, Speed right)
        {
            return new Speed(left.Value - right.ConvertTo(left.Unit), left.Unit);
        }
        public static Speed operator *(double left, Speed right)
        {
            return new Speed(left * right.Value, right.Unit);
        }
        public static Speed operator *(Speed left, double right)
        {
            return new Speed(left.Value * right, left.Unit);
        }
        public static Speed operator /(Speed left, double right)
        {
            return new Speed(left.Value / right, left.Unit);
        }
        public static double operator /(Speed left, Speed right)
        {
            return left.MetresPerSecond / right.MetresPerSecond;
        }
        public static bool operator <=(Speed left, Speed right)
        {
            return left.Value <= right.ConvertTo(left.Unit);
        }
        public static bool operator >=(Speed left, Speed right)
        {
            return left.Value >= right.ConvertTo(left.Unit);
        }
        public static bool operator <(Speed left, Speed right)
        {
            return left.Value < right.ConvertTo(left.Unit);
        }
        public static bool operator >(Speed left, Speed right)
        {
            return left.Value > right.ConvertTo(left.Unit);
        }
        public static bool operator ==(Speed left, Speed right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Speed left, Speed right)
        {
            return !(left == right);
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Speed objSpeed))
                return false;

            return Equals(objSpeed);
        }
        public bool Equals(Speed other)
        {
            return Value.Equals(other.ConvertTo(Unit));
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            if (!(obj is Speed objSpeed)) throw new ArgumentException("Object is not a Speed", nameof(obj));

            return CompareTo(objSpeed);
        }

        public int CompareTo(Speed other)
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
            throw new InvalidCastException($"Converting {typeof(Speed)} to bool is not supported.");
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Speed)} to char is not supported.");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException($"Converting {typeof(Speed)} to DateTime is not supported.");
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
            if (conversionType == typeof(Speed))
                return this;
            else if (conversionType == typeof(SpeedUnit))
                return Unit;
            else
                throw new InvalidCastException($"Converting {typeof(Speed)} to {conversionType} is not supported.");
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
            return UnitFormatter.Format<SpeedUnit>(this, format);
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
