using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Commands
{
    using static Core.Engine<char>;
    public class SelectCommand : Command
    {
        public SelectCommand(string prompt) : base("Select", "Selects object in coordinated", prompt)
        {
        }
        public override void Activate(params string[] parameters)
        {
            int pos; uint charvalue = 0;
            if (parameters.Length <= 1) { ShowMessage("No parameters provided"); return; }
            else if (parameters.Length == 2)
            {
                var match = Regex.Matches(parameters[1], @"[a-zA-Z]+|[0-9]+(?:[0-9]+|)").ToArray();
                if (match.Length != 2) { ShowMessage("Invalid parameters"); return; }
                int temp;
                if (int.TryParse(match[0].Value, out pos) && !int.TryParse(match[1].Value, out temp))
                {
                    var en = match[1].Value.GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else if (int.TryParse(match[1].Value, out pos) && !int.TryParse(match[0].Value, out temp))
                {
                    var en = match[0].Value.GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else
                {
                    ShowMessage("Invalid parameters, they should look like a4 or 4a or 4 a");
                    return;
                }
            }
            else if (parameters.Length == 3)
            {
                int temp;
                if (int.TryParse(parameters[1], out pos) && !int.TryParse(parameters[2], out temp))
                {
                    var en = parameters[2].GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else if (int.TryParse(parameters[2], out pos) && !int.TryParse(parameters[1], out temp))
                {
                    var en = parameters[1].GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else
                {
                    ShowMessage("Invalid parameters, they should look like a4 or 4a or 4 a");
                    return;
                }
            }
            else { ShowMessage("Too Much Parameters"); return; }
            if (charvalue >= CurrentScene.Width || pos >= CurrentScene.Height)
            {
                ShowMessage("Invalid parameters, out of bounds");
                return;
            }
            var obj = CurrentScene[new Position2D(pos, (int)charvalue)].TileObject;
            if (obj == null)
            {
                ShowMessage("No object at this position");
                return;
            }
            CommandSystem.Instance.SelectedObject = obj;
            ShowMessage($"{obj.Position.ToString()} selected");
        }
    }
}
