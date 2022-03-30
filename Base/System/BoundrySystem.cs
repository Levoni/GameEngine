using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Scenes;
using Base.Components;
using Base.Entities;
using Base.Events;

namespace Base.System
{
   [Serializable]
   public class BoundrySystem:EngineSystem
   {
      public int xMin, yMin, xMax, yMax;
      public BoundrySystem(Scene s)
      {
         systemSignature = (uint)(1 << Sprite.GetFamily() | (1 << Transform.GetFamily()));
         RegisterScene(s);
         registeredEntities = new List<Entity>();
         xMin = 0;
         yMin = 0;
         xMax = 1920;
         yMax = 1080;
      }

      public BoundrySystem()
      {
         systemSignature = (uint)(1 << Sprite.GetFamily() | (1 << Transform.GetFamily()));
         registeredEntities = new List<Entity>();
         xMin = 0;
         yMin = 0;
         xMax = 1920;
         yMax = 1080;
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }

      public override void Update(int dt)
      {
         for (int i = registeredEntities.Count - 1; i >= 0; i--)
         {
            Transform t = parentScene.GetComponent<Transform>(registeredEntities[i]);
            Sprite s = parentScene.GetComponent<Sprite>(registeredEntities[i]);

            if (t.X < xMin)
            {
               t.X = xMin;
               parentScene.bus.Publish(this, new BoundryHitEvent(registeredEntities[i], new Utility.EngineVector2(xMin, 0)));
            }
            else if (t.Y < yMin)
            {
               t.Y = yMin;
               parentScene.bus.Publish(this, new BoundryHitEvent(registeredEntities[i], new Utility.EngineVector2(0, yMin)));
            }
            else if (t.X + s.imageWidth * t.widthRatio > xMax)
            {
               t.X += xMax - (t.X + s.imageWidth * t.widthRatio);
               parentScene.bus.Publish(this, new BoundryHitEvent(registeredEntities[i], new Utility.EngineVector2(xMax, 0)));
            }
            else if (t.Y + s.imageHeight * t.heightRatio > yMax)
            {
               t.Y += yMax - (t.Y + s.imageHeight * t.heightRatio);
               parentScene.bus.Publish(this, new BoundryHitEvent(registeredEntities[i], new Utility.EngineVector2(0, yMax)));
            }
         }
      }

   }
}
