using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseEditorViewModel : BaseViewModel
  {
    private readonly DelegateCommand mAcceptCommand;
    private readonly DelegateCommand mRejectCommand;

    public BaseEditorViewModel()
    {
      mAcceptCommand = new DelegateCommand(DoAccept, CanAccept);
      mRejectCommand = new DelegateCommand(DoReject, CanReject);
    }

    public BaseCommand AcceptCommand
    {
      get { return mAcceptCommand; }
    }

    public BaseCommand RejectCommand
    {
      get { return mRejectCommand; }
    }

    protected override void AfterPropertyChanged(string propertyName)
    {
      RefreshAcceptReject();
    }

    protected void RefreshAcceptReject()
    {
      RefreshAccept();
      RefreshReject();
    }

    protected void RefreshReject()
    {
      mRejectCommand.Refresh();
    }

    protected virtual bool CanReject()
    {
      return true;
    }

    protected virtual void BeforeRejected()
    {
    }

    protected virtual void AfterRejected()
    {
    }

    private void DoReject()
    {
      BeforeRejected();
      SetCompleted(true);
      AfterRejected();
    }

    protected void RefreshAccept()
    {
      mAcceptCommand.Refresh();
    }

    protected virtual bool CanAccept()
    {
      return true;
    }

    protected virtual void BeforeAccepted()
    {
    }

    protected virtual void AfterAccepted()
    {
    }

    private void DoAccept()
    {
      BeforeAccepted();
      SetCompleted(true);
      AfterAccepted();
    }
  }
}
