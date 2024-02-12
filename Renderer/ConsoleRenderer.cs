using FinalProject.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Renderer
{
    internal class ConsoleRenderer : IRenderer
    {
        public void Render(TileMap map)
        {
            var color = new ColorConverter();
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {

                    Console.BackgroundColor = map.GetColor(i, j);
                    if (map[i, j] != null)
                    {
                        Console.ForegroundColor = map[i, j].ObjectColor;
                        Console.Write(map[i, j].Symbol);
                    }
                    else Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
