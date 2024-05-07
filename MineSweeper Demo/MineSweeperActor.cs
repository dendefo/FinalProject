using Core.Actors;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper_Demo
{
    internal class MineSweeperActor : Actor
    {
        public override void StartTurn()
        {
            CommandSystem.Instance.Listen(() => Console.ReadLine());
        }
    }
}
