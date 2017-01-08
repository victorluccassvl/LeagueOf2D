using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    /*\
     * Map class that represents the terrain in which the game will run
     * Contains a structure to verify terrain colision
    \*/
    class Map
    {
        /*\
         * Attributes and Properties
        \*/

        // Game's content manager reference
        public ContentManager content;
        // Map texture
        public Texture2D map;
        // Map of pixels that warns if that area is solid or not
        public bool[,] obstruction;
        // Map size
        public Vector2 size;



        /*\
         * Map constructor
         * 
         * :content: content reference given from the game
        \*/
        public Map (ContentManager content)
        {
            this.content = content;
        }



        /*\
         * Map Load method
         * 
         * Loads map texture and creates the obstruction map from it
         * 
         * :return: nothing
        \*/
        public void LoadMap ()
        {
            // Loads map texture
            this.map = this.content.Load<Texture2D>(Ctt.map_texture);

            // Initializes map size variables
            this.size.X = this.map.Width;
            this.size.Y = this.map.Height;

            // Transforms this Texture2D into a Bitmap and call CreateMap
            this.CreateMap(Methods.ConvertToBitmap(this.map));
        }



        /*\
         * Map Unload method
         * 
         * Unload map related textures and structures
         * 
         * :return: nothing
        \*/
        public void UnloadMap ()
        {
            //TODO
        }



        /*\
         * Map Update method
         * 
         * Where all map dynamic  map modifications should be done
         * 
         * :gameTime: snapshot variable that holds all Game time information
         * 
         * :return: nothing
        \*/
        public void UpdateMap (GameTime gameTime)
        {
            //Empty for now
        }



        /*\
         * Map Draw method
         * 
         * Where all sprites and graphics will be shown at the screen
         * 
         * :gameTime: snapshot variable that holds all Game time information
         * :spriteBatch: Game's spriteBatch reference, already prepared
         * :cam: Player's camera origin
         * 
         * :return: nothing
        \*/
        public void DrawMap (GameTime gameTime, SpriteBatch spriteBatch, Vector2 cam)
        {
            int X = (int) Ctt.screen_size.X;
            int Y = (int) Ctt.screen_size.Y;

            // Initializes screen rectangle where it'll draw
            Microsoft.Xna.Framework.Rectangle screen = new Microsoft.Xna.Framework.Rectangle(0, 0, X, Y);
            // Initializes texture rectangle that will be draw
            Microsoft.Xna.Framework.Rectangle source = new Microsoft.Xna.Framework.Rectangle((int)cam.X, (int)cam.Y, X, Y);

            // Draws the map texture at defined position, with a transparent background
            spriteBatch.Draw(this.map, screen, source, Microsoft.Xna.Framework.Color.White);
        }



        /*\
         * Method that initializes the obstruction map, given it's Bitmap form
         * 
         * :map: Bitmap representation created by Load Map method
         * 
         * :return: nothing
        \*/
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


        
        /*\
         * Method that informs if given position is obstructed or not
         * 
         * :x,y: coordenates
         * 
         * :return: nothing
        \*/
        public bool isObstructed (int x, int y)
        {
            return this.obstruction[x, y];
        }
    }
}
