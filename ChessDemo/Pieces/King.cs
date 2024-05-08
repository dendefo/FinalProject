using Core;
using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo.Pieces
{
    internal class King : ChessComponent
    {
        public bool isFirstMove = true;
        private List<Position2D> _positions = new()
        {
            new Position2D(1, 0),
            new Position2D(0, 1),
            new Position2D(-1, 0),
            new Position2D(0, -1),
            new Position2D(1, 1),
            new Position2D(1, -1),
            new Position2D(-1, -1),
            new Position2D(-1, 1)
        };
        public override IEnumerable<Position2D> GetPossibleMoves(Position2D selfPosition, Scene currentGameState)
        {
            var thisControllerComponent = currentGameState[selfPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
            List<Position2D> _moves = new();
            foreach (var move in _positions)
            {
                var newPosition = selfPosition + move;
                if (currentGameState.IsInside(newPosition))
                {
                    if (currentGameState[newPosition].TileObject != null)
                    {
                        var comp = currentGameState[newPosition].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
                        if (comp != null && comp.ControllerID != thisControllerComponent.ControllerID)
                            _moves.Add(newPosition);
                    }
                    else
                        _moves.Add(newPosition);
                }
            }
            //Check fo castling
            if (isFirstMove)
            {
                var rooks = currentGameState.Where(x => x.TileObject != null && x.TileObject.TryGetComponent<Rook>(typeof(Rook), out var rook) && rook.isFirstMove && rook.GetComponent<ControllerComponent>(typeof(ControllerComponent)).ControllerID == thisControllerComponent.ControllerID);
                foreach (var rook in rooks)
                {
                    var direction = rook.Position.x > selfPosition.x ? new Position2D(1, 0) : new Position2D(-1, 0);
                    var distance = Math.Abs(rook.Position.x - selfPosition.x);
                    bool canCastle = true;
                    for (int i = 1; i < distance; i++)
                    {
                        if (!currentGameState.IsEmpty(selfPosition + direction * i))
                        {
                            canCastle = false;
                            break;
                        }
                    }
                    if (canCastle)
                    {
                        _moves.Add(selfPosition + direction * 2);
                    }
                }
            }
            return _moves;
        }
        public override void MoveCallback(Position2D lastPosition, Position2D newPostion)
        {
            if (isFirstMove)
            {
                isFirstMove = false;
                //if castled (with little error handling)
                if (lastPosition.Distance(newPostion) == 2)
                {
                    //Look for the closest rook and move next to king
                    var isMovedLeft = lastPosition.x > newPostion.x;
                    var direction = isMovedLeft ? new Position2D(-2, 0) : new Position2D(1, 0);
                    var rookPosition = newPostion + direction;
                    var rook = Engine.CurrentScene[rookPosition].TileObject.GetComponent<Rook>(typeof(Rook));
                    var newRookPosition = newPostion + new Position2D(isMovedLeft ? 1 : -1, 0);
                    Engine.MoveObject(rook.TileObject, newRookPosition);
                }
            }
            base.MoveCallback(lastPosition, newPostion);
        }

        public override string ToString()
        {
            return "k";
        }
    }
}
