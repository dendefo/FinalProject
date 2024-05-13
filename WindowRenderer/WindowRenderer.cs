using Core;
using Core.Components;
using Core.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowRenderer
{
    public class WindowRenderer : IRenderer
    {
        public Form1 form;
        Graphics graphics;
        public float size = 75;
        int lastMessageHeight = 0;
        public void RenderScene(Scene scene)
        {
            lastMessageHeight = 0;
            graphics.Clear(Color.Black);
            foreach (var tile in scene)
            {
                RenderObject(tile.TileObject?.GetComponent<RenderingComponent>(typeof(RenderingComponent)), tile);
            }
        }
        public void RenderObject(IRenderable @object, IRenderable BackgroundObject)
        {
            if (BackgroundObject is Tile tile && tile.isHighLighted)
            {
                graphics.FillRectangle(new SolidBrush(tile.HighlightColor), BackgroundObject.Position.x * size, BackgroundObject.Position.y * size, size, size);
            }
            else
                graphics.FillRectangle(new SolidBrush(BackgroundObject.Visuals.Color), BackgroundObject.Position.x * size, BackgroundObject.Position.y * size, size, size);
            if (@object != null)
            {
                if (@object.Visuals.Visual is Image image)
                {
                    graphics.DrawImage(@object.Visuals.Visual as Image, BackgroundObject.Position.x * size+10, BackgroundObject.Position.y * size+10, size-20, size-20);
                }
                else
                    graphics.DrawString(@object.Visuals.Visual.ToString(), new Font("Arial", size * 0.75f), new SolidBrush(@object.Visuals.Color), BackgroundObject.Position.x * size, BackgroundObject.Position.y * size);
            }
        }
        public void ShowMessage(MessageLine message)
        {
            graphics.DrawString(message.Message, new Font("Arial", 22), new SolidBrush(message.Color), 0, Engine.CurrentScene.Height * size + lastMessageHeight);
            lastMessageHeight += 30;
        }


        public WindowRenderer()
        {
            Start();
        }
        private void Start()
        {

            ApplicationConfiguration.Initialize();
            form = new Form1();
            form.BackColor = Color.Black;
            form.Size = new Size(800, 800);
            graphics = form.CreateGraphics();
            form.Show();
        }
    }
}
