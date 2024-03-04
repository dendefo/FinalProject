using System.Drawing;
using System.Numerics;

namespace Renderer
{
    public interface IRenderable<T>
    {
        Vector2 Position { get; set; }
        VisualRepresentation<T> Visuals { get; set; }
    }
}
