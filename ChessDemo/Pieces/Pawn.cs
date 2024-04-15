using Core;
using Core.Actors;
using Core.Components;
using Renderer;

namespace ChessDemo.Pieces
{
    using static Core.Engine<char>;
    internal class Pawn : ChessComponent
    {
        bool isFirstMove = true;
        public override IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition, Scene<T> currentGameState)
        {
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
            var controller = Controllers[thisControllerComponent.ControllerID] as ChessPlayerActor;
            var moves = CheckInDirection(selfPosition, new(0, controller.WinningDirection), currentGameState, thisControllerComponent);
            moves = moves.Where(x => selfPosition.Distance(x) <= (isFirstMove ? 2f : 1f));
            return moves;
        }
        public override IEnumerable<Position2D> GetPossibleDestroyMoves<T>(Position2D selfPosition, Scene<T> currentGameState)
        {
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
            var controller = Controllers[thisControllerComponent.ControllerID] as ChessPlayerActor;
            List<Position2D> possibleDestroyPositions = new() { selfPosition + new Position2D(-1, controller.WinningDirection), selfPosition + new Position2D(1, controller.WinningDirection) };
            return possibleDestroyPositions.Where(x => currentGameState.IsInside(x) && currentGameState[x].TileObject != null && currentGameState[x].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent)).ControllerID != thisControllerComponent.ControllerID);
        }

        public override string ToString()
        {
            return $"Pawn at Position {TileObject.Position}";
        }
        public override void MoveCallback(Position2D lastPosition, Position2D newPostion)
        {
            base.MoveCallback(lastPosition, newPostion);
            if (isFirstMove)
            {
                isFirstMove = false;
                return;
            }
        }
    }
}
