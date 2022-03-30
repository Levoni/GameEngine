using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Components;
using Base.Entities;
using Base.Scenes;
using Base.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Base.System
{
   //TODO: find way to make sure GUI componets have the screen viewport bounds available to them
   [Serializable]
   public class GUISystem : EngineSystem
   {
      public GUISystem(Scene s)
      {
         systemSignature = (uint)((1 << Component<GUI>.GetFamily()));
         registeredEntities = new List<Entity>();
         Init(s);
      }

      public GUISystem()
      {
         systemSignature = (uint)(1 << Component<GUI>.GetFamily());
         registeredEntities = new List<Entity>();
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }

      public override void Terminate()
      {
         if (parentScene != null)
         {
         }
      }

      public override void Update(int dt)
      {
         for (int i = registeredEntities.Count - 1; i >= 0; i--)
         {
            Entity e = registeredEntities[i];
            GUI g = parentScene.GetComponent<GUI>(e);
            if (g != null)
               g.Update(dt);
         }
      }

      public override void Render(SpriteBatch sb)
      {
         sb.Begin();
         for (int i = registeredEntities.Count - 1; i >= 0; i--)
         {
            Entity e = registeredEntities[i];
            GUI g = parentScene.GetComponent<GUI>(e);

            g.Render(sb);
         }
         sb.End();
      }

      public override void RegisterEnitity(Entity entity)
      {
         base.RegisterEnitity(entity);
         GUI g = parentScene.GetComponent<GUI>(entity);
         g.Init(parentScene.bus, parentScene);
      }
      //TODO: add DeregisterEntity that will call an g.unInitialize to the Ehandlers can be deregistered.
      public override void DeregisterEntity(Entity entity)
      {
         foreach (Entity e in registeredEntities)
         {
            if (e.id == entity.id)
            {
               GUI g = parentScene.GetComponent<GUI>(entity);
               g.UnInitialize();
               registeredEntities.Remove(e);
               return;
            }
         }
      }
   }
}
