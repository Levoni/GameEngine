using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility.Services
{
   public static class ExitGameService
   {
      public static bool isInitialized = false;
      public static Game game;

      public static void Initialize(Game gameEntity)
      {
         isInitialized = true;
         game = gameEntity;
      }

      public static void ExitGame()
      {
         if (isInitialized)
         {
            game.Exit();
         }
      }
   }
}
