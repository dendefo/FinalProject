using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    public abstract class TileComponent
    {
        public TileObject TileObject { get; set; }
        public TileComponent(TileObject tileObject = null)
        {
            if (tileObject == null)
            {
                TileObject = Engine.Instantiate<TileObject>(new(), default);
                TileObject.components.Add(this);
            }
            else TileObject = tileObject;
        }
        public T AddComponent<T>() where T : TileComponent => TileObject.AddComponent<T>();
    }
}
