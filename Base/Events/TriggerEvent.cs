using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Collision;

namespace Base.Events
{
   [Serializable]
   public class TriggerEvent:Event
   {
      public ColliderMapTwoDEntity triggerCollider;
      public ColliderMapTwoDEntity collidedWith;
      public string triggerType;

      public TriggerEvent(ColliderMapTwoDEntity trigger, ColliderMapTwoDEntity collisionWith, string triggerType)
      {
         this.triggerCollider = trigger;
         this.collidedWith = collisionWith;
         this.triggerType = triggerType;
      }
   }
}
