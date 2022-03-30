using Base.Entities;
using Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
   [Serializable]
   public class VelocitySetEvent
   {
      public Entity MovedEntity;
      public EngineVector2 newVector;

      public VelocitySetEvent(Entity e, EngineVector2 newVector)
      {
         MovedEntity = e;
         this.newVector = newVector;
      }
   }
}
