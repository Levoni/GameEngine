using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Scenes;
using Base.Components;
using Base.Entities;
using Base.Utility.Services;

using Microsoft.Xna.Framework.Input;

namespace Base.System
{
   [Serializable]
   public class InputSystem:EngineSystem
   {
      public InputSystem(Scene s)
      {
         systemSignature = 0;
         RegisterScene(s);
      }

      public InputSystem()
      {
         systemSignature = 0;
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }

      public override void Update(int dt)
      {
         base.Update(dt);

         KeyboardService.UpdateKeyboardState();
         List<Keys> keysDownList = KeyboardService.GetNewKeyDownKeys();
         foreach (Keys k in keysDownList)
         {
            parentScene.bus.Publish(this, new Events.KeyPressedEvent(k, Utility.Enums.gameControlState.keyDown));
         }

         List<Keys> keysPressedList = KeyboardService.currentState.GetPressedKeys().ToList();
         Keys[] oldlist = KeyboardService.oldState.GetPressedKeys();
         foreach(Keys k in keysPressedList)
         {
            if(keysDownList.FindIndex((x) => k == x) == -1)
            {
               parentScene.bus.Publish(this, new Events.KeyPressedEvent(k, Utility.Enums.gameControlState.keyPressed));
            }
         }
         //TODO: Add key up keypressedevents
         //TODO: Add mouse up MousePressedEvents
         MouseService.UpdateMouseState();
         if (MouseService.IsLeftButtonDown())
         {
            if(MouseService.DidLeftClickOccur())
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.leftClick, Utility.Enums.gameControlState.keyDown));
            }
            else
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.leftClick, Utility.Enums.gameControlState.keyPressed));
            }
         }
         if (MouseService.IsRightButtonDown())
         {
            if (MouseService.DidRightClickOccur())
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.rightClick, Utility.Enums.gameControlState.keyDown));
            }
            else
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.rightClick, Utility.Enums.gameControlState.keyPressed));
            }
         }
         if (MouseService.IsMiddleButtonDown())
         {
            if (MouseService.DidMiddleClickOccur())
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.rightClick, Utility.Enums.gameControlState.keyDown));
            }
            else
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.rightClick, Utility.Enums.gameControlState.keyPressed));
            }
         }
         if (MouseService.IsXOneButtonPresed())
         {
            if (MouseService.DidXOneClickOccur())
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.x1Click, Utility.Enums.gameControlState.keyDown));
            }
            else
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.x1Click, Utility.Enums.gameControlState.keyPressed));
            }
         }
         if (MouseService.IsXTwoButtonPresed())
         {
            if (MouseService.DidXTwoClickOccur())
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.x2Click, Utility.Enums.gameControlState.keyDown));
            }
            else
            {
               parentScene.bus.Publish(this, new Events.MousePressedEvent(Utility.Enums.mouseButton.x2Click, Utility.Enums.gameControlState.keyPressed));
            }
         }
      }
   }
}
