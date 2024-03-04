using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
    public interface IRenderer<T>
    {
        public void RenderObject(IRenderable<T> map, IRenderable<T> BackGround);
        public void RenderBackGroundObject(IRenderable<T> @object);
    }
}
