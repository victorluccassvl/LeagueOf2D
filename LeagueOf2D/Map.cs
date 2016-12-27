using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace LeagueOf2D
{
    class Map
    {
        private ContentManager content;
        private Texture2D wall;
        private bool[,] obstruction;
        private Vector2 size;
        private Bitmap img;

        public Map(ContentManager content)
        {
            this.content = content;
        }

        public void LoadMap()
        {
            this.wall = this.content.Load<Texture2D>("Wall");
            using (MemoryStream MS = new MemoryStream())
            {
                this.wall.SaveAsPng(MS, this.wall.Width, this.wall.Height);
                MS.Seek(0, SeekOrigin.Begin);
                this.img = (Bitmap) Bitmap.FromStream(MS);
            }
            this.CreateMap();
        }

        public void UnloadMap()
        {

        }

        public void UpdateMap(GameTime gameTime)
        {

        }

        public void DrawMap(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.wall, new Vector2(0,0), Microsoft.Xna.Framework.Color.White);
        }

        private void CreateMap()
        {
            this.size.X = img.Width;
            this.size.Y = img.Height;
            this.obstruction = new bool[img.Width,img.Height];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    System.Drawing.Color pixel = img.GetPixel(i, j);

                    this.obstruction[i, j] = !(pixel.R == 255 && pixel.G == 255 && pixel.B == 255);
                }
            }
        }

        public bool isObstructed (int x, int y)
        {
            return this.obstruction[x, y];
        }
    }
}
