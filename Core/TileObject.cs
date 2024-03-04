using System.Numerics;
using Renderer;

namespace QuarterEngine.Core
{
    public class TileObject<T> : IRenderable<T>
    {
        public Vector2 Position { get; set; }

        public VisualRepresentation<T> Visuals { get; set; }
        public TileObject(VisualRepresentation<T> visuals)
        {
            Visuals = visuals;
        }
    }
}
