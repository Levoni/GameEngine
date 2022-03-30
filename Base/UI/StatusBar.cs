using Base.Utility;
using Base.Utility.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI
{
   [Serializable]
   public class StatusBar:ProgressBar
   {
      public string maxValue;
      public float percentOfMax;

      public StatusBar() : base()
      {
         Name = "Sidebar";
         value = "0";
         maxValue = "100";
         percentOfMax = 0;
         bounds = new EngineRectangle(0, 0, 400, 100);
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "statusbar_default_" + Enums.cState.none.ToString());
         imageReference.Add(Enums.cState.hover.ToString(), "statusbar_default_" + Enums.cState.hover.ToString());
         imageReference.Add(Enums.cState.pressed.ToString(), "statusbar_default_" + Enums.cState.pressed.ToString());
         imageReference.Add(Enums.cState.released.ToString(), "statusbar_default_" + Enums.cState.released.ToString());
         init();
      }

      public StatusBar(string name, EngineRectangle bounds) : base(name, "", bounds, Color.White, "statusbar_default_none", Color.White)
      {
         this.maxValue = maxValue;
         this.setImageReferences("statusbar_default_" + Enums.cState.none.ToString(), "statusbar_default_" + Enums.cState.hover.ToString(), 
            "statusbar_default_" + Enums.cState.pressed.ToString(), "statusbar_default_" + Enums.cState.released.ToString());
         init();
      }

      public override void Update(int dt)
      {
         base.Update(dt);
         percentOfMax = CalculatePercent();
      }

      public void SetValue(string newValue)
      {
         string tempValue = value;
         value = newValue;
         percentOfMax = CalculatePercent();
         ValueChange(tempValue, newValue);
      }

      private float CalculatePercent()
      {
         if(float.Parse(value) <= 0)
         {
            return 0;
         }
         if(float.Parse(maxValue) <= 0)
         {
            return 1;
         }
         return float.Parse(value) / float.Parse(maxValue);
      }

      public override void Render(SpriteBatch sb)
      {
         if (isEnabled)
         {
            sb.Draw(images[clickState.ToString()], bounds.toRectangle(), null, Color.Red, 0, new Vector2(), SpriteEffects.None,0);
            sb.Draw(images[clickState.ToString()], new Rectangle((int)bounds.X, (int)bounds.Y, (int)(bounds.Width * percentOfMax), (int)bounds.Height),null,Color.Green,0,new Vector2(),SpriteEffects.None,1);
         }
      }
   }
}
