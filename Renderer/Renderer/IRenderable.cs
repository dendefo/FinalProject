using System.Drawing;
using System.Numerics;

namespace Renderer.Renderer
{
    public interface IRenderable<T>
    {
        Vector2 Position { get; set; }
        T Visual { get; set; }
        Color Color { get; set; }
    }
}
