using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Utility;
using Microsoft.Xna.Framework.Input;

namespace Base.Events
{
   [Serializable]
   public class KeyPressedEvent
   {
      public Enums.gameControlState keyState;
      public Keys key;

      public KeyPressedEvent()
      {
         keyState = Enums.gameControlState.keyNotPressed;
         key = Keys.None;
      }

      public KeyPressedEvent(Keys key, Enums.gameControlState keyState)
      {
         this.key = key;
         this.keyState = keyState;
      }
   }
}
