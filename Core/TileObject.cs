using System.Numerics;
using Renderer;

namespace Core
{
    using Components;
    public class TileObject
    {
        public List<TileComponent> components = new();
        public Vector2 Position { get; set; }

        public TileObject()
        {
        }
        public T AddComponent<T>() where T : TileComponent
        {

            var ctr = typeof(T).GetConstructors();
            var Component = (T)ctr[0].Invoke(new object[] { this });
            components.Add(Component);
            return Component;
        }

    }
}
