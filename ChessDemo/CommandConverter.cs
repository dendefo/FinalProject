using Core;
using Core.Commands;
using Core.Components;
using Core.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessDemo
{
    using WindowRenderer;
    public class CommandConverter
    {
        static private CommandConverter _instance;
        WindowRenderer _renderer;
        static string command = "";
        public static void Initialize(WindowRenderer renderer)
        {
            _instance = new();
            AssignEvents();
            _instance._renderer = renderer;
        }

        public static void AssignEvents()
        {
            Application.OpenForms[0].Click += _instance.OnClick;
        }

        private void OnClick(object? sender, EventArgs e)
        {
            MouseEventArgs E = e as MouseEventArgs;
            int x = (int)(E.X / _renderer.size);
            int y = (int)(E.Y / _renderer.size);
            if(!Engine.CurrentScene.IsInside(x, y))
            {
                command = "deselect";
                return;
            }
            if (E.Button == MouseButtons.Left)
            {
                if (Engine.CurrentScene[x, y].TileObject == null)
                {
                    command = "move " + IRenderer.ConvertIntToString(x + 1) + (8 - y);
                }
                else if (Engine.CurrentScene[x, y].TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var comp))
                {
                    if (comp.ControllerID == Engine.CurrentController)
                        command = "select " + IRenderer.ConvertIntToString(x + 1) + (8 - y);
                    else command = "move " + IRenderer.ConvertIntToString(x + 1) + (8 - y);
                }
            }
            else if(E.Button == MouseButtons.Right)
            {
                command = "show";
            
            }

        }

        public static string GetCommand()
        {
            while (command == "")
            {

            }
            string temp = command;
            command = "";
            return temp;
        }
    }
}
