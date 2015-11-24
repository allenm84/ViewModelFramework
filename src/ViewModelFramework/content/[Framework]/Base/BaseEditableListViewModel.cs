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
    where T : BaseEditableViewModel
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

    public ICommand AddCommand
    {
      get { return mAddCommand; }
    }

    public ICommand EditCommand
    {
      get { return mEditCommand; }
    }

    public ICommand RemoveCommand
    {
      get { return mRemoveCommand; }
    }

    public ICommand ClearCommand
    {
      get { return mClearCommand; }
    }

    private void RefreshEditCommands()
    {
      mEditCommand.Refresh();
      mRemoveCommand.Refresh();
      mClearCommand.Refresh();
    }

    private void OnListChanged()
    {
      RefreshEditCommands();
      InternalOnListChanged();
    }

    protected virtual void InternalOnListChanged()
    {
      
    }

    protected abstract T CreateNew();

    protected override void AfterPropertyChanged(string propertyName)
    {
      base.AfterPropertyChanged(propertyName);
      RefreshEditCommands();
    }

    internal Task<T> AddItem(BaseBroadcastViewModel broadcaster)
    {
      var item = CreateNew();
      return AddItem(item, broadcaster);
    }

    internal async Task<T> AddItem(T item, BaseBroadcastViewModel broadcaster)
    {
      var editor = item.CreateEditor();
      if (!broadcaster.Send(editor))
      {
        return default(T);
      }

      var accepted = await editor.Completed;
      if (accepted)
      {
        mItems.Add(item);
        OnListChanged();
        return item;
      }
      else
      {
        return default(T);
      }
    }

    private async void DoAdd()
    {
      await AddItem(this);
    }

    private bool CanEdit()
    {
      return (mCurrentItem != null);
    }

    private async void DoEdit()
    {
      var editor = mCurrentItem.CreateEditor();
      if (!Send(editor))
      {
        return;
      }

      var accepted = await editor.Completed;
      if (accepted)
      {
        OnListChanged();
      }
    }

    private bool CanRemove()
    {
      return mItems.Any(i => i.Selected);
    }

    private async void DoRemove()
    {
      bool confirmed = await ConfirmViewModel.Confirm(this, "Are you sure you want to remove the selected items?");
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
      OnListChanged();
    }

    private bool CanClear()
    {
      return mItems.Count > 0;
    }

    private async void DoClear()
    {
      bool confirmed = await ConfirmViewModel.Confirm(this, "Are you sure you want to clear all of the items?");
      if (!confirmed)
      {
        return;
      }

      mItems.Clear();
      OnListChanged();
    }

    private void mItems_ListChanged(object sender, ListChangedEventArgs e)
    {
      OnListChanged();
    }
  }
}
