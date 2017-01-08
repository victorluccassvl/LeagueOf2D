using Microsoft.Xna.Framework;

namespace LeagueOf2D
{
    /*\
     * Static class of config constants used at every other class
    \*/
    static class Ctt
    {
        /*\
         * LO2D.cs related constants
        \*/

        // Screen size
        public static Vector2 screen_size = new Vector2(1000, 500);
        // Mouse visibility
        public static bool mouse_visibility = true;




        /*\
         * Map.cs related constants
        \*/

        // Map texture path
        public static string map_texture = "Map";



        /*\
         * Player.cs related constants
        \*/

        // Player initial position, must be inside map dimensions
        public static Vector2 player_initial_position = new Vector2(2572.0f, 2150.0f);
        // Player cam initial position
        public static Vector2 player_initial_cam = new Vector2(Ctt.player_initial_position.X - Ctt.screen_size.X, Ctt.player_initial_position.X - Ctt.screen_size.Y);
        // Player initial velocity, must have norm 1
        public static Vector2 player_initial_velocity = new Vector2(0.0f, 0.0f);
        // Player initial destination
        public static Vector2 player_initial_destination = new Vector2(0.0f, 0.0f);

        // Player default speed
        public static float player_speed = 10.0f;
        // Cam default speed
        public static float cam_initial_speed = 0.7f;
        // Cam hitbox constant
        public static float cam_border_size = 20.0f;
        // Delta to compare if Player is at specific position
        public static float player_delta = 5.0f;
        // Player initial moving state
        public static bool player_initial_moving = false;
        // Player skin path

        public static string player_lux = "Lux";
        // Player's radius model path
        public static string player_radius = "Radius";
    }
}
