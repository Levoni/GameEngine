using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility;

namespace Base.Entities
{
   [Serializable]
   public class EntityManager
   {
      int entityIDCount;
      List<Entity> entities;
      Dictionary<int, ComponentMask> entityMask;

      public EntityManager()
      {
         entityIDCount = 0;
         entities = new List<Entity>();
         entityMask = new Dictionary<int, ComponentMask>();
      }

      public Entity CreateEntity()
      {
         Entity e = new Entity(entityIDCount++);
         entities.Add(e);
         entityMask[e.id] = new ComponentMask();
         return e;
      }

      public void AddEntity(Entity e)
      {
         if (!entities.Contains(e))
            entities.Add(e);
         if (!entityMask.ContainsKey(e.id))
            entityMask[e.id] = new ComponentMask();
      }

      public void AddEntity(Entity e, ComponentMask m)
      {
         if (!entities.Contains(e))
            entities.Add(e);
         if (!entityMask.ContainsKey(e.id))
            entityMask[e.id] = m;
      }

      public void DestroyEnitity(Entity e)
      {
         Entity entityToRemove = entities.Find(item => item.id == e.id);
         if (entityToRemove != null)
         {
            entities.Remove(entityToRemove);
            entityMask.Remove(entityToRemove.id);
         }
      }


      // Getters
      public ComponentMask GetMask(Entity e)
      {
         return entityMask[e.id];
      }

      public List<Entity> GetAllEntities()
      {
         return entities;
      }

      public Entity GetEntity(int entityID)
      {
         return entities.Find((value) => { return (value.id == entityID); });
      }
   }
}
