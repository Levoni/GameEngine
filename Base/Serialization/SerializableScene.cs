using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Scenes;

namespace Base.Serialization
{
   /// <summary>
   /// Class used to serialize a scene's information
   /// </summary>
   [Serializable]
   public class SerializableScene
   {
      public List<SerializableDictionary<int, Components.BaseComponent>> components;
      public List<System.EngineSystem> systems;
      public List<Entities.Entity> entities;
      public string sceneID;

      #region Initialization
      /// <summary>
      /// Initializes a new serializable scene
      /// </summary>
      public SerializableScene()
      {
         sceneID = "";
         components = new List<SerializableDictionary<int, Components.BaseComponent>>();
         systems = new List<System.EngineSystem>();
         entities = new List<Entities.Entity>();
      }

      /// <summary>
      /// Initializes a new serializable scene and loads the information
      /// from a scene into it.
      /// </summary>
      /// <param name="s">Scene to load information</param>
      public SerializableScene(Scene s)
      {
         sceneID = string.Empty;
         components = new List<SerializableDictionary<int, Components.BaseComponent>>();
         systems = new List<System.EngineSystem>();
         entities = new List<Entities.Entity>();
         LoadInfoFromScene(s);
      }
      #endregion

      #region Instance Functions
      /// <summary>
      /// Loads the information from a scene into the serializable scene
      /// </summary>
      /// <param name="s">Scene to load information</param>
      private void LoadInfoFromScene(Scene s)
      {
         sceneID = s.sceneID;

         foreach (Entities.Entity e in s.entityManager.GetAllEntities())
         {
            entities.Add(e);
         }
         foreach (System.EngineSystem sys in s.systems)
         {
            systems.Add(sys);
         }
         foreach (Components.BaseComponentManager BM in s.componentManagers)
         {
            if (BM != null)
               components.Add(new SerializableDictionary<int, Components.BaseComponent>(BM.GetEntityIComponentDictionary()));
         }
      }

      /// <summary>
      /// Serializes this serializable scene to a specific file location
      /// </summary>
      /// <param name="filePath">Path to save the file to</param>
      public void SerializeScene(string filePath = "./scenes/")
      {
         Serializer<SerializableScene>.SerializeToFile(this, GetSceneTypes(), sceneID, filePath);
      }

      /// <summary>
      /// Gets the Types that are in the Serialized scene
      /// </summary>
      /// <returns>Array of Types used in the Serialized scene</returns>
      public Type[] GetSceneTypes()
      {
         List<Type> types = new List<Type>();
         foreach (SerializableDictionary<int, Components.BaseComponent> entry in components)
         {
            foreach (KeyValuePair<int, Components.BaseComponent> KVP in entry.ToDictionary())
            {
               if (types.IndexOf(KVP.Value.GetType()) == -1)
               {
                  types.Add(KVP.Value.GetType());
               }
            }
         }
         foreach (System.EngineSystem sys in systems)
         {
            if (types.IndexOf(sys.GetType()) == -1)
            {
               types.Add(sys.GetType());
            }
         }
         if (entities.Count > 0)
            types.Add(typeof(Entities.Entity));
         return types.ToArray();
      }
      #endregion
   }
}
