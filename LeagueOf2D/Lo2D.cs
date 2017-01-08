using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LeagueOf2D
{
    /*\
     * Main class of League Of 2D, which inherit from Game class of MonoGame
    \*/
    public class Lo2D : Game
    {
        /*\
         * Attributes and Properties
        \*/

        // Object to control game window
        private GraphicsDeviceManager graphics;
        // Object to allow drawing
        private SpriteBatch spriteBatch;
        // Size of screen (x and Y fields)
        private Vector2 screen_size;
        // Player example for tests
        private Player lux;
        // Game map
        private Map map;



        /*\
         * League of Legends 2D constructor
        \*/
        public Lo2D ()
        {
            // Create the graphic object
            this.graphics = new GraphicsDeviceManager(this);
            // Create the game map
            this.map = new Map(this.Content);
            // Create the test player
            this.lux = new Player(this.Content, this.map);
        }



        /*\
         * MonoGame Initialize method
         *
         * Where things should be initialize at the beggining of game loop
         * ( is called only once )
        \*/
        protected override void Initialize ()
        {
            // Initializes screen size variable with defined constant
            this.screen_size = Ctt.screen_size;

            // Sets screen size with it's associated variable and apply the change
            this.graphics.PreferredBackBufferWidth = (int)this.screen_size.X;
            this.graphics.PreferredBackBufferHeight = (int)this.screen_size.Y;
            this.graphics.ApplyChanges();

            // Sets the content directory with MonoGame suggested name
            this.Content.RootDirectory = "Content";

            // Initializes mouse as visible, with defined constant
            this.IsMouseVisible = Ctt.mouse_visibility;

            // Initializes Game base ( MonoGame requires )
            base.Initialize();
        }



        /*\
         * MonoGame Load method
         *
         * Where things should be loaded in order to be used at the game
         * ( is called only once )
        \*/
        protected override void LoadContent ()
        {
            // Loads spriteBatch object
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads the map
            this.map.LoadMap();

            // Loads the test player
            this.lux.LoadPlayer();

        }



        /*\
         * MonoGame Unload method
         *
         * Where things should be unloaded in order to close the game
         * ( is called only once )
        \*/
        protected override void UnloadContent ()
        {
            //TODO
        }



        /*\
         * MonoGame Update method
         *
         * Where all dynamic logic that controls the game should be implemented
         * ( is called every game tick )
         *
         * :gameTime: snapshot variable that holds all Game time information
        \*/
        protected override void Update (GameTime gameTime)
        {
            // Monogame sugested exit shortcuts
            if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
            {
                System.Environment.Exit (0);
            }

            // Updates test player, passing time info as param
            this.lux.UpdatePlayer(gameTime);

            // Updates Game base ( MonoGame requires )
            base.Update(gameTime);
        }



        /*\
         * MonoGame Draw method
         *
         * Where all sprites and graphics will be shown at the screen
         * ( is called every game tick )
         *
         * :gameTime: snapshot variable that holds all Game time information
        \*/
        protected override void Draw (GameTime gameTime)
        {
            // Monogame suggested screen initialization
            this.GraphicsDevice.Clear(Color.White);

            // Prepares spriteBatch for drawing
            this.spriteBatch.Begin();

            // Draws game map, sending the prepared spriteBatch and game time
            this.map.DrawMap(gameTime, this.spriteBatch, this.lux.cam);
            // Draws test player, sending the prepared spriteBatch and game time
            this.lux.DrawPlayer(gameTime, this.spriteBatch);

            // Ends spriteBatch drawing proccess
            this.spriteBatch.End();

            // Draws Game base ( MonoGame requires )
            base.Draw(gameTime);
        }
    }
}
