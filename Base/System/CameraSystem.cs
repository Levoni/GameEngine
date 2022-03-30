using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using Base.Components;
using Base.Events;
using Base.Entities;
using Base.Scenes;
using Base.Utility.Services;

namespace Base.System
{
   [Serializable]
   public class CameraSystem : EngineSystem
   {
      public bool isClampedToBounds;

      public CameraSystem(Scene s)
      {
         systemSignature = uint.MaxValue;
         registeredEntities = new List<Entity>();
         Init(s);
         isClampedToBounds = false;
      }

      public CameraSystem()
      {
         systemSignature = uint.MaxValue;
         registeredEntities = new List<Entity>();
         isClampedToBounds = false;
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }

      public override void Update(int dt)
      {
         KeyboardState kState = KeyboardService.currentState;
         if (kState.IsKeyDown(Keys.Up))
         {
            CameraService.camera.MoveCamera(new Microsoft.Xna.Framework.Vector2(0, -5), isClampedToBounds);
         }
         if (kState.IsKeyDown(Keys.Down))
         {
            CameraService.camera.MoveCamera(new Microsoft.Xna.Framework.Vector2(0, 5), isClampedToBounds);
         }
         if (kState.IsKeyDown(Keys.Right))
         {
            CameraService.camera.MoveCamera(new Microsoft.Xna.Framework.Vector2(5, 0), isClampedToBounds);
         }
         if (kState.IsKeyDown(Keys.Left))
         {
            CameraService.camera.MoveCamera(new Microsoft.Xna.Framework.Vector2(-5, 0), isClampedToBounds);
         }
         if (kState.IsKeyDown(Keys.OemPeriod) || MouseService.GetScrollDirection() == Utility.Enums.ScrollWheelDirection.up)
         {
            CameraService.camera.AdjuctZoom(.1f);
         }
         if (kState.IsKeyDown(Keys.OemComma) || MouseService.GetScrollDirection() == Utility.Enums.ScrollWheelDirection.down)
         {
            CameraService.camera.AdjuctZoom(-.1f);
         }
      }
   }
}
