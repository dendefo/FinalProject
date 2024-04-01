using System.Drawing;
using System.Numerics;
using Renderer;

namespace Core.Rendering
{
    /// <summary>
    /// Interface for objects that can be rendered
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRenderable<T>
    {
        Position2D Position { get; }
        VisualRepresentation<T> Visuals { get; set; }
    }
}
