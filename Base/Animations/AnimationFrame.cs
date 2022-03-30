using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Base.Collision;
using Base.Utility;

namespace Base.Animations
{
   [Serializable]
   public class AnimationFrame
   {
      public EngineRectangle sourceRect;
      public List<ICollisionBound2D> collision;
      public int collisionMaskId;
      public float frameDuration;

      public AnimationFrame()
      {
         sourceRect = new EngineRectangle();
         collision = null;
         frameDuration = 0;
         collisionMaskId = 0;
      }

      public AnimationFrame(EngineRectangle source, List<ICollisionBound2D> collision, int duration, int collisionMaskId)
      {
         this.sourceRect = source;
         this.collision = collision;
         this.frameDuration = duration;
         this.collisionMaskId = collisionMaskId;
      }
   }
}
