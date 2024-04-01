using System.Drawing;
using Core.Rendering;

namespace Renderer
{
    /// <summary>
    /// Basic Renderer to Console
    /// </summary>
    public class ConsoleRenderer : IRenderer<char>
    {
        public void RenderObject(IRenderable<char> @object, IRenderable<char> BackgroundObject)
        {
            Console.BackgroundColor = FromColor(BackgroundObject.Visuals.Color);
            if (@object == null)
            {
                SetCursorPosition(BackgroundObject.Position);
                Console.Write(' ');
            }
            else
            {
                SetCursorPosition(@object.Position);
                Console.ForegroundColor = FromColor(@object.Visuals.Color);
                Console.Write(@object.Visuals.Visual);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

        }

        public static ConsoleColor FromColor(Color c)
        {
            int index = c.R > 128 | c.G > 128 | c.B > 128 ? 8 : 0; // Bright bit
            index |= c.R > 64 ? 4 : 0; // Red bit
            index |= c.G > 64 ? 2 : 0; // Green bit
            index |= c.B > 64 ? 1 : 0; // Blue bit
            return (ConsoleColor)index;
        }
        static public void SetCursorPosition(Position2D pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
        }

    }
}
