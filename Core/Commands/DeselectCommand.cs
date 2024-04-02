using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class DeselectCommand:Command
    {
        public DeselectCommand(string prompt):base("Deselect", "Deselects the Object",prompt)
        {
        }
        public override void Activate(params string[] parameters)
        {
            CommandSystem.Instance.SelectedObject = null;
            Console.WriteLine("Object Deselected");
        }
    }
}
