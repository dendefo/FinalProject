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
    /// <typeparam name="T"></typeparam>
    abstract public class RenderingComponent<T> : TileComponent, IRenderable<T>
    {
        /// <summary>
        /// Reference to the TileObject position
        /// </summary>
        [JsonIgnore]
        public Vector2 Position { get { return TileObject == null ? default : TileObject.Position; } }
        /// <summary>
        /// Defines how component should be rendered
        /// </summary>
        public VisualRepresentation<T> Visuals { get; set; }
    }
}
