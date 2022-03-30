using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Base.Utility.Services
{
   public static class ContentService
   {
      public static bool isInitialized = false;
      public static bool usesCache = true;
      public static Dictionary<string, Texture2D> textureCache;
      public static Dictionary<string, SpriteFont> fontCache;
      static ContentManager content;

      public static void InitService(ContentManager cm)
      {
         isInitialized = true;
         textureCache = new Dictionary<string, Texture2D>();
         fontCache = new Dictionary<string, SpriteFont>();
         content = cm;
      }

      public static Texture2D Get2DTexture(string assetName)
      {
         if(usesCache)
         {
            if(textureCache.ContainsKey(assetName))
            {
               return textureCache[assetName];
            }
            Texture2D loadedTexture = content.Load<Texture2D>(assetName);
            textureCache[assetName] = loadedTexture;
            return loadedTexture;
         }
         return content.Load<Texture2D>(assetName);
      }

      public static SpriteFont GetSpriteFont(string fontName)
      {
         if (usesCache)
         {
            if (fontCache.ContainsKey(fontName))
            {
               return fontCache[fontName];
            }
            SpriteFont loadedFont = content.Load<SpriteFont>(fontName);
            fontCache[fontName] = loadedFont;
            return loadedFont;
         }
         return content.Load<SpriteFont>(fontName);
      }
   }
}
