using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actors
{
    public interface IController
    {
        int ControllerID { get; set; }
        string Name { get; set; }
        Color Color { get; set; }
        public void StartTurn();
    }
}
