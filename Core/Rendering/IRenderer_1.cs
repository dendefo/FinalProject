using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rendering
{
    public interface IRenderer
    {
        public abstract void RenderObject(IRenderable map, IRenderable BackGround);
        public abstract void RenderScene(Scene scene);
        public void ShowMessage(MessageLine message);
    }
}
