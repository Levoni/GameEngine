using System;
using System.Collections.Generic;

using Base.Components;
using Base.Events;
using Base.Entities;
using Base.Scenes;
using Base.Utility;
using static Base.Utility.Enums;

namespace Base.System
{
   //TODO: Add More Control Events (list of controls that can be fired if input system sends the correct control)
   [Serializable]
   public class PlayerControllerSystem : EngineSystem
   {
      EHandler<KeyPressedEvent> KeyPressed;
      EHandler<MousePressedEvent> MousePressed;
      public PlayerControllerSystem(Scene s)
      {
         systemSignature = (uint)(1 << PlayerController.GetFamily());
         RegisterScene(s);
         registeredEntities = new List<Entity>();
      }

      public PlayerControllerSystem()
      {
         systemSignature = (uint)(1 << PlayerController.GetFamily());
         registeredEntities = new List<Entity>();
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
         KeyPressed = new EHandler<KeyPressedEvent>(new Action<object, KeyPressedEvent>(HandleKeyPressedEvent));
         MousePressed = new EHandler<MousePressedEvent>(new Action<object, MousePressedEvent>(HandleMousePressedEvent));
         parentScene.bus.Subscribe(KeyPressed);
         parentScene.bus.Subscribe(MousePressed);
      }

      public override void Terminate()
      {
         base.Terminate();
         parentScene.bus.Unsubscribe(KeyPressed);
         parentScene.bus.Unsubscribe(MousePressed);
      }

      public override void Update(int dt) { }

      public void HandleKeyPressedEvent(object sender, KeyPressedEvent keyPressedEvent)
      {
         if (keyPressedEvent.keyState == Enums.gameControlState.keyDown ||
            keyPressedEvent.keyState == Enums.gameControlState.keyPressed)
         {
            for (int i = registeredEntities.Count - 1; i >= 0; i--)
            {
               PlayerController PC = parentScene.GetComponent<PlayerController>(registeredEntities[i]);
               foreach (KeyValuePair<ControlType, GameControl> kvp in PC.controls)
               {
                  if (kvp.Value.isControlValue(keyPressedEvent.key))
                  {
                     parentScene.bus.Publish(this, new ControlEvent(registeredEntities[i], kvp.Key, keyPressedEvent.keyState));
                  }
               }
            }
         }
      }

      public void HandleMousePressedEvent(object sender, MousePressedEvent mousePressedEvent)
      {
         if (mousePressedEvent.controlState == Enums.gameControlState.keyDown ||
            mousePressedEvent.controlState == Enums.gameControlState.keyPressed)
         {
            for (int i = registeredEntities.Count - 1; i >= 0; i--)
            {
               PlayerController PC = parentScene.GetComponent<PlayerController>(registeredEntities[i]);
               foreach (KeyValuePair<ControlType, GameControl> kvp in PC.controls)
               {
                  if (kvp.Value.isControlValue(mousePressedEvent.button))
                  {
                     parentScene.bus.Publish(this, new ControlEvent(registeredEntities[i], kvp.Key, mousePressedEvent.controlState));
                  }
               }
            }
         }
      }
   }
}
