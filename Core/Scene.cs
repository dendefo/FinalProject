using System.Collections;
using System.Numerics;
using Core.Rendering;
using Renderer;

namespace Core
{
    /// <summary>
    /// A class that represents a Tile Map
    /// Still not finished
    /// </summary>
    public class Scene : IEnumerable<Tile>
    {
        protected Tile[,] floorTiles;
        public Tile this[int x, int y] => floorTiles[x, y];
        public Tile this[Position2D position] => floorTiles[position.x, position.y];
        public int Height => floorTiles.GetLength(0);
        public int Width => floorTiles.GetLength(1);
        internal Scene(int width, int height)
        {
            floorTiles = new Tile[width, height];
            for (int i = 0; i < floorTiles.GetLength(0); i++)
            {
                for (int j = 0; j < floorTiles.GetLength(1); j++)
                {
                    floorTiles[i, j] = new Tile(new(i,j),default);
                }
            }
        }

        public void ChessFloor()
        {
            for (int i = 0; i < floorTiles.GetLength(0); i++)
            {
                for (int j = 0; j < floorTiles.GetLength(1); j++)
                {
                    if ((i + j) % 2 != 0)
                        floorTiles[i, j] = new Tile(new Position2D(i, j), new VisualRepresentation(visual: default, System.Drawing.Color.Black));
                    else
                        floorTiles[i, j] = new Tile(new Position2D(i, j), new VisualRepresentation(visual: default, System.Drawing.Color.White));
                }
            }
        }
        public IEnumerator<Tile> GetEnumerator()
        {
            return new SceneEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool IsInside(Position2D position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < Width && position.y < Height;
        }
        public bool IsInside(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }
        public bool IsEmpty(Position2D position)
        {
            return IsInside(position) && this[position].TileObject == null;
        }
        public bool IsEmpty(int x, int y)
        {
            return IsInside(x, y) && this[x, y].TileObject == null;
        }
        public void HighLightMoves(IEnumerable<Position2D> moves)
        {
            foreach (var item in moves)
            {
                if (IsInside(item))
                {
                    this[item].isHighLighted = true;
                    this[item].HighlightColor = System.Drawing.Color.Green;
                }
            }
        }
        public void ClearHighlights()
        {
            foreach (var item in this)
            {
                item.isHighLighted = false;
            }
        }

    }
    internal class SceneEnumerator : IEnumerator<Tile>
    {
        private Scene scene;
        private int x = -1;
        private int y = 0;
        public SceneEnumerator(Scene scene)
        {
            this.scene = scene;
        }
        public Tile Current => scene[x, y];

        object IEnumerator.Current => scene[x, y];

        public void Dispose()
        {
            scene = null;
        }

        public bool MoveNext()
        {
            if (x < scene.Width - 1)
            {
                x++;
                return true;
            }
            else if (y < scene.Height - 1)
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
