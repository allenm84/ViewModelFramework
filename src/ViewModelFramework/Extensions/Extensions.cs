using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public static class Extensions
  {
    public static void AddRange<T>(this IList<T> list, IEnumerable<T> values)
    {
      foreach (T value in values)
      {
        list.Add(value);
      }
    }
  }
}
