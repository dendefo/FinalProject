using Core.Actors;
using Core.Commands;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowRenderer;

namespace ChessDemo
{
    internal class ChessPlayerActor : ChessActor
    {
        override public void StartTurn()
        {
            switch (Programm.renderer)
            {
                case WindowRenderer.WindowRenderer renderer:
                    CommandSystem.Instance.Listen(() => CommandConverter.GetCommand());
                    break;
                case ConsoleRenderer:
                default:
                    CommandSystem.Instance.Listen(() => Console.ReadLine());
                    break;
            }
        }
    }
}
