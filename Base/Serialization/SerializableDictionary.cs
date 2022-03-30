using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Serialization
{
   [Serializable]
   public class SerializableDictionary<typeOne, TypeTwo>
   {
      public List<typeOne> Key;
      public List<TypeTwo> Value;

      public SerializableDictionary()
      {
         Key = new List<typeOne>();
         Value = new List<TypeTwo>();
      }

      public SerializableDictionary(Dictionary<typeOne, TypeTwo> Dic)
      {
         Key = new List<typeOne>();
         Value = new List<TypeTwo>();
         CreateFromDictionary(Dic);
      }

      public Dictionary<typeOne, TypeTwo> ToDictionary()
      {
         Dictionary<typeOne, TypeTwo> tempDic = new Dictionary<typeOne, TypeTwo>();
         for (int i = 0; i < Key.Count; i++)
         {
            tempDic[Key[i]] = Value[i];
         }
         return tempDic;
      }

      public void CreateFromDictionary(Dictionary<typeOne, TypeTwo> tempDic)
      {
         Key.Clear();
         Value.Clear();
         foreach (KeyValuePair<typeOne, TypeTwo> kvp in tempDic)
         {
            Key.Add(kvp.Key);
            Value.Add(kvp.Value);
         }
      }

      public void Add(typeOne addKey, TypeTwo addValue)
      {
         Key.Add(addKey);
         Value.Add(addValue);
      }

      public void Remove(typeOne removeKey)
      {
         int index = -1;
         for(int i = 0; i < Key.Count; i++)
         {
            if (Key[i].Equals(removeKey))
            {
               index = i;
            }
         }
         if(index != -1)
         {
            Key.RemoveAt(index);
            Value.RemoveAt(index);
         }
      }
   }
}
