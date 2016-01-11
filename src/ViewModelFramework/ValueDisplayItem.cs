using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class ValueDisplayItem
  {
    public abstract object Value { get; }
    public abstract string Display { get; }

    public override string ToString()
    {
      return Display;
    }
  }
}
