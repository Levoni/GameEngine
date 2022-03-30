using Base.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI.Mobile
{
   [Serializable]
   public class Button: Control
   {
      public Button() : base()
      {
         Name = "button";
         value = "Click";
         bounds = new EngineRectangle(0, 0, 200, 100);
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "button_default_" + Enums.cState.none.ToString());
         imageReference.Add(Enums.cState.hover.ToString(), "button_default_" + Enums.cState.hover.ToString());
         imageReference.Add(Enums.cState.pressed.ToString(), "button_default_" + Enums.cState.pressed.ToString());
         imageReference.Add(Enums.cState.released.ToString(), "button_default_" + Enums.cState.released.ToString());
         textAnchor = Enums.TextAchorLocation.center;
         init();
      }

      public Button(string name, string value, EngineRectangle bounds, Color color) : base(name, value, bounds, color)
      {
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "button_default_" + Enums.cState.none.ToString());
         imageReference.Add(Enums.cState.hover.ToString(), "button_default_" + Enums.cState.hover.ToString());
         imageReference.Add(Enums.cState.pressed.ToString(), "button_default_" + Enums.cState.pressed.ToString());
         imageReference.Add(Enums.cState.released.ToString(), "button_default_" + Enums.cState.released.ToString());
         textAnchor = Enums.TextAchorLocation.center;
         init();
      }
   }
}
