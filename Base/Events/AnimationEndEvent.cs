using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Entities;
using Base.Animations;

namespace Base.Events
{
   [Serializable]
   public class AnimationEndEvent:Event
   {
      public Entity entity;
      public AnimationSequence sequence;

      public AnimationEndEvent(Entity entity, AnimationSequence sequence)
      {
         this.entity = entity;
         this.sequence = sequence;
      }
   }
}
