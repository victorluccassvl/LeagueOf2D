﻿using System;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    /**
     * Player class, that represents a character with it's skills, status and interactions
     */
    class Player
    {
        /**
         * Attributes and Properties
         */

        // Game's content manager reference
        private ContentManager content;
        // Skin texture
        private Texture2D skin;
        // Radius model
        private Texture2D radius;
        // Current position
        private Vector2 position;
        // Velocity vector ( movement direction, with norm 1)
        private Vector2 velocity;
        // Player destination if moving
        private Vector2 destination;
        // Speed intenisty ( movement magnetude )
        private float speed;
        // Locality delta
        private float delta;
        // Moving or not moving variable
        private bool moving;
        // Structure that helps finding radius pixels
        private int[,] radius_border;
        // Structure size
        private int border_size;
        // Map where the player is in
        private Map map;



        /**
         * Player constructor
         * 
         * :content: content reference given from the game
         * :map: map where the player is at
         */
        public Player (ContentManager content, Map map)
        {
            this.content = content;
            this.map = map;

            // Initializes all variables with defined constants
            this.position = Ctt.player_initial_position;
            this.velocity = Ctt.player_initial_velocity;
            this.destination = Ctt.player_initial_destination;
            this.speed = Ctt.player_speed;
            this.delta = Ctt.player_delta;
            this.moving = Ctt.player_initial_moving;
        }



        /**
         * Player Load method
         * 
         * Loads player skin and creates the radius structure
         */
        public void LoadPlayer ()
        {
            // Loads player skin and radius model
            this.skin = this.content.Load<Texture2D>(Ctt.player_lux);
            this.radius = this.content.Load<Texture2D>(Ctt.player_radius);

            // Block that transforms this Texture2D into a Bitmap, to call CreateRadius
            {
                // Creates a memory stream
                MemoryStream MS = new MemoryStream();
                // Fills it with Texture2D information
                this.radius.SaveAsPng(MS, this.radius.Width, this.radius.Height);
                // Rewinds it
                MS.Seek(0, SeekOrigin.Begin);
                // Calls CreateRadius method, passing the Bitmap formed from memory stream
                this.CreateRadius((Bitmap) Bitmap.FromStream(MS));
            }

        }



        /**
         * Player Unload method
         * 
         * Unload player related textures and structures
         */
        public void UnloadPlayer ()
        {
            //TODO
        }



        /**
         * Player Update method
         * 
         * Where all player dynamic actions should be done
         * 
         * :gameTime: snapshot variable that holds all Game time information
         */
        public void UpdatePlayer (GameTime gameTime)
        {
           
            // Gets current mouse state
            MouseState mouseState = Mouse.GetState();

            // If the right button has been pressed
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                // This is the new player destination
                this.destination.X = mouseState.X;
                this.destination.Y = mouseState.Y;

                // Check if this destination is not the origin itself
                bool at_this_x = Math.Abs(this.position.X - this.destination.X) < this.delta;
                bool at_this_y = Math.Abs(this.position.Y - this.destination.Y) < this.delta;

                // If it's not the origin, them the player will have to move
                this.moving = (!(at_this_x && at_this_y));
            
                // If the player will move, we need to find the new direction
                if (this.moving)
                {
                    // Creates the new direction vector
                    this.velocity.X = destination.X - position.X;
                    this.velocity.Y = destination.Y - position.Y;
                    // Normalizes it
                    float normalizer = (float)Math.Sqrt((Math.Pow(this.velocity.X, 2)) + (Math.Pow(this.velocity.Y, 2)));
                    this.velocity.X /= normalizer;
                    this.velocity.Y /= normalizer;
                }
            }
            // If the right button has not been pressed
            else
            {
                // We have to verify if the player has already arrivet it's destination

                // Check if this destination is not the origin itself
                bool at_this_x = Math.Abs(this.position.X - this.destination.X) < this.delta;
                bool at_this_y = Math.Abs(this.position.Y - this.destination.Y) < this.delta;

                // If it's not the origin, them the player will have to move
                this.moving = (!(at_this_x && at_this_y));
            }

            // If the player has not reached it's destiny yet
            if (this.moving)
            {
                // Save it's current position in case of a colision happen
                float initial_x = this.position.X;
                float initial_y = this.position.Y;

                // Make it move
                this.position.X += this.speed * this.velocity.X * gameTime.ElapsedGameTime.Milliseconds;
                this.position.Y += this.speed * this.velocity.Y * gameTime.ElapsedGameTime.Milliseconds;

                // Creates auxiliar X and Y variables
                int x, y;
                // Creates an structur to save all colision points and another to count them
                int colisions = 0;
                int[,] colisions_points = new int[2, 4 * this.skin.Width + 4 * this.skin.Height];

                // For each border pixel
                for (int i = 0; i < this.border_size; i++)
                {
                    // Find the border based on player position
                    x = (int)Math.Round(this.radius_border[0, i] + this.position.X);
                    y = (int)Math.Round(this.radius_border[1, i] + this.position.Y);
                    
                    // If this pixel is a wall
                    if (this.map.isObstructed(x, y))
                    {
                        // Save it at colision points
                        colisions_points[0, colisions] = x;
                        colisions_points[1, colisions] = y;
                        colisions++;
                    }
                }

                //TODO : Use this colisions_points to smooth colision ( not make it go back to starter point, but the edge of the wall )

                // If there was at least one colision
                if ( colisions != 0)
                {
                    // Stop player move
                    this.moving = false;
                    // Make it go back to origin point
                    this.position.X = initial_x;
                    this.position.Y = initial_y;
                }

            }
        }



        /**
         * Player Draw method
         * 
         * Show player animation and textures
         * 
         * :gameTime: snapshot variable that holds all Game time information
         * :spriteBatch: Game's spriteBatch reference, already prepared
         */
        public void DrawPlayer(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Finds right position to print the skin based on it's size
            float printX = this.position.X - (this.skin.Width / 2);
            float printY = this.position.Y - (this.skin.Height / 2);

            // Prepare this position
            Vector2 print = new Vector2(printX, printY);

            // Prints it
            spriteBatch.Draw(this.skin, print, Microsoft.Xna.Framework.Color.White);
        }




        /**
         * Method that initializes the radius of the player, given it's Bitmap form
         * 
         * :rad: Bitmap representation created by Player Load
         */
        private void CreateRadius(Bitmap rad)
        {

            // Initializes radius border with sufficient size
            this.radius_border = new int[2, 4 * this.skin.Width + 4 * this.skin.Height];
            this.border_size = 0;

            // Creates a reading pixel object
            System.Drawing.Color pixel;
            // Iterates the texture
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
                        this.radius_border[0, this.border_size] = x - (int) Math.Round((double) this.skin.Width / 2);
                        this.radius_border[1, this.border_size] = y - (int) Math.Round((double) this.skin.Height / 2);
                        this.border_size++;
                    }
                }
            }
        }
    }
}