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
   public class PolygonCollisionBound2D : ICollisionBound2D
   {
      public Enums.colliderShape Shape { get; set; }
      public bool IsTrigger { get; set; }
      public string TriggerType { get; set; }
      public int collisionMaskId { get; set; }
      public List<EngineVector2> verticyRatios;



      public PolygonCollisionBound2D()
      {
         Shape = Enums.colliderShape.Polygon;
         IsTrigger = false;
         verticyRatios = new List<EngineVector2>();
         TriggerType = "";
         collisionMaskId = 0;
      }

      public PolygonCollisionBound2D(List<EngineVector2> verticyRatios, int collisionMaskId, bool isTrigger = false, string  triggerType = "")
      {
         Shape = Enums.colliderShape.Polygon;
         IsTrigger = isTrigger;
         this.collisionMaskId = collisionMaskId;
         this.verticyRatios = verticyRatios;
         this.TriggerType = triggerType;
      }

      public void DetermineCollisionHeightWidth(Transform t, Sprite s, out float width, out float height)
      {
         //float minXRatio = float.MaxValue;
         //float minYRatio = float.MaxValue;
         //float maxXRatio = float.MinValue;
         //float maxYRatio = float.MinValue;

         //foreach (EngineVector2 vector2 in verticyRatios)
         //{
         //   minXRatio = Math.Min(minXRatio, vector2.X);
         //   maxXRatio = Math.Max(maxXRatio, vector2.X);
         //   minYRatio = Math.Min(minYRatio, vector2.Y);
         //   maxYRatio = Math.Max(maxYRatio, vector2.Y);
         //}

         //width = (maxXRatio - minXRatio) * t.widthRatio * s.imageWidth;
         //height = (maxYRatio - minYRatio) * t.heightRatio * s.imageHeight;

         List<EngineVector2> vectors = GetVerticiesList(t, s);

         EngineVector2 center = GetColliderCenterPoint(t, s);
         float magnitude = 0;
         int index = -1;
         for (int i = 0; i < vectors.Count; i++)
         {
            float newMagnitude = (vectors[i] - center).ToMagnitudeSquared();
            if (magnitude < newMagnitude)
            {
               magnitude = newMagnitude;
               index = i;
            }
         }

         width = (float)Math.Sqrt((vectors[index] - center).ToMagnitudeSquared()) * 2;
         height = (float)Math.Sqrt((vectors[index] - center).ToMagnitudeSquared()) * 2;
      }


      public EngineVector2 GetColliderCenterPoint(Transform t, Sprite s)
      {
         float minXRatio = float.MaxValue;
         float minYRatio = float.MaxValue;
         float maxXRatio = float.MinValue;
         float maxYRatio = float.MinValue;

         foreach (EngineVector2 vector2 in verticyRatios)
         {
            minXRatio = Math.Min(minXRatio, vector2.X);
            maxXRatio = Math.Max(maxXRatio, vector2.X);
            minYRatio = Math.Min(minYRatio, vector2.Y);
            maxYRatio = Math.Max(maxYRatio, vector2.Y);
         }

         return new EngineVector2(t.X + ((maxXRatio - minXRatio) * t.widthRatio * s.imageWidth), t.Y + ((maxYRatio - minYRatio) * t.heightRatio * s.imageHeight));
      }

      public EngineVector2 GetImageCenterPoint(Transform t, Sprite s)
      {
         return new EngineVector2(t.X + ((s.imageWidth * t.widthRatio) / 2), t.Y + ((s.imageHeight * t.heightRatio) / 2));
      }

      public List<EngineVector2> GetVerticiesList(Transform t, Sprite s)
      {
         List<EngineVector2> verticies = new List<EngineVector2>();
         foreach (EngineVector2 ev in verticyRatios)
         {
            float vertX = t.X + (ev.X * t.widthRatio * s.imageWidth);
            float vertY = t.Y + (ev.Y * t.heightRatio * s.imageHeight);
            verticies.Add(new EngineVector2(vertX, vertY));
         }

         foreach (EngineVector2 vector in verticies)
         {
            vector.RotateVectorAroundPoint(GetImageCenterPoint(t, s), t.rotation);
         }

         return verticies;
      }

      public List<string> GetSpacialGridHashKeys(float gridBoxWidth, float gridBoxHeight, Transform t, Sprite s)
      {
         List<string> hashKeys = new List<string>();

         float minXRatio = float.MaxValue;
         float minYRatio = float.MaxValue;
         float maxXRatio = float.MinValue;
         float maxYRatio = float.MinValue;

         foreach (EngineVector2 vector2 in verticyRatios)
         {
            minXRatio = Math.Min(minXRatio, vector2.X);
            maxXRatio = Math.Max(maxXRatio, vector2.X);
            minYRatio = Math.Min(minYRatio, vector2.Y);
            maxYRatio = Math.Max(maxYRatio, vector2.Y);
         }

         float xStart = (t.X + minXRatio * (t.widthRatio * s.imageWidth));
         float yStart = (t.Y + minYRatio * (t.heightRatio * s.imageHeight));
         float xEnd = xStart + (maxXRatio * (t.widthRatio * s.imageWidth));
         float yEnd = yStart + (maxYRatio * (t.heightRatio * s.imageHeight));

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
