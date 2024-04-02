﻿using System.Drawing;
using Core;
using Core.Rendering;

namespace Renderer
{
    /// <summary>
    /// Basic Renderer to Console
    /// </summary>
    public class ConsoleRenderer : IRenderer<char>
    {
        static Position2D borders = new(1, 1);
        public void RenderScene(Scene<char> scene)
        {
            Console.SetCursorPosition(1, 0);
            for (int i = 0; i < scene.Width; i++)
            {
                Console.Write(i);
            }

            for (int i = 0; i < scene.Height; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write(ConvertIntToString(i + 1));
            }
            foreach (var item in scene)
            {
                RenderObject(item.TileObject?.components.Find(x => x is IRenderable<char>) as IRenderable<char>, item);
            }
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);
        }
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
            Console.SetCursorPosition(pos.x + borders.x, pos.y + borders.y);
        }

        public void ShowMessage(MessageLine message)
        {
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);
            var col = Console.ForegroundColor;
            Console.ForegroundColor = FromColor(message.Color);
            Console.WriteLine(message);
            Console.ForegroundColor = col;
        }
        static string ConvertIntToString(int value)
        {
            string result = string.Empty;
            while (--value >= 0)
            {
                result = (char)('A' + value % 26) + result;
                value /= 26;
            }
            return result;
        }
    }
}
