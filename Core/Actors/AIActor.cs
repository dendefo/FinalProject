using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actors
{
    public class AIActor : IController
    {
        public int ControllerID { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public void StartTurn()
        {
            throw new NotImplementedException();
        }
    }
}
