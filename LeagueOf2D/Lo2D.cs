using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    public class Lo2D : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 screen_size;
        Player lux;
        Map map;

        public Lo2D()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.screen_size = new Vector2(1000, 500);
            this.graphics.PreferredBackBufferWidth  = (int) this.screen_size.X;
            this.graphics.PreferredBackBufferHeight = (int) this.screen_size.Y;
            this.graphics.ApplyChanges();
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.map = new Map(this.Content);
            this.lux = new Player(this.Content, this.map);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            map.LoadMap();
            lux.LoadPlayer();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            lux.UpdatePlayer(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            map.DrawMap(gameTime, this.spriteBatch);
            lux.DrawPlayer(gameTime, this.spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
