using System.Drawing;

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
                Console.SetCursorPosition((int)BackgroundObject.Position.X, (int)BackgroundObject.Position.Y);
                Console.Write(BackgroundObject.Visuals.Visual);
            }
            else
            {
                Console.SetCursorPosition((int)@object.Position.X, (int)@object.Position.Y);
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


    }
    /// <summary>
    /// A struct that holds a visual representation of an object
    /// </summary>
    /// <typeparam name="T"> Renderable Object Of Same type as IRenderer type</typeparam>
    public struct VisualRepresentation<T>
    {
        public T Visual;
        public Color Color;
        public VisualRepresentation(T visual, Color color)
        {
            Visual = visual;
            Color = color;
        }
    }
}
