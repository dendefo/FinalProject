using System.Drawing;
using System.Numerics;
using Renderer;

namespace Core.Rendering
{
    /// <summary>
    /// Interface for objects that can be rendered
    /// </summary>
    public interface IRenderable
    {
        Position2D Position { get; }
        public VisualRepresentation Visuals { get; set; }
    }
}
