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
        public override IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition, Scene<T> currentGameState)
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
            return _moves;
        }
    }
}
