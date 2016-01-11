using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseIDViewModel : BaseEditableViewModel
  {
    internal string ID
    {
      get { return GetField<string>(); }
      set { SetField(value); }
    }
  }
}
