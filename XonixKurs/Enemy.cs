
namespace SFML
{
    class Enemy
    {
        public int x, y, dx, dy;
        public Enemy()
        {
            
            x = 300;
            y = 300;
            dx = 4 ;
            dy = 4 ;
        }
        
        public void move()
        {
            x += dx;
            if(Program.game.grid[y/18,x/18]==1)
            {
                dx = -dx;
                x += dx;
            }
            y += dy;
            if (Program.game.grid[y / 18, x / 18] == 1)
            {
                dy = -dy;
                y += dy;
            }
        }
    }
}
