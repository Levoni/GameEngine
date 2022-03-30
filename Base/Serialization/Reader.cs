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
   public static class Reader
   {
      public static string GetSimpleElementsValue(string filename, string filePath, string element)
      {
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            bool foundElement = false;
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  if (reader.Name == element)
                     foundElement = true;
               }
               else if (reader.NodeType == XmlNodeType.Text && foundElement)
               {
                  return reader.Value;
               }
            }
         }
         return null;
      }

      public static List<string> GetSimpleElementsValueList(string filename, string filePath, string element)
      {
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            bool foundElement = false;
            List<string> values = new List<string>();
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  if (reader.Name == element)
                     foundElement = true;
               }
               else if (reader.NodeType == XmlNodeType.Text && foundElement)
               {
                  values.Add(reader.Value);
                  foundElement = false;
               }
            }
            return values;
         }
      }

      public static string GetAttributeValue(string filename, string filePath, string attribute)
      {
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  while (reader.MoveToNextAttribute())
                  {
                     if (reader.Name == attribute)
                        return reader.Value;
                  }
               }
            }
         }
         return null;
      }

      public static string GetAttributeValue(string filename, string filePath, string attribute, string element)
      {
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  if (reader.Name == element)
                  {
                     while (reader.MoveToNextAttribute())
                     {
                        if (reader.Name == attribute)
                           return reader.Value;
                     }
                  }
               }
            }
         }
         return null;
      }

      public static List<string> GetAttributeValueList(string filename, string filePath, string attribute)
      {
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            List<string> values = new List<string>();
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  while (reader.MoveToNextAttribute())
                  {
                     if (reader.Name == attribute)
                        values.Add(reader.Value);
                  }
               }
            }
            return values;
         }
      }

      public static Dictionary<string,string> GetSimpleElementAttributes(string filename,string filePath, string element)
      {
         Dictionary<string, string> attributes = new Dictionary<string, string>();
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  if (reader.Name == element)
                  {
                     while (reader.MoveToNextAttribute())
                     {
                        attributes[reader.Name] = reader.Value;
                     }
                     return attributes;
                  }
               }
            }
         }
         return null;
      }

      public static List<List<KeyValuePair<string,string>>> GetAllSimpleElemntAttributes(string filename, string filePath, string element)
      {
         List<List<KeyValuePair<string, string>>> elements = new List<List<KeyValuePair<string, string>>>();
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  if (reader.Name == element)
                  {
                     List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
                     while (reader.MoveToNextAttribute())
                     {
                        
                        KeyValuePair<string, string> attribute = new KeyValuePair<string, string>(reader.Name, reader.Value);
                        attributes.Add(attribute);
                     }
                     if (attributes.Count > 0)
                        elements.Add(attributes);
                  }
               }
            }
         }
         return elements;
      }

      //TODO: (longterm) create function to create object from specified element based on a passed in entity. (look into using xmlDocumentReader.
      //TODO: (very-longterm) create function to add to write object to XML file
      // can use    [XmlRoot(ElementName = "error")] to specify name of the corisponding xml element for a c# class
      public static Dictionary<int, List<Dictionary<string, string>>> GetTileToObjectCollection(string filename, string filePath)
      {
         Dictionary<int,List<Dictionary<string, string>>> elements =  new Dictionary<int, List<Dictionary<string, string>>>();
         using (StreamReader sr = new StreamReader(filePath + filename))
         {
            XmlReaderSettings xrs = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(sr);
            int curtileID = -1;
            while (reader.Read())
            {
               List<string> attributeValues = new List<string>();
               if (reader.NodeType == XmlNodeType.Attribute)
                  ;
               else if (reader.NodeType == XmlNodeType.Element)
               {
                  if (reader.Name == "object")
                  {
                     Dictionary<string, string> attributes = new Dictionary<string, string>();
                     while (reader.MoveToNextAttribute())
                     {
                        attributes[reader.Name] = reader.Value;
                     }
                     elements[curtileID].Add(attributes);
                  }
                  else if(reader.Name == "tile")
                  {
                     while (reader.MoveToNextAttribute())
                     {
                        if(reader.Name == "id")
                        {
                           curtileID = int.Parse(reader.Value);
                           elements[curtileID] = new List<Dictionary<string, string>>();
                        }
                     }
                  }
               }
            }
         }
         return elements;
      }
   }
}