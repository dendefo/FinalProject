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

        public void StartGame()
        {
            foreach (var item in CurrentScene)
            {
                renderer.RenderObject(item.TileObject?.components.Find(x => x is IRenderable<char>) as IRenderable<char>, item);
            }
            Console.SetCursorPosition(0, CurrentScene.Height);
            Console.ReadKey();
        }
        #region Instantiation functions
        /// <summary>
        /// Creates a new TileObject and Sets it's position right away
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static public TileObject Instantiate(Position2D position)
        {
            var tileObject = new TileObject(position.x, position.y);
            return tileObject;
        }
        /// <summary>
        /// Creates a new TileObject based on Origin
        /// </summary>
        /// <param name="origin"> Origin to copy from</param>
        /// <param name="position"> Position on tileMap to place on</param>
        /// <returns></returns>
        static public TileObject Instantiate(TileObject origin, Position2D position = default)
        {
            var tileObject = Instantiate(position);
            foreach (var component in origin.components)
            {
                var newComponent = tileObject.AddComponent(component);
                newComponent.TileObject = tileObject;
            }
            return tileObject;
        }
        /// <summary>
        /// Creates a new TileObject based on Origin`s TileObject
        /// </summary>
        /// <typeparam name="T"> Type of component </typeparam>
        /// <param name="origin"> TileComponent to copy TileObject from </param>
        /// <param name="position"> Position to put new object on TileMap</param>
        /// <returns> Component of type <typeparamref name="T"/> </returns>
        static public T Instantiate<T>(T origin, Position2D position = default) where T : TileComponent, new()
        {
            var NewObject = Instantiate(position);
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
        #endregion
        /// <summary>
        /// Deletes object from the scene
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        static public void Destroy<T>(T obj) where T : TileObject
        {
            if (obj == null) return;
            CurrentScene[obj.Position].TileObject = null;
            obj.Dispose();
        }
    }
}
