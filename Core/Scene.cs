using System.Collections;
using System.Numerics;
using Renderer;

namespace Core
{
    /// <summary>
    /// A class that represents a Tile Map
    /// Still not finished
    /// </summary>
    public class Scene :IEnumerable<Tile<char>>
    {
        private Tile<char>[,] floorTiles;
        public Tile<char> this[int x, int y] => floorTiles[x, y];
        public Tile<char> this[Position2D position] => floorTiles[position.x, position.y];
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
                        floorTiles[i, j] = new Tile<char>(new Position2D(i, j), new VisualRepresentation<char>(' ', System.Drawing.Color.Black));
                    else
                        floorTiles[i, j] = new Tile<char>(new Position2D(i, j), new VisualRepresentation<char>(' ', System.Drawing.Color.Gray));
                }
            }
        }

        public IEnumerator<Tile<char>> GetEnumerator()
        {
            return new SceneEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    internal class SceneEnumerator: IEnumerator<Tile<char>>
    {
        private Scene scene;
        private int x = -1;
        private int y = 0;
        public SceneEnumerator(Scene scene)
        {
            this.scene = scene;
        }
        public Tile<char> Current => scene[x, y];

        object IEnumerator.Current => scene[x, y];

        public void Dispose()
        {
            scene = null;
        }

        public bool MoveNext()
        {
            if (x < scene.Width-1)
            {
                x++;
                return true;
            }
            else if (y < scene.Height-1)
            {
                x = 0;
                y++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            x = 0;
            y = 0;
        }
    }
}
