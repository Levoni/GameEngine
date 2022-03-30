using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

using Base.System;
using Base.Scenes;
using Base.Components;
using Base.Entities;
using Base.Utility.Services;
using Base.Utility;

namespace Base
{
   /// <summary>
   /// This is the main type for your game.
   /// </summary>
   class Game1 : Game
   {
      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;
      Scene scene;
      UI.Control c;
      UI.Label l;
      UI.Textbox tb;
      UI.MainMenuExample MME;

      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         // TODO: Add your initialization logic here
         base.Initialize();
         //graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
         //graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
         //graphics.ToggleFullScreen();

         IsMouseVisible = true;
         ContentService.InitService(Content);

         //Gets width, height of game screen
         Rectangle r = graphics.GraphicsDevice.Viewport.Bounds;

         //Gets width, height of the screen
         //float f1 = graphics.GraphicsDevice.DisplayMode.Width;
         //float f2 = graphics.GraphicsDevice.DisplayMode.Height;
         EngineRectangle viewport = new EngineRectangle(r.X, r.Y, r.Width, r.Height);
         MME = new UI.MainMenuExample(viewport);

         Type ty = Type.GetType("Base.Components.Transform");

         //Database.DatabaseWrapper.ConnectDatabaseWrapper();
         //Database.DatabaseWrapper.executeSQLQueryCommand("Select * from Character WHERE Character_Id = 0");

         c = new UI.Button();
         l = new UI.Label("label", "This is the label", new EngineRectangle(0, 200, 50, 50), Color.Black);
         tb = new UI.Textbox();
         tb.bounds = new EngineRectangle(300, 200, 200, 100);

         c.onClick = new Events.EHandler<Events.OnClick>(new Action<object, Events.OnClick>(TestOnClick));

         //UI.Control c1 = new UI.Control();
         Events.EHandler<Events.ControlEvent> testintEhandle = new Events.EHandler<Events.ControlEvent>(new Action<object,Events.ControlEvent>(Testint));

         //c1 += testintEhandle;

         testintEhandle.SetFunction(Testint);
         //c1 -= new Events.EHandler<Events.EventDown>(new Action<object,Events.EventDown>(Testint));

         //c1.onClick = new Events.EHandler<Events.OnClick>(new Action<object, Events.OnClick>(TestOnClick));

         //c1.Click();

         //Events.EHandler<int> testingeh = new Base.Events.EHandler(this, new Action<Events.EventDown>(Testint));

         scene = new Scene();
         scene.sceneID = "scene";


         Transform t = new Transform();
         t.X = 5;
         t.Y = 5;
         Sprite s = new Sprite();
         s.Init();
         PlayerController pc = new PlayerController();
         global::System.Console.WriteLine(t.Family());
         Entity e = scene.CreateEntity();


         //TODO: put some check in so events don't get added twice in situation
         // like the one below
         TwoDRenderSystem TDRS = new TwoDRenderSystem(scene);
         PlayerControllerSystem pcs = new PlayerControllerSystem(scene);
         scene.AddSystem(TDRS);
         scene.AddSystem(pcs);
         //end example

         ComponentManager<Transform> t2 = new ComponentManager<Transform>();
         ComponentManager<Sprite> s2 = new ComponentManager<Sprite>();
         ComponentManager<PlayerController> pc2 = new ComponentManager<PlayerController>();
         scene.AddComponentManager(t2);
         scene.AddComponentManager(s2);
         scene.AddComponentManager(pc2);
         scene.AddComponent(e, t);
         scene.AddComponent(e, pc);


         Transform tTest = scene.GetComponent<Transform>(e);

         scene.Update(new GameTime());

         scene.AddComponent(e, s);

         scene.Update(new GameTime());
         Serialization.SerializableDictionary<int,BaseComponent> ent = new Serialization.SerializableDictionary<int, BaseComponent>();


         Serialization.SerializableScene ss = new Serialization.SerializableScene(scene);
         ss.SerializeScene();

         Scene firstScene = scene;

         firstScene.RemoveSystem(pcs);

         //scene = Serialization.SerializableScene.CreateSceneFromSerializableScene("scene");


         ////Transform tTest = t2.Lookup(1);
         //if (t == tTest)
         //   global::System.Console.WriteLine("Equal");
      }

      BaseComponentManager[] ComponentManagers = new BaseComponentManager[32];
      public void AddComponent<T>(T component)
      {
         ComponentManager<T> tempTransform = (ComponentManager<T>)ComponentManagers[Component<T>.GetFamily()];
         tempTransform.AddComponent(1, component);
      }

      public void AddComponentManager<T>(ComponentManager<T> componentManager)
      {
         ComponentManagers[Component<T>.GetFamily()] = componentManager;
      }

      private void Testint(object send, Events.ControlEvent ed)
      {
         ;
      }

      private void TestOnClick(object send, Events.OnClick oc)
      {
         ;
      }


      /// <summary>
      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      /// </summary>
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures.
         spriteBatch = new SpriteBatch(GraphicsDevice);

         // TODO: use this.Content to load your game content here
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// game-specific content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
         //   Exit();

         // TODO: Add your update logic here
         MME.Update(gameTime.ElapsedGameTime.Milliseconds);
         //scene.Update();
         //c.Update();
         //l.Update();
         //tb.Update();
         base.Update(gameTime);
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         // TODO: Add your drawing code here
         SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
         spriteBatch.Begin();
         MME.Render(spriteBatch);
         //scene.Render(spriteBatch);
         //c.Render(spriteBatch);
         //l.Render(spriteBatch);
         //tb.Render(spriteBatch);

         spriteBatch.End();
         spriteBatch.Dispose();


         base.Draw(gameTime);
      }
   }
}
