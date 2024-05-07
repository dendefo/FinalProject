using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Interface for providing moving restrictions for objects. Should be implemented onto the componenets
    /// </summary>
    public interface IMovementProvider
    {
        public IEnumerable<Position2D> GetPossibleMoves(Position2D selfPosition, Scene currentGameState);
        public IEnumerable<Position2D> GetPossibleDestroyMoves(Position2D selfPosition, Scene currentGameState);
        public IEnumerable<Position2D> FilterMoves(IEnumerable<Position2D> moves, Scene currentGameState, ControllerComponent controller, Position2D startPosition);
        abstract public void MoveCallback(Position2D lastPosition, Position2D newPostion);
    }
}
