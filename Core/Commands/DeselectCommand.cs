using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class DeselectCommand : Command
    {

        public DeselectCommand(string prompt) : base("Deselects the Object", prompt, false)
        {
        }

        public override void Activate(params string[] parameters)
        {
            CommandSystem.Instance.SelectedObject = null;
            ShowMessage(new("Object Deselected", System.Drawing.Color.Green));
            base.Activate(parameters);
        }
    }
}
