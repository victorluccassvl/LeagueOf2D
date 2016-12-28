using Microsoft.Xna.Framework;

namespace LeagueOf2D
{
    /**
     * Static class of config constants used at every other class
     */
    static class Ctt
    {
        /**
         * LO2D.cs related constants
         */
        // Screen size
        public static Vector2 screen_size = new Vector2(1000, 500);
        // Mouse visibility
        public static bool mouse_visibility = true;

        /**
         * Map.cs related constants
         */
        // Map texture path
        public static string map_texture = "Map";
        // Map origin point
        public static Vector2 map_origin = new Vector2(0, 0);

        /**
         * Player.cs related constants
         */
        // Player initial position, must be inside map dimensions
        public static Vector2 player_initial_position = new Vector2(100.0f, 100.0f);
        // Player initial velocity, must have norm 1
        public static Vector2 player_initial_velocity = new Vector2(0.0f, 0.0f);
        // Player initial destination
        public static Vector2 player_initial_destination = new Vector2(0.0f, 0.0f);

        // Player default speed
        public static float player_speed = 0.2f;
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
