using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Utility;

namespace Base.Collision
{
   [Serializable]
   public class SeperationContext
   {
      public EngineVector2 pushVector;
      public PushFromTo direction;

      public SeperationContext()
      {
         pushVector = new EngineVector2();
         direction = PushFromTo.none;
      }

      public SeperationContext(EngineVector2 pushVector, PushFromTo direction)
      {
         this.pushVector = pushVector;
         this.direction = direction;
      }
   }

   public enum PushFromTo
   {
      EntityOneToTwo,
      EntityTwoToOne,
      none
   }
}
