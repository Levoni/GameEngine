using Base.Utility;
using Base.Utility.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI
{
   [Serializable]
   public class ListBoxItem:IListBoxItem
   {
      public object Value { get; set; }
      public bool isSelected { get; set; }
      public float MaxFontSize { get; set; }
      public float MinFontSize { get; set; }
      protected SpriteFont font;

      public ListBoxItem()
      {
         font = ContentService.GetSpriteFont("defaultFont");
         isSelected = false;
         Value = null;
      }

      public ListBoxItem(object item)
      {
         font = ContentService.GetSpriteFont("defaultFont");
         isSelected = false;
         Value = item;
      }

      public virtual void RenderToBounds(SpriteBatch sb, EngineRectangle renderBounds)
      {
         if (isSelected)
         {
            Vector2 fontSize = font.MeasureString(Value.ToString());
            EngineVector2 vectorScaling = RenderUtil.CalculateFontScaling(Value.ToString(), renderBounds, font);
            float scaling = Math.Min(vectorScaling.X, vectorScaling.Y);
            if(scaling > MaxFontSize)
            {
               scaling = MaxFontSize;
            }
            else if (scaling < MinFontSize)
            {
               scaling = MinFontSize;
            }
            sb.DrawString(font, Value.ToString(), new Vector2(renderBounds.X, renderBounds.Y + renderBounds.Height/2), Color.Blue, 0, new Vector2(0, fontSize.Y / 2), scaling, SpriteEffects.None, 0);
         }
         else
         {
            Vector2 fontSize = font.MeasureString(Value.ToString());
            EngineVector2 vectorScaling = RenderUtil.CalculateFontScaling(Value.ToString(), renderBounds, font);
            float scaling = Math.Min(vectorScaling.X, vectorScaling.Y);
            if (scaling > MaxFontSize)
            {
               scaling = MaxFontSize;
            }
            else if (scaling < MinFontSize)
            {
               scaling = MinFontSize;
            }
            sb.DrawString(font, Value.ToString(), new Vector2(renderBounds.X, renderBounds.Y + renderBounds.Height / 2), Color.Black, 0, new Vector2(0, fontSize.Y / 2), scaling, SpriteEffects.None, 0);
         }
      }
   }
}
