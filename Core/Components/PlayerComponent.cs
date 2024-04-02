using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Actors;

namespace Core.Components
{
    internal class PlayerComponent : TileComponent, IControllable
    {
        public int ControllerID { get; set; }
    }
}
