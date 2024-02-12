using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Engine
{
    internal interface ITileObject
    {
        public char Symbol { get; }
        public ConsoleColor ObjectColor { get; }
    }
}
