global using static Core.Engine<char>;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using Core.Actors;
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
        static public IController[] Controllers;
        public static int CurrentController = 0;
        static private Queue<MessageLine> messageToShow = new();
        private static IRenderer<TVisual> Renderer;
        public static Scene<TVisual> CurrentScene;
        public static Engine<TVisual> SetUp(int width, int height, IRenderer<TVisual> renderer)
        {
            return new Engine<TVisual>(width, height, renderer);
        }
        public static void DefinePlayers(params IController[] controllers)
        {
            Controllers = controllers;
            for (int i = 0; i < Controllers.Length; i++)
            {
                Controllers[i].ControllerID = i;
            }
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
                bool isMovingPosition = moves.Contains(position);
                bool isDestroyingPosition = movProvider.GetPossibleDestroyMoves(obj.Position, CurrentScene).Contains(position);
                if (!isMovingPosition && !isDestroyingPosition)
                    return false;

                if (IsEmpty(position) && isMovingPosition)
                {
                    movProvider.MoveCallback(obj.Position, position);
                    SetPosition(obj, position);
                    return true;
                }
                else if (DestroyIfOccupied && isDestroyingPosition)
                {
                    movProvider.MoveCallback(obj.Position, position);
                    Destroy(CurrentScene[position].TileObject);
                    SetPosition(obj, position);

                    return true;
                }
            }
            return false;
        }
        public static bool ShowMoves(TileObject obj)
        {
            var movProvider = obj.components.FirstOrDefault(comp => comp is IMovingProvider) as IMovingProvider;
            if (movProvider != null)
            {
                var moves = movProvider.GetPossibleMoves(obj.Position, CurrentScene);
                ShowMessage(new("Possible moves: ", Color.Green));
                
                foreach (var move in moves)
                {
                    ShowMessage(new($"{move}", Color.Green));
                }
                return true;
            }
            return false;
        }
        static public void Render()
        {
            Console.Clear();
            Renderer.RenderScene(CurrentScene);
            while (messageToShow.TryDequeue(out MessageLine message))
            {
                Renderer.ShowMessage(message);
            }

        }
        #region Instantiation functions
        /// <summary>
        /// Creates a new TileObject and Sets it's position right away
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static public TileObject Instantiate(Position2D position, IController controller = null)
        {
            var tileObject = new TileObject();
            if (controller != null)
            {
                if (controller is AIActor c)
                {
                    var comp = tileObject.AddComponent<AIComponent>();
                    comp.ControllerID = c.ControllerID;
                }
                else if (controller is PlayerActor p)
                {
                    var comp = tileObject.AddComponent<PlayerComponent>();
                    comp.ControllerID = p.ControllerID;
                }
            }
            SetPosition(tileObject, position);
            return tileObject;
        }
        /// <summary>
        /// Creates a new TileObject based on Origin
        /// </summary>
        /// <param name="origin"> Origin to copy from</param>
        /// <param name="position"> Position on tileMap to place on</param>
        /// <returns></returns>
        static public TileObject Instantiate(TileObject origin, Position2D position = default, IController controller = null)
        {
            var tileObject = Instantiate(position, controller);
            foreach (var component in origin.components)
            {
                var newComponent = tileObject.AddComponent(component);
                newComponent.TileObject = tileObject;
            }
            if (tileObject.components.First(x => x is RenderingComponent<TVisual>) is RenderingComponent<TVisual> rend)
            {
                var Color = Controllers[(tileObject.components.First((x => x is PlayerComponent || x is AIComponent)) as IControllable).ControllerID].Color;
                if (Color != default) rend.Visuals = new(rend.Visuals, Color);
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
        static public T Instantiate<T>(T origin, Position2D position = default, IController controller = null) where T : TileComponent, new()
        {
            var NewObject = Instantiate(position, controller);
            T toReturn = null;
            foreach (var OriginalComponent in origin.TileObject.components)
            {
                var t = OriginalComponent.GetType();

                var component = NewObject.AddComponent(OriginalComponent);
                component.TileObject = NewObject;
                if (t == typeof(T)) toReturn = component as T;
            }
            if (NewObject.components.First(x => x is RenderingComponent<TVisual>) is RenderingComponent<TVisual> rend)
            {
                var Color = Controllers[(NewObject.components.First((x => x is PlayerComponent || x is AIComponent)) as IControllable).ControllerID].Color;
                if (Color != default) rend.Visuals = new(rend.Visuals, Color);
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
        static public void ShowMessage(MessageLine message)
        {
            messageToShow.Enqueue(message);
        }
        public static void Play()
        {
            Command.CommandExecuted += Command_CommandExecuted;
            while (true)
            {
                var controller = Controllers[CurrentController];
                ShowMessage(new($"{controller.Name} Player turn", controller.Color));
                Render();
                controller.StartTurn();
            }
        }

        private static void Command_CommandExecuted(Command obj)
        {
            if (obj.DoesEndTurn)
            {
                CurrentController = (CurrentController + 1) % Controllers.Length;
                CommandSystem.Instance.SelectedObject = null;
            }
        }
    }
}
