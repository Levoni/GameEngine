using Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Components;

namespace Base.Collision
{
   [Serializable]
   public class CircleCollisionBound2D:ICollisionBound2D
   {
      public Enums.colliderShape Shape { get; set; }
      public bool IsTrigger { get; set; }
      public string TriggerType { get; set; }
      public int collisionMaskId { get; set; }
      public float Radius;
      public float XCenterRatio;
      public float YCenterRatio;

      public CircleCollisionBound2D()
      {
         Shape = Enums.colliderShape.Circle;
         IsTrigger = false;
         TriggerType = "";
         collisionMaskId = 0;
         Radius = 0;
         XCenterRatio = 0;
         YCenterRatio = 0;
      }

      public CircleCollisionBound2D(float Radius, float XCenterRatio, float YCenterRatio, int collisionMaskId, bool isTrigger = false, string triggerType = "")
      {
         Shape = Enums.colliderShape.Circle;
         IsTrigger = isTrigger;
         this.TriggerType = triggerType;
         this.collisionMaskId = collisionMaskId;
         this.Radius = Radius;
         this.XCenterRatio = XCenterRatio;
         this.YCenterRatio = YCenterRatio;
      }

      public void DetermineCollisionHeightWidth(Transform t, Sprite s, out float width, out float height)
      {
         float newDiamater = Radius * 2 * s.imageWidth * t.widthRatio;
         width = newDiamater;
         height = newDiamater;
      }

      // Returns empty list since there isn't any verticies
      public List<EngineVector2> GetVerticiesList(Transform t, Sprite s)
      {
         List<EngineVector2> verticies = new List<EngineVector2>();
         return verticies;
      }

      public List<string> GetSpacialGridHashKeys(float gridBoxWidth, float gridBoxHeight, Transform t, Sprite s)
      {
         List<string> hashKeys = new List<string>();
         float tempRadius = (Radius * (t.widthRatio * s.imageWidth));
         float xCenter = (t.X + XCenterRatio * (t.widthRatio * s.imageWidth));
         float yCenter = (t.Y + YCenterRatio * (t.heightRatio * s.imageHeight));


         float xStart = xCenter - tempRadius;
         float yStart = yCenter - tempRadius;
         float xEnd = xCenter + tempRadius;
         float yEnd = yCenter + tempRadius;

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


      #region class specific funcitons

      public EngineVector2 GetColliderCenterPoint(Transform t, Sprite s)
      {
         float col1XStart = t.X + (XCenterRatio * t.widthRatio * s.imageWidth);
         float col1YStart = t.Y + (YCenterRatio * t.heightRatio * s.imageHeight);
         return new EngineVector2(col1XStart, col1YStart);
      }

      public EngineVector2 GetImageCenterPoint(Transform t, Sprite s)
      {
         return new EngineVector2(t.X + ((s.imageWidth * t.widthRatio) / 2), t.Y + ((s.imageHeight * t.heightRatio) / 2));
      }

      public float GetRadiusMagnitude(Transform t, Sprite s)
      {
         return (Radius * (t.widthRatio * s.imageWidth));
      }


      #endregion
   }
}
