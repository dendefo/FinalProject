using System.Drawing;

namespace Renderer.Renderer
{
    public class ConsoleRenderer : IRenderer<char>
    {
        public void RenderObject(IRenderable<char> @object)
        {
            Console.SetCursorPosition((int)@object.Position.X, (int)@object.Position.Y);
            Console.ForegroundColor = FromColor(@object.Color);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(@object.Visual);

        }
        public static ConsoleColor FromColor(Color c)
        {
            int index = c.R > 128 | c.G > 128 | c.B > 128 ? 8 : 0; // Bright bit
            index |= c.R > 64 ? 4 : 0; // Red bit
            index |= c.G > 64 ? 2 : 0; // Green bit
            index |= c.B > 64 ? 1 : 0; // Blue bit
            return (ConsoleColor)index;
        }
    }
}
