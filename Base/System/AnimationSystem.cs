using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Scenes;
using Base.Entities;
using Base.Components;
using Base.Events;
using Base.Utility.Services;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Base.System
{
   [Serializable]
   public class AnimationSystem : EngineSystem
   {
      public AnimationSystem(Scene s)
      {
         systemSignature = (uint)((1 << Animation.GetFamily()) | 1 << Transform.GetFamily());
         registeredEntities = new List<Entity>();
         Init(s);
      }

      public AnimationSystem()
      {
         systemSignature = (uint)((1 << Animation.GetFamily() | 1 << Transform.GetFamily()));
         registeredEntities = new List<Entity>();
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }

      public override void Update(int dt)
      {
         foreach(Entity e in registeredEntities)
         {
            Animation a = parentScene.GetComponent<Animation>(e);
            a.frameDurationRemaining -= dt;
         }
      }

      public override void Render(SpriteBatch sb)
      {
         sb.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, CameraService.camera.translationMatrix);
         foreach (Entity e in registeredEntities)
         {
            Animation a = parentScene.GetComponent<Animation>(e);
            Transform entityTransform = parentScene.GetComponent<Transform>(e);
            Sprite entitySprite = parentScene.GetComponent<Sprite>(e);
            if(entitySprite == null)
            {
               entitySprite = new Sprite("none");
               parentScene.AddComponent(e, entitySprite);
            }

            sb.Draw(a.animationSequence.AnimationSheet, new Vector2(entityTransform.X,entityTransform.Y), a.animationSequence.Frames[a.curAnimationFrame].sourceRect.toRectangle(), Color.White, MathHelper.ToRadians(entityTransform.rotation), new Vector2(0,0),new Vector2(entityTransform.widthRatio, entityTransform.heightRatio),SpriteEffects.None,0);

            if (a.frameDurationRemaining <= 0)
            {
               a.curAnimationFrame++;
               if (a.curAnimationFrame == a.animationSequence.Frames.Count)
               {
                  if (a.animationSequence.isLooping)
                  {
                     a.curAnimationFrame = 0;
                     a.frameDurationRemaining = (int)a.animationSequence.Frames[a.curAnimationFrame].frameDuration;
                  }
                  else
                  {
                     parentScene.bus.Publish(this, new AnimationEndEvent(e, a.animationSequence));
                     a.curAnimationFrame--;
                     a.frameDurationRemaining = -1;
                  }
               }
               else
               {
                  a.frameDurationRemaining = (int)a.animationSequence.Frames[a.curAnimationFrame].frameDuration + a.frameDurationRemaining;
                  if (entitySprite != null)
                  {
                     entitySprite.imageHeight = (int)a.animationSequence.Frames[a.curAnimationFrame].sourceRect.Height;
                     entitySprite.imageWidth = (int)a.animationSequence.Frames[a.curAnimationFrame].sourceRect.Width;
                  }
               }


               if (a.animationSequence.Frames[a.curAnimationFrame].collision != null)
               {
                  ColliderTwoD c2D = parentScene.GetComponent<ColliderTwoD>(e);
                  if (c2D != null)
                  {
                     c2D.colliders.Clear();
                     foreach(Collision.ICollisionBound2D CB in a.animationSequence.Frames[a.curAnimationFrame].collision)
                     {
                        c2D.colliders.Add(CB);
                     }
                  }
                  else
                  {
                     var animationFrame = a.animationSequence.Frames[a.curAnimationFrame];
                     ColliderTwoD newC2D = new ColliderTwoD(animationFrame.collision);
                     parentScene.AddComponent(e, newC2D);
                  }
               }
               else
               {
                  ColliderTwoD c2D = parentScene.GetComponent<ColliderTwoD>(e);
                  if (c2D != null)
                  {
                     parentScene.RemoveComponent<ColliderTwoD>(e);
                  }
               }
            }
         }
         sb.End();
      }
   }
}
