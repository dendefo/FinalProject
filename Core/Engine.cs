using System.Numerics;
using Renderer;

namespace Core
{
    public class Engine
    {
        private readonly IRenderer<char> renderer;
        public TileMap Scene;

        public Engine(int width, int height)
        {
            Scene = new TileMap(width, height);
            renderer = new ConsoleRenderer();
        }

        public void EndTurn()
        {
            Scene.RenderFloor(renderer);
            foreach (var item in Scene.Objects)
            {
                foreach (var component in item.components)
                {
                    if (component is IRenderable<char> renderable)
                    {
                        renderer.RenderObject(renderable, renderable);
                    }
                }
                //item.components.Cast<CharacterRenderer>().ToList().ForEach(x => renderer.RenderObject(x, x));
            }
        }
        static public T Instantiate<T>(T tileObject,Vector2 position = default) where T : TileObject
        {
            TileMap.Instance.Objects.Add(tileObject);
            tileObject.Position = position;
            return tileObject;
        }
        static public T Instantiate<T>(Vector2 position = default) where T : Components.TileComponent, new()
        {
            var tileObject = new T();
            TileMap.Instance.Objects.Add(tileObject.TileObject);
            tileObject.TileObject.Position = position;
            return tileObject;
        }
    }
}
