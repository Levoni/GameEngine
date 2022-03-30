using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Base.Utility;
using Base.Collision;


namespace Base.Components
{
   [Serializable]
   public class ColliderTwoD : Component<ColliderTwoD>
   {
      public Enums.colliderShape shape;
      public List<ICollisionBound2D> colliders;

      public ColliderTwoD()
      {
         colliders = new List<ICollisionBound2D>();
      }

      public ColliderTwoD(List<ICollisionBound2D> cols)
      {
         colliders = cols;
      }
   }
}
