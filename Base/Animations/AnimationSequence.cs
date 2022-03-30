using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Graphics;

using Base.Utility.Services;

namespace Base.Animations
{
   [Serializable]
   public class AnimationSequence
   {
      public List<AnimationFrame> Frames;
      public string AnimationSheetFileName;
      public bool isLooping;
      [XmlIgnore, NonSerialized]
      public Texture2D AnimationSheet;

      public AnimationSequence()
      {
         Frames = new List<AnimationFrame>();
         AnimationSheetFileName = "";
         AnimationSheet = null;
         isLooping = false;
      }

      public AnimationSequence(List<AnimationFrame> frames, string SourceFileName, bool isLooping)
      {
         this.Frames = frames;
         this.AnimationSheetFileName = SourceFileName;
         this.isLooping = isLooping;
         AnimationSheet = ContentService.Get2DTexture(SourceFileName);
      }

      public void Init()
      {
         AnimationSheet = ContentService.Get2DTexture(AnimationSheetFileName);
      }
   }
}
