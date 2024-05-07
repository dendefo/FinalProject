using Core.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actors
{
    abstract public class Actor : IController
    {
        public int ControllerID { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        abstract public void StartTurn();
    }
}
