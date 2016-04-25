using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class ConfirmViewModel : BasePopupViewModel
  {
    private readonly DelegateCommand mYesCommand;
    private readonly DelegateCommand mNoCommand;

    public ConfirmViewModel(string message, string caption)
    {
      Message = message;
      Caption = caption;

      mYesCommand = new DelegateCommand(() => SetCompleted(true));
      mNoCommand = new DelegateCommand(() => SetCompleted(false));
    }

    public BaseCommand YesCommand
    {
      get { return mYesCommand; }
    }

    public BaseCommand NoCommand
    {
      get { return mNoCommand; }
    }
  }
}
