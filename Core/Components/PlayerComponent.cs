using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Actors;

namespace Core.Components
{
    public class PlayerComponent : ControllerComponent
    {
        public override int ControllerID { get; set; }
    }
}
