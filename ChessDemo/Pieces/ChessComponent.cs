using Core;
using Renderer;
using System.Drawing;
using Core.Components;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    using Core.Commands;
    using System.Collections.Generic;

    [AppearOnlyOnce]
    internal abstract class ChessComponent : CustomComponent, IMovingProvider
    {
        abstract public IEnumerable<Position2D> GetPossibleMoves<T>(Position2D selfPosition, Scene<T> currentGameState);
        public static IEnumerable<Position2D> CheckInDirection<T>(Position2D startPos, Position2D direction, Scene<T> currentGameState, ControllerComponent thisControllerComponent)
        {
            int i = 0;
            List<Position2D> possibleMoves = new();
            while (currentGameState.IsInside(startPos + direction * (++i)))
            {
                if (currentGameState.IsEmpty(startPos + direction * i))
                    possibleMoves.Add(startPos + direction * i);
                else
                {
                    var comp = currentGameState[startPos + direction * i].TileObject.GetComponent<ControllerComponent>(typeof(ControllerComponent));
                    if (comp != null && comp.ControllerID != thisControllerComponent.ControllerID)
                        possibleMoves.Add(startPos + direction * i);
                    break;
                }
            }
            return possibleMoves;
        }
    }
}
