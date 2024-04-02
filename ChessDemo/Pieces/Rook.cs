using Newtonsoft.Json;
using Renderer;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    internal class Rook : ChessComponent
    {
        public override string ToString()
        {
            return $"Rook at Position {TileObject.Position}";
        }
    }
}
