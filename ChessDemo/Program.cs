using FinalProject.ChessDemo.Pieces;
using FinalProject.Engine;

Core engine = new Core(8, 8);
engine.map[0, 0] = new Rook(0);
engine.map[0, 7] = new Rook(0);
engine.map[7, 0] = new Rook(1);
engine.map[7, 7] = new Rook(1);
for (int i = 0; i < 8; i++)
{
    engine.map[1, i] = new Pawn(0);
}
for (int i = 0; i < 8; i++)
{
    engine.map[6, i] = new Pawn(1);
}
engine.map.SetColorMatrix(ConsoleColor.White,ConsoleColor.Gray);
engine.EndTurn();