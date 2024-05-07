using System.Numerics;
using Renderer;

namespace Core
{
    using Components;
    using Core.Actors;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents a tile object
    /// The core of the engine
    /// </summary>
    [Serializable]
    public class TileObject : IDisposable
    {
        public Action<TileObject> OnPassedOn;
        public Action<TileObject> OnPassedOver;
        public List<TileObjectComponent> components = new();
        [JsonIgnore]
        private Position2D position;
        public Position2D Position
        {
            get { return position; }
            set { position = value; }
        }
        public Position2D PositionToPrint
        {
            get { return new Position2D(Position.x, CurrentScene.Height - Position.y-1); }
        }
        internal TileObject() { }

        /// <summary>
        /// Adds a component to the tile object
        /// </summary>
        /// <typeparam name="T"> Type of new Component</typeparam>
        /// <param name="origin"> If provided, copies the value of origin to new component</param>
        /// <returns></returns>
        public T AddComponent<T>(T origin = default) where T : TileObjectComponent, new()
        {

            if (origin != null && GetComponent<T>(origin.GetType()) != null && origin.GetType().GetCustomAttributes(true).Any(x => x.GetType() == typeof(AppearOnlyOnceAttribute)))
            {
                return GetComponent<T>(origin.GetType());
            }
            if (origin != null)
            {
                T newComponent = origin.Clone() as T;
                components.Add(newComponent);
                newComponent.TileObject = this;
                UpdateColorByController();
                return newComponent.GetComponent<T>(origin.GetType());
            }
            var ctr = new T();
            if (ctr.TileObject != null) ctr.TileObject.Dispose();
            ctr.TileObject = this;
            components.Add(ctr);
            UpdateColorByController();
            return ctr;
        }
        public void UpdateColorByController()
        {
            if (GetComponent<RenderingComponent>(typeof(RenderingComponent)) is RenderingComponent rend)
            {
                var Color = Controllers[(components.First((x => x is ControllerComponent)) as IControllable).ControllerID].Color;
                if (Color != default) rend.Visuals = new(rend.Visuals, Color);
            }
        }
        /// <summary>
        /// Gets a component of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns> First instance of T component in TileObject</returns>
        public T GetComponent<T>(Type type) where T : TileObjectComponent
        {
            foreach (var component in components)
            {
                var t1 = component.GetType();
                if (type.IsInstanceOfType(component) || t1 == type)
                {
                    return component as T;
                }
            }
            return null;
        }

        public bool TryGetComponent<T>(Type type, out T component) where T : TileObjectComponent
        {
            foreach (var c in components)
            {
                if (c.GetType() == type || type.IsInstanceOfType(c))
                {
                    component = c as T;
                    return true;
                }
            }
            component = null;
            return false;
        }

        public bool RemoveComponent<T>(Type type) where T : TileObjectComponent
        {
            foreach (var component in components)
            {
                var t1 = component.GetType();
                if (t1 == type)
                {
                    components.Remove(component);
                    component.TileObject = null;
                    component.Dispose();
                    return true;
                }
            }
            return false;
        }

        internal void Dispose()
        {
            components.ForEach(c => c.Dispose());
            components.Clear();
            OnPassedOn = null;
            OnPassedOver = null;

        }
        void IDisposable.Dispose()
        {
            Dispose();

        }
    }
}
