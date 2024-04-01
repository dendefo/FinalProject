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
    public interface IRenderer<T>
    {
        public void RenderObject(IRenderable<T> map, IRenderable<T> BackGround);
    }
}
