using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseLookUpListViewModel<T> : BaseEditableListViewModel<T>
    where T : BaseLookUpItemViewModel
  {
    private readonly Dictionary<string, T> mLookUp = new Dictionary<string, T>();

    public T this[string id]
    {
      get
      {
        T viewModel;
        if (!mLookUp.TryGetValue(id, out viewModel))
        {
          viewModel = default(T);
        }
        return viewModel;
      }
    }

    protected override void OnListCleared()
    {
      base.OnListCleared();
      mLookUp.Clear();
    }

    protected override void OnViewModelAdded(T viewModel)
    {
      base.OnViewModelAdded(viewModel);
      mLookUp[viewModel.Id] = viewModel;
    }

    protected override void OnViewModelRemoved(T viewModel)
    {
      base.OnViewModelRemoved(viewModel);
      mLookUp.Remove(viewModel.Id);
    }
  }
}
