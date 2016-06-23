using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public struct Money : 
    IConvertible,
    IComparable, IComparable<Money>,  IComparable<decimal>, 
    IEquatable<Money>, IEquatable<decimal>
  {
    public decimal Amount;

    public static implicit operator decimal(Money m)
    {
      return m.Amount;
    }

    public static implicit operator Money(decimal d)
    {
      return new Money { Amount = d };
    }

    public override int GetHashCode()
    {
      return Amount.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("{0:C2}", Amount);
    }

    public override bool Equals(object obj)
    {
      var m = obj as Money?;
      if (m != null)
      {
        return decimal.Equals(Amount, m.Value.Amount);
      }

      var d = obj as decimal?;
      if (d != null)
      {
        return decimal.Equals(Amount, d.Value);
      }

      return false;
    }

    #region IConvertible Implementation

    TypeCode IConvertible.GetTypeCode()
    {
      return TypeCode.Decimal;
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(Amount);
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(Amount);
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(Amount);
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(Amount);
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(Amount);
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(Amount);
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(Amount);
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(Amount);
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(Amount);
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(Amount);
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(Amount);
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(Amount);
    }

    decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Amount;
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return Convert.ToDateTime(Amount);
    }

    string IConvertible.ToString(IFormatProvider provider)
    {
      return Convert.ToString(Amount);
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
      return Convert.ChangeType(Amount, conversionType, provider);
    }

    #endregion

    #region IComparable Implementation

    int IComparable.CompareTo(object obj)
    {
      var m = obj as Money?;
      if (m != null)
      {
        return decimal.Compare(Amount, m.Value.Amount);
      }

      var d = obj as decimal?;
      if (d != null)
      {
        return decimal.Compare(Amount, d.Value);
      }

      throw new InvalidOperationException("Unable to compare value with this object");
    }

    #endregion

    #region IComparable<Money> Implementation

    int IComparable<Money>.CompareTo(Money other)
    {
      return decimal.Compare(Amount, other.Amount);
    }

    #endregion

    #region IComparable<decimal> Implementation

    int IComparable<decimal>.CompareTo(decimal other)
    {
      return decimal.Compare(Amount, other);
    }

    #endregion

    #region IEquatable<Money> Implementation

    bool IEquatable<Money>.Equals(Money other)
    {
      return decimal.Equals(Amount, other.Amount);
    }

    #endregion

    #region IEquatable<decimal> Implementation

    bool IEquatable<decimal>.Equals(decimal other)
    {
      return decimal.Equals(Amount, other);
    }

    #endregion
  }
}
