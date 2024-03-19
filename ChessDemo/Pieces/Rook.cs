using Renderer;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    [Serializable]
    internal class Rook : ChessObject
    {
        bool hasMoved = false;
        public Rook() : base() { }
    }
}
