global using Renderer.Renderer;
using System.Drawing;
using System.Numerics;

namespace QuarterEngine.Core
{
    public class TileObject<T> : IRenderable<T>
    {
        public Vector2 Position { get; set; }
        public T Visual { get; set; }
        public Color Color { get; set; }
    }
}
