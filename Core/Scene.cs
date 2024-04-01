using System.Numerics;
using Renderer;

namespace Core
{
    /// <summary>
    /// A class that represents a Tile Map
    /// Still not finished
    /// </summary>
    public class Scene
    {
        public Tile<char>[,] floorTiles;
        public int Height => floorTiles.GetLength(0);
        public int Width => floorTiles.GetLength(1);
        public Scene(int width, int height)
        {
            floorTiles = new Tile<char>[width, height];
            ChessFloor();
        }
        private void ChessFloor()
        {
            for (int i = 0; i < floorTiles.GetLength(0); i++)
            {
                for (int j = 0; j < floorTiles.GetLength(1); j++)
                {
                    if ((i + j) % 2 == 0)
                        floorTiles[i, j] = new Tile<char>(new Vector2(i, j), new VisualRepresentation<char>(' ', System.Drawing.Color.Black));
                    else
                        floorTiles[i, j] = new Tile<char>(new Vector2(i, j), new VisualRepresentation<char>(' ', System.Drawing.Color.Gray));
                }
            }
        }
    }
}
