using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Utility;
using Base.Events;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Base.UI
{
   [Serializable]
   public class Label : Control
   {
      public Label() : base()
      {
         Name = "label";
         value = "";
         bounds = new EngineRectangle(0, 0, 200, 100);
         init();
      }

      public Label(string name, string value, EngineRectangle bounds, Color textColor) : base(name, value, bounds, textColor)
      {
         init();
      }

      public override void Render(SpriteBatch sb)
      {
         if (isEnabled)
         {
            EngineRectangle textBounds = new EngineRectangle(bounds.X + padding[3], bounds.Y + padding[0], bounds.Width - (padding[1] + padding[3]), bounds.Height - (padding[0] + padding[2]));

            Vector2 textAnchorVector2 = new Vector2();
            EngineVector2 textOrigin = new EngineVector2();
            // Determine anchor Location
            if (textAnchor == Enums.TextAchorLocation.topLeft)
            {
               textAnchorVector2 = new Vector2(textBounds.X, textBounds.Y);
               textOrigin = new EngineVector2(0, 0);
            }
            else if (textAnchor == Enums.TextAchorLocation.center)
            {
               textAnchorVector2 = new Vector2(textBounds.X + textBounds.Width / 2, textBounds.Y + textBounds.Height / 2);
               textOrigin = new EngineVector2(.5f, .5f);
            }
            else if (textAnchor == Enums.TextAchorLocation.centerLeft)
            {
               textAnchorVector2 = new Vector2(textBounds.X, textBounds.Y + textBounds.Height / 2);
               textOrigin = new EngineVector2(0, .5f);
            }

            //TODO: get font origin working corectly


            if (!isMultiLine)
            {
               EngineVector2 textScale = RenderUtil.CalculateFontScaling(value, textBounds, font);
               EngineVector2 fontSize = new EngineVector2(font.MeasureString(value).X, font.MeasureString(value).Y);
               var minimumScale = Math.Min(textScale.X, textScale.Y);
               if (minimumScale < minFontScale)
                  minimumScale = minFontScale;
               if (minimumScale > maxFontScale)
                  minimumScale = maxFontScale;
               sb.DrawString(font, value, textAnchorVector2, textColor, 0, new Vector2(fontSize.X * textOrigin.X, fontSize.Y * textOrigin.Y), minimumScale, SpriteEffects.None, 0);
            }
            else
            {
               EngineVector2 textScale = RenderUtil.CalculateFontScaling(value, textBounds, font);
               // Create return object containing calculated scale, total line height, lines (string array of lines)
               var lines = RenderUtil.CalculateMultilineFontScale(value, textBounds, font, minFontScale, maxFontScale, out float calculatedScale);
               var totalheight = font.MeasureString(lines[0]).Y * calculatedScale * lines.Count();
               for (int i = 0; i < lines.Count(); i++)
               {
                  var textSize = font.MeasureString(lines[i]) * calculatedScale;
                  if (textAnchor == Enums.TextAchorLocation.topLeft)
                  {
                     sb.DrawString(font, lines[i], textAnchorVector2 + new Vector2(0, (i * textSize.Y)), textColor, 0, new Vector2((textSize.X * calculatedScale * textOrigin.X), (textSize.Y * calculatedScale * textOrigin.Y)), calculatedScale, SpriteEffects.None, 0);
                  }
                  else if (textAnchor == Enums.TextAchorLocation.centerLeft)
                  {
                     sb.DrawString(font, lines[i], textAnchorVector2 + new Vector2(0, (totalheight / 2) + (i * textSize.Y)), textColor, 0, new Vector2((textSize.X * calculatedScale * textOrigin.X), (textSize.Y * calculatedScale * textOrigin.Y)), calculatedScale, SpriteEffects.None, 0);
                  }
                  else if (textAnchor== Enums.TextAchorLocation.center)
                  {
                     sb.DrawString(font, lines[i], textAnchorVector2 + new Vector2(0, (totalheight / 2) + (i * textSize.Y)), textColor, 0, new Vector2((textSize.X * calculatedScale * textOrigin.X), (textSize.Y * calculatedScale * textOrigin.Y)), calculatedScale, SpriteEffects.None, 0);
                  }
               }
            }
         }
      }
   }
}
