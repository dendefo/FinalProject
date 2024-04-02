using System.Drawing;

namespace Core.Rendering
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
        public VisualRepresentation(VisualRepresentation<T> origin, Color newColor)
        {
            Visual = origin.Visual;
            Color = newColor;
        }
        public void SetColor(Color color)
        {
            this.Color = color;
        }
    }
}
