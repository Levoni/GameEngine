using Base.Serialization;
using Base.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI
{
   public class CheckBox:Control
   {
      public bool IsChecked { get; set; }

      public CheckBox():base() { }
      public CheckBox(string name, bool value, EngineRectangle bounds):base(name, "", bounds, Color.White)
      {
         IsChecked = value;
         var newImages = new SerializableDictionary<string, string>();
         newImages.Add(Enums.cState.none.ToString() + "unchecked", "button_square_outline");
         newImages.Add(Enums.cState.hover.ToString() + "unchecked", "button_square_outline_hover");
         newImages.Add(Enums.cState.pressed.ToString() + "unchecked", "button_square_outline_hover");
         newImages.Add(Enums.cState.released.ToString() + "unchecked", "button_square_outline_hover");
         newImages.Add(Enums.cState.none.ToString() + "checked", "button_square_Checked");
         newImages.Add(Enums.cState.hover.ToString() + "checked", "button_square_Checked_hover");
         newImages.Add(Enums.cState.pressed.ToString() + "checked", "button_square_Checked_hover");
         newImages.Add(Enums.cState.released.ToString() + "checked", "button_square_Checked_hover");
         imageReference = newImages;
         init();
      }

      public override void Click()
      {
         IsChecked = !IsChecked;
         ValueChange(!IsChecked, IsChecked);
         base.Click();

      }

      public override void ValueChange(object oldValue, object newValue)
      {
         var oValue = (bool)oldValue;
         var nValue = (bool)newValue;
         base.ValueChange(oValue,nValue);
      }


      public override void Render(SpriteBatch sb)
      {
         string imageKey = IsChecked ? clickState.ToString() + "checked" : clickState.ToString() + "unchecked";
         sb.Draw(images[imageKey], bounds.toRectangle(), null, drawColor, 0, new Vector2(0, 0), SpriteEffects.None, 0);
      }
   }
}
