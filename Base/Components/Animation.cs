using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Animations;

namespace Base.Components
{
  [Serializable]
   public class Animation:Component<Animation>
   {
      public AnimationSequence animationSequence;
      public int curAnimationFrame;
      public int frameDurationRemaining;

      public Animation()
      {
         animationSequence = null;
         curAnimationFrame = 0;
         frameDurationRemaining = 0;
      }

      public Animation(AnimationSequence sequence, int curAnimationFrame, int frameDurationRemaining)
      {
         animationSequence = sequence;
         this.curAnimationFrame = curAnimationFrame;
         this.frameDurationRemaining = frameDurationRemaining;
      }

      public override void Init()
      {
         animationSequence.Init();
      }

      public override void onDeserialized()
      {
         animationSequence.Init();
      }
   }
}
