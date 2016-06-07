using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class NotifyValue<T>
  {
    private Action mNotify;
    private T mValue;

    public NotifyValue(Action notify) 
      : this(default(T), notify)
    {

    }

    public NotifyValue(T initialValue, Action notify)
    {
      if (notify == null)
      {
        throw new ArgumentNullException("notify");
      }

      mValue = initialValue;
      mNotify = notify;
    }

    public T Value
    {
      get { return mValue; }
      set
      {
        mValue = value;
        mNotify();
      }
    }
  }
}
