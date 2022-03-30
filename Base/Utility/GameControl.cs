using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Base.Utility;

namespace Base.Utility
{
   [Serializable]
   public class GameControl
   {
      private Keys keyValue;
      private Enums.mouseButton mouseValue;

      public GameControl()
      {
         keyValue = Keys.None;
         mouseValue = Enums.mouseButton.none;
      }

      public GameControl(Keys keyValue)
      {
         this.keyValue = keyValue;
         mouseValue = Enums.mouseButton.none;
      }

      public GameControl(Enums.mouseButton mouseValue)
      {
         this.mouseValue = mouseValue;
         keyValue = Keys.None;
      }

      public void setControlValue(Keys keyValue)
      {
         this.keyValue = keyValue;
         mouseValue = Enums.mouseButton.none;
      }

      public void setControlValue(Enums.mouseButton mouseValue)
      {
         this.mouseValue = mouseValue;
         this.keyValue = Keys.None;
      }

      public bool isControlValue(Keys testKey)
      {
         return keyValue == testKey;
      }

      public bool isControlValue(Enums.mouseButton testMouseButton)
      {
         return testMouseButton == mouseValue;
      }

      public string currentControlValue()
      {
         if (keyValue != Keys.None)
            return keyValue.ToString();
         else if (mouseValue != Enums.mouseButton.none)
            return mouseValue.ToString();
         else
            return "none";
      }
   }
}
