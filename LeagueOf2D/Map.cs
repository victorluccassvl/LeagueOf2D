using System;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    /**
     * Map class that represents the terrain in which the game will run
     * Contains a structure to verify terrain colision
     */
    class Map
    {
        /**
         * Attributes and Properties
         */

        // Game's content manager reference
        private ContentManager content;
        // Map texture
        private Texture2D map;
        // Map of pixels that warns if that area is solid or not
        private bool[,] obstruction;
        // Map size
        public Vector2 size;



        /**
         * Map constructor
         * 
         * :content: content reference given from the game
         */
        public Map (ContentManager content)
        {
            this.content = content;
        }



        /**
         * Map Load method
         * 
         * Loads map texture and creates the obstruction map from it
         */
        public void LoadMap ()
        {
            // Loads map texture
            this.map = this.content.Load<Texture2D>(Ctt.map_texture);

            // Initializes map size variables
            this.size.X = this.map.Width;
            this.size.Y = this.map.Height;

            // Block that transforms this Texture2D into a Bitmap, to call CreateMap
            {
                // Creates a memory stream
                MemoryStream MS = new MemoryStream();
                // Fills it with Texture2D information
                this.map.SaveAsPng(MS, this.map.Width, this.map.Height);
                // Rewinds it
                MS.Seek(0, SeekOrigin.Begin);
                // Calls CreateMap method, passing the Bitmap formed from memory stream
                this.CreateMap((Bitmap) Bitmap.FromStream(MS));
            }
        }



        /**
         * Map Unload method
         * 
         * Unload map related textures and structures
         */
        public void UnloadMap ()
        {
            //TODO
        }



        /**
         * Map Update method
         * 
         * Where all map dynamic  map modifications should be done
         * 
         * :gameTime: snapshot variable that holds all Game time information
         */
        public void UpdateMap (GameTime gameTime)
        {
            //Empty for now
        }



        /**
         * Map Draw method
         * 
         * Where all sprites and graphics will be shown at the screen
         * 
         * :gameTime: snapshot variable that holds all Game time information
         * :spriteBatch: Game's spriteBatch reference, already prepared
         */
        public void DrawMap (GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draws the map texture at defined position, with a transparent background
            spriteBatch.Draw(this.map, Ctt.map_origin, Microsoft.Xna.Framework.Color.White);
        }



        /**
         * Method that initializes the obstruction map, given it's Bitmap form
         * 
         * :map: Bitmap representation created by Load Map method
         */
        private void CreateMap (Bitmap map)
        {
            // Creates the obstruction map with appropriate size
            this.obstruction = new bool[this.map.Width, this.map.Height];

            // Creates a reading pixel object
            System.Drawing.Color pixel;
            // Iterates the map
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    // Reads the pixel at position X Y
                    pixel = map.GetPixel(x, y);

                    // Initializes it as false if its not a black pixel
                    // Initializes it as true if its a black pixel, therefore, an obstruction
                    this.obstruction[x, y] = !(pixel.R == 255 && pixel.G == 255 && pixel.B == 255);
                }
            }
        }



        /**
         * Method that informs if given position is obstructed or not
         * 
         * :x,y: coordenates
         */
        public bool isObstructed (int x, int y)
        {
            return this.obstruction[x, y];
        }
    }
}
