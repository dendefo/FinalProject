using System.Drawing;

namespace Core.Rendering
{
    /// <summary>
    /// A struct that holds a visual representation of an object
    /// </summary>
    public struct VisualRepresentation
    {
        public object Visual { get; set; }
        public Color Color { get; set; }

        public VisualRepresentation(object visual, Color color)
        {
            Visual = visual;
            Color = color;
        }
        public VisualRepresentation(VisualRepresentation origin, Color newColor)
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
