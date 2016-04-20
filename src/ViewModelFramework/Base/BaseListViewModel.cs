using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseListViewModel<T> : BaseViewModel, IBindingListObserver<T>
    where T : BaseViewModel
  {
    protected readonly BindingList<T> mItems;

    public BaseListViewModel()
    {
      mItems = new ObservableBindingList<T>(this);
    }

    public BindingList<T> Items
    {
      get { return mItems; }
    }

    protected virtual void OnListCleared()
    {
    }

    protected virtual void OnViewModelAdded(T viewModel)
    {
    }

    protected virtual void OnViewModelRemoved(T viewModel)
    { 
    }

    void IBindingListObserver<T>.ListCleared()
    {
      OnListCleared();
    }

    void IBindingListObserver<T>.ItemAdded(T item)
    {
      OnViewModelAdded(item);
    }

    void IBindingListObserver<T>.ItemRemoved(T item)
    {
      OnViewModelRemoved(item);
    }
  }
}
