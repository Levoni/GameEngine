using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Events;
using Base.Scenes;

using Microsoft.Xna.Framework.Graphics;

namespace Base.UI
{
   [Serializable]
   public class GUI: Components.Component<GUI>
   {
      public Events.EventBus EventBus;
      public Scenes.Scene parentScene;

      //TODO: enum to create state machine for different menus

      public virtual void Update(int dt) { ; }

      public virtual void Render(SpriteBatch sb) { ; }

      public virtual void Init(EventBus ebus, Scene parentScene)
      {
         EventBus = ebus;
         this.parentScene = parentScene;
      }

      public virtual void UnInitialize(){ ; }
   }
}
