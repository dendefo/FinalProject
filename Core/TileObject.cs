using System.Numerics;
using Renderer;

namespace Core
{
    using Components;
    /// <summary>
    /// A class that represents a tile object
    /// The core of the engine
    /// </summary>
    [Serializable]
    public class TileObject
    {
        public List<TileComponent> components = new();
        public Vector2 Position { get; set; }

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

    }
}
