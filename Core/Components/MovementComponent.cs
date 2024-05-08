using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    abstract public class MovementComponent : TileObjectComponent, IMovementProvider
    {
        public abstract IEnumerable<Position2D> FilterMoves(IEnumerable<Position2D> moves, Scene currentGameState, ControllerComponent controller, Position2D startPosition);
        public abstract IEnumerable<Position2D> GetPossibleDestroyMoves(Position2D selfPosition, Scene currentGameState);
        public abstract IEnumerable<Position2D> GetPossibleMoves(Position2D selfPosition, Scene currentGameState);
        public abstract void MoveCallback(Position2D lastPosition, Position2D newPostion);
    }
}
