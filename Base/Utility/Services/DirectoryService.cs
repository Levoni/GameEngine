using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Base.Utility.Services
{
   public static class DirectoryService
   {
      public static bool DoesSaveDirectoryExist()
      {
         return Directory.Exists("./Save");
      }

      public static bool DoesSceneDirectoryExist()
      {
         return Directory.Exists("./scenes");
      }

      public static bool DoesFileExist(string fileName)
      {
         return File.Exists(fileName);
      }

      public static void CreateBasicGameDirectories()
      {
         CreateDirectory("./Save");
         CreateDirectory("./scenes");
      }

      public static void CreateDirectory(string path)
      {
         Directory.CreateDirectory(path);
      }
   }
}
