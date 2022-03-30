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
   public class BoundryHitEvent
   {
      public Entity entity;
      public EngineVector2 hitLocation;

      public BoundryHitEvent()
      {
         entity = null;
         hitLocation = new EngineVector2();
      }

      public BoundryHitEvent(Entity entity, EngineVector2 hitLocation)
      {
         this.entity = entity;
         this.hitLocation = hitLocation;
      }
   }
}
