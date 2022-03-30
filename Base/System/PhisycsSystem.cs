using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Components;
using Base.Events;
using Base.Entities;
using Base.Scenes;
using Base.Collision;
using Base.Utility;

namespace Base.System
{
   [Serializable]
   public class PhisycsSystem:EngineSystem
   {
      EHandler<CollisionEvent> onCollision;
      EHandler<VelocityChangeEvent> onVelocityChange;
      EHandler<VelocitySetEvent> onVelocitySet;

      public PhisycsSystem(Scene s)
      {
         systemSignature = (uint)((1 << RigidBody2D.GetFamily()) | (1 << Transform.GetFamily()));
         registeredEntities = new List<Entity>();
         Init(s);
      }

      public PhisycsSystem()
      {
         systemSignature = (uint)((1 << RigidBody2D.GetFamily()) | (1 << Transform.GetFamily()));
         registeredEntities = new List<Entity>();
      }

      public override void Update(int dt)
      {
         // Gravity
         foreach(Entity e in registeredEntities)
         {
            RigidBody2D rg2d = parentScene.GetComponent<RigidBody2D>(e);
            if(rg2d.gravity && !rg2d.isStatic)
            {
               rg2d.velocity.Y += 9.8f;
            }
            if(rg2d.velocity.X != 0 || rg2d.velocity.Y != 0)
            {
               Transform transform = parentScene.GetComponent<Transform>(e);
               transform.X += rg2d.velocity.X * ((float)dt / 1000f);
               transform.Y += rg2d.velocity.Y * ((float)dt / 1000f);
               float calculatedXNegationForce = EngineMath.ClosestToZero(rg2d.velocity.X, rg2d.velocity.X * rg2d.friciton * ((float)dt / 1000f));
               rg2d.velocity.X -= calculatedXNegationForce;
               if(rg2d.velocity.X <= .1f && rg2d.velocity.X >= -.1f)
               {
                  rg2d.velocity.X = 0;
               }
               float calculatedYNegationForce = EngineMath.ClosestToZero(rg2d.velocity.Y, rg2d.velocity.Y * rg2d.friciton * ((float)dt / 1000f));
               rg2d.velocity.Y -= calculatedYNegationForce;
               if (rg2d.velocity.Y <= .1f && rg2d.velocity.Y >= -.1f)
               {
                  rg2d.velocity.Y = 0;
               }
            }
         }
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
         onCollision = new EHandler<CollisionEvent>(new Action<object, CollisionEvent>(HandleCollisionEvent));
         parentScene.bus.Subscribe(onCollision);


         onVelocityChange = new EHandler<VelocityChangeEvent>(new Action<object, VelocityChangeEvent>(HandleVelocityChange));
         parentScene.bus.Subscribe(onVelocityChange);

         onVelocitySet = new EHandler<VelocitySetEvent>(new Action<object, VelocitySetEvent>(HandleVelocitySet));
         parentScene.bus.Subscribe(onVelocitySet);

      }

      public override void Terminate()
      {
         base.Terminate();

         if (parentScene != null)
         {
            parentScene.bus.Unsubscribe(onCollision);
            parentScene.bus.Unsubscribe(onVelocityChange);
            parentScene.bus.Unsubscribe(onVelocitySet);
         }

      }

      public void HandleCollisionEvent(object sender, CollisionEvent e)
      {
         if(registeredEntities.Contains(e.context.entity1) &&
            registeredEntities.Contains(e.context.entity2))
         {
            HandleCollision(e.context);
         }
      }

      public virtual void HandleCollision(CollisionContext cc)
      {
         RigidBody2D RB1 = parentScene.GetComponent<RigidBody2D>(cc.entity1);
         RigidBody2D RB2 = parentScene.GetComponent<RigidBody2D>(cc.entity2);

         Transform T1 = parentScene.GetComponent<Transform>(cc.entity1);
         Transform T2 = parentScene.GetComponent<Transform>(cc.entity2);

         var dotProductOne = Utility.EngineVector2.dotProduct(cc.seperationContext.pushVector, new Utility.EngineVector2(T1.X, T1.Y));
         var dotProductTwo = Utility.EngineVector2.dotProduct(cc.seperationContext.pushVector, new Utility.EngineVector2(T2.X, T2.Y));

         //Makes sure the seperation vector is pointing from entity1 to entity2
         if (cc.seperationContext.direction == PushFromTo.EntityTwoToOne)
         {
            cc.seperationContext.pushVector *= -1;
         }


         if (RB1.isStatic)
         {
            T2.X += (float)Math.Ceiling(cc.seperationContext.pushVector.X);
            T2.Y += (float)Math.Ceiling(cc.seperationContext.pushVector.Y);
         }
         else if(RB2.isStatic)
         {
            T1.X -= (float)Math.Ceiling(cc.seperationContext.pushVector.X);
            T1.Y -= (float)Math.Ceiling(cc.seperationContext.pushVector.Y);
         }
         else
         {
            var totalWeight = RB1.weight + RB2.weight;

            Utility.EngineVector2 SeperationVectorOne = cc.seperationContext.pushVector * (RB1.weight / totalWeight);
            Utility.EngineVector2 SeperationVectorTwo = cc.seperationContext.pushVector * (RB2.weight / totalWeight);

            T1.X -= Utility.EngineMath.CelingAwayFromZero(SeperationVectorTwo.X);
            T1.Y -= Utility.EngineMath.CelingAwayFromZero(SeperationVectorTwo.Y);
            T2.X += Utility.EngineMath.CelingAwayFromZero(SeperationVectorOne.X);
            T2.Y += Utility.EngineMath.CelingAwayFromZero(SeperationVectorOne.Y);
         }
      }

      void HandleVelocityChange(object sender, VelocityChangeEvent move)
      {
         foreach (Entity e in registeredEntities)
         {
            if (e == move.MovedEntity)
            {
               RigidBody2D rigidBody2D = parentScene.GetComponent<RigidBody2D>(e);
               rigidBody2D.velocity.X += move.moveVector.X;
               rigidBody2D.velocity.Y += move.moveVector.Y;
               break;
            }
         }
      }
      void HandleVelocitySet(object sender, VelocitySetEvent velocitySet)
      {
         foreach (Entity e in registeredEntities)
         {
            if (e == velocitySet.MovedEntity)
            {
               RigidBody2D rigidBody2D = parentScene.GetComponent<RigidBody2D>(e);
               rigidBody2D.velocity.X = velocitySet.newVector.X;
               rigidBody2D.velocity.Y = velocitySet.newVector.Y;
               break;
            }
         }
      }
   }
}
