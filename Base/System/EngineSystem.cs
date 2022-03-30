using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Base.Scenes;
using Base.Entities;
using Base.Serialization.Interfaces;

namespace Base.System
{
   [Serializable]
   public class EngineSystem: IOnDeserialization
   {
      public uint systemSignature;
      protected List<Entity> registeredEntities;
      protected Scene parentScene;

      public EngineSystem()
      {
         systemSignature = 0;
         registeredEntities = new List<Entity>();
         parentScene = null;
      }

      public virtual void Init(Scene s) { }

      public virtual void Terminate() { }

      public virtual void Update(int dt) { }

      public virtual void Render(SpriteBatch sb) { }

      public void RegisterScene(Scene w)
      {
         parentScene = w;
      }

      public virtual void RegisterEnitity(Entity entity)
      {
         registeredEntities.Add(entity);
      }

      public virtual void DeregisterEntity(Entity entity)
      {
         foreach(Entity e in registeredEntities)
         {
            if (e.id == entity.id)
            {
               registeredEntities.Remove(e);
               return;
            }
         }
      }

      public virtual void ClearRegisteredEntities()
      {
         List<Entity> entities = new List<Entity>();

         foreach (Entity e in registeredEntities)
         {
            entities.Add(e);
         }

         foreach(Entity e in entities)
         {
            parentScene.DestroyEntity(e);
         }
      }

      public virtual List<Entity> GetAllRegisteredEntities()
      {
         return registeredEntities;
      }

      public virtual void onDeserialized() { }
   }
}
