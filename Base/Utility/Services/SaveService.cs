using Base.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility.Services
{
   [Serializable]
   public static class SaveService
   {
      public static bool isInitialized = false;
      public static SaveFile Save;

      public static void LoadSave()
      {
         if (DirectoryService.DoesFileExist("save.save"))
         {

            Save = (SaveFile)BSerializer.deserializeObject("save", "save");
         }
         else
         {
            Save = new SaveFile();
            BSerializer.serializeObject("save", "save", Save);
         }
      }

      public static void SaveSave()
      {
         if (Save != null)
         {
            BSerializer.serializeObject("save", "save", Save);
         }
      }
   }
}
