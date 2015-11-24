using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseEditableViewModel : BaseSelectableViewModel
  {
    internal abstract BaseViewModel CreateEditor();
  }
}
