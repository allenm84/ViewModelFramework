using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseEditableItemViewModel : BaseSelectableViewModel
  {
    public abstract BaseViewModel CreateEditor(bool isForAdding);
  }
}
