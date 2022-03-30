using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   [Serializable]
   public class Bi_DirectionalDictionary<keyType, valueType>
   {
      // <entityID,Component array index>
      Dictionary<keyType, valueType> entityToInstance;

      // <Component array index, entityID>
      Dictionary<valueType, keyType> instanceToEntity;

      public Bi_DirectionalDictionary()
      {
         entityToInstance = new Dictionary<keyType, valueType>();
         instanceToEntity = new Dictionary<valueType, keyType>();
      }


      public keyType GetEntity(valueType instanceIndex) { return instanceToEntity[instanceIndex]; }

      public valueType GetInstanceIndex(keyType entityID) { return entityToInstance[entityID]; }

      public void Add(keyType eID, valueType instanceIndex)
      {
         entityToInstance[eID] = instanceIndex;
         instanceToEntity[instanceIndex] = eID;
      }

      public void Update(keyType eID, valueType instanceIndex)
      {
         entityToInstance[eID] = instanceIndex;
         instanceToEntity[instanceIndex] = eID;
      }

      public void Remove(keyType eID, valueType instanceIndex)
      {
         entityToInstance.Remove(eID);
         instanceToEntity.Remove(instanceIndex);
      }

      //TODO: Make this an enumirator fuction so You can utilize foreach
      public Dictionary<keyType,valueType> GetDictionary()
      {
         return entityToInstance;
      }

      public Dictionary<valueType, keyType> GetIDictionary()
      {
         return instanceToEntity;
      }

   }
}
