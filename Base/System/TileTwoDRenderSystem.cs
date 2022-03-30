using Base.Components;
using Base.Entities;
using Base.Scenes;
using Base.Utility;
using Base.Utility.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.System
{
   [Serializable]
   public class TileTwoDRenderSystem:EngineSystem
   {
      public TileTwoDRenderSystem(Scene s)
      {
         systemSignature = (uint)((1 << Transform.GetFamily()) | (1 << TileSprite.GetFamily()));
         registeredEntities = new List<Entity>();
         RegisterScene(s);
      }

      public TileTwoDRenderSystem()
      {
         systemSignature = (uint)((1 << Transform.GetFamily()) | (1 << TileSprite.GetFamily()));
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
            TileSprite s = parentScene.GetComponent<TileSprite>(e);
            if(t.X == 0)
            {
               ;
            }
           sb.Draw(TileMapService.tileSetImages[s.TileSetName], new Rectangle((int)t.X + ((int)(s.ImageWidth * t.widthRatio) / 2), (int)t.Y + ((int)(s.ImageHeight * t.heightRatio) / 2), (int)(s.ImageWidth * t.widthRatio), (int)(s.ImageHeight * t.heightRatio)), new Rectangle(s.SourceStartX,s.SourceStartY,s.ImageWidth - 1,s.ImageHeight - 1), Color.White, MathHelper.ToRadians(t.rotation), new Vector2(s.ImageWidth / 2, s.ImageHeight / 2), SpriteEffects.None, 0);
         }
         sb.End();
      }
   }
}
//s.SourceStartX + s.ImageWidth / 2, s.SourceStartY + s.ImageHeight / 2