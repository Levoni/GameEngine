using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Utility;
using Base.Components;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Base.Collision
{
   public interface ICollisionBound2D
   {
      Enums.colliderShape Shape { get; set; }
      bool IsTrigger { get; set; }
      string TriggerType { get; set; }
      int collisionMaskId { get; set; }

      void DetermineCollisionHeightWidth(Transform t, Sprite s, out float width, out float height);

      List<EngineVector2> GetVerticiesList(Transform t, Sprite s);

      List<string> GetSpacialGridHashKeys(float gridBoxWidth, float gridBoxHeight, Transform t, Sprite s);

      EngineVector2 GetColliderCenterPoint(Transform t, Sprite s);

      EngineVector2 GetImageCenterPoint(Transform t, Sprite s);
   }
}
