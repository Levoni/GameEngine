using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;
using Base.Components;
using Base.Serialization;
using Base.System;
using Base.Events;

namespace Base.Scenes
{
   //TODO: add support for creating a scene while carrying over persistant
   //entities.
   //TODO: use filename and directory path and not filepath
   public static class SceneService
   {
      public static Scene CreateEmptyScene()
      {
         Scene s = new Scene();
         return s;
      }

      /// <summary>
      /// Removes all entities from a specified scene
      /// </summary>
      /// <param name="s">Scene to destroy</param>
      public static void DestoyScene(Scene s)
      {
         World w = s.parentWorld;
         s = new Scene(w);
      }

      /// <summary>
      /// Removes all entities that are not persistant from the scene
      /// </summary>
      /// <param name="s">Scene to remove entities from</param>
      public static void ClearScene(Scene s)
      {
         s.componentManagers = new BaseComponentManager[32];
         s.systems = new List<EngineSystem>();
         s.entityManager = new EntityManager();
         s.bus = new EventBus();
         s.parentWorld = null;
      }

      /// <summary>
      /// Serializes a Scene to a specific file location
      /// </summary>
      /// <param name="s">Scene to serialize</param>
      /// <param name="filePath">Path to save the file to</param>
      public static void CreateFileFromScene(Scene s, string filePath = "./scenes/")
      {
         SerializableScene ss = new SerializableScene(s);
         Serializer<SerializableScene>.SerializeToFile(ss, ss.GetSceneTypes(), s.sceneID, filePath);
      }

      /// <summary>
      /// Creates a Scene from a serializableScene
      /// </summary>
      /// <param name="ss">SerializableScene to create scene from</param>
      /// <returns>Created Scene</returns>
      public static Scene CreateSceneFromSerializableScene(SerializableScene ss)
      {
         Scene s = new Scene();
         foreach (Entities.Entity e in ss.entities)
         {
            s.AddEntity(e);
         }
         foreach (System.EngineSystem es in ss.systems)
         {
            s.AddSystem(es);
         }
         foreach (SerializableDictionary<int, Components.BaseComponent> entry in ss.components)
         {
            foreach (KeyValuePair<int, Components.BaseComponent> KVP in entry.ToDictionary())
            {
               dynamic component = KVP.Value;
               component.Init();
               s.AddComponent(KVP.Key, component);
            }
         }
         return s;
      }

      /// <summary>
      /// Creates a Scene from a .save file located in scenes subdirectory with 
      /// sceneID as the file name.
      /// </summary>
      /// <param name="sceneID">File Name/Scene's ID</param>
      /// <returns>Created Scene</returns>
      public static Scene CreateSceneFromFile(string sceneID)
      {
         SerializableScene ss = Serializer<SerializableScene>.DeserializeFromFile("./scenes/" + sceneID);
         return CreateSceneFromSerializableScene(ss);
      }

      /// <summary>
      /// Creates a Scene from a .save file located in scenes subdirectory with
      /// sceneId as the file name. Sets Created scene's parent world.
      /// </summary>
      /// <param name="sceneID">File Name/Scene's ID</param>
      /// <param name="w">Parent World for the scene</param>
      /// <returns>Created Scene</returns>
      public static Scene CreateSceneFromFile(string sceneID, World w)
      {
         Scene s = CreateSceneFromFile("/scenes/" + sceneID);
         s.parentWorld = w;
         return s;
      }

      /// <summary>
      /// Creates a Scene from a .save file located in specified directory with 
      /// sceneID as the file name.
      /// </summary>
      /// <param name="sceneID">File Name/Scene's ID</param>
      /// <param name="filePath">Path to directory of file</param>
      /// <returns>Created Scene</returns>
      public static Scene CreateSceneFromFile(string sceneID, string filePath)
      {
         SerializableScene ss = Serializer<SerializableScene>.DeserializeFromFile(filePath + sceneID);
         return CreateSceneFromSerializableScene(ss);
      }

      /// <summary>
      /// Creates a Scene from a .save file located in specified directory with 
      /// sceneID as the file name.
      /// </summary>
      /// <param name="sceneID">File Name/Scene's ID</param>
      /// <param name="filePath">Path to directory of file</param>
      /// <returns>Created Scene</returns>
      public static Scene CreateSceneFromFile(string sceneID, string filePath, World w)
      {
         SerializableScene ss = Serializer<SerializableScene>.DeserializeFromFile(filePath + sceneID);
         Scene s = CreateSceneFromSerializableScene(ss);
         s.parentWorld = w;
         return s;
      }

      /// <summary>
      /// Gets the Types that are in the Serialized scene
      /// </summary>
      /// <param name="s">Scene to Get types from</param>
      /// <returns>Array of Types used in the Serialized scene</returns>
      public static Type[] GetSceneTypes(Scene s)
      {
         Serialization.SerializableScene ss = new Serialization.SerializableScene(s);
         List<Type> types = new List<Type>();
         foreach (Serialization.SerializableDictionary<int, Components.BaseComponent> entry in ss.components)
         {
            foreach (KeyValuePair<int, Components.BaseComponent> KVP in entry.ToDictionary())
            {
               if (types.IndexOf(KVP.Value.GetType()) == -1)
               {
                  types.Add(KVP.Value.GetType());
               }
            }
         }
         foreach (System.EngineSystem sys in ss.systems)
         {
            if (types.IndexOf(sys.GetType()) == -1)
            {
               types.Add(sys.GetType());
            }
         }
         if (ss.entities.Count > 0)
            types.Add(typeof(Entities.Entity));
         return types.ToArray();
      }
   }
}




//public static Scene TestScene(World w)
//{
//   Scene s = new Scene(w);
//   Entity e1 = s.CreateEntity();
//   Transform t1 = new Transform(50, 50);
//   Sprite s1 = new Sprite("Default");
//   // Add new system for rendering sprites
//   s.AddComponent(e1, t1);
//   s.AddComponent(e1, s1);
//   return s;
//}
