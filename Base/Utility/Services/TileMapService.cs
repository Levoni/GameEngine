using Base.Components;
using Base.Entities;
using Base.Scenes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Base.Utility.Services
{
   public static class TileMapService
   {
      public static TmxMap map = null;
      public static Dictionary<string, Texture2D> tileSetImages = new Dictionary<string, Texture2D>();
      public static List<Entity> EntityList = new List<Entity>();
      public static bool mapIsLoaded = false;

      public const string tileSetDirectory = "Tiled/tilesets/";
      public const string tileMapDirectory = "Tiled/tilemaps/";

      public static void LoadMap(string fileName)
      {
         map = new TmxMap(tileMapDirectory + fileName);
         foreach (var tileset in map.Tilesets)
         {
            tileSetImages[tileset.Name] = ContentService.Get2DTexture(tileset.Name);
         }
         mapIsLoaded = map != null;
      }

      public static void UnloadMap()
      {
         map = null;
         mapIsLoaded = false;
         tileSetImages.Clear();
         EntityList.Clear();
      }

      public static void CreateEntitiesForLoadedMap(Scene scene)
      {
         foreach (var layer in map.TileLayers)
         {
            foreach (var t in layer.Tiles)
            {
               Entity entity = scene.entityManager.CreateEntity();


               Transform transform = new Transform(t.X * map.TileWidth, t.Y * map.TileHeight, 1, 1, 0);
               scene.AddComponent(entity, transform);



               // Create Sprite
               TileSprite sprite = new TileSprite();
               string test = string.Empty;
               foreach (var tileset in map.Tilesets)
               {
                  if (t.Gid >= tileset.FirstGid && t.Gid < tileset.FirstGid + tileset.TileCount)
                  {
                     sprite.TileId = t.Gid;
                     sprite.TileSetName = tileset.Name;
                     int tileNumber = t.Gid - tileset.FirstGid;
                     sprite.SourceStartX = (tileNumber % (int)tileset.Columns) * tileset.TileWidth;
                     sprite.SourceStartY = (tileNumber / (int)tileset.Columns) * tileset.TileHeight;
                     sprite.ImageWidth = tileset.TileWidth;
                     sprite.ImageHeight = tileset.TileHeight;
                     scene.AddComponent(entity, sprite);

                     //Old sprite, hacky way to link it to collision system
                     Sprite spriteOld = new Sprite("none");
                     spriteOld.SetSize(tileset.TileWidth, tileset.TileHeight);
                     scene.AddComponent(entity, spriteOld);

                     //Create Colliders
                     if (tileset.Tiles.ContainsKey(t.Gid - tileset.FirstGid))
                     {
                        List<Collision.ICollisionBound2D> collisions = new List<Collision.ICollisionBound2D>();
                        foreach (var o in tileset.Tiles[t.Gid - tileset.FirstGid].ObjectGroups[0].Objects)
                        {
                           Collision.BoxCollisionBound2D boxCollisionBound2D = new Collision.BoxCollisionBound2D((float)(o.X / tileset.TileWidth), (float)(o.Y / tileset.TileHeight), (float)(o.Width / tileset.TileWidth), (float)(o.Height / tileset.TileHeight), 4, o.Type == "t");
                           collisions.Add(boxCollisionBound2D);
                        }
                        ColliderTwoD colliderTwoD = new ColliderTwoD(collisions);
                        scene.AddComponent(entity, colliderTwoD);
                        RigidBody2D rigidBody2D = new RigidBody2D(1, true, false, 1);
                        scene.AddComponent(entity, rigidBody2D);
                     }
                  }
               }
            }
         }
      }
   }
}
