using Core.Rendering;
using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Tile : IRenderable
    {
        public static Action<Tile, TileObject> ObjectEntered;
        public static Action<Tile, TileObject> ObjectPassedOver;
        public Position2D Position { get; set; }
        public bool isHighLighted;
        public Color HighlightColor { get; set; }
        private TileObject tileObject;
        public TileObject TileObject
        {
            get
            {
                return tileObject;
            }
            set
            {
                tileObject = value;
            }
        }
        public VisualRepresentation Visuals { get; set; }
        public Tile(Position2D position, VisualRepresentation visuals)
        {
            Position = position;
            Visuals = visuals;
        }
    }
}
