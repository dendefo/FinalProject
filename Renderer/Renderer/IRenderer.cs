using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer.Renderer
{
    public interface IRenderer<T>
    {
        public void RenderObject(IRenderable<T> map);
    }
}
