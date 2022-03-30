using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;

using Base.Utility;

namespace Base.Events
{
   [Serializable]
   public class VelocityChangeEvent:Event
   {
      public Entity MovedEntity;
      public EngineVector2 moveVector;

      public VelocityChangeEvent(Entity e, EngineVector2 moveVector)
      {
         MovedEntity = e;
         this.moveVector = moveVector;
      }
   }
}
