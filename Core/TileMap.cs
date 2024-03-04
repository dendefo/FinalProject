namespace QuarterEngine.Core
{
    public class TileMap
    {
        ConsoleColor[,] colors;
        public TileObject<char> this[int x, int y]
        {
            get => floor[x, y];
        }
        private TileObject<char>[,] floor;
        public List<TileObject<char>> Objects; 

        public int Height => floor.GetLength(0);
        public int Width => floor.GetLength(1);
        public TileMap(int width, int height)
        {
            Objects = new List<TileObject<char>>();
            floor = new TileObject<char>[width, height];
            colors = new ConsoleColor[width, height];
        }
        public void SetColor(int x, int y, ConsoleColor color)
        {
            colors[x, y] = color;
        }
        public ConsoleColor GetColor(int x, int y)
        {
            return colors[x, y];
        }
        public void SetColorMatrix(ConsoleColor color, ConsoleColor secondColor)
        {
            for (int i = 0; i < colors.GetLength(0); i++)
            {
                for (int j = 0; j < colors.GetLength(1); j++)
                {
                    if ((i+j) % 2 == 0) colors[i, j] = color;
                    else colors[i, j] = secondColor;
                }
            }
        }

    }
}
