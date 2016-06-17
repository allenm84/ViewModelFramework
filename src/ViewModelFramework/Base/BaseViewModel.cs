using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelFramework
{
  public abstract class BaseViewModel : BasePropertyBag, IDisposable
  {
    private readonly DelegateCommand mAcceptCommand;
    private readonly DelegateCommand mRejectCommand;

    private readonly TaskCompletionSource<bool> mSource;

    public BaseViewModel()
    {
      mAcceptCommand = new DelegateCommand(DoAccept, CanAccept);
      mRejectCommand = new DelegateCommand(DoReject, CanReject);

      mSource = new TaskCompletionSource<bool>();
    }

    public BaseCommand AcceptCommand
    {
      get { return mAcceptCommand; }
    }

    public BaseCommand RejectCommand
    {
      get { return mRejectCommand; }
    }

    public Task<bool> Completed
    {
      get { return mSource.Task; }
    }

    protected void SetCompleted(bool result)
    {
      mSource.SetResult(result);
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
      SetCompleted(false);
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

    public void Dispose()
    {
      mSource.TrySetResult(false);
      OnDispose(true);
    }

    protected virtual void OnDispose(bool disposing)
    {
      // do nothing
    }

    public bool Send(BaseViewModel viewModel)
    {
      return ViewModelBroadcaster.Instance.Send(viewModel);
    }

    public async Task<bool> SendAndWait(BaseViewModel viewModel)
    {
      return Send(viewModel) && await viewModel.Completed;
    }

    protected async void ShowAlert(string message, string caption = "Alert")
    {
      await SendAndWait(new AlertViewModel(message, caption));
    }

    protected async Task<bool> ShowConfirmation(string message, string caption = "Confirm")
    {
      return await SendAndWait(new ConfirmViewModel(message, caption));
    }
  }
}
