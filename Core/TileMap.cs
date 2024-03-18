using System.Numerics;
using Renderer;

namespace Core
{
    public class TileMap
    {
        public TileObject this[int x, int y]
        {
            get => floor[x, y];
        }
        public TileObject[,] floor;
        public List<TileObject> Objects; 
        public static TileMap Instance;

        public int Height => floor.GetLength(0);
        public int Width => floor.GetLength(1);
        public TileMap(int width, int height)
        {
            Objects = new List<TileObject>();
            floor = new TileObject[width, height];
            Instance = this;
        }
        public void SetFloorTile(TileObject tile, int x, int y)
        {
            floor[x, y] = tile;
        }
        public void DefaulChessFloor()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var col = (i + j) % 2 == 0 ? System.Drawing.Color.Gray : System.Drawing.Color.Black;
                    //var tile = new TileObject<char>(new VisualRepresentation<char>(' ',col));
                    //SetFloorTile(tile,i,j);
                    //tile.Position = new Vector2(i, j);
                }
            }
        }
        public void RenderFloor(IRenderer<char> renderer)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    //renderer.RenderBackGroundObject(floor[i, j]);
                }
            }
        }
    }
}
