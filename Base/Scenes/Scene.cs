using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Base.Components;
using Base.System;
using Base.Entities;
using Base.Events;
using System.Runtime.Serialization;
using Base.Serialization.Interfaces;

namespace Base.Scenes
{
   [Serializable]
   public class Scene: IOnDeserialization
   {
      public BaseComponentManager[] componentManagers;
      public List<EngineSystem> systems;
      public EntityManager entityManager;
      public EventBus bus;

      public string sceneID;
      [NonSerialized]
      public World parentWorld;


      public Scene()
      {
         componentManagers = new BaseComponentManager[32];
         systems = new List<EngineSystem>();
         entityManager = new EntityManager();
         bus = new EventBus();
         parentWorld = null;
      }

      public Scene(World w)
      {
         componentManagers = new BaseComponentManager[32];
         systems = new List<EngineSystem>();
         entityManager = new EntityManager();
         bus = new EventBus();
         parentWorld = w;
      }

      public void Update(GameTime gameTime)
      {
         foreach (EngineSystem s in systems)
         {
            s.Update(gameTime.ElapsedGameTime.Milliseconds);
         }
      }

      public void Render(SpriteBatch sb)
      {
         foreach (EngineSystem s in systems)
         {
            s.Render(sb);
         }
      }

      public void AddSystem(EngineSystem s)
      {
         s.Init(this);
         systems.Add(s);
         foreach (Entity e in entityManager.GetAllEntities())
         {
            if (entityManager.GetMask(e).GetCurrentMask().Contains(s.systemSignature))// (s.systemSignature & entityManager.GetMask(e).GetCurrentMask()) == s.systemSignature)
               s.RegisterEnitity(e);
         }
      }

      public void RemoveSystem(EngineSystem s)
      {
         s.Terminate();
         systems.Remove(s);
      }

      public Entity CreateEntity()
      {
         return entityManager.CreateEntity();
      }

      public void DestroyEntity(Entity e)
      {
         entityManager.DestroyEnitity(e);

         foreach (EngineSystem s in systems)
         {
            s.DeregisterEntity(e);
         }

         // Removes all the components from the different component managers
         for (int i = 0; i < componentManagers.Length; i++)
         {
            if (componentManagers[i] != null)
            {
               componentManagers[i].Destroy(e.id);
            }
         }

      }

      public void AddEntity(Entity e)
      {
         entityManager.AddEntity(e);
      }

      public void AddComponent<componentType>(Entity entity, componentType component)where componentType:BaseComponent
      {
         if (component != null)
         {
            if (entityManager.GetEntity(entity.id) == null)
            {
               entityManager.AddEntity(entity);
            }
            dynamic tempManager = componentManagers[component.Family()];
            if (tempManager == null)
            {
               Type genericType = typeof(ComponentManager<>).MakeGenericType(component.FamilyType());
               tempManager = Activator.CreateInstance(genericType);
               componentManagers[component.Family()] = tempManager;
            }

            tempManager.AddComponent(entity.id, component);

            entityManager.GetMask(entity).AddComponent(component.Family());

            // Check systems 
            foreach (EngineSystem s in systems)
            {
               if (entityManager.GetMask(entity).IsNewMatch(s.systemSignature))
                  s.RegisterEnitity(entity);
            }
         }
      }

      public void AddComponent(int entityID, BaseComponent component)
      {
         if (component != null)
         {
            Entity entity = entityManager.GetEntity(entityID);
            if (entity == null)
            {
               entityManager.AddEntity(entity);
            }
            dynamic tempManager = componentManagers[component.Family()];
            if (tempManager == null)
            {
               Type genericType = typeof(ComponentManager<>).MakeGenericType(component.FamilyType());
               tempManager = Activator.CreateInstance(genericType);
               componentManagers[component.Family()] = tempManager;
            }

            tempManager.AddComponent(entity.id, component);

            entityManager.GetMask(new Entity(entityID)).AddComponent(component.Family());

            // Check systems 
            foreach (EngineSystem s in systems)
            {
               if (entityManager.GetMask(entity).IsNewMatch(s.systemSignature))
                  s.RegisterEnitity(entity);
            }
         }
      }

      public void RemoveComponent<componentType>(Entity entity)where componentType: BaseComponent,new()
      {
         componentType obj = new componentType();
         if (entityManager.GetEntity(entity.id) != null)
         {
            dynamic tempManager = componentManagers[obj.Family()];
            tempManager.Destroy(entity.id);

            entityManager.GetMask(entity).RemoveComponent(obj.Family());

            // Check systems
            foreach (EngineSystem s in systems)
            {
               if (entityManager.GetMask(entity).noLongerMatch(s.systemSignature))
                  s.DeregisterEntity(entity);
            }
         }
      }

      public void AddComponentManager<ComponentType>(ComponentManager<ComponentType> componentManager)
      {
         componentManagers[Component<ComponentType>.GetFamily()] = componentManager;
      }

      public T GetComponent<T>(Entity e)where T:BaseComponent,new()
      {
         T obj = new T();
         dynamic tempManager = componentManagers[obj.Family()];

         if (tempManager == null)
            return tempManager;

         obj = tempManager.Lookup(e.id);
         return obj;
      }

      public systemType GetSystem<systemType>()where systemType: EngineSystem
      {
         object ES = systems.FirstOrDefault(x => x.GetType() == typeof(systemType));
         return (systemType) ES;
      }

      //TODO: make this hook into deserializzation callback methods so This is called automatically
      // Stack overflow: https://stackoverflow.com/questions/25582842/dictionary-is-empty-on-deserialization
      //[OnDeserialized]
      public void onDeserialized()
      {
         foreach (EngineSystem s in systems)
         {
            s.onDeserialized();
         }
         foreach (BaseComponentManager BCM in componentManagers)
         {
            if (BCM != null)
            {
               Dictionary<int, BaseComponent> BC = BCM.GetEntityIComponentDictionary();
               foreach (KeyValuePair<int, BaseComponent> component in BC)
               {
                  component.Value.onDeserialized();
               }
            }
         }
      }
   }
}
