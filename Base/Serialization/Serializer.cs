using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Base.Serialization
{
   //TODO: add support for using FullFilePath
   /// <summary>
   /// Class is used to save objects to xml files and load xml files
   /// into objects.
   /// </summary>
   /// <typeparam name="type">Type of object to serialize</typeparam>
   public static class Serializer<type>
   {
      static XmlSerializer xmlSerializer;
      //TODO: create overloades for save/load game with type arrays
      //TODO: create a XML reader class that will read and get value for specific simple type attribute (part way done)

      //TODO: look into using bianary serializer instead
      //TODO: look into using iheriting Iserializer interface

      /// <summary>
      /// Serialize an object to the save game diriectory
      /// </summary>
      /// <param name="serializeObject">Object to serialize</param>
      /// <param name="SaveName">Name of Save File</param>
      /// <returns>True if save correctly, false otherwise</returns>
      public static bool SaveGame(type serializeObject, string SaveName)
      {
         Type[] tempTypes = new Type[1];
         tempTypes[0] = serializeObject.GetType();
         WriteTypesToTxtFile(SaveName, tempTypes, "./Save/");
         return Serialize(serializeObject, tempTypes, SaveName + ".save", "./Save/");
      }

      /// <summary>
      /// Deserialize a xml file from the save directory to an object.
      /// </summary>
      /// <param name="fileName">Name of serialized file</param>
      /// <param name="path">path to save file location</param>
      /// <returns>Object of serializer's type</returns>
      public static type LoaGame(string fileName, string path = "./Save/")
      {
         return DeserializeFromFile(fileName, path);
      }

      /// <summary>
      /// Serialize an object to the same directory
      /// </summary>
      /// <param name="serializeObject">Object to serialize</param>
      /// <param name="fileName">Name of serialized file</param>
      /// <returns>True if serialization succeeds, false otherwise</returns>
      public static bool SerializeToFile(type serializeObject, string fileName)
      {
         return Serialize(serializeObject, new Type[0], fileName);
      }

      /// <summary>
      /// Serialize an object and writes a type file to the same directory
      /// </summary>
      /// <param name="serializeObject">Object to serialize</param>
      /// <param name="types">Derived Types needed for serialization</param>
      /// <param name="fileName">Name of serialized file</param>
      /// <param name="path">File Path</param>
      /// <returns>True if serialization succeeds, false otherwise</returns>
      public static bool SerializeToFile(type serializeObject, Type[] types, string fileName, string path)
      {
         WriteTypesToTxtFile(fileName, types, path);
         return Serialize(serializeObject, types, fileName, path);
      }

      /// <summary>
      /// Deserialize a xml file using the types from a type file.
      /// The files are from the same directory.
      /// </summary>
      /// <param name="filename">Name of file to deserialize</param>
      /// <returns>Object of serializer's type</returns>
      public static type DeserializeFromFile(string filename)
      {
         Type[] tempTypes = GetTypesFromTxtFile(filename.Split('.')[0]).ToArray();
         xmlSerializer = new XmlSerializer(typeof(type), tempTypes);
         return Deserialize(filename); 
      }

      /// <summary>
      /// Deserialize a xml file using the types from a type file.
      /// The files are from a specific directory
      /// </summary>
      /// <param name="filename">Name of file to deserialize</param>
      /// <param name="path">FilePath</param>
      /// <returns>Object of serializer's type</returns>
      public static type DeserializeFromFile(string fileName, string path)
      {
         Type[] tempTypes = GetTypesFromTxtFile(path + fileName.Split('.')[0]).ToArray();
         xmlSerializer = new XmlSerializer(typeof(type), tempTypes);
         return Deserialize(fileName, path);
      }

      /// <summary>
      /// Creates a list of types from a type file
      /// </summary>
      /// <param name="filename">Name of file to get types from</param>
      /// <returns>List of types in type file</returns>
      private static List<Type> GetTypesFromTxtFile(string filename)
      {
         string[] types;
         try
         {
            using (StreamReader sr = new StreamReader(filename + ".types"))
            {
               string allTypes = sr.ReadLine();
               if (!string.IsNullOrEmpty(allTypes))
                  types = allTypes.Split(':');
               else
                  types = new string[0];
            }
            List<Type> tempTypes = new List<Type>();
            foreach (string sType in types)
            {
               Type t = Type.GetType(sType);
               if (t != null)
                  tempTypes.Add(t);
            }
            return tempTypes;
         }
         catch
         {
            List<Type> tempTypes = new List<Type>();
            return tempTypes;
         }
      }

      /// <summary>
      /// Creates a type file from a list of types
      /// </summary>
      /// <param name="fileName">Name of file to be created</param>
      /// <param name="types">Types to write to file</param>
      /// <param name="path">Path to directory for file</param>
      private static void WriteTypesToTxtFile(string fileName, Type[] types, string path = "")
      {
         using (StreamWriter sw = new StreamWriter(path + fileName + ".types"))
         {
            string CombinedTypes = string.Empty;
            foreach (Type t in types)
            {
               CombinedTypes += t.FullName + ',' + global::System.Reflection.Assembly.GetAssembly(t).ToString() + ':';
            }
            CombinedTypes.TrimEnd(':');
            CombinedTypes += typeof(type).ToString() + ',' + global::System.Reflection.Assembly.GetAssembly(typeof(type)).ToString();
            sw.WriteLine(CombinedTypes);
         }
      }

      /// <summary>
      /// Base serializing function. Serializes an object to a specified file
      /// location with a specified name. Also writes a type file to help with
      /// deserialization.
      /// </summary>
      /// <param name="serializeObject">Object to serialize</param>
      /// <param name="types">Derived Types needed for serialization</param>
      /// <param name="fileName">Name of type file and serialized file</param>
      /// <param name="path">File path to directory</param>
      /// <returns>True if serialization succeeds, false otherwise</returns>
      public static bool Serialize(type serializeObject, Type[] types, string fileName, string path = "")
      {
         string serializeFile = string.Empty;
         if (path != null && path != "")
            serializeFile = path;
         serializeFile += fileName;
         try
         {
            using (StreamWriter sw = new StreamWriter(serializeFile))
            {
               xmlSerializer = new XmlSerializer(typeof(type), types);
               xmlSerializer.Serialize(sw, serializeObject);
            }
         }
         catch (Exception ex)
         {
            return false;
         }
         return true;
      }

      private static bool Serialize(type serializeObject, Type[] types, string fileName, string path, Stream writeStream)
      {
         string serializeFile = string.Empty;
         if (path != null && path != "")
            serializeFile = path;
         serializeFile += fileName;
         try
         {
            using (StreamWriter sw = new StreamWriter(serializeFile))
            {
               xmlSerializer = new XmlSerializer(typeof(type), types);
               xmlSerializer.Serialize(sw, serializeObject);
            }
         }
         catch (Exception ex)
         {
            return false;
         }
         return true;
      }

      /// <summary>
      /// Base deserializing function. Deserializes an xml file with specific
      /// name and file path to a object.
      /// </summary>
      /// <param name="fileName">Name of file to be deserialized</param>
      /// <param name="path">File path to directory</param>
      /// <returns>Object of serializer's type</returns>
      private static type Deserialize(string fileName, string path = "")
      {
         string serializeFile = string.Empty;
         if (path != null && path != "")
            serializeFile = path;
         serializeFile += fileName;
         try
         {
            using (StreamReader sw = new StreamReader(serializeFile))
            {
               return (type)xmlSerializer.Deserialize(sw);
            }
         }
         catch (Exception ex)
         {
            return default(type);
         }
      }
   }
}
