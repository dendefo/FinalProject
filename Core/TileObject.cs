using System.Numerics;
using Renderer;

namespace Core
{
    using Components;
    [Serializable]
    public class TileObject
    {
        //how to save components without casting to TileComponent
        //copilot
        public List<TileComponent> components = new();
        public Vector2 Position { get; set; }

        public TileObject()
        {
        }
        public T AddComponent<T>(T origin = default) where T : TileComponent
        {
            if (origin != null)
            {
                var newComponent = origin.Copy(origin);
                components.Add(newComponent);
                newComponent.TileObject = this;
                return newComponent;
            }
            var ctr = typeof(T).GetConstructors();
            var Component = (T)ctr[ctr.Length-1].Invoke(new object[] { this });
            components.Add(Component);
            return Component;
        }
        public T GetComponent<T>() where T : TileComponent
        {
            return components.Find(x => x is T) as T;
        }

    }
}
