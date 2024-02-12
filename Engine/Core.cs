using FinalProject.Renderer;

namespace FinalProject.Engine
{
    internal class Core
    {
        private IRenderer renderer;
        public TileMap map;

        public Core(int width, int height)
        {
            map = new TileMap(width, height);
            renderer = new ConsoleRenderer();
        }

        public void EndTurn()
        {
            renderer.Render(map);
        }
    }
}
