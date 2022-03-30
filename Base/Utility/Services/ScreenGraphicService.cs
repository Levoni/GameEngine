using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility.Services
{
   public static class ScreenGraphicService
   {
      public static GraphicsDeviceManager graphics;
      private static bool isInitialized = false;

      public static bool checkInitialized()
      {
         return isInitialized;
      }

      public static void InitializeService(GraphicsDeviceManager graphicsManager)
      {
         graphics = graphicsManager;
         isInitialized = true;
      }

      public static EngineRectangle GetViewportBounds()
      {
         return new EngineRectangle(graphics.GraphicsDevice.Viewport.Bounds);
      }

      public static EngineVector2 GetPreferedBackbuffer()
      {
         return new EngineVector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
      }

      public static bool CheckFullScreen()
      {
         return graphics.IsFullScreen;
      }

      public static void SetFullScreen(bool isFullScreen)
      {
         if (graphics.IsFullScreen != isFullScreen)
         {
            graphics.ToggleFullScreen();
            graphics.ApplyChanges();
         }
      }

   }
}
