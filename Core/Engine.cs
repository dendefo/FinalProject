global using static Core.Engine<char>;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using Core.Commands;
using Core.Components;
using Core.Rendering;
using Renderer;

namespace Core
{
    /// <summary>
    /// Core class of the engine
    /// Has the main methods to interact with the engine
    /// Should be included in using as "using static Core.Engine;"
    /// </summary>
    public class Engine<TVisual>
    {
        static private string messageToShow = "";
        private static IRenderer<TVisual> Renderer;
        public static Scene<TVisual> CurrentScene;
        public static Engine<TVisual> Start(int width, int height, IRenderer<TVisual> renderer)
        {
            return new Engine<TVisual>(width, height, renderer);
        }

        /// <summary>
        /// Hides constructor
        /// </summary>
        private Engine() { }
        private Engine(int width, int height, IRenderer<TVisual> renderer)
        {
            CurrentScene = new Scene<TVisual>(width, height);
            Renderer = renderer;
        }

        internal static bool SetPosition(TileObject obj, Position2D position)
        {
            if (position.x < 0 || position.y < 0 || position.x >= CurrentScene.Width || position.y >= CurrentScene.Height)
            {
                return false;
            }
            if (CurrentScene[obj.Position].TileObject == obj)
            {
                CurrentScene[obj.Position].TileObject = null;
            }
            if (CurrentScene[position].TileObject == null)
            {
                CurrentScene[position].TileObject = obj;
                obj.Position = position;
                return true;
            }
            return false;
        }
        public static bool IsEmpty(Position2D position) => CurrentScene[position].TileObject == null;
        public static bool IsEmpty(int x, int y) => CurrentScene[x, y].TileObject == null;
        /// <summary>
        /// Moves object to new Tile If it's empty
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="position"></param>
        /// <param name="DestroyIfOccupied"> Specify if Object in <paramref name="position"/> shoud be destroyed </param>
        /// <returns> Returns true if object changed it's position </returns>
        public static bool MoveObject(TileObject obj, Position2D position, bool DestroyIfOccupied = false)
        {
            var movProvider = obj.components.FirstOrDefault(comp => comp is IMovingProvider) as IMovingProvider;
            if (movProvider != null)
            {
                var moves = movProvider.GetPossibleMoves(obj.Position, CurrentScene);
                if (!moves.Contains(position)) return false;
            }
            if (IsEmpty(position))
            {
                SetPosition(obj, position);
                return true;
            }
            else if (DestroyIfOccupied)
            {
                Destroy(CurrentScene[position].TileObject);
                SetPosition(obj, position);
                return true;
            }
            return false;
        }
        static public void Render()
        {
            Console.Clear();
            foreach (var item in CurrentScene)
            {
                Renderer.RenderObject(item.TileObject?.components.Find(x => x is IRenderable<TVisual>) as IRenderable<TVisual>, item);
            }
            Console.SetCursorPosition(0, CurrentScene.Height);
            Renderer.ShowMessage(messageToShow);
            messageToShow = "";

        }
        #region Instantiation functions
        /// <summary>
        /// Creates a new TileObject and Sets it's position right away
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static public TileObject Instantiate(Position2D position)
        {
            var tileObject = new TileObject();
            SetPosition(tileObject, position);
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
        static public void ShowMessage(string message)
        {
            messageToShow += message + "\n";
        }
    }
}
