using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelFramework
{
  public abstract class BaseEditableListViewModel<T> : BaseListViewModel<T>
    where T : BaseEditableItemViewModel
  {
    private T mCurrentItem;

    private readonly DelegateCommand mAddCommand;
    private readonly DelegateCommand mEditCommand;
    private readonly DelegateCommand mRemoveCommand;
    private readonly DelegateCommand mClearCommand;

    public BaseEditableListViewModel()
    {
      mAddCommand = new DelegateCommand(DoAdd);
      mEditCommand = new DelegateCommand(DoEdit, CanEdit);
      mRemoveCommand = new DelegateCommand(DoRemove, CanRemove);
      mClearCommand = new DelegateCommand(DoClear, CanClear);
      mItems.ListChanged += mItems_ListChanged;
    }

    public T Current
    {
      get { return mCurrentItem; }
      set
      {
        mCurrentItem = value;
        FirePropertyChanged();
      }
    }

    [DisplayName("Add")]
    public ICommand AddCommand
    {
      get { return mAddCommand; }
    }

    [DisplayName("Edit")]
    public ICommand EditCommand
    {
      get { return mEditCommand; }
    }

    [DisplayName("Remove")]
    public ICommand RemoveCommand
    {
      get { return mRemoveCommand; }
    }

    [DisplayName("Clear")]
    public ICommand ClearCommand
    {
      get { return mClearCommand; }
    }

    private void RefreshEditCommands()
    {
      mEditCommand.Refresh();
      mRemoveCommand.Refresh();
      mClearCommand.Refresh();
      InternalRefreshEditCommands();
    }

    protected virtual void InternalRefreshEditCommands()
    {
      
    }

    private void OnListChanged(ListChangedType type)
    {
      RefreshEditCommands();
      InternalOnListChanged(type);
    }

    protected virtual void InternalOnListChanged(ListChangedType type)
    {
      
    }

    protected abstract T CreateNew();

    protected override void AfterPropertyChanged(string propertyName)
    {
      base.AfterPropertyChanged(propertyName);
      RefreshEditCommands();
    }

    protected Task<T> AddItem()
    {
      return AddItem(CreateNew());
    }

    protected async Task<T> AddItem(T item)
    {
      var editor = item.CreateEditor(true);
      if (!Send(editor))
      {
        return default(T);
      }

      var accepted = await editor.Completed;
      if (accepted)
      {
        mItems.Add(item);
        return item;
      }
      else
      {
        return default(T);
      }
    }

    private async void DoAdd()
    {
      await AddItem();
    }

    private bool CanEdit()
    {
      return (mCurrentItem != null);
    }

    private async void DoEdit()
    {
      var editor = mCurrentItem.CreateEditor(false);
      if (!Send(editor))
      {
        return;
      }

      var accepted = await editor.Completed;
      if (accepted)
      {
        OnListChanged(ListChangedType.ItemChanged);
      }
    }

    private bool CanRemove()
    {
      return mItems.Any(i => i.Selected);
    }

    protected virtual async Task<bool> BeforeDoRemove()
    {
      return await Task.FromResult(true);
    }

    protected async void DoRemove()
    {
      bool cancelled = await BeforeDoRemove();
      if (!cancelled)
      {
        return;
      }

      bool confirmed = await ShowConfirmation("Are you sure you want to remove the selected items?");
      if (!confirmed)
      {
        return;
      }

      for (int i = mItems.Count - 1; i > -1; --i)
      {
        if (mItems[i].Selected)
        {
          mItems.RemoveAt(i);
        }
      }
    }

    private bool CanClear()
    {
      return mItems.Count > 0;
    }

    protected virtual async Task<bool> BeforeDoClear()
    {
      return await Task.FromResult(true);
    }

    private async void DoClear()
    {
      bool cancelled = await BeforeDoClear();
      if (!cancelled)
      {
        return;
      }

      bool confirmed = await ShowConfirmation("Are you sure you want to clear all of the items?");
      if (!confirmed)
      {
        return;
      }

      mItems.Clear();
    }

    private void mItems_ListChanged(object sender, ListChangedEventArgs e)
    {
      OnListChanged(e.ListChangedType);
    }
  }
}
