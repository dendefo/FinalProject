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
        public TileObject[,] floor;
        public List<TileObject> Objects; 

        public int Height => floor.GetLength(0);
        public int Width => floor.GetLength(1);
        public Scene(int width, int height)
        {
            Objects = new List<TileObject>();
            floor = new TileObject[width, height];
        }
    }
}
