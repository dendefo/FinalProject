using ChessDemo.Pieces;
using Core;
using Core.Actors;
using Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    public abstract class ChessActor : Actor
    {
        public int WinningDirection { get; set; }

        public bool IsInCheck(Scene gameState)
        {
            King thisPlayerKing = null;
            gameState.First(x => x.TileObject != null && x.TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var controller) && controller.ControllerID == ControllerID
            && x.TileObject.TryGetComponent(typeof(King), out thisPlayerKing));
            if (thisPlayerKing == null) { throw new Exception("No King on the Board"); }
            foreach (var tile in gameState)
            {
                if (tile.TileObject == null)
                    continue;
                //Goes Through all chess pieces on the board
                if (tile.TileObject.TryGetComponent<ChessComponent>(typeof(ChessComponent), out var comp))
                {
                    if (comp is King) continue;
                    //If the piece belongs to the other player
                    if (tile.TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var controller))
                    {
                        if (controller.ControllerID != ControllerID)
                        {
                            //If the other player can attack the king
                            if (comp.GetPossibleDestroyMoves(tile.Position, gameState).Contains(thisPlayerKing.Position))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
