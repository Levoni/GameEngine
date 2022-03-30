using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility;

namespace Base.Components
{
   [Serializable]
   public class ComponentManager<ComponentType> : BaseComponentManager
   {
      const int MAX_COMPONENT_NUMBER = 1024;

      // <entityID,Component array index>
      Bi_DirectionalDictionary<int, int> hashMap;
      ComponentType[] components;
      int arraySize;

      public ComponentManager() : base()
      {
         arraySize = 0;
         hashMap = new Bi_DirectionalDictionary<int, int>();
         components = new ComponentType[MAX_COMPONENT_NUMBER];
      }

      public int AddComponent(int eID, ComponentType component)
      {
         components[arraySize] = component;
         hashMap.Add(eID, arraySize);
         ++arraySize;
         return arraySize - 1;
      }

      public ComponentType Lookup(int entityID)
      {
         if (hashMap.GetDictionary().ContainsKey(entityID))
            return components[hashMap.GetInstanceIndex(entityID)];
         return default(ComponentType);
      }

      public Dictionary<int, ComponentType> GetEntityComponentDictionary()
      {
         Dictionary<int, ComponentType> tempDictionary = new Dictionary<int, ComponentType>();
         foreach (KeyValuePair<int, int> kvp in hashMap.GetIDictionary())
         {
            if (components[kvp.Key] != null)
               tempDictionary[kvp.Value] = components[kvp.Key];
         }
         return tempDictionary;
      }

      public override Dictionary<int, BaseComponent> GetEntityIComponentDictionary()
      {
         Dictionary<int, BaseComponent> tempDictionary = new Dictionary<int, BaseComponent>();
         foreach (KeyValuePair<int, int> kvp in hashMap.GetIDictionary())
         {
            if (components[kvp.Key] != null)
               tempDictionary[kvp.Value] = components[kvp.Key] as BaseComponent;
         }
         return tempDictionary;
      }

      public override void Destroy(int entityID)
      {
         if (hashMap.GetDictionary().ContainsKey(entityID))
         {
            var temkp = typeof(ComponentType).ToString();
            if (arraySize == 1 && typeof(ComponentType).ToString() == "TestGame.Components.BulletComponent")
            {
               ;
            }

            //Remove the old component and replace it with last component in array
            int indexInstance = hashMap.GetInstanceIndex(entityID);
            int lastEntity = hashMap.GetEntity(arraySize - 1);
            int lastEntityIndex = hashMap.GetInstanceIndex(lastEntity);
            hashMap.Update(lastEntity, indexInstance);
            hashMap.Remove(entityID, lastEntityIndex);
            components[indexInstance] = components[arraySize - 1];
            --arraySize;

            //updates hash map (bi-directional Map)
            //hashMap.Remove(entityID, indexInstance);
            //if (arraySize != 0)
            //{
            //   hashMap.Update(lastEntity, indexInstance);
            //}
         }
      }
   }
}
