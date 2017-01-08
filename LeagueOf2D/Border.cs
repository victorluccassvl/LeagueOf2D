using System;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace LeagueOf2D
{
    /*\
     * Class that represents a radius border of an entity ( player, monster, skill, etc )
    \*/
    class Border
    {
        /*\
         * Attributes and Properties
        \*/

        // Array that helps finding the border pixels from a X,Y point
        public int[,] radius_border;
        // The size of this array
        public int size;
        // Texture from which we creates the array
        public Texture2D radius;



        /*\
         * Border constructor
         * 
         * :radius: required texture to create border array
        \*/
        public Border (Texture2D radius)
        {
            this.radius = radius;

            // Convert this Texture2D to Bitmap;
            Bitmap rad = Methods.ConvertToBitmap(radius);

            // Initializes radius border with sufficient size
            // Note that this array has [2,BLAH] format, where index 0 = X and index 1 = Y
            this.radius_border = new int[2, 2 * this.radius.Width + 2 * this.radius.Height];
            this.size = 0;

            // Creates a reading pixel object
            Color pixel;

            // Iterates through the bitmap
            for (int x = 0; x < rad.Width; x++)
            {
                for (int y = 0; y < rad.Height; y++)
                {
                    // Reads the pixel at position X Y
                    pixel = rad.GetPixel(x, y);

                    // If it is a black pixel, it is part of the border
                    if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
                    {
                        // Add it to the border
                        this.radius_border[0, this.size] = x - (int) Math.Round((double) this.radius.Width / 2);
                        this.radius_border[1, this.size] = y - (int) Math.Round((double) this.radius.Height / 2);
                        this.size++;
                    }
                }
            }
        }
    }
}