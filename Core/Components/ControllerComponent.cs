using Core.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    [AppearOnlyOnce]
    abstract public class ControllerComponent : TileObjectComponent, IControllable
    {
        abstract public int ControllerID { get; set; }
    }
}
