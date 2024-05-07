using Core.Rendering;
using Newtonsoft.Json;
using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Core.Components
{
    /// <summary>
    /// Base component for all components that have a visual representation
    /// </summary>
    abstract public class RenderingComponent : TileComponent, IRenderable
    {
        /// <summary>
        /// Reference to the TileObject position
        /// </summary>
        [JsonIgnore]
        public Position2D Position { get { return TileObject == null ? default : TileObject.Position; } }
        /// <summary>
        /// Defines how component should be rendered
        /// </summary>
        public VisualRepresentation Visuals { get; set; }
        public bool isHighLighted { get; set; }
        public Color HighlightColor { get; set; }
    }
}
