using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Utility;
using Base.Components;

namespace Base.Collision
{
   [Serializable]
   public class BoxCollisionBound2D:ICollisionBound2D
   {
      public Enums.colliderShape Shape { get; set; }
      public bool IsTrigger { get; set; }
      public string TriggerType { get; set; }
      public int collisionMaskId { get; set; }
 
      public float xStartRatio;
      public float yStartRatio;
      public float widthRatio;
      public float heightRatio;

      public BoxCollisionBound2D()
      {
         Shape = Enums.colliderShape.Box;
         IsTrigger = false;
         collisionMaskId = 0;
         xStartRatio = 0;
         yStartRatio = 0;
         widthRatio = 0;
         heightRatio = 0;
         TriggerType = "";
      }

      public BoxCollisionBound2D(float xStartRatio, float yStartRatio, float widthRatio, float heightRatio, int collisionMaskId, bool isTrigger = false, string triggerType = "")
      {
         Shape = Enums.colliderShape.Box;
         IsTrigger = isTrigger;
         this.collisionMaskId = collisionMaskId;
         this.TriggerType = triggerType;
         this.xStartRatio = xStartRatio;
         this.yStartRatio = yStartRatio;
         this.widthRatio = widthRatio;
         this.heightRatio = heightRatio;
      }

      public void DetermineCollisionHeightWidth(Transform t, Sprite s, out float width, out float height)
      {
         List<EngineVector2> vectors = GetVerticiesList(t, s);

         EngineVector2 center = GetColliderCenterPoint(t, s);
         float magnitude = 0;
         int index = -1;
         for (int i = 0; i < vectors.Count; i ++)
         {
            float newMagnitude =  (vectors[i] - center).ToMagnitudeSquared();
            if (magnitude < newMagnitude)
            {
               magnitude = newMagnitude;
               index = i;
            }
         }

         width = (float)Math.Sqrt((vectors[index] - center).ToMagnitudeSquared()) * 2;
         height = (float)Math.Sqrt((vectors[index]- center).ToMagnitudeSquared()) * 2;
      }

      public EngineVector2 GetColliderCenterPoint(Transform t, Sprite s)
      {
         float col1XStart = t.X + (xStartRatio * t.widthRatio * s.imageWidth);
         float col1YStart = t.Y + (yStartRatio * t.heightRatio * s.imageHeight);
         float centerX = col1XStart + ((widthRatio * t.widthRatio * s.imageWidth) / 2);
         float centerY = col1YStart + ((heightRatio * t.heightRatio * s.imageHeight) / 2);
         return new EngineVector2(centerX, centerY);
      }

      public EngineVector2 GetImageCenterPoint(Transform t, Sprite s)
      {
         return new EngineVector2(t.X + ((s.imageWidth * t.widthRatio) / 2), t.Y + ((s.imageHeight * t.heightRatio) / 2));
      }

      public List<EngineVector2> GetVerticiesList(Transform t, Sprite s)
      {
         List<EngineVector2> verticies = new List<EngineVector2>();
         float col1XStart = t.X + (xStartRatio * t.widthRatio * s.imageWidth);
         float col1YStart = t.Y + (yStartRatio * t.heightRatio * s.imageHeight);
         float col1XEnd = col1XStart + (widthRatio * t.widthRatio * s.imageWidth);
         float col1YEnd = col1YStart + (heightRatio * t.heightRatio * s.imageHeight);
         verticies.Add(new EngineVector2 { X = col1XStart, Y = col1YStart });
         verticies.Add(new EngineVector2 { X = col1XStart, Y = col1YEnd });
         verticies.Add(new EngineVector2 { X = col1XEnd, Y = col1YEnd });
         verticies.Add(new EngineVector2 { X = col1XEnd, Y = col1YStart });

         foreach(EngineVector2 vector in verticies)
         {
            vector.RotateVectorAroundPoint(GetImageCenterPoint(t,s), t.rotation);
         }

         return verticies;
      }
      
      public List<string> GetSpacialGridHashKeys(float gridBoxWidth, float gridBoxHeight, Transform t, Sprite s)
      {
         List<string> hashKeys = new List<string>();
         float xStart = (t.X + (xStartRatio * (t.widthRatio * s.imageWidth)));
         float yStart = (t.Y + (yStartRatio * (t.heightRatio * s.imageHeight)));
         float xEnd = (xStart + (widthRatio * (t.widthRatio * s.imageWidth)));
         float yEnd = (yStart + (heightRatio * (t.heightRatio * s.imageHeight)));

         int minXSpacialColliderBox = gridBoxWidth != 0 ? (int)(xStart / gridBoxWidth) : 0;
         int maxXSpacialColliderBox = gridBoxWidth != 0 ? (int)((xEnd) / gridBoxWidth) : 0;
         int minYSpacialColliderBox = gridBoxHeight != 0 ? (int)(yStart / gridBoxHeight) : 0;
         int maxYSpacialColliderBox = gridBoxHeight != 0 ? (int)((yEnd) / gridBoxHeight) : 0;

         for (float x = minXSpacialColliderBox; x <= maxXSpacialColliderBox; x++)
            for (float y = minYSpacialColliderBox; y <= maxYSpacialColliderBox; y++)
            {
               hashKeys.Add($"{x},{y}");
            }
         return hashKeys;
      }

   }
}
