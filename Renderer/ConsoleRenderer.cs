using System.Drawing;
using Core;
using Core.Rendering;

namespace Renderer
{
    /// <summary>
    /// Basic Renderer to Console
    /// </summary>
    public class ConsoleRenderer : IRenderer
    {
        static Position2D borders = new(1, 1);
        public void RenderScene(Scene scene)
        {
            //Prints numbers on the top and bottom side of the screen
            borders = new Position2D((scene.Width/10)+1, 1);
            Console.SetCursorPosition(1, 0);
            for (int i = 0; i < scene.Width; i++)
            {
                Console.SetCursorPosition((i + 1) * 3 - 1, 0);
                Console.Write(IRenderer.ConvertIntToString(i + 1));
            }
            Console.SetCursorPosition(1, scene.Height + 1);
            for (int i = 0; i < scene.Width; i++)
            {
                Console.SetCursorPosition((i + 1) * 3 - 1, scene.Height + 1);
                Console.Write(IRenderer.ConvertIntToString(i + 1));
            }
            //Prints numbers on the right and left side of the screen
            for (int i = 0; i < scene.Height; i++)
            {
                Console.SetCursorPosition(0, scene.Height - (i));
                Console.Write(i + 1);
            }
            for (int i = 0; i < scene.Height; i++)
            {
                Console.SetCursorPosition(3 * scene.Width + borders.x, scene.Height - (i));
                Console.Write(i + 1);
            }
            foreach (var item in scene)
            {
                RenderObject(item.TileObject?.components.Find(x => x is IRenderable) as IRenderable, item);
            }
            Console.SetCursorPosition(0, 0);
        }
        public void RenderObject(IRenderable @object, IRenderable BackgroundObject)
        {
            if (BackgroundObject is Tile tile)
            {
                if (tile.isHighLighted)
                    Console.BackgroundColor = FromColor(tile.HighlightColor);
                else
                {
                    Console.BackgroundColor = FromColor(BackgroundObject.Visuals.Color);
                }
            }
            if (@object == null)
            {
                Position2D position = new(BackgroundObject.Position.x * 3, BackgroundObject.Position.y);
                SetCursorPosition(position);
                Console.Write(' ');
                position = new(position.x + 1, position.y);
                SetCursorPosition(position);
                Console.Write(' ');
                position = new(position.x + 1, position.y);
                SetCursorPosition(position);
                Console.Write(' ');
            }
            else
            {
                Console.ForegroundColor = FromColor(@object.Visuals.Color);
                Position2D position = new(@object.Position.x * 3, @object.Position.y);

                SetCursorPosition(position);
                Console.Write(" ");

                position = new(position.x + 1, position.y);
                SetCursorPosition(position);
                Console.Write(@object.Visuals.Visual);

                position = new(position.x + 1, position.y);
                SetCursorPosition(position);
                Console.Write(" ");
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
            Console.SetCursorPosition(pos.x + borders.x, pos.y + borders.y);
        }

        public void ShowMessage(MessageLine message)
        {
            Console.SetCursorPosition(Core.Engine.CurrentScene.Width*3+(borders.x*2)+5, Console.GetCursorPosition().Top + 1);
            var col = Console.ForegroundColor;
            Console.ForegroundColor = FromColor(message.Color);
            Console.Write(message);
            Console.ForegroundColor = col;
            Console.SetCursorPosition(Core.Engine.CurrentScene.Width * 3 + (borders.x * 2) + 5, Console.GetCursorPosition().Top + 1);
        }
    }
}
