using System.Numerics;
using Renderer;

namespace Core
{
    using Components;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents a tile object
    /// The core of the engine
    /// </summary>
    [Serializable]
    public class TileObject : IDisposable
    {
        public List<TileComponent> components = new();
        [JsonIgnore]
        private Position2D position;
        public Position2D Position
        {
            get { return position; }
            set
            {
                if (value.x < 0 || value.y < 0 || value.x >= Engine.CurrentScene.Width || value.y >= Engine.CurrentScene.Height)
                {
                    return;
                }
                if (Engine.CurrentScene[position].TileObject == this)
                    Engine.CurrentScene[position].TileObject = null;
                position = value;
                if (Engine.CurrentScene[position].TileObject == null)
                    Engine.CurrentScene[position].TileObject = this;
            }
        }
        internal TileObject() { }
        internal TileObject(int x, int y)
        {
            Position = new Position2D(x, y);
        }

        /// <summary>
        /// Adds a component to the tile object
        /// </summary>
        /// <typeparam name="T"> Type of new Component</typeparam>
        /// <param name="origin"> If provided, copies the value of origin to new component</param>
        /// <returns></returns>
        public T AddComponent<T>(T origin = default) where T : TileComponent, new()
        {
            if (origin != null)
            {
                var newComponent = TileComponent.Copy(origin);
                components.Add(newComponent);
                newComponent.TileObject = this;
                return newComponent.GetComponent<T>(origin.GetType());
            }
            var ctr = new T();
            Engine.Destroy(ctr.TileObject);
            ctr.TileObject = this;
            components.Add(ctr);
            return ctr.GetComponent<T>(typeof(T));
        }

        /// <summary>
        /// Gets a component of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns> First instance of T component in TileObject</returns>
        public T GetComponent<T>(Type type) where T : TileComponent
        {
            foreach (var component in components)
            {
                var t = typeof(T);
                var t1 = component.GetType();
                if (t1 == type)
                {
                    return component as T;
                }
            }
            return null;
        }

        internal void Dispose()
        {
            components.ForEach(c => c.TileObject = null);
            components.Clear();

        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}
