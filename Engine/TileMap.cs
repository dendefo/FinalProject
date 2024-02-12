using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Engine
{
    internal class TileMap
    {
        ConsoleColor[,] colors;
        public ITileObject this[int x, int y]
        {
            get => map[x, y];
            set => map[x, y] = value;
        }
        private ITileObject[,] map;

        public int Height => map.GetLength(0);
        public int Width => map.GetLength(1);
        public TileMap(int width, int height)
        {
            map = new ITileObject[width, height];
            colors = new ConsoleColor[width, height];
        }
        public void SetColor(int x, int y, ConsoleColor color)
        {
            colors[x, y] = color;
        }
        public ConsoleColor GetColor(int x, int y)
        {
            return colors[x, y];
        }
        public void SetColorMatrix(ConsoleColor color, ConsoleColor secondColor)
        {
            for (int i = 0; i < colors.GetLength(0); i++)
            {
                for (int j = 0; j < colors.GetLength(1); j++)
                {
                    if ((i+j) % 2 == 0) colors[i, j] = color;
                    else colors[i, j] = secondColor;
                }
            }
        }

    }
}
