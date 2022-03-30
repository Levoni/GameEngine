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
using Base.Utility.Services;
using Base.Utility;

namespace Base.System
{
   [Serializable]
   public class CollisionSystemTwoD : EngineSystem
   {
      public Dictionary<string, List<ColliderMapTwoDEntity>> colliderMap;
      //private List<CollisionEvent> currentCollisions;
      private List<TriggerEvent> currentTriggerCollision;
      public MaskCollection collisionLayerMask;
      //TODO: see if I really need these (width,height)
      public float mapWidth, mapHeight;
      public bool isProccessingCollisions;

      public float largestColliderWidth, largestColliderHeight;

      public CollisionSystemTwoD(Scene s, int mapWidth, int mapHeight, MaskCollection collisionLayerMask)
      {
         systemSignature = (uint)(1 << ColliderTwoD.GetFamily() | 1 << Transform.GetFamily() | 1 << Sprite.GetFamily());
         registeredEntities = new List<Entity>();
         colliderMap = new Dictionary<string, List<ColliderMapTwoDEntity>>();
         isProccessingCollisions = false;
         this.mapWidth = mapWidth;
         this.mapHeight = mapHeight;
         this.collisionLayerMask = collisionLayerMask;
         CreateSpacialColliderMap();
         Init(s);
      }

      public CollisionSystemTwoD()
      {
         systemSignature = (uint)(1 << ColliderTwoD.GetFamily() | 1 << Transform.GetFamily() | 1 << Sprite.GetFamily());
         registeredEntities = new List<Entity>();
         colliderMap = new Dictionary<string, List<ColliderMapTwoDEntity>>();
         collisionLayerMask = new MaskCollection();
         this.mapWidth = 0;
         this.mapHeight = 0;
         CreateSpacialColliderMap();
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }

      public override void Update(int dt)
      {
         for (int i = 0; i <= 1; i++) //possibly update to > 1 to handle multiple collisions in one frame
         {
            //currentCollisions = new List<CollisionEvent>();
            CreateSpacialColliderMap();
            CollisionCheck();
            //foreach (CollisionEvent info in currentCollisions)
            //{
            //   parentScene.bus.Publish(this, info);
            //}
         }
      }

      private void CollisionCheck()
      {
         isProccessingCollisions = true;
         foreach (KeyValuePair<string, List<ColliderMapTwoDEntity>> colliders in colliderMap)
         {
            for (int i = 0; i < colliders.Value.Count - 1; i++)
            {
               for (int j = i + 1; j < colliders.Value.Count; j++)
               {
                  if (colliders.Value[i].owner != colliders.Value[j].owner && canLayersCollide(colliders.Value[i].collider, colliders.Value[j].collider))
                  {
                     SeperationContext seperation;
                     bool isCollision = CollisionService.IsCollision(colliders.Value[i], colliders.Value[j], out seperation);
                     if (isCollision && seperation.pushVector.ToMagnitudeSquared() != 0)
                     {
                        if (colliders.Value[i].collider.IsTrigger)
                        {
                           currentTriggerCollision.Add(new TriggerEvent(colliders.Value[i], colliders.Value[j], "standard"));
                        }
                        if (colliders.Value[j].collider.IsTrigger)
                        {
                           currentTriggerCollision.Add(new TriggerEvent(colliders.Value[j], colliders.Value[i], "standard"));
                        }
                        if(!colliders.Value[i].collider.IsTrigger && !colliders.Value[j].collider.IsTrigger)
                        {
                           CollisionContext context = new CollisionContext(colliders.Value[i].owner, colliders.Value[j].owner, seperation);
                           //TODO:fix this system so I don't have to check everyentry to see if the collision is already in there
                           //bool alreadyExists = false;
                           //foreach (CollisionEvent cc in currentCollisions)
                           //{
                           //   if (context.Equals(cc.context))
                           //      alreadyExists = true;
                           //}
                           //if (!alreadyExists)
                              parentScene.bus.Publish(this, new CollisionEvent(context));
                           //currentCollisions.Add(new CollisionEvent(context));
                        }
                     }
                  }
               }
            }
         }
      }

      private bool canLayersCollide(ICollisionBound2D colliderOne, ICollisionBound2D colliderTwo)
      {
         return collisionLayerMask.isIdInMask(colliderOne.collisionMaskId, colliderTwo.collisionMaskId) &&
                collisionLayerMask.isIdInMask(colliderTwo.collisionMaskId, colliderOne.collisionMaskId);
      }

      public override void RegisterEnitity(Entity entity)
      {
         base.RegisterEnitity(entity);
         ColliderTwoD colliderTwoD = parentScene.GetComponent<ColliderTwoD>(entity);
         Transform transform = parentScene.GetComponent<Transform>(entity);
         Sprite sprite = parentScene.GetComponent<Sprite>(entity);
         foreach (ICollisionBound2D bcb in colliderTwoD.colliders)
         {
            float newWidth = 0;
            float newHeight = 0;

            bcb.DetermineCollisionHeightWidth(transform, sprite, out newWidth, out newHeight);

            if (largestColliderHeight < newHeight)
               largestColliderHeight = newHeight;
            if (largestColliderWidth < newWidth)
               largestColliderWidth = newWidth;
            if (!isProccessingCollisions)
            {
               if (newWidth != largestColliderWidth || newHeight != largestColliderHeight)
                  CreateSpacialColliderMap();
               ColliderMapTwoDEntity colliderMapTwoDEntity = new ColliderMapTwoDEntity(bcb, entity, transform, sprite);
               HashCollider(colliderMapTwoDEntity);
            }
         }
      }

      public override void DeregisterEntity(Entity entity)
      {
         //TODO: find way to recalculate largest colliderHeight and width
         base.DeregisterEntity(entity);
         if (!isProccessingCollisions)
         {
            CreateSpacialColliderMap();
         }
      }

      private void CreateSpacialColliderMap()
      {
         colliderMap = new Dictionary<string, List<ColliderMapTwoDEntity>>();


         float xAmount = largestColliderWidth != 0 ? (mapWidth / largestColliderWidth) : 0;
         float yAmount = largestColliderHeight != 0 ? (mapHeight / largestColliderHeight) : 0;
         foreach (Entity entity in registeredEntities)
         {
            ColliderTwoD ctd = parentScene.GetComponent<ColliderTwoD>(entity);
            Transform t = parentScene.GetComponent<Transform>(entity);
            Sprite s = parentScene.GetComponent<Sprite>(entity);
            foreach (ICollisionBound2D bcb in ctd.colliders)
            {
               ColliderMapTwoDEntity colliderMapTwoDEntity = new ColliderMapTwoDEntity(bcb, entity, t, s);
               HashCollider(colliderMapTwoDEntity);
            }
         }
      }

      private void HashCollider(ColliderMapTwoDEntity colliderEntity)
      {
         List<string> hashKeys = new List<string>();

         hashKeys = colliderEntity.collider.GetSpacialGridHashKeys(largestColliderHeight, largestColliderWidth, colliderEntity.transform, colliderEntity.sprite);
         foreach (string key in hashKeys)
         {
            if (!colliderMap.ContainsKey(key))
               colliderMap[key] = new List<ColliderMapTwoDEntity>();
            colliderMap[key].Add(colliderEntity);
         }
      }
   }
}
