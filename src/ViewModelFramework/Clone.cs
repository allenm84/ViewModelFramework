using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public static class Clone
  {
    public static object Do(object obj)
    {
      if (obj == null)
      {
        return null;
      }

      using (var stream = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, obj);
        stream.Position = 0;
        return formatter.Deserialize(stream);
      }
    }
  }
}
