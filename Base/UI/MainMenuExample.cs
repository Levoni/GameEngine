using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Base.Utility.Services;
using Base.Utility;

namespace Base.UI
{
   [Serializable]
   public enum MenuStates
   {
      Main,
      Options,
      SaveMenu,
      NewGame,
   }

   [Serializable]
   public class MainMenuExample:GUI
   {
      MenuStates curMenu;
      EngineRectangle bounds;

      // Main Menu controls
      Button btnOptions;
      Button btnLoadGame;
      Button btnNewGame;
      Label lblTitle;

      //Option Menu controls
      Label lblOptionTitle;
      Textbox txtboxVolumeOption;

      //Save Menu controls
      SaveInfo siSaveOne;
      //SaveInfo

      //New Game Menu
      Label lblNewGameTitle;
      Textbox txtboxName;


      public MainMenuExample(EngineRectangle viewportBounds)
      {
         bounds = viewportBounds;
         curMenu = MenuStates.Main;
         LoadMainMenu();
      }

      public override void Update(int dt)
      {
         if (curMenu == MenuStates.Main)
         {
            UpdateMainMenu(dt);
         }
         else if (curMenu == MenuStates.Options)
         {
            UpdateOptionMenu(dt);
            if (KeyboardService.currentState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
               LoadMainMenu();
         }
         else if(curMenu == MenuStates.SaveMenu)
         {
            UpdateSaveMenu(dt);
            if (KeyboardService.currentState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
               LoadMainMenu();
         }
         else if (curMenu == MenuStates.NewGame)
         {
            UpdateNewGameMenu(dt);
            if (KeyboardService.currentState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
               LoadMainMenu();
         }
      }

      public override void Render(SpriteBatch sb)
      {
         if (curMenu == MenuStates.Main)
         {
            RenderMainMenu(sb);
         }
         else if (curMenu == MenuStates.Options)
         {
            RenderOptionMenu(sb);
         }
         else if (curMenu == MenuStates.SaveMenu)
         {
            RenderSaveMenu(sb);
         }
         else if (curMenu == MenuStates.NewGame)
         {
            RenderNewGameMenu(sb);
         }
      }

      public void UpdateMainMenu(int dt)
      {
         btnOptions.Update(dt);
         btnLoadGame.Update(dt);
         btnNewGame.Update(dt);
         lblTitle.Update(dt);
      }

      public void UpdateOptionMenu(int dt)
      {
         lblOptionTitle.Update(dt);
         txtboxVolumeOption.Update(dt);
      }

      public void UpdateSaveMenu(int dt)
      {
         siSaveOne.Update(dt);
      }

      public void UpdateNewGameMenu(int dt)
      {
         lblNewGameTitle.Update(dt);
         btnNewGame.Update(dt);
         txtboxName.Update(dt);
      }

      public void RenderMainMenu(SpriteBatch sb)
      {
         btnOptions.Render(sb);
         btnLoadGame.Render(sb);
         btnNewGame.Render(sb);
         lblTitle.Render(sb);
      }

      public void RenderOptionMenu(SpriteBatch sb)
      {
         lblOptionTitle.Render(sb);
         txtboxVolumeOption.Render(sb);
      }

      public void RenderSaveMenu(SpriteBatch sb)
      {
         siSaveOne.Render(sb);
      }

      public void RenderNewGameMenu(SpriteBatch sb)
      {
         lblNewGameTitle.Render(sb);
         btnNewGame.Render(sb);
         txtboxName.Render(sb);
      }

      public void LoadMainMenu()
      {
         curMenu = MenuStates.Main;
         Events.EHandler<Events.OnClick> optionMenuTransfer = new Events.EHandler<Events.OnClick>(new Action<object, Events.OnClick>(GoToOptionMenu));
         Events.EHandler<Events.OnClick> saveMenuTransfer = new Events.EHandler<Events.OnClick>(new Action<object, Events.OnClick>(GoToSaveMenu));
         Events.EHandler<Events.OnClick> newGameMenuTransfer = new Events.EHandler<Events.OnClick>(new Action<object, Events.OnClick>(GoToNewGameMenu));


         lblTitle = new Label("lblTitle", "Test Menu", new EngineRectangle(bounds.Width / 2 - 100, 50, 200, 50), Microsoft.Xna.Framework.Color.Black);

         btnNewGame = new Button();
         btnNewGame.Name = "btnNewGame";
         btnNewGame.value = "New Game";
         btnNewGame.bounds = new EngineRectangle(bounds.Width / 2 - 100, 100, 200, 50);
         btnNewGame.textColor = Microsoft.Xna.Framework.Color.Black;
         btnNewGame.onClick =  newGameMenuTransfer;

          btnLoadGame = new Button();
         btnLoadGame.Name = "btnLoadGame";
         btnLoadGame.value = "Load Game";
         btnLoadGame.bounds = new EngineRectangle(bounds.Width / 2 - 100, 200, 200, 50);
         btnLoadGame.textColor = Microsoft.Xna.Framework.Color.Black;
         btnLoadGame.onClick = saveMenuTransfer;

         btnOptions = new Button();
         btnOptions.Name = "btnOption";
         btnOptions.value = "Option";
         btnOptions.bounds = new EngineRectangle(bounds.Width / 2 - 100, 300, 200, 50);
         btnOptions.textColor = Microsoft.Xna.Framework.Color.Black;
         btnOptions.onClick = optionMenuTransfer;
      }

      public void LoadOptionMenu()
      {
         curMenu = MenuStates.Options;
         Events.EHandler<Events.OnChange> eh = new Events.EHandler<Events.OnChange>(new Action<object, Events.OnChange>(changeVolume));
         lblOptionTitle = new Label("lblOptionTitle", "Options", new EngineRectangle(bounds.Width / 2, 50, 200, 100), Color.Black);
         txtboxVolumeOption = new Textbox();
         txtboxVolumeOption.Name = "txtboxMasterVolume";
         txtboxVolumeOption.value = "100";
         txtboxVolumeOption.bounds = new EngineRectangle(bounds.Width / 2, 150, 300, 100);
         txtboxVolumeOption.textColor = Color.Black;
         txtboxVolumeOption.validChars = new HashSet<char>()
         {
            '1','2','3','4','5','6','7','8','9','0',
         };
         txtboxVolumeOption.maxCharCount = 3;
         txtboxVolumeOption.onValueChange = eh;

      }

      public void LoadSaveMenu()
      {
         curMenu = MenuStates.SaveMenu;
         siSaveOne = new SaveInfo("saveInfoOne", "", new EngineRectangle(bounds.X + bounds.Width / 2, bounds.Y, bounds.Width / 2, bounds.Height),Color.Black,new List<Control>());
      }

      public void LoadNewGameMenu()
      {
         curMenu = MenuStates.NewGame;
         lblNewGameTitle = new Label("lblNewGame", "Select Name Of Your Save", new EngineRectangle(bounds.Width / 2, 50, 200, 100), Color.Black);

         txtboxName = new Textbox();
         txtboxName.Name = "txtboxName";
         txtboxName.value = "";
         txtboxName.bounds = new EngineRectangle(bounds.Width / 2, 150, 300, 100);
         txtboxName.textColor = Color.Black;
         txtboxName.maxCharCount = 10;

         btnNewGame = new Button();
         btnNewGame.Name = "btnNewGame";
         btnNewGame.value = "Save Name";
         btnNewGame.bounds = new EngineRectangle(bounds.Width / 2, 250, 200, 100);
         btnNewGame.onClick = new Events.EHandler<Events.OnClick>(new Action<object, Events.OnClick>(SaveFile));
      }

      public void changeVolume(object sender, Events.OnChange change)
      {

         string trimedString = ((string)change.newValue).TrimEnd('_');
         if(!string.IsNullOrEmpty(trimedString) && int.Parse(trimedString) > 100)
         {
            trimedString = "100";
         }
         AudioService.SetBackgroundVolume(int.Parse(trimedString));
         ((Textbox)sender).value = trimedString + "_";
      }

      public void GoToOptionMenu(object sender, Events.OnClick click)
      {
         LoadOptionMenu();
      }

      public void GoToSaveMenu(object sender, Events.OnClick click)
      {
         LoadSaveMenu();
      }

      public void GoToNewGameMenu(object sender, Events.OnClick click)
      {
         LoadNewGameMenu();
      }

      public void GoToLoadMenu(object sender, Events.OnClick click)
      {
         LoadSaveMenu();
      }

      public void SaveFile(object sender, Events.OnClick click)
      {
         Serialization.Serializer<string>.SaveGame(txtboxName.value,txtboxName.value);
      }

   }
}
