using System.Numerics;
using Renderer.Renderer;

namespace QuarterEngine.Core
{
    public class Core
    {

        private IRenderer<char> renderer;
        public TileMap map;

        public Core(int width, int height)
        {
            map = new TileMap(width, height);
            renderer = new ConsoleRenderer();
        }

        public void EndTurn()
        {
            foreach (var item in map.Objects)
            {
                renderer.RenderObject(item);
            }
        }
        private string GetExperienceLevel_CSharp7(uint yearsOfExperience) => yearsOfExperience switch
        {
            0 => "Inexperienced",
            > 0 and <= 2 => "Beginner",
            > 2 and <= 5 => "Intermediate",
            _ => "Expert",
        };
        private string GetExperienceLevel_CSharp8(uint yearsOfExperience)
        {
            switch (yearsOfExperience)
            {
                case <= 0: return "Inexperienced";
                case > 0 and <= 2: return "Beginner";
                case > 2 and <= 5: return "Intermediate";
                default: return "Expert";
            }
        }
        List<Vector2> FilterBySameSign(List<Vector2> list) => list.Where(vec => vec.X switch
            {
                < 0 when vec.Y < 0 => true,
                > 0 when vec.Y > 0 => true,
                _ => false
            }).ToList();
        public void Instantiate(TileObject<char> tileObject)
        {
            map.Objects.Add(tileObject);
        }
        public void Instantiate(TileObject<char> tileObject,Vector2 position)
        {
            map.Objects.Add(tileObject);
            tileObject.Position = position;
        }
    }
}
