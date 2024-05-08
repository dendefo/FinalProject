using Core;
using Renderer;
using System.Drawing;
using Core.Components;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    using Core.Actors;
    using System.Collections.Generic;

    [AppearOnlyOnce]
    internal abstract class ChessComponent : TileObjectComponent, IMovementProvider
    {
        abstract public IEnumerable<Position2D> GetPossibleMoves(Position2D selfPosition, Scene currentGameState);
        /// <summary>
        /// Returns all possible moves in a given direction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startPos"> Starting position</param>
        /// <param name="direction"> Direction to check the moves</param>
        /// <param name="currentGameState"> Current Map</param>
        /// <param name="thisControllerComponent"></param>
        /// <returns></returns>
        public static IEnumerable<Position2D> CheckInDirection(Position2D startPos, Position2D direction, Scene currentGameState, ControllerComponent thisControllerComponent)
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
        virtual public IEnumerable<Position2D> GetPossibleDestroyMoves(Position2D selfPosition, Scene currentGameState) =>
            GetPossibleMoves(selfPosition, currentGameState);

        virtual public IEnumerable<Position2D> FilterMoves(IEnumerable<Position2D> possibleMoves, Scene gameState, ControllerComponent thisControllerComponent, Position2D startPos)
        {
            //This is a lot of if statements, but it's the obly way i could think of to make it work
            ChessActor actor = Engine.Controllers[thisControllerComponent.ControllerID] as ChessActor;
            bool isInCheck = actor.IsInCheck(gameState);
            if (!isInCheck)
            {
                var temp = gameState[startPos].TileObject;
                if (temp.TryGetComponent<King>(typeof(King), out var king))
                {
                    King EnemyKing = null;
                    gameState.First(x => x.TileObject != null && x.TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var controller) &&
                    controller.ControllerID != thisControllerComponent.ControllerID && x.TileObject.TryGetComponent<King>(typeof(King), out EnemyKing));
                    if (EnemyKing == null) { throw new Exception("No King on the Board!"); }
                    possibleMoves = possibleMoves.Where(x => x.Distance(EnemyKing.Position) >= 2);
                    //This is dirty and slow, but works
                    possibleMoves = FilterForSelfCheck(possibleMoves, gameState, startPos, actor);
                    return possibleMoves;
                }
                //If piece that wants to move is not a player's king
                else
                {
                    return FilterForSelfCheck(possibleMoves, gameState, startPos, actor);
                }
            }
            else
            {
                return FilterForSelfCheck(possibleMoves, gameState, startPos, actor);
            }

        }
        private IEnumerable<Position2D> FilterForSelfCheck(IEnumerable<Position2D> attemptedMoves, Scene gameState, Position2D startPos, ChessActor actor)
        {
            List<Position2D> notCheckMoves = new();
            var temp = gameState[startPos].TileObject;
            foreach (var move in attemptedMoves)
            {
                gameState[startPos].TileObject = null;
                var tempPiece = gameState[move].TileObject;
                gameState[move].TileObject = temp;
                temp.Position = move;
                if (actor.IsInCheck(gameState))
                {
                    gameState[startPos].TileObject = temp;
                    gameState[move].TileObject = tempPiece;
                }
                else
                {
                    gameState[startPos].TileObject = temp;
                    gameState[move].TileObject = tempPiece;
                    notCheckMoves.Add(move);
                }
                temp.Position = startPos;
            }
            return notCheckMoves;
        }
        virtual public void MoveCallback(Position2D lastPosition, Position2D newPostion)
        {

        }
    }
}
