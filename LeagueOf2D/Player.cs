using System;
using System.Drawing;
using System.IO;
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
        private Texture2D radius;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 destination;
        private float speed;
        private float delta;
        private bool moving;
        private int[,] radius_border;
        private int border_tam;
        private Map map;
        private Bitmap rad;

        public Player(ContentManager content, Map map)
        {
            this.content = content;
            this.map = map;
            this.position = new Vector2(100.0f, 100.0f);
            this.velocity = new Vector2(0.0f, 1.0f);
            this.destination = new Vector2(0.0f, 0.0f);
            this.speed = 0.5f;
            this.delta = 5.0f;
            this.moving = false;
            this.radius_border = new int[2, 220];
            this.border_tam = 0;
        }

        public void LoadPlayer()
        {
            this.skin = this.content.Load<Texture2D>("Lux");
            this.radius = this.content.Load<Texture2D>("Radius");
            using (MemoryStream MS = new MemoryStream())
            {
                this.radius.SaveAsPng(MS, this.radius.Width, this.radius.Height);
                MS.Seek(0, SeekOrigin.Begin);
                this.rad = (Bitmap)Bitmap.FromStream(MS);
            }
            this.CreateRadius();
        }

        public void UnloadPlayer()
        {

        }

        public void UpdatePlayer(GameTime gameTime)
        {
           
            MouseState mouseState = Mouse.GetState();

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                destination.X = mouseState.X;
                destination.Y = mouseState.Y;
                moving = !(Math.Abs(position.X - destination.X) < delta && Math.Abs(position.Y - destination.Y) < delta);
                if (moving)
                {
                    velocity.X = destination.X - position.X;
                    velocity.Y = destination.Y - position.Y;
                    float normalizer = (float)Math.Sqrt((Math.Pow(velocity.X, 2)) + (Math.Pow(velocity.Y, 2)));
                    velocity.X /= normalizer;
                    velocity.Y /= normalizer;
                }
            }
            else
            {
                moving = !(Math.Abs(position.X - destination.X) < delta && Math.Abs(position.Y - destination.Y) < delta);
            }


            float initial_x = this.position.X;
            float initial_y = this.position.Y;

            if (moving)
            {
                position.X += speed * velocity.X * gameTime.ElapsedGameTime.Milliseconds;
                position.Y += speed * velocity.Y * gameTime.ElapsedGameTime.Milliseconds;

                int x, y;
                int colisions = 0;
                int[,] colisions_points = new int[2, 220];
                for (int i = 0; i < this.border_tam; i++)
                {
                    x = (int)Math.Round(this.radius_border[0, i] + position.X);
                    y = (int)Math.Round(this.radius_border[1, i] + position.Y);
                    if (this.map.isObstructed(x, y))
                    {
                        colisions_points[0, colisions] = x;
                        colisions_points[1, colisions] = y;
                        colisions++;
                        this.moving = false;
                        this.position.X = initial_x;
                        this.position.Y = initial_y;
                    }
                }
            }
            /*
            while ( colisions != 0)
            {
                this.position.X += this.velocity.X * (-1);
                this.position.Y += this.velocity.Y * (-1);
                for (int i = 0; i < colisions; i++)
                {
                    x = (int)Math.Round(this.radius_border[0, i] + position.X);
                    y = (int)Math.Round(this.radius_border[1, i] + position.Y);
                    if (this.map.isObstructed(x, y))
                    {
                        colisions_points[0, colisions] = x;
                        colisions_points[1, colisions] = y;
                        colisions++;
                    }
                }

            }*/
        }

        public void DrawPlayer(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 print = new Vector2(this.position.X-24, this.position.Y-24);
            spriteBatch.Draw(this.skin, print, Microsoft.Xna.Framework.Color.White);
        }

        private void CreateRadius()
        {
            for (int i = 0; i < rad.Width; i++)
            {
                for (int j = 0; j < rad.Height; j++)
                {
                    System.Drawing.Color pixel = rad.GetPixel(i, j);

                    if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
                    {
                        this.radius_border[0, this.border_tam] = i;
                        this.radius_border[1, this.border_tam] = j;
                        this.border_tam++;
                    }
                }
            }
        }
    }
}