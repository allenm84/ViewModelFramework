using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BasePopupViewModel : BaseViewModel
  {
    public string Message
    {
      get { return GetField<string>(); }
      protected set { SetField(value); }
    }

    public string Caption
    {
      get { return GetField<string>(); }
      protected set { SetField(value); }
    }
  }
}
