using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Base.Components;
using Base.Entities;
using Base.Scenes;
using Base.Utility.Services;

namespace Base.System
{
   [Serializable]
   public class TwoDRenderSystem:EngineSystem
   {
      public TwoDRenderSystem(Scene s)
      {
         systemSignature = (uint)((1 << Transform.GetFamily()) | (1 << Sprite.GetFamily()));
         registeredEntities = new List<Entity>();
         RegisterScene(s);
      }

      public TwoDRenderSystem()
      {
         systemSignature = (uint)((1 << Transform.GetFamily()) | (1 << Sprite.GetFamily()));
         registeredEntities = new List<Entity>();
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }


      public override void Render(SpriteBatch sb)
      {
         sb.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, CameraService.camera.translationMatrix);
         //sb.Begin();
         for (int i = registeredEntities.Count - 1; i >= 0; i--)
         {
            Entity e = registeredEntities[i];
            Transform t = parentScene.GetComponent<Transform>(e);
            Sprite s = parentScene.GetComponent<Sprite>(e);
            
            sb.Draw(s.image, new Rectangle((int)t.X + ((int)(s.imageWidth * t.widthRatio)/2), (int)t.Y + ((int)(s.imageHeight * t.heightRatio)/2), (int)(s.imageWidth * t.widthRatio),(int)(s.imageHeight * t.heightRatio)), null, Color.White, MathHelper.ToRadians(t.rotation), new Vector2(s.image.Width/2,s.image.Height/2), SpriteEffects.None, s.zOrder);
         }
         sb.End();
      }
   }
}
