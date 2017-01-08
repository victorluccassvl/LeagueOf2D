using System;

namespace LeagueOf2D
{
    // The main class.
    public static class Program
    {
        // The main entry point for the application.
        [STAThread]
        static void Main()
        {
            using (var game = new Lo2D())
                game.Run();
        }
    }
}
