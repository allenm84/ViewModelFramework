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
    private readonly TaskCompletionSource<bool> mSource;

    public BaseViewModel()
    {
      mSource = new TaskCompletionSource<bool>();
    }

    public Task<bool> Completed
    {
      get { return mSource.Task; }
    }

    protected void SetCompleted(bool result)
    {
      mSource.SetResult(result);
    }

    public void Dispose()
    {
      mSource.TrySetResult(false);
      Dispose(true);
    }

    protected virtual void Dispose(bool isExplicit)
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
