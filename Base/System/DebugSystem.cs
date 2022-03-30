using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Base.System
{
   [Serializable]
   public class DebugSystem:EngineSystem
   {
      public bool IsEnabled;
      public KeyboardState oldState;
      public KeyboardState newState;
      public Base.UI.Label lblFPS;

      public float elapsedFrames, elapsedTime, avgFPS;

      public DebugSystem():base()
      {
         IsEnabled = true;
         oldState = new KeyboardState();
         newState = new KeyboardState();
         elapsedFrames = 0;
         elapsedTime = 0;
         avgFPS = 0;
         lblFPS = new UI.Label("FPS Label", "0", new EngineRectangle(0, 0, 50, 50), Color.White);
      }

      public override void Update(int dt)
      {
         if(IsEnabled)
         {
            elapsedTime += dt;
            elapsedFrames++;
            if(elapsedTime > 1000)
            {
               avgFPS = (1000 / (elapsedTime / elapsedFrames));
               lblFPS.value = avgFPS.ToString();
               elapsedFrames = 0;
               elapsedTime = 0;
            }
         }
      }

      public override void Render(SpriteBatch sb)
      {
         sb.Begin();
         if(IsEnabled)
         {
            lblFPS.Render(sb);
         }
         sb.End();
      }

   }
}
