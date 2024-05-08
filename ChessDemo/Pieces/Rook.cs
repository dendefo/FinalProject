using Core;
using Core.Components;
using Newtonsoft.Json;
using Renderer;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    internal class Rook : ChessComponent
    {
        public bool isFirstMove = true;
        public override IEnumerable<Position2D> GetPossibleMoves(Position2D selfPosition, Scene currentGameState)
        {
            IEnumerable<Position2D> possibleMoves = new List<Position2D>();
            int i = 0;
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(0, 1), currentGameState, thisControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(0, -1), currentGameState, thisControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(-1, 0), currentGameState, thisControllerComponent));
            possibleMoves = possibleMoves.Concat(CheckInDirection(selfPosition, new Position2D(1, 0), currentGameState, thisControllerComponent));
            return possibleMoves;
        }
        public override void MoveCallback(Position2D lastPosition, Position2D newPostion)
        {
            if (isFirstMove)
            {
                isFirstMove = false;
                return;
            }
            base.MoveCallback(lastPosition, newPostion);
        }
        public override string ToString()
        {
            return "r";
        }
    }
}
