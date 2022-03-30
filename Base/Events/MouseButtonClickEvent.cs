using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Entities;
using Base.Utility;

namespace Base.Events
{
   [Serializable]
   public class MousePressedEvent:Event
   {
      public Enums.mouseButton button;
      public Enums.gameControlState controlState;

      public MousePressedEvent()
      {
         button = Enums.mouseButton.none;
         controlState = Enums.gameControlState.keyNotPressed;
      }

      public MousePressedEvent(Enums.mouseButton button, Enums.gameControlState controlState)
      {
         this.button = button;
         this.controlState = controlState;
      }
   }
}
