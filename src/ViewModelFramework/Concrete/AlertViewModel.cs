using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class AlertViewModel : BasePopupViewModel
  {
    private readonly DelegateCommand mAcceptCommand;

    public AlertViewModel(string message, string caption)
    {
      Message = message;
      Caption = caption;

      mAcceptCommand = new DelegateCommand(() => SetCompleted(true));
    }

    public BaseCommand AcceptCommand
    {
      get { return mAcceptCommand; }
    }
  }
}
