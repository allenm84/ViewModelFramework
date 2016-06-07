using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BasePropertyBag : BaseNotifyPropertyChanged
  {
    private readonly Dictionary<string, object> mFields = new Dictionary<string, object>();

    protected void Push(IDictionary<string, object> values)
    {
      foreach (var kvp in values)
      {
        PushValue(kvp.Key, kvp.Value);
      }
    }

    protected ReadOnlyDictionary<string, object> Pull()
    {
      return new ReadOnlyDictionary<string, object>(mFields);
    }

    protected bool TryGetValue(string key, out object value)
    {
      return mFields.TryGetValue(key, out value);
    }

    protected T GetField<T>([CallerMemberName] string key = "")
    {
      object value;
      if (!mFields.TryGetValue(key, out value))
      {
        value = default(T);
        mFields[key] = value;
      }
      return (T)value;
    }

    protected bool SetField<T>(T value, [CallerMemberName] string key = "", bool force = true)
    {
      bool pushed = false;

      if (force || ValueChanged(key, value))
      {
        PushValue(key, value);
        pushed = true;
      }

      return pushed;
    }

    private bool ValueChanged<T>(string key, T value)
    {
      return !(GetField<T>(key).Equals(value));
    }

    private void PushValue(string key, object value)
    {
      mFields[key] = value;
      FirePropertyChanged(key);
    }
  }
}
