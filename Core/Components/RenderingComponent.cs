using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    abstract public class RenderingComponent<T> : TileComponent, IRenderable<T>
    {
        public Vector2 Position { get { return TileObject.Position; } set { } }
        public VisualRepresentation<T> Visuals { get; set; }
        
        public RenderingComponent(TileObject tileObject = null) : base(tileObject)
        {
        }
        public void SetVisuals(T visual, Color color)
        {
            Visuals = new(visual, color);
        }
    }
}
