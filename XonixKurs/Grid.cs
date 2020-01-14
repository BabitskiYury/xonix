
namespace SFML
{
    class Grid 
    {
        public const int GRID_SIZE_M = 25;
        public const int GRID_SIZE_N = 40;

        public int[,] grid;

        public int this[int index1,int index2]
        {
            get { return grid[index1, index2]; }
            set { grid[index1, index2] = value; }
        }

        public Grid()
        {
            grid = new int[GRID_SIZE_M, GRID_SIZE_N];

            for (int i = 0; i < GRID_SIZE_M; i++)
            {
                for (int j = 0; j < GRID_SIZE_N; j++)
                {
                    if (i == 0 || j == 0 || i == GRID_SIZE_M - 1 || j == GRID_SIZE_N - 1)
                        grid[i, j] = 1;
                }
            }

            
            
        }

    }
}
