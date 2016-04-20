using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public sealed class ConfirmViewModel : BasePopupViewModel
  {
    internal ConfirmViewModel(string message, string caption)
    {
      Message = message;
      Caption = caption;
    }
  }
}
