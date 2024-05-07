using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rendering
{
    
    /// <summary>
    /// Interface for Creating your own Renderer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRenderer<T> : IRenderer
    {
        public void RenderObject(IRenderable map, IRenderable BackGround);
        public void RenderScene(Scene scene);
        public void ShowMessage(MessageLine message);
    }
}
