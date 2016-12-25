using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    class Player
    {
        private ContentManager content;
        private Texture2D skin;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 destination;
        private float speed;
        private float delta;
        private bool moving;

        public Player(ContentManager content)
        {
            this.content = content;
            this.position = new Vector2(50.0f, 50.0f);
            this.velocity = new Vector2(0.0f, 1.0f);
            this.destination = new Vector2(0.0f, 0.0f);
            this.speed = 0.5f;
            this.delta = 5.0f;
            this.moving = false;
        }

        public void LoadPlayer()
        {
            this.skin = this.content.Load<Texture2D>("Lux");
        }

        public void UnloadPlayer()
        {

        }

        public void UpdatePlayer(GameTime gameTime)
        {

            MouseState mouseState = Mouse.GetState();
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                moving = true;
                destination.X = mouseState.X;
                destination.Y = mouseState.Y;
                velocity.X = destination.X - position.X;
                velocity.Y = destination.Y - position.Y;
                float normalizer = (float)Math.Sqrt((Math.Pow(velocity.X, 2)) + (Math.Pow(velocity.Y, 2)));
                velocity.X /= normalizer;
                velocity.Y /= normalizer;
            }
            if (position.X < destination.X + delta && position.X > destination.X - delta)
            {
                if (position.Y < destination.Y + delta && position.Y > destination.Y - delta)
                {
                    moving = false;
                }
            }
            if (moving)
            {
                position.X = position.X + speed * velocity.X * gameTime.ElapsedGameTime.Milliseconds;
                position.Y = position.Y + speed * velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
            }
        }

        public void DrawPlayer(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            this.position.X -= 25;
            this.position.Y -= 25;
            spriteBatch.Draw(this.skin, this.position, Color.White);
            this.position.X += 25;
            this.position.Y += 25;
            spriteBatch.End();
        }
    }
}