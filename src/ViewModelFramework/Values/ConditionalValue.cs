using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class ConditionalValue<T> : BasePropertyBag
  {
    public ConditionalValue()
    {
      Enabled = true;
    }

    public virtual bool Enabled
    {
      get { return GetField<bool>(); }
      set { SetField(value); }
    }

    public virtual T Value
    {
      get { return GetField<T>(); }
      set { SetField(value); }
    }
  }
}
