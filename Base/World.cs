using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Scenes;
using Base.Utility.Services;
using Microsoft.Xna.Framework;


namespace Base
{
   //TODO:  updatePrioritySceneOnly
   //       renderPrioritySceneOnly
   [Serializable]
   public class World
   {
      public List<Scene> scenes;

      public World()
      {
         scenes = new List<Scene>();
      }

      public void UpdateScenes(GameTime gameTime)
      {
         scenes[0].Update(gameTime);
      }

      public void UpdateScene(string sceneID, GameTime gameTime)
      {
         foreach(Scene s in scenes)
         {
            if (s.sceneID == sceneID)
               s.Update(gameTime);
         }
      }

      public void RenderScenes(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
      {
         scenes[0].Render(sb);
      }

      public void RenderScene(string sceneID, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
      {
         foreach(Scene s in scenes)
         {
            if (s.sceneID == sceneID)
               s.Render(sb);
         }
      }

      public void AddScene(Scene newScene)
      {
         newScene.parentWorld = this;
         scenes.Insert(0, newScene);
      }

      public void RemoveScene(string sceneID)
      {
         for(int i = 0; i < scenes.Count; i++)
         {
            if(scenes[i].sceneID != sceneID)
            {
               //scenes[i].destroyScene();
               scenes.RemoveAt(i);
            }
         }
      }

      public void RemoveScene(Scene s)
      {
         for (int i = 0; i < scenes.Count; i++)
         {
            if (scenes[i].sceneID != s.sceneID)
            {
               //scenes[i].destroyScene();
               scenes.RemoveAt(i);
            }
         }
      }

      //Switch scene with SceneID with the current index 0 scene
      public void PrioritizeScene(string sceneID)
      {
         int index = -1;
         for(int i = 0; i < scenes.Count; i ++)
         {
            if(scenes[i].sceneID == sceneID)
            {
               index = i;
            }
         }

         Scene tempScene = scenes[index];
         scenes[index] = scenes[0];
         scenes[0] = tempScene;
      }
   }
}
