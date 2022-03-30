using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Collision;
using Base.Components;


namespace Base.Utility.Services
{
   public class CollisionService
   {
      public static bool IsCollision(ColliderMapTwoDEntity entityOne, ColliderMapTwoDEntity entityTwo, out SeperationContext seperationContext)
      {
         seperationContext = new SeperationContext();
         seperationContext.pushVector = new EngineVector2(float.MaxValue, float.MaxValue);

         // If both objects are circles do special collision check
         if(entityOne.collider.Shape == Enums.colliderShape.Circle && entityTwo.collider.Shape == Enums.colliderShape.Circle)
         {
            //TODO: Clean up type conversion bc it is no longer necessary
            CircleCollisionBound2D CCBOne = entityOne.collider as CircleCollisionBound2D;
            CircleCollisionBound2D CCBTwo = entityTwo.collider as CircleCollisionBound2D;
            EngineVector2 centerOne = CCBOne.GetColliderCenterPoint(entityOne.transform, entityOne.sprite);
            EngineVector2 centerTwo = CCBTwo.GetColliderCenterPoint(entityTwo.transform, entityTwo.sprite);
            EngineVector2 centerOneToTwoVector = new EngineVector2(centerTwo.X - centerOne.X, centerTwo.Y - centerOne.Y);
            float radiusTotal = CCBOne.Radius + CCBTwo.Radius;
            float centerTotal = (float)Math.Sqrt(centerOneToTwoVector.ToMagnitudeSquared());
            if(radiusTotal > centerTotal)
            {
               float distance = radiusTotal - centerTotal;
               seperationContext = new SeperationContext(centerOneToTwoVector.ToUnitVector() * distance, PushFromTo.EntityOneToTwo);
               return true;
            }
            return false;
            // find distance between centers
            // find total amount of radiuses added together
            // find difference between center point distances and radiuses total
            // if (radiuses total > center point distance return the push vector or return null
         }

         List<EngineVector2> verts1 = entityOne.collider.GetVerticiesList(entityOne.transform, entityOne.sprite);
         List<EngineVector2> verts2 = entityTwo.collider.GetVerticiesList(entityTwo.transform, entityTwo.sprite);

         List<EngineVector2> edges = new List<EngineVector2>();
         foreach (EngineVector2 edge in GetEdges(verts1))
         {
            edges.Add(edge);
         }
         foreach (EngineVector2 edge in GetEdges(verts2))
         {
            edges.Add(edge);
         }

         List<EngineVector2> orthoginals = new List<EngineVector2>();

         foreach (EngineVector2 e in edges)
            orthoginals.Add(e.GetOrthoginal());

         //Creates vectors for circle either collider is a circle
         if(entityOne.collider.Shape == Enums.colliderShape.Circle)
         {
            CircleCollisionBound2D CCB = entityOne.collider as CircleCollisionBound2D;
            EngineVector2 center = CCB.GetColliderCenterPoint(entityOne.transform, entityOne.sprite);
            foreach (EngineVector2 vert in verts2)
            {
               EngineVector2 newEdge = new EngineVector2(vert.X - center.X, vert.Y - center.Y);
               EngineVector2 newOrthoginal = newEdge.GetOrthoginal();
               EngineVector2 orthUnit = newOrthoginal.ToUnitVector();
               orthoginals.Add(newEdge);
            }
            foreach(EngineVector2 orthoginal in  orthoginals)
            {
               // TODO: add addition as custom operator in the enginvector2 class

               EngineVector2 distanceVector = (orthoginal.ToUnitVector() * CCB.GetRadiusMagnitude(entityOne.transform, entityOne.sprite));
               //Add min and max verticies
               verts1.Add(new EngineVector2(center.X + distanceVector.X, center.Y + distanceVector.Y));
               verts1.Add(new EngineVector2(center.X - distanceVector.X, center.Y - distanceVector.Y));
            }

         }
         if (entityTwo.collider.Shape == Enums.colliderShape.Circle)
         {
            CircleCollisionBound2D CCB = entityTwo.collider as CircleCollisionBound2D;
            EngineVector2 center = CCB.GetColliderCenterPoint(entityTwo.transform, entityTwo.sprite);
            foreach (EngineVector2 vert in verts1)
            {
               EngineVector2 newEdge = new EngineVector2(vert.X - center.X, vert.Y - center.Y);
               EngineVector2 newOrthoginal = newEdge.GetOrthoginal();
               EngineVector2 orthUnit = newOrthoginal.ToUnitVector();
               orthoginals.Add(newEdge);
            }
            foreach (EngineVector2 orthoginal in orthoginals)
            {
               // TODO: add addition as custom operator in the enginvector2 class

               EngineVector2 distanceVector = (orthoginal.ToUnitVector() * CCB.GetRadiusMagnitude(entityTwo.transform, entityTwo.sprite));
               //Add min and max verticies
               verts2.Add(new EngineVector2(center.X + distanceVector.X, center.Y + distanceVector.Y));
               verts2.Add(new EngineVector2(center.X - distanceVector.X, center.Y - distanceVector.Y));
            }
         }

         SeperationContext MPV;
         foreach (EngineVector2 o in orthoginals)
         {
            if (IsSeperatingAxis(o, verts1, verts2, out MPV))
            {
               return false;
            }
            if (MPV.pushVector.ToMagnitudeSquared() < seperationContext.pushVector.ToMagnitudeSquared())
            {
               seperationContext.pushVector = MPV.pushVector;
               seperationContext.direction = MPV.direction;
            }
         }
         return true;
      }

       public static List<EngineVector2> GetEdges(List<EngineVector2> verticies)
      {
         List<EngineVector2> edges = new List<EngineVector2>();
         for (int i = 0; i < verticies.Count - 1; i++)
         {
            float x = verticies[i + 1].X - verticies[i].X;
            float y = verticies[i + 1].Y - verticies[i].Y;
            edges.Add(new EngineVector2 { X = x, Y = y });
         }
         if (verticies.Count >= 2)
         {
            edges.Add(new EngineVector2
            {
               X = verticies[0].X - verticies[verticies.Count - 1].X,
               Y = verticies[0].Y - verticies[verticies.Count - 1].Y
            });
         }
         return edges;
      }

      public static bool IsSeperatingAxis(EngineVector2 o, List<EngineVector2> objectOne, List<EngineVector2> objectTwo, out SeperationContext context)
      {
         float min1, max1, min2, max2;
         min1 = min2 = float.PositiveInfinity;
         max1 = max2 = float.NegativeInfinity;
         context = new SeperationContext();

         foreach (EngineVector2 e in objectOne)
         {
            float projection = EngineVector2.dotProduct(o, e);
            min1 = Math.Min(min1, projection);
            max1 = Math.Max(max1, projection);
         }

         foreach (EngineVector2 e in objectTwo)
         {
            float projection = EngineVector2.dotProduct(o, e);
            min2 = Math.Min(min2, projection);
            max2 = Math.Max(max2, projection);
         }
         //(enityx - entiyy) means push from entity entity x to entity Y
         if (max1 >= min2 && max2 >= min1)
         {
            PushFromTo pushDirection = max2 - min1 > max1 - min2 ? PushFromTo.EntityOneToTwo : PushFromTo.EntityTwoToOne;
            float distance = Math.Min(max2 - min1, max1 - min2);
            distance = distance / EngineVector2.dotProduct(o, o);
            EngineVector2 pushVector = o * distance;
            SeperationContext seperation = new SeperationContext(pushVector, pushDirection);
            context = seperation;
            return false;
         }
         return true;
      }
   }
}
