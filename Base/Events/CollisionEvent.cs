using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Entities;
using Base.Components;

using Base.Collision;

namespace Base.Events
{
   [Serializable]
   public class CollisionEvent : Event
   {
      public CollisionContext context;

      public CollisionEvent(CollisionContext context)
      {
         this.context = context;
      }
   }
}
