using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class AvailableValue<T> : BasePropertyBag
  {
    public AvailableValue()
    {
      IsAvailable = true;
    }

    public bool IsAvailable
    {
      get { return GetField<bool>(); }
      set { SetField(value); }
    }

    public T Value
    {
      get { return GetField<T>(); }
      set { SetField(value); }
    }
  }
}
