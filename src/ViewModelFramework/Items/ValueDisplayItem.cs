using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public interface IValueDisplayItem
  {
    object Value { get; }
    string Display { get; }
  }

  public class ValueDisplayItem<T> : IValueDisplayItem
  {
    private readonly T mValue;
    private readonly string mDisplay;

    public ValueDisplayItem(T value)
      : this(value, value.ToString())
    {

    }

    public ValueDisplayItem(T value, string display)
    {
      mValue = value;
      mDisplay = display;
    }

    public T Value 
    {
      get { return mValue; }
    }

    public string Display 
    {
      get { return mDisplay; }
    }

    object IValueDisplayItem.Value
    {
      get { return Value; }
    }

    public override string ToString()
    {
      return Display;
    }
  }
}
