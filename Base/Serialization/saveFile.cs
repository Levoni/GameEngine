using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Base.Serialization
{
   [Serializable]
   public class SaveFile
   {
      public int currentLevel { get; set; }
      public string FileName { get; set; }
      public string SaveVersion { get; set; }
   }
}
