using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelFramework
{
  public abstract class BaseAcceptableViewModel : BaseNotifyPropertyChanged
  {
    private readonly TaskCompletionSource<bool> mTaskSource;

    private readonly DelegateCommand mCancel;
    public ICommand Cancel { get { return mCancel; } }

    private readonly DelegateCommand mAccept;
    public ICommand Accept { get { return mAccept; } }

    internal Task<bool> Completed
    {
      get { return mTaskSource.Task; }
    }

    public BaseAcceptableViewModel()
    {
      mTaskSource = new TaskCompletionSource<bool>();

      mCancel = new DelegateCommand(DoCancel, CanCancel);
      mAccept = new DelegateCommand(DoAccept, CanAccept);
    }

    internal event EventHandler Canceled;
    private void FireCanceled()
    {
      var canceled = Canceled;
      if (canceled != null)
      {
        canceled(this, EventArgs.Empty);
      }
    }

    internal event EventHandler Accepted;
    private void FireAccepted()
    {
      var accepted = Accepted;
      if (accepted != null)
      {
        accepted(this, EventArgs.Empty);
      }
    }

    protected override void AfterPropertyChanged(string propertyName)
    {
      base.AfterPropertyChanged(propertyName);
      RefreshAcceptCancel();
    }

    protected void RefreshAcceptCancel()
    {
      RefreshAccept();
      RefreshCancel();
    }

    protected void RefreshCancel()
    {
      mCancel.Refresh();
    }

    protected virtual bool CanCancel()
    {
      return true;
    }

    protected virtual void InternalDoCancel()
    {
    }

    private void DoCancel()
    {
      InternalDoCancel();
      FireCanceled();
      mTaskSource.TrySetResult(false);
    }

    protected void RefreshAccept()
    {
      mAccept.Refresh();
    }

    protected virtual bool CanAccept()
    {
      return true;
    }

    protected virtual void InternalDoAccept()
    {
    }

    private void DoAccept()
    {
      InternalDoAccept();
      FireAccepted();
      mTaskSource.TrySetResult(true);
    }
  }
}
