using SFML.Graphics;


namespace SFML
{
    class Content
    {
        public const string CONTENT_DIR = "..\\Content\\";

        public static Texture enemy;
        public static Texture gameover;
        public static Texture tiles;
        public static Font fontN;
        public static Font fontPixel;

        public static void Load()
        {
            enemy = new Texture(CONTENT_DIR+"enemy.png");
            gameover = new Texture(CONTENT_DIR + "gameover.png");
            tiles= new Texture(CONTENT_DIR + "tiles.png");
            fontN = new Font("font.ttf");
            fontPixel = new Font("pixel.otf");
        }
    }
}
