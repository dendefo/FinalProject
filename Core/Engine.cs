using System.Numerics;
using System.Text.Json;
using Core.Components;
using Renderer;

namespace Core
{
    /// <summary>
    /// Core class of the engine
    /// Has the main methods to interact with the engine
    /// Should be included in using as "using static Core.Engine;"
    /// </summary>
    public class Engine
    {
        private readonly IRenderer<char> renderer;
        public static Scene CurrentScene;
        public static Engine Start(int width, int height)
        {
            return new Engine(width, height);
        }

        /// <summary>
        /// Hides constructor
        /// </summary>
        private Engine() { }
        private Engine(int width, int height)
        {
            CurrentScene = new Scene(width, height);
            renderer = new ConsoleRenderer();
        }

        public void EndTurn()
        {
            foreach (var item in CurrentScene.Objects)
            {
                foreach (var component in item.components)
                {
                    if (component is IRenderable<char> renderable)
                    {
                        renderer.RenderObject(renderable, renderable);
                    }
                }
            }
            Console.SetCursorPosition(CurrentScene.Width, CurrentScene.Height);
        }
        public TileObject[] tileObjects => CurrentScene.Objects.ToArray();
        static public T Instantiate<T>() where T : TileObject, new()
        {
            var tileObject = new T();
            CurrentScene.Objects.Add(tileObject);
            return tileObject;
        }
        static public TileObject Instantiate(TileObject origin)
        {
            var tileObject = Instantiate<TileObject>();
            foreach (var component in origin.components)
            {
                var newComponent = tileObject.AddComponent(component);
                newComponent.TileObject = tileObject;
            }
            return tileObject;
        }
        static public T Instantiate<T>(T origin) where T : TileComponent, new()
        {
            var NewObject = Instantiate<TileObject>();
            T toReturn = null;
            foreach (var OriginalComponent in origin.TileObject.components)
            {
                var t = OriginalComponent.GetType();

                var component = NewObject.AddComponent(OriginalComponent);
                component.TileObject = NewObject;
                if (t == typeof(T)) toReturn = component as T;
            }
            return toReturn;
        }
        static public void Destroy<T>(T obj) where T : TileObject
        {
            CurrentScene.Objects.Remove(obj);
        }
    }
}
