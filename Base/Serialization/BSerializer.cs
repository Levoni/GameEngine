using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Base.Serialization
{
   public static class BSerializer
   {
      public static void serializeObject(string filename, string fileExtension, object objectToSerialize, string relativePath = "")
      {
         IFormatter formatter = new BinaryFormatter();
         Stream stream = new FileStream(relativePath + filename + '.' + fileExtension, FileMode.Create);
         formatter.Serialize(stream, objectToSerialize);
         stream.Close();
      }


      //formatter = new BinaryFormatter();
      //stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
      //formatter.Serialize(stream, sertest);
      //stream.Close();

      public static object deserializeObject(string filename, string fileExtension, string relativePath = "")
      {
         IFormatter formatter = new BinaryFormatter();
         Stream stream = new FileStream(relativePath + filename + '.' + fileExtension, FileMode.Open);
         object obj = formatter.Deserialize(stream);
         stream.Close();
         return obj;
      }
   }
}
