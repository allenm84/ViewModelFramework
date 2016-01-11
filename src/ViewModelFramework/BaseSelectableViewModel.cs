using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseSelectableViewModel : BaseViewModel
  {
    public bool Selected
    {
      get { return GetField<bool>(); }
      set { SetField(value); }
    }
  }
}
