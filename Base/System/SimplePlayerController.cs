using Base.Components;
using Base.Entities;
using Base.Events;
using Base.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.System
{
   [Serializable]
   public class SimplePlayerController:EngineSystem
   {
      EHandler<ControlEvent> onControl;

      public SimplePlayerController(Scene s)
      {
         systemSignature = (uint)(1 << PlayerController.GetFamily());
         registeredEntities = new List<Entity>();
         Init(s);
      }

      public SimplePlayerController()
      {
         systemSignature = (uint)(1 << PlayerController.GetFamily());
         registeredEntities = new List<Entity>();
      }

      public override void Update(int dt)
      {
         
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);

         onControl = new EHandler<ControlEvent>(new Action<object, ControlEvent>(onControlEvent));

         parentScene.bus.Subscribe(onControl);
      }

      public override void Terminate()
      {
         base.Terminate();

         if (parentScene != null)
         {
            parentScene.bus.Unsubscribe(onControl);
         }
      }

      public void onControlEvent(object sender, ControlEvent controlUpEvent)
      {
         foreach (Entity e in registeredEntities)
         {
            if (controlUpEvent.controlType == Utility.Enums.ControlType.up)
            {
               var playerController = parentScene.GetComponent<PlayerController>(e);
               parentScene.bus.Publish(this, new Events.VelocityChangeEvent(e, new Utility.EngineVector2(0, -playerController.Speed)));
            }
            else if (controlUpEvent.controlType == Utility.Enums.ControlType.down)
            {
               var playerController = parentScene.GetComponent<PlayerController>(e);
               parentScene.bus.Publish(this, new Events.VelocityChangeEvent(e, new Utility.EngineVector2(0, playerController.Speed)));
            }
            else if (controlUpEvent.controlType == Utility.Enums.ControlType.right)
            {
               var playerController = parentScene.GetComponent<PlayerController>(e);
               parentScene.bus.Publish(this, new Events.VelocityChangeEvent(e, new Utility.EngineVector2(playerController.Speed, 0)));
            }
            else if (controlUpEvent.controlType == Utility.Enums.ControlType.left)
            {
               var playerController = parentScene.GetComponent<PlayerController>(e);
               parentScene.bus.Publish(this, new Events.VelocityChangeEvent(e, new Utility.EngineVector2(-playerController.Speed, 0)));
            }
         }
      }
   }
}
