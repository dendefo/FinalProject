using ChessDemo.Commands;
using Core;
using Core.Actors;
using Core.AssetManagement;
using Core.Commands;
using Core.Components;
using Renderer;

namespace ChessDemo.Pieces
{
    using static Core.Engine;
    internal class Pawn : ChessComponent
    {
        static public Position2D CurrentEnPassaunt = default;
        bool isFirstMove = true;
        public override IEnumerable<Position2D> GetPossibleMoves(Position2D selfPosition, Scene currentGameState)
        {
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
            var controller = Controllers[thisControllerComponent.ControllerID] as ChessActor;
            var moves = new List<Position2D>();
            if (currentGameState.IsInside(selfPosition + new Position2D(0, controller.WinningDirection)) && currentGameState.IsEmpty(selfPosition + new Position2D(0, controller.WinningDirection)))
            {
                moves.Add(selfPosition + new Position2D(0, controller.WinningDirection));
                if (isFirstMove && currentGameState.IsEmpty(selfPosition + new Position2D(0, controller.WinningDirection * 2)))
                    moves.Add(selfPosition + new Position2D(0, controller.WinningDirection * 2));
            }
            return moves.Concat(GetPossibleDestroyMoves(selfPosition, currentGameState));
        }
        public override IEnumerable<Position2D> GetPossibleDestroyMoves(Position2D selfPosition, Scene currentGameState)
        {
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
            var controller = Controllers[thisControllerComponent.ControllerID] as ChessActor;
            List<Position2D> possibleDestroyPositions = new() { selfPosition + new Position2D(-1, controller.WinningDirection), selfPosition + new Position2D(1, controller.WinningDirection) };
            possibleDestroyPositions = possibleDestroyPositions.Where(x => currentGameState.IsInside(x) && currentGameState[x].TileObject != null && currentGameState[x].TileObject.GetComponent<ControllerComponent>
            (typeof(ControllerComponent)).ControllerID != thisControllerComponent.ControllerID).ToList();
            if (CurrentEnPassaunt != default)
            {
                if (selfPosition + new Position2D(-1, controller.WinningDirection) == CurrentEnPassaunt || selfPosition + new Position2D(1, controller.WinningDirection) == CurrentEnPassaunt)
                    possibleDestroyPositions.Add(CurrentEnPassaunt);
            }
            return possibleDestroyPositions;
        }

        public override string ToString()
        {
            return "p";
        }
        public override void MoveCallback(Position2D lastPosition, Position2D newPostion)
        {
            var controller = (Controllers[CurrentController] as ChessActor);
            Programm.HalfMoves = -1;
            if (newPostion == CurrentEnPassaunt && newPostion.Distance(lastPosition) > 1)
            {
                var direction = controller.WinningDirection;
                Destroy(CurrentScene[newPostion + new Position2D(0, -direction)].TileObject);
            }
            base.MoveCallback(lastPosition, newPostion);
            if (isFirstMove)
            {
                if (lastPosition.Distance(newPostion) == 2)
                    CurrentEnPassaunt = lastPosition + (newPostion - lastPosition) / 2;
                isFirstMove = false;
                return;
            }
        }

        public void Promote<T>(string PieceName) where T : ChessComponent, new()
        {
            var tileObj = TileObject;
            TileObject.RemoveComponent<Pawn>(typeof(Pawn));
            if (AssetManager.LoadAsset<T>(PieceName).TileObject.TryGetComponent<RenderingComponent>(typeof(RenderingComponent), out var rendering))
            {
                if (tileObj.TryGetComponent<RenderingComponent>(typeof(RenderingComponent), out var rend2)) rend2.Visuals = new(rendering.Visuals.Visual, rend2.Visuals.Color);
            }
            tileObj.AddComponent<T>();
        }
    }
}
