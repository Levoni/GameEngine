using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Components
{
   [Serializable]
   public class Transform : Component<Transform>
   {
      public float X { get; set; }
      public float Y { get; set; }
      public float rotation { get; set; }
      public float widthRatio { get; set; }
      public float heightRatio { get; set; }

      public Transform() : base()
      {
         X = 0;
         Y = 0;
         widthRatio = 1;
         heightRatio = 1;
         rotation = 0;
      }

      public Transform(float x, float y, float widthRatio, float heightRatio, float rotation) : base()
      {
         this.X = x;
         this.Y = y;
         this.widthRatio = widthRatio;
         this.heightRatio = heightRatio;
         this.rotation = rotation;
      }

   }
}
