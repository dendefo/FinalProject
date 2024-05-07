using Core.Components;
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
    public interface IMovingProvider
    {
        public IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition,Scene<T> currentGameState);
        public IEnumerable<Position2D> GetPossibleDestroyMoves<T>(Position2D selfPosition, Scene<T> currentGameState);
        public IEnumerable<Position2D> FilterMoves<T>(IEnumerable<Position2D> moves, Scene<T> currentGameState, ControllerComponent controller, Position2D startPosition);
        abstract public void MoveCallback(Position2D lastPosition, Position2D newPostion);
    }
}
