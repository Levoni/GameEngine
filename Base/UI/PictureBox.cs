using Base.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI
{
   public class PictureBox : Control
   {
      public PictureBox() : base()
      {
         Name = "button";
         value = "Click";
         bounds = new EngineRectangle(0, 0, 200, 100);
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "nonw");
         imageReference.Add(Enums.cState.hover.ToString(), "none");
         imageReference.Add(Enums.cState.pressed.ToString(), "none");
         imageReference.Add(Enums.cState.released.ToString(), "none");
         textAnchor = Enums.TextAchorLocation.center;
         init();
      }

      public PictureBox(string name, EngineRectangle bounds, Color color, string imageReference) : base(name, "", bounds, color)
      {
         this.imageReference = new Serialization.SerializableDictionary<string, string>();
         this.imageReference.Add(Enums.cState.none.ToString(), imageReference);
         this.imageReference.Add(Enums.cState.hover.ToString(), imageReference);
         this.imageReference.Add(Enums.cState.pressed.ToString(), imageReference);
         this.imageReference.Add(Enums.cState.released.ToString(), imageReference);
         textAnchor = Enums.TextAchorLocation.center;
         padding = new int[]
         {
            0,50,0,50
         };
         init();
      }
   }
}
