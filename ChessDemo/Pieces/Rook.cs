using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ChessDemo.Pieces
{
    internal class Rook : ChessObject
    {
        public Rook(int PlayerId) : base(PlayerId)
        {
            Symbol = 'R';
        }
    }
}
