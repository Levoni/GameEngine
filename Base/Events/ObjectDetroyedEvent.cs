using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Entities;

namespace Base.Events
{
   [Serializable]
   public class ObjectDetroyedEvent:Event
   {
      public Entity entityToDestroy;
      
      public ObjectDetroyedEvent()
      {
         entityToDestroy = null;
      }

      public ObjectDetroyedEvent(Entity e)
      {
         entityToDestroy = e;
      }

   }
}
