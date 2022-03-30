using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Components;
using Base.Entities;

namespace Base.Collision
{
   [Serializable]
   public class ColliderMapTwoDEntity
   {
      public ICollisionBound2D collider;
      public Entity owner;
      public Transform transform;
      public Sprite sprite;

      public ColliderMapTwoDEntity(ICollisionBound2D colliderTwoD, Entity owner, Transform transform, Sprite sprite)
      {
         this.collider = colliderTwoD;
         this.owner = owner;
         this.transform = transform;
         this.sprite = sprite;
      }
   }
}
