using Core;
using Core.Components;
using Newtonsoft.Json;
using Renderer;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    internal class Bishop : ChessComponent
    {
        public override IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition, Scene<T> currentGameState)
        {

            IEnumerable<Position2D> possibleMoves = new List<Position2D>();
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));

            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(1, 1), currentGameState, thisControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(1, -1), currentGameState, thisControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(-1, -1), currentGameState, thisControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(-1, 1), currentGameState, thisControllerComponent));
            return possibleMoves;
        }

        public override string ToString()
        {
            return $"Bishop at Position {TileObject.Position}";
        }
    }
}
