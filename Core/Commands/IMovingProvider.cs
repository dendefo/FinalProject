using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    /// <summary>
    /// Interface for providing moving restrictions for objects. Should be implemented onto the componenets
    /// </summary>
    internal interface IMovingProvider
    {
        public IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition,Scene<T> currentGameState);
    }
}
