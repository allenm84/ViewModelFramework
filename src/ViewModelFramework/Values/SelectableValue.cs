using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class SelectableValue<T> : BasePropertyBag
  {
    private readonly BindingList<ValueDisplayItem<T>> mItems;

    public SelectableValue()
    {
      mItems = new BindingList<ValueDisplayItem<T>>();
      Enabled = true;
    }

    public bool Enabled
    {
      get { return GetField<bool>(); }
      set { SetField(value); }
    }

    public T Value
    {
      get { return Item.Value; }
      set { Item = mItems.SingleOrDefault(i => i.Value.Equals(value)); }
    }

    public ValueDisplayItem<T> Item
    {
      get { return GetField<ValueDisplayItem<T>>(); }
      set { SetField(value); }
    }

    public BindingList<ValueDisplayItem<T>> Items
    {
      get { return mItems; }
    }

    public event EventHandler Changed;

    public void Set(IEnumerable<ValueDisplayItem<T>> items)
    {
      mItems.Clear();
      Add(items);
    }

    public void Add(IEnumerable<ValueDisplayItem<T>> items)
    {
      mItems.AddRange(items);
      OnChanged(EventArgs.Empty);
    }

    protected virtual void OnChanged(EventArgs e)
    {
      Changed?.Invoke(this, e);
    }

    protected override void AfterPropertyChanged(string propertyName)
    {
      OnChanged(EventArgs.Empty);
    }
  }
}
