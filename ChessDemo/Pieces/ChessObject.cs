using Core;
using Renderer;
using System.Drawing;
using Core.Components;
using System.Text.Json.Serialization;

namespace ChessDemo.Pieces
{
    [Serializable]
    internal abstract class ChessObject : CustomComponent
    {
        [JsonConstructor]
        public ChessObject():base() { }
    }
}
