using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using VerletIntegration.Simulation;
using VerletIntegration.Simulation.Shapes;
using VerletIntegration.Simulation.Textile;

namespace VerletIntegration
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Texture2D vertext, pixel;
        public static int Width, Height;

        Cloth cloth;
        Box box;
        Triangle triangle;
        StickMan hero;

        Vertex mouseRadius;

        KeyboardState kbd, oldkbd;
        public static MouseState mouse = Mouse.GetState(), oldmouse;

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
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Width = Window.ClientBounds.Width;
            Height = Window.ClientBounds.Height;
            vertext = Content.Load<Texture2D>("circ");
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            box = new Box(100, 100, 75, 85);
            box.PinAll();
            cloth = new Cloth(300, 100, 10, 20, 8);
            triangle = new Triangle(200, 100, 50, 50);
            hero = new StickMan(300, 300);
            mouseRadius = new Vertex();
            mouseRadius.SetRadius(3);
            mouseRadius.Pinned = true;
            mouseRadius.Hidden = false;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            oldmouse = mouse;
            mouse = Mouse.GetState();
            oldkbd = kbd;
            kbd = Keyboard.GetState();
            mouseRadius.SetX(mouse.Position.X);
            mouseRadius.SetY(mouse.Position.Y);

            if (kbd.IsKeyUp(Keys.R) && oldkbd.IsKeyDown(Keys.R))
            {
                box = new Box(100, 100, 75, 85);
                hero = new StickMan(300, 300);
                triangle = new Triangle(200, 200, 50, 50);
            }

            if (kbd.IsKeyDown(Keys.D))
                hero.MoveX(2);
            else if (kbd.IsKeyDown(Keys.A))
                hero.MoveX(-2);

            if (kbd.IsKeyDown(Keys.W) && hero.AtRest)
                hero.MoveY(-5);

            cloth.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            triangle.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            box.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            hero.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            mouseRadius.Draw(spriteBatch);
            cloth.Draw(spriteBatch);
            triangle.Draw(spriteBatch);
            box.Draw(spriteBatch);
            hero.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
