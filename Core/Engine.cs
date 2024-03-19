using System.Numerics;
using System.Text.Json;
using Core.Components;
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
        static public T Instantiate<T>(T origin, Vector2 position = default) where T : TileObject
        {
            var newtileObject = JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(origin));

            origin.components.ForEach(x => newtileObject.AddComponent(x.Copy(x)).TileObject = newtileObject);
            TileMap.Instance.Objects.Add(newtileObject);
            newtileObject.Position = position;
            return newtileObject;
        }
        static public T Instantiate<T>() where T : TileObject, new()
        {
            var tileObject = new T();
            TileMap.Instance.Objects.Add(tileObject);
            return tileObject;
        }

        static public T Instantiate<T>(Vector2 position = default) where T : TileComponent, new()
        {
            var component = new T();
            TileMap.Instance.Objects.Add(component.TileObject);
            component.TileObject.Position = position;
            return component;
        }
        static public T Instantiate<T>(T origin) where T : TileComponent, new()
        {
            var obj = Instantiate<TileObject>();
            T toReturn = null;
            foreach (var x in origin.TileObject.components)
            {
                var t = x.GetType();
                
                var lol = obj.AddComponent(x.Copy(x));
                if (t==typeof(T)) toReturn = lol as T;
            }

            TileMap.Instance.Objects.Add(obj);
            return toReturn;
        }
    }
}
