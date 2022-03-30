using Base.Utility;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility.Services;

namespace Base.UI
{
   [Serializable]
   public class ProgressBar : Control
   {
      [NonSerialized]
      Texture2D barOverlay;
      [NonSerialized]
      public Color barDrawColor;
      public uint barDrawColorUint;
      public bool isHorizontal;
      public float barPercent;
      public string overlayImageReference;

      public ProgressBar() : base()
      {

      }

      public ProgressBar(string name, string value, EngineRectangle bounds, Color textColor, string overlayReference, Color overlayColor) : base(name, value, bounds, textColor)
      {
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "black");
         imageReference.Add(Enums.cState.hover.ToString(), "black");
         imageReference.Add(Enums.cState.pressed.ToString(), "black");
         imageReference.Add(Enums.cState.released.ToString(), "black");
         textAnchor = Enums.TextAchorLocation.center;
         barPercent = .75f;
         isHorizontal = true;
         barDrawColor = overlayColor;
         barDrawColorUint = overlayColor.PackedValue;
         this.overlayImageReference = overlayReference;
         barOverlay = ContentService.Get2DTexture(overlayImageReference);
         init();
      }

      public override void Update(int dt)
      {
         base.Update(dt);
      }

      public override void Render(SpriteBatch sb)
      {
         base.Render(sb);
         if (isHorizontal)
         {
            sb.Draw(barOverlay, new Rectangle((int)bounds.X, (int)bounds.Y, (int)(bounds.Width * barPercent), (int)bounds.Height), null, barDrawColor);
         }
         else
         {
            sb.Draw(barOverlay, new Rectangle((int)bounds.X, (int)bounds.Y + ((int)bounds.Height - (int)(bounds.Height * barPercent)) , (int)(bounds.Width), (int)(bounds.Height * barPercent)), null, barDrawColor);
         }
      }

      public override void onDeserialized()
      {
         base.onDeserialized();
         barOverlay = ContentService.Get2DTexture(overlayImageReference);
         barDrawColor = new Color(barDrawColorUint);
      }
   }
}
