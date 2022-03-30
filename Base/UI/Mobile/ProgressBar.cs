using Base.Utility;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility.Services;

namespace Base.UI.Mobile
{
   [Serializable]
   public class ProgressBar:Control
   {
      [NonSerialized]
      Texture2D barOverlay;
      public bool isHorizontal;
      public float barPercent;
      public string overlayImageReference;

      public ProgressBar() : base()
      {
         Name = "bar";
         value = "";
         bounds = new EngineRectangle(0, 0, 200, 100);
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "black");
         imageReference.Add(Enums.cState.hover.ToString(), "black");
         imageReference.Add(Enums.cState.pressed.ToString(), "black");
         imageReference.Add(Enums.cState.released.ToString(), "black");
         textAnchor = Enums.TextAchorLocation.center;
         barPercent = 0;
         isHorizontal = true;
         barOverlay = ContentService.Get2DTexture("black");
         init();
      }

      public ProgressBar(string name, string value, EngineRectangle bounds, Color color, string overlayReference) : base(name, value, bounds, color)
      {
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "black");
         imageReference.Add(Enums.cState.hover.ToString(), "black");
         imageReference.Add(Enums.cState.pressed.ToString(), "black");
         imageReference.Add(Enums.cState.released.ToString(), "black");
         textAnchor = Enums.TextAchorLocation.center;
         barPercent = .75f;
         isHorizontal = true;
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
         if(isHorizontal)
         {
            sb.Draw(barOverlay, new Rectangle((int)bounds.X, (int)bounds.Y, (int)(bounds.Width * barPercent), (int)bounds.Height), null, Color.White);
         }
         else
         {
            sb.Draw(barOverlay, new Rectangle((int)bounds.X, (int)bounds.Y, (int)(bounds.Width), (int)(bounds.Height * barPercent)), null, Color.White);
         }
         base.Render(sb);
      }

      public override void onDeserialized()
      {
         base.onDeserialized();
         barOverlay = ContentService.Get2DTexture(overlayImageReference);
      }
   }
}
