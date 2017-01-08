using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    /*\
     * Player class, that represents a character with it's skills, status and interactions
    \*/
    class Player
    {
        /*\
         * Attributes and Properties
        \*/

        // Game's content manager reference
        public ContentManager content;
        // Skin texture
        public Texture2D skin;
        // Radius model
        public Texture2D radius;
        // Current position
        public Vector2 position;
        // Cam initial position
        public Vector2 cam;
        // Cam speed
        public float cam_speed;
        // Velocity vector ( movement direction, with norm 1)
        public Vector2 velocity;
        // Player destination if moving
        public Vector2 destination;
        // Speed intenisty ( movement magnetude )
        public float speed;
        // Locality delta
        public float delta;
        // Moving or not moving variable
        public bool moving;
        // Structure that helps finding radius pixels
        public Border border;
        // Map where the player is at
        public Map map;
        // Path structure used to find move paths
        public Path path;
        


        /*\
         * Player constructor
         * 
         * :content: content reference given from the game
         * :map: map where the player is at
        \*/
        public Player (ContentManager content, Map map)
        {
            this.content = content;
            this.map = map;

            // Initializes all variables with defined constants
            this.position = Ctt.player_initial_position;
            this.cam = Ctt.player_initial_cam;
            this.velocity = Ctt.player_initial_velocity;
            this.destination = Ctt.player_initial_destination;
            this.speed = Ctt.player_speed;
            this.delta = Ctt.player_delta;
            this.moving = Ctt.player_initial_moving;
            this.cam_speed = Ctt.cam_initial_speed;
        }



        /*\
         * Player Load method
         * 
         * Loads player skin and creates the radius structure
        \*/
        public void LoadPlayer ()
        {
            // Loads player skin and radius model
            this.skin = this.content.Load<Texture2D>(Ctt.player_lux);
            this.radius = this.content.Load<Texture2D>(Ctt.player_radius);

            // Creates the border and path structures
            this.border = new Border(this.radius);
            this.path = new Path(this.border,this.map);
        }



        /*\
         * Player Unload method
         * 
         * Unload player related textures and structures
        \*/
        public void UnloadPlayer ()
        {
            //TODO
        }



        /*\
         * Player Update method
         * 
         * Where all player dynamic actions should be done
         * 
         * :gameTime: snapshot variable that holds all Game time information
        \*/
        public void UpdatePlayer(GameTime gameTime)
        {

            // Gets current mouse state
            MouseState mouse_state = Mouse.GetState();
            KeyboardState keyboard_state = Keyboard.GetState();

            // If the mouse is inside the screen
            if (mouse_state.X >= 0 && mouse_state.Y >= 0 && mouse_state.X < Ctt.screen_size.X && mouse_state.Y < Ctt.screen_size.Y)
            {
                // Verifies if it is at screen border, so cam will move
                bool left = mouse_state.X < Ctt.cam_border_size;
                bool right = mouse_state.X > Ctt.screen_size.X - Ctt.cam_border_size;
                bool up = mouse_state.Y < Ctt.cam_border_size;
                bool down = mouse_state.Y > Ctt.screen_size.Y - Ctt.cam_border_size;

                // Creates a vector to guide cam movement
                Vector2 direction = new Vector2(0, 0);

                // Properly initializes it
                if (left)
                {
                    direction.X = -1;
                }
                if (right)
                {
                    direction.X = 1;
                }
                if (up)
                {
                    direction.Y = -1;
                }
                if (down)
                {
                    direction.Y = 1;
                }

                // Normalizes it if needed
                if (direction.X != 0 && direction.Y != 0)
                {
                    direction.X /= (float)Math.Sqrt(2);
                    direction.Y /= (float)Math.Sqrt(2);
                }

                // Moves it if possible
                {
                    float new_x = this.cam.X + this.cam_speed * direction.X * gameTime.ElapsedGameTime.Milliseconds;
                    float new_y = this.cam.Y + this.cam_speed * direction.Y * gameTime.ElapsedGameTime.Milliseconds;

                    if ((new_x >= 0) && (new_x < this.map.size.X - Ctt.screen_size.X) && (new_y >= 0) && (new_y < this.map.size.Y - Ctt.screen_size.Y))
                    {
                        this.cam.X = new_x;
                        this.cam.Y = new_y;
                    }
                }

                // Makes cam move to player by pressing Space
                if (keyboard_state.IsKeyDown(Keys.Space))
                {
                    this.cam.X = this.position.X - Ctt.screen_size.X / 2;
                    this.cam.Y = this.position.Y - Ctt.screen_size.Y / 2;
                }

                // If the right button has been pressed
                if (mouse_state.RightButton == ButtonState.Pressed)
                {
                    // Find the destination
                    this.destination.X = this.cam.X + mouse_state.X;
                    this.destination.Y = this.cam.Y + mouse_state.Y;

                    // Check if this destination is not the origin itself
                    bool at_this_x = Math.Abs(this.position.X - this.destination.X) < this.delta;
                    bool at_this_y = Math.Abs(this.position.Y - this.destination.Y) < this.delta;

                    // If it's not the origin, them the player will have to move
                    this.moving = !(at_this_x && at_this_y);

                    // If it's not the origin, and it will move
                    if (this.moving)
                    {
                        Console.WriteLine("Creating Path");
                        // Creates a path for it to follow
                        this.path.CreatePath(this.destination, this.position);
                        Console.WriteLine("Path created");
                    }
                }

                // If the player is moving through the path
                if (this.moving)
                {
                    // Finds how far it can move at this tick
                    float distance = this.speed * gameTime.ElapsedGameTime.Milliseconds;

                    // Makes it move
                    this.moving = this.path.Walk(distance);

                    // Update player position
                    this.position.X = this.path.current_pixel.X;
                    this.position.Y = this.path.current_pixel.Y;
                }
            }
        }
                  
        //        // If the player will move, we need to find the new direction
        //        if (this.moving)
        //        {
        //            // Creates the new direction vector
        //            this.velocity.X = destination.X - position.X;
        //            this.velocity.Y = destination.Y - position.Y;
        //            // Normalizes it
        //            float normalizer = (float)Math.Sqrt((Math.Pow(this.velocity.X, 2)) + (Math.Pow(this.velocity.Y, 2)));
        //            this.velocity.X /= normalizer;
        //            this.velocity.Y /= normalizer;
        //        }
        //    }
        //    // If the right button has not been pressed
        //    else
        //    {
        //        // We have to verify if the player has already arrivet it's destination

        //        // Check if this destination is not the origin itself
        //        bool at_this_x = Math.Abs(this.position.X - this.destination.X) < this.delta;
        //        bool at_this_y = Math.Abs(this.position.Y - this.destination.Y) < this.delta;

        //        // If it's not the origin, them the player will have to move
        //        this.moving = (!(at_this_x && at_this_y));
        //    }

        //    // If the player has not reached it's destiny yet
        //    if (this.moving)
        //    {
        //        // Save it's current position in case of a colision happen
        //        float initial_x = this.position.X;
        //        float initial_y = this.position.Y;

        //        // Make it move
        //        this.position.X += this.speed * this.velocity.X * gameTime.ElapsedGameTime.Milliseconds;
        //        this.position.Y += this.speed * this.velocity.Y * gameTime.ElapsedGameTime.Milliseconds;

        //        // Creates auxiliar X and Y variables
        //        int x, y;
        //        // Creates an structur to save all colision points and another to count them
        //        int colisions = 0;
        //        int[,] colisions_points = new int[2, 4 * this.skin.Width + 4 * this.skin.Height];

        //        // For each border pixel
        //        for (int i = 0; i < this.border.size; i++)
        //        {
        //            // Find the border based on player position
        //            x = (int)Math.Round(this.border.radius_border[0, i] + this.position.X);
        //            y = (int)Math.Round(this.border.radius_border[1, i] + this.position.Y);
                    
        //            // If this pixel is a wall
        //            if (this.map.isObstructed(x, y))
        //            {
        //                // Save it at colision points
        //                colisions_points[0, colisions] = x;
        //                colisions_points[1, colisions] = y;
        //                colisions++;
        //            }
        //        }

        //        //TODO : Use this colisions_points to smooth colision ( not make it go back to starter point, but the edge of the wall )

        //        // If there was at least one colision
        //        if ( colisions != 0)
        //        {
        //            // Stop player move
        //            this.moving = false;
        //            // Make it go back to origin point
        //            this.position.X = initial_x;
        //            this.position.Y = initial_y;
        //        }

        //    }
        //}



        /*\
         * Player Draw method
         * 
         * Show player animation and textures
         * 
         * :gameTime: snapshot variable that holds all Game time information
         * :spriteBatch: Game's spriteBatch reference, already prepared
        \*/
        public void DrawPlayer(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Finds right position to print the skin based on it's size, position and camera origin
            float printX = this.position.X - this.cam.X - (this.skin.Width / 2);
            float printY = this.position.Y - this.cam.Y - (this.skin.Height / 2);

            // Prepare this position
            Vector2 print = new Vector2(printX, printY);

            // Prints it
            spriteBatch.Draw(this.skin, print, Microsoft.Xna.Framework.Color.White);
        }




        /*\
         * Method that initializes the radius of the player, given it's Bitmap form
         * 
         * :rad: Bitmap representation of this radius
        \*/
        public void CreateRadius(Bitmap rad)
        {

            // Initializes radius border with sufficient size
            this.border.radius_border = new int[2, 4 * this.skin.Width + 4 * this.skin.Height];
            this.border.size = 0;

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
                        this.border.radius_border[0, this.border.size] = x - (int) Math.Round((double) this.skin.Width / 2);
                        this.border.radius_border[1, this.border.size] = y - (int) Math.Round((double) this.skin.Height / 2);
                        this.border.size++;
                    }
                }
            }
        }
    }
}