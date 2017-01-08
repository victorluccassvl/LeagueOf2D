using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace LeagueOf2D
{
    class Methods
    {
        public static Bitmap ConvertToBitmap (Texture2D texture)
        {
            // Creates a memory stream
            MemoryStream MS = new MemoryStream();

            // Fills it with Texture2D information
            texture.SaveAsPng(MS, texture.Width, texture.Height);

            // Rewinds it
            MS.Seek(0, SeekOrigin.Begin);

            // Recieves the Bitmap formed from memory stream
            Bitmap img = (Bitmap)Bitmap.FromStream(MS);

            // Returns it
            return img;
        }
    }
}