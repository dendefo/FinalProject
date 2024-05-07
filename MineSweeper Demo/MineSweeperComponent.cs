using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper_Demo
{
    using static Core.Engine;
    internal class MineSweeperComponent : TileObjectComponent
    {
        public static event Action OnMineHit;
        public static event Action OnWin;
        public static Dictionary<int, Color> MineAmount = new()
        {
            {0,Color.White},
            {1,Color.Blue},
            {2,Color.Green},
            {3,Color.Red},
            {4,Color.DarkBlue},
            {5,Color.DarkRed},
            {6,Color.DarkGreen},
            {7,Color.DarkOrange},
            {8,Color.DarkViolet}
        };
        public bool IsMine { get; set; }
        public CharacterRenderer Renderer { get; set; }
        private int mines = 0;
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }

        public void Reveal()
        {
            if (IsFlagged) return;
            if (IsRevealed) return;
            IsRevealed = true;

            if (IsMine)
            {
                Renderer.Visuals = new('*', Color.Red);
                OnMineHit?.Invoke();
            }
            else
            {
                Open();
                if (mines == 0)
                {
                    foreach (var tile in GetAdjacentPosition())
                    {
                        if (CurrentScene[tile.x, tile.y].TileObject.TryGetComponent<MineSweeperComponent>(typeof(MineSweeperComponent), out var comp))
                        {
                            comp.Reveal();
                        }
                    }
                }
            }
        }
        public void Open()
        {
            mines = 0;
            foreach (var tile in GetAdjacentPosition())
            {
                if (CurrentScene[tile.x, tile.y].TileObject.TryGetComponent<MineSweeperComponent>(typeof(MineSweeperComponent), out var comp))
                {
                    if (comp.IsMine) mines++;
                }
            }
            Renderer.Visuals = new(mines, MineAmount[mines]);
        }
        public List<Position2D> GetAdjacentPosition()
        {
            List<Position2D> positions = new();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (CurrentScene.IsInside(Position.x + x, Position.y + y))
                    {
                        positions.Add(new(Position.x + x, Position.y + y));
                    }
                }
            }
            return positions;

        }
        public void Flag()
        {
            if (IsRevealed) return;
            if (IsFlagged)
            {
                IsFlagged = false;
                Renderer.Visuals = new(' ', Color.Yellow);
            }
            else
            {
                IsFlagged = true;
                Renderer.Visuals = new('F', Color.Yellow);
                int countMines = 0;
                int countFlaggedMines = 0;
                foreach (var minetile in CurrentScene)
                {
                    if (minetile.TileObject.TryGetComponent<MineSweeperComponent>(typeof(MineSweeperComponent), out var comp))
                    {
                        if (comp.IsMine)
                        {
                            countMines++;

                            if (comp.IsFlagged)
                            {
                                countFlaggedMines++;
                            }
                        }
                    }
                }
                if (countMines == countFlaggedMines) OnWin?.Invoke();
            }

        }
    }

}
