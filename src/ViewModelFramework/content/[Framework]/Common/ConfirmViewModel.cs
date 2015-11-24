using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public sealed class ConfirmViewModel : BaseViewModel
  {
    internal ConfirmViewModel(string message)
      : this(message, "Confirm")
    {

    }

    internal ConfirmViewModel(string message, string caption)
    {
      Message = message;
      Caption = caption;
      Commit();
    }

    public string Message
    {
      get { return GetField<string>(); }
      private set { SetField(value); }
    }

    public string Caption
    {
      get { return GetField<string>(); }
      private set { SetField(value); }
    }

    internal static Task<bool> Confirm(BaseBroadcastViewModel viewModel, string message)
    {
      return Confirm(viewModel, message, "Confirm");
    }

    internal static async Task<bool> Confirm(BaseBroadcastViewModel viewModel, string message, string caption)
    {
      var confirm = new ConfirmViewModel(message, caption);
      return viewModel.Send(confirm) && (await confirm.Completed);
    }
  }
}
