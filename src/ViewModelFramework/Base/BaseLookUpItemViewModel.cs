using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseLookUpItemViewModel : BaseEditableItemViewModel
  {
    public string Id
    {
      get { return GetField<string>(); }
      protected set { SetField(value); }
    }
  }
}
