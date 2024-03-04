using System.Numerics;
using Renderer;

namespace QuarterEngine.Core
{
    public class Core
    {
        private readonly IRenderer<char> renderer;
        public TileMap Scene;

        public Core(int width, int height)
        {
            Scene = new TileMap(width, height);
            renderer = new ConsoleRenderer();
        }

        public void EndTurn()
        {
            Scene.RenderFloor(renderer);
            foreach (var item in Scene.Objects)
            {
                renderer.RenderObject(item, Scene.floor[(int)item.Position.X,(int)item.Position.Y]);
            }
        }
        public void Instantiate(TileObject<char> tileObject)
        {
            Scene.Objects.Add(tileObject);
        }
        public void Instantiate(TileObject<char> tileObject,Vector2 position)
        {
            Scene.Objects.Add(tileObject);
            tileObject.Position = position;
        }
    }
}
