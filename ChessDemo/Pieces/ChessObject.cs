using QuarterEngine.Core;
using Renderer;
using System.Drawing;

namespace QuarterEngine.ChessDemo.Pieces
{
    internal abstract class ChessObject<T> : TileObject<T>
    {
        public ChessObject(int PlayerId,VisualRepresentation<T> visuals) : base(visuals)
        {
        }
    }
}
