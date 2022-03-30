using Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Components
{
   [Serializable]
   public class RigidBody2D:Component<RigidBody2D>
   {
      //TODO: Add friction coefficient  eventually
      public float weight;
      public bool isStatic;
      public EngineVector2 velocity;
      public bool gravity;
      public float friciton;

      public RigidBody2D()
      {
         weight = 1;
         isStatic = false;
         gravity = false;
         velocity = new EngineVector2();
         friciton = .5f;
      }

      public RigidBody2D(float weight, bool isStatic, bool gravity, float friction)
      {
         this.weight = weight;
         this.isStatic = isStatic;
         this.gravity = gravity;
         this.velocity = new EngineVector2(); ;
         this.friciton = friction;
      }
   }
}
