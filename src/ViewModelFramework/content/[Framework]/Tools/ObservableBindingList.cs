using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public interface IBindingListObserver<T>
  {
    void ListCleared();
    void ItemAdded(T item);
    void ItemRemoved(T item);
  }

  public class ObservableBindingList<T> : BindingList<T>
  {
    private readonly IBindingListObserver<T> mObserver;

    public ObservableBindingList(IBindingListObserver<T> observer)
    {
      mObserver = observer;
    }

    protected override void ClearItems()
    {
      base.ClearItems();
      mObserver.ListCleared();
    }

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      mObserver.ItemAdded(item);
    }

    protected override void RemoveItem(int index)
    {
      T item = this[index];
      base.RemoveItem(index);
      mObserver.ItemRemoved(item);
    }

    protected override void SetItem(int index, T item)
    {
      mObserver.ItemRemoved(this[index]);
      base.SetItem(index, item);
      mObserver.ItemAdded(item);
    }
  }
}
