using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Entities;
using Base.Utility;

namespace Base.Collision
{
   [Serializable]
   public class CollisionContext
   {
      public Entity entity1;
      public Entity entity2;
      public SeperationContext seperationContext;

      public CollisionContext()
      {
         entity1 = null;
         entity2 = null;
         seperationContext = null;
      }

      public CollisionContext(Entity entity1, Entity entity2, SeperationContext seperationVector)
      {
         this.entity1 = entity1;
         this.entity2 = entity2;
         this.seperationContext = seperationVector;
      }

      public bool Equals(CollisionContext obj)
      {
         return (entity1.id == obj.entity1.id || entity1.id == obj.entity2.id)
            && (entity2.id == obj.entity1.id || entity2.id == obj.entity2.id)
            && (seperationContext.pushVector.X == obj.seperationContext.pushVector.X 
            && seperationContext.pushVector.Y == obj.seperationContext.pushVector.Y
            && seperationContext.direction == obj.seperationContext.direction);
      }
   }
}
