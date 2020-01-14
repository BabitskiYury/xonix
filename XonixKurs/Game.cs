using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;


namespace SFML
{
    class Game
    {
        void drop(int y, int x)
        {
            if (grid[y,x] == 0) grid[y,x] = -1;
            if (grid[y - 1,x] == 0) drop(y - 1, x);
            if (grid[y + 1,x] == 0) drop(y + 1, x);
            if (grid[y,x - 1] == 0) drop(y, x - 1);
            if (grid[y,x + 1] == 0) drop(y, x + 1);
        }
        int score;
        Text scoreText = new Text(" ", Content.fontN, 32);
        Text scoreToWinText = new Text(" ", Content.fontN, 16);
        Text winText= new Text("YOU WIN", Content.fontPixel, 48);
        Text loseText = new Text("YOU LOSE", Content.fontPixel, 48);
        Text restartText= new Text("Press 'space' to restart", Content.fontPixel, 24);
        Text nextLvlText = new Text("Press 'N' to next level", Content.fontPixel, 24);
        Text levelText = new Text(" ", Content.fontN, 16);

        int CountBlue;
        int scoreToWin;
        float percentToWin;
        int level;

        int x, y, dx, dy;

        int enemyCount;

        float timer, delay;
        Clock clock;

        public Grid grid;
        public Grid Grid { get { return grid; } }

        Tile mainT;
        Sprite sGameover;
        Sprite sEnemy;
        Sprite sTile;

        List<Enemy> enemy;

        public bool game;
        public Game()
        {
            score = 0;
            level = 1;
            scoreText.Position = new System.Vector2f(100, 455);
            scoreToWinText.Position = new System.Vector2f(5, 475);
            levelText.Position = new System.Vector2f(5, 455);

            percentToWin = 0.25f;
            winText.Color = Color.Green;
            loseText.Color = Color.Red;
            winText.Position = new System.Vector2f(250, 150);
            loseText.Position = new System.Vector2f(237, 150);
            restartText.Position= new System.Vector2f(190, 250);
            nextLvlText.Position = new System.Vector2f(210, 280);

            scoreToWin =Convert.ToInt32((Grid.GRID_SIZE_N * Grid.GRID_SIZE_M - (Grid.GRID_SIZE_M * 2 + (Grid.GRID_SIZE_N - 1) * 2)) *percentToWin);
            scoreToWinText.DisplayedString = "" + (int)(percentToWin * 100) + "% (" +scoreToWin+")";
            levelText.DisplayedString = "level: " + level;

            enemyCount = 2;
            sTile = new Sprite(Content.tiles);
            sEnemy = new Sprite(Content.enemy);
            sEnemy.Origin = new Vector2f(20, 20);
            enemy = new List<Enemy>();
            sGameover = new Sprite(Content.gameover);
            sGameover.Position = new Vector2f(100,0);
            Random rand = new Random();
            for (int i = 0; i < enemyCount; i++)
            {
                var en = new Enemy();
                en.x = rand.Next(100, 400);
                en.y = rand.Next(100, 400);
                en.dx = rand.Next(1, 6);
                en.dy = rand.Next(1, 6);
                enemy.Add(en);
            }

            game = true;

            mainT = new Tile(TileType.RED);
            x = 0;y = 0;dx = 0;dy = 0;
            timer = 0; delay = 0.07f;
            grid = new Grid();
            clock = new Clock();
        }
       public void Restart()
       {
            score = 0;
            x = 0; y = 0; dx = 0; dy = 0;
            Random rand = new Random();
            for (int i = 0; i < enemyCount; i++)
            {
                var en = new Enemy();
                en.x = rand.Next(100, 400);
                en.y = rand.Next(100, 400);
                en.dx = rand.Next(1, 6);
                en.dy = rand.Next(1, 6);
                enemy.Add(en);
            }
            scoreToWin = Convert.ToInt32((Grid.GRID_SIZE_N * Grid.GRID_SIZE_M - (Grid.GRID_SIZE_M * 2 + (Grid.GRID_SIZE_N - 1) * 2)) * percentToWin);
            scoreToWinText.DisplayedString ="" + (int)(percentToWin * 100) + "% (" + scoreToWin + ")";
            grid = new Grid();
            clock = new Clock();
            game = true;
       }
        
