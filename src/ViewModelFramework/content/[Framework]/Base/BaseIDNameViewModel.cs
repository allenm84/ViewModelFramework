using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseIDNameViewModel : BaseIDViewModel
  {
    public string Name
    {
      get { return GetField<string>(); }
      internal set { SetField(value); }
    }
  }
}
