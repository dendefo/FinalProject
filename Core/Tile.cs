using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Tile<T> : IRenderable<T>
    {
        public event Action<Tile<T>,TileObject> ObjectEntered;
        public Vector2 Position { get; set; }
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
                if (tileObject!=null) ObjectEntered?.Invoke(this, tileObject);
            }
        }
        public VisualRepresentation<T> Visuals { get; set; }
        public Tile(Vector2 position, VisualRepresentation<T> visuals)
        {
            Position = position;
            Visuals = visuals;
        }
    }
}