        public void NextLevel()
        {
            level++;
            levelText.DisplayedString = "level: " + level;
            enemyCount++;
            if (percentToWin < 0.85f)
                percentToWin += 0.05f;
            Restart();

        }
        public void Update()
        {

            if (game)
            {
                float time = clock.ElapsedTime.AsSeconds();
                clock.Restart();
                timer += time;

                if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                {
                    dx = -1;
                    dy = 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                {
                    dx = 1;
                    dy = 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    dx = 0;
                    dy = -1;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                {
                    dx = 0;
                    dy = 1;
                }

                for (int i = 0; i < enemyCount; i++)
                    enemy[i].move();

                if (timer > delay)
                {

                    x += dx;
                    y += dy;
                    
                    if (x < 0)
                        x = 0;
                    if (x > Grid.GRID_SIZE_N - 1)
                        x = Grid.GRID_SIZE_N - 1;
                    if (y < 0)
                        y = 0;
                    if (y > Grid.GRID_SIZE_M - 1)
                        y = Grid.GRID_SIZE_M - 1;
                    mainT.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE);
                    if (grid[y,x] == 2) game = false;
                    if (grid[y, x] == 0) { grid[y, x] = 2; score++; }
                    timer = 0;
                }

                if (grid[y,x] == 1)
                {
                    dx = dy = 0;

                    for (int i = 0; i < enemyCount; i++)
                        drop(enemy[i].y / 18, enemy[i].x / 18);
                    CountBlue = 126;
                    for (int i = 0; i < Grid.GRID_SIZE_M; i++)
                        for (int j = 0; j < Grid.GRID_SIZE_N; j++)
                            if (grid[i, j] == -1) { grid[i, j] = 0; CountBlue++; }
                            else { grid[i, j] = 1; score++; }
                    score = Grid.GRID_SIZE_M*Grid.GRID_SIZE_N-CountBlue;
                }

                for (int i = 0; i < enemyCount; i++)
                    if (grid[enemy[i].y / 18,enemy[i].x / 18] == 2) game = false;
                if (score > scoreToWin)
                    game = false;
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    Restart();
                if (Keyboard.IsKeyPressed(Keyboard.Key.N) && score > scoreToWin)
                    NextLevel();
            }
            
        }


        public void Draw()
        {
            Program.Window.Clear();
            for (int i = 0; i < Grid.GRID_SIZE_M; i++)
                for (int j = 0; j < Grid.GRID_SIZE_N; j++)
                {
                    if (grid[i,j] == 0) continue;
                    if (grid[i,j] == 1) sTile.TextureRect=new IntRect(0, 0, 18, 18);
                    if (grid[i,j] == 2) sTile.TextureRect = new IntRect(54, 0, 18, 18);
                    sTile.Position=new Vector2f(j * 18, i * 18);
                    Program.Window.Draw(sTile);
                }
            sTile.TextureRect = new IntRect(36, 0, 18, 18);
            sTile.Position = new Vector2f(x * 18, y * 18);
            Program.Window.Draw(sTile);
            
            for (int i = 0; i < enemyCount; i++)
            {
                
                sEnemy.Position = new Vector2f(enemy[i].x, enemy[i].y);
                sEnemy.Rotation+=5;
                Program.Window.Draw(sEnemy);
            }
            scoreText.DisplayedString = "SCORE: "+score;
            Program.Window.Draw(scoreText);
            Program.Window.Draw(scoreToWinText);
            Program.Window.Draw(levelText);

            if (!game)
            {
                if (score > scoreToWin)
                {
                    Program.Window.Draw(sGameover);
                    Program.Window.Draw(winText);
                    Program.Window.Draw(restartText);
                    Program.Window.Draw(nextLvlText);
                }
                else
                {
                    Program.Window.Draw(sGameover);
                    Program.Window.Draw(loseText);
                    Program.Window.Draw(restartText);
                }
            }  
        }
        
    }
}
