using System.Drawing;

namespace Renderer
{
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
