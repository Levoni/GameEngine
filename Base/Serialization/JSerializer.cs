using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Base.Serialization
{
   public static class JSerializer
   {
      public static T Deserialize<T>(string fileName, string fileExtension, string relativePath)
      {
         Stream stream = new FileStream(relativePath + fileName + '.' + fileExtension, FileMode.Open);
         using (StreamReader sr = new StreamReader(stream))
         {
            string jsonString = sr.ReadToEnd();
            var temp = JsonSerializer.Deserialize<T>(jsonString);
            stream.Close();
            return temp;
         }
      }

      public static object Serialize(string fileName, string fileExtension, string relativePath, object objectToSerialize)
      {
         var temp = JsonSerializer.Serialize(objectToSerialize);
         Stream stream = new FileStream(relativePath + fileName + '.' + fileExtension, FileMode.OpenOrCreate);
         using (StreamWriter sw = new StreamWriter(stream))
         {
            sw.Write(temp);
         }
         stream.Close();
         return temp;
      }
   }
}
