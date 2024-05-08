using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class ShowCommand : Command
    {
        public ShowCommand(string prompt) : base("Shows possible moves for piece or pieces, that can move", prompt, false)
        {
        }
        public override void Activate(params string[] parameters)
        {
            if (CommandSystem.Instance.SelectedObject != null)
            {
                ShowMoves(CommandSystem.Instance.SelectedObject);
            }
            else
            {
                foreach (var tile in CurrentScene)
                {
                    if (tile.TileObject == null) continue;
                    if (tile.TileObject.TryGetComponent<ControllerComponent>(typeof(ControllerComponent), out var comp))
                    {
                        if (comp.ControllerID == CurrentController)
                        {
                            if (tile.TileObject.TryGetComponent<MovementComponent>(typeof(MovementComponent), out var movProvider))
                            {
                                var moves = movProvider.FilterMoves(movProvider.GetPossibleMoves(tile.Position, CurrentScene).
                                    Concat(movProvider.GetPossibleDestroyMoves(tile.Position, CurrentScene)), CurrentScene, comp, tile.Position);
                                if (moves.Count() > 0)
                                {
                                    CurrentScene.HighLightMoves(new List<Position2D>() { tile.Position });
                                }
                            }
                        }
                    }
                }
            }
            base.Activate(parameters);
        }
    }
}
