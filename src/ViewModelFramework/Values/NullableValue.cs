using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class NullableValue<T> : BasePropertyBag 
    where T : struct
  {
    public NullableValue(T? value)
    {
      HasValue = value.HasValue;
      Value = value.GetValueOrDefault();
    }

    public bool HasValue
    {
      get { return GetField<bool>(); }
      set { SetField(value); }
    }

    public T Value
    {
      get { return GetField<T>(); }
      set { SetField(value); }
    }

    public T? ToNullable()
    {
      if (HasValue)
      {
        return Value;
      }
      else
      {
        return null;
      }
    }
  }
}
