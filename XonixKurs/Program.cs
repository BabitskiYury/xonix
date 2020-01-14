using SFML.Graphics;
using SFML.Window;
using System;


namespace SFML
{
    class Program
    {
        static RenderWindow win;
        
        public static RenderWindow Window { get { return win; } }
        public static Game game { private set; get; }
        static void Main(string[] args)
        {
           
            win = new RenderWindow(new Window.VideoMode(Grid.GRID_SIZE_N*Tile.TILE_SIZE, Grid.GRID_SIZE_M * Tile.TILE_SIZE+50), "XONIX");
            win.SetFramerateLimit(60);
            win.SetVerticalSyncEnabled(true);

            win.Closed += Win_Closed;
            win.Resized += Win_Resized;
            
            Content.Load();
            
            game = new Game();
            
            
            while (win.IsOpen)
            {
                win.DispatchEvents();

                game.Update();
                game.Draw();

                win.Display();  
            }
            
        }

        private static void Win_Resized(object sender, SizeEventArgs e)
        {
            win.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            win.Close();
        }
    }
}
