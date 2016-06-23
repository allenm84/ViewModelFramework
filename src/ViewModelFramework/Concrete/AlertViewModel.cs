using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class AlertViewModel : BasePopupViewModel
  {
    public AlertViewModel(string message, string caption)
    {
      Message = message;
      Caption = caption;
    }
  }
}
