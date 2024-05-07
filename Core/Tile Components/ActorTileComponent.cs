using Core.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tile_Components
{
    public class ActorTileComponent : TileComponent, IControllable
    {
        public int ControllerID { get; set; }
    }
}
