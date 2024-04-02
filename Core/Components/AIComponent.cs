using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Actors;

namespace Core.Components
{
    internal class AIComponent : ControllerComponent, IControllable
    {
        public override int ControllerID { get; set; }
    }
}
