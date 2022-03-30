using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   public static class RenderUtil
   {
      static public EngineVector2 CalculateFontScaling(string stringToDraw, EngineRectangle boundary, SpriteFont font)
      {
         Vector2 size = font.MeasureString(stringToDraw);
         EngineVector2 scale = new EngineVector2(boundary.Width / size.X, boundary.Height / size.Y);
         return scale;
      }

      static public List<string> CalculateMultilineSingleFontScale(string stringToDraw, EngineRectangle boundary, SpriteFont font, float scale)
      {
         string[] words = stringToDraw.Split(' ');
         List<string> lines = new List<string>();
         string lastTestString = string.Empty;
         string curLine = string.Empty;
         int wordIndex = 0;
         if (words.Length > 0)
         {
            curLine += words[0];
            wordIndex++;
         }
         while (wordIndex < words.Length)
         {
            lastTestString = curLine;
            curLine += " " + words[wordIndex];

            Vector2 lineSize = font.MeasureString(curLine);
            if (lineSize.X * scale > boundary.Width)
            {
               lines.Add(lastTestString);
               curLine = words[wordIndex];
            }
            wordIndex++;
         }
         lines.Add(curLine);

         return lines;
      }

      static public List<string> CalculateMultilineFontScale(string stringToDraw, EngineRectangle boundary, SpriteFont font, float minScale, float MaxScale, out float endScale)
      {
         float currentScale = MaxScale;
         float scaleStep = currentScale / 5;
         scaleStep = Math.Min(.1f, scaleStep);
         bool fits = false;
         while (!fits)
         {
            Vector2 stringSize = font.MeasureString(stringToDraw);
            stringSize *= currentScale;
            int minimumRows = (int)(stringSize.X / boundary.Width) + 1;
            int totalHeight = (int)(minimumRows * stringSize.Y);
            if (totalHeight < boundary.Height)
            {
               fits = true;
            }
            else
            {
               currentScale -= scaleStep;
               if (currentScale < minScale)
               {
                  currentScale = minScale;
                  break;
               }
            }
         }

         fits = false;
         List<string> lines = new List<string>();

         while (!fits)
         {
            string[] words = stringToDraw.Split(' ');
            string lastTestString = string.Empty;
            string curLine = string.Empty;
            int wordIndex = 0;
            if (words.Length > 0)
            {
               curLine += words[0];
               wordIndex++;
            }
            while (wordIndex < words.Length)
            {
               lastTestString = curLine;
               curLine += " " + words[wordIndex];

               Vector2 lineSize = font.MeasureString(curLine);
               if (lineSize.X * currentScale > boundary.Width)
               {
                  lines.Add(lastTestString);
                  curLine = words[wordIndex];
               }
               wordIndex++;
            }
            lines.Add(curLine);

            // Check if the lines total height is greater than the boundary height
            float totalheight = 0;
            foreach(string s in lines)
            {
               totalheight += font.MeasureString(stringToDraw).Y * currentScale;
            }


            if (totalheight <= boundary.Height)
            {
               fits = true;
            }
            else
            {
               if (currentScale == minScale)
               {
                  break;
               }
               else
               {
                  currentScale -= scaleStep;
                  lines.Clear();
               }
            }
         }
         endScale = currentScale;
         return lines;
      }
   }
}
