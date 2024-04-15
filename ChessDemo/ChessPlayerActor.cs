using Core.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    internal class ChessPlayerActor : PlayerActor
    {
        public int WinningDirection { get; set; }
    }
}
