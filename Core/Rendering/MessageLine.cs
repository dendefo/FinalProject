using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rendering
{
    readonly public struct MessageLine
    {
        readonly string message;
        public string Message { get => message; }
        readonly Color color;
        public Color Color { get => color; }
        public MessageLine(string message, Color color)
        {
            this.message = message;
            this.color = color;
        }
        public override string ToString()
        {
            return message;
        }

    }
}
