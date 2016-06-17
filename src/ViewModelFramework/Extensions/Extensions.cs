using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public static class Extensions
  {
    public static bool IsImplementationOf(this Type type, Type target)
    {
      Type t = type;
      while (t != null)
      {
        if (t.IsGenericType && target.IsGenericType)
        {
          t = t.GetGenericTypeDefinition();
        }

        if (t == target)
        {
          return true;
        }

        t = t.BaseType;
      }

      return false;
    }

    public static void AddRange<T>(this IList<T> list, IEnumerable<T> values)
    {
      foreach (T value in values)
      {
        list.Add(value);
      }
    }

    public static IEnumerable<T> Excluding<T>(this IEnumerable<T> values, params T[] args)
    {
      foreach (T value in values)
      {
        if (Array.IndexOf(args, value) >= 0)
        {
          continue;
        }

        yield return value;
      }
    }
  }
}
