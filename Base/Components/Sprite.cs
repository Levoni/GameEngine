using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace Base.Components
{
   [Serializable]
   public class Sprite:Component<Sprite>
   {
      public string TwoDAssetName;
      public int imageWidth;
      public int imageHeight;
      public int zOrder;
      [XmlIgnore, NonSerialized]
      public Texture2D image;

      public Sprite()
      {
         TwoDAssetName = "none";
         imageWidth = 0;
         imageHeight = 0;
         zOrder = 1;
         image = null;
      }

      public Sprite(string assetName)
      {
         TwoDAssetName = assetName;
         zOrder = 1;
         LoadContent(TwoDAssetName);
         imageWidth = image.Width;
         imageHeight = image.Height;
      }

      public Sprite(string assetName, int zOrder)
      {
         TwoDAssetName = assetName;
         this.zOrder = zOrder;
         LoadContent(TwoDAssetName);
         imageWidth = image.Width;
         imageHeight = image.Height;
      }

      public void SetSize(int width, int height)
      {
         imageWidth = width;
         imageHeight = height;
      }

      public override void Init()
      {
         LoadContent(TwoDAssetName);
      }

      public void LoadContent(string assetName)
      {
         if (TwoDAssetName != null)
         {
            image = Utility.Services.ContentService.Get2DTexture(TwoDAssetName);
         }
         else
            image = Utility.Services.ContentService.Get2DTexture("defaultTextur");
      }
   }
}
