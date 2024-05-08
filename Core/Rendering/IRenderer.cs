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
        public void RenderScene(Scene scene);
        public void ShowMessage(MessageLine message); 
        static string ConvertIntToString(int value)
        {
            string result = string.Empty;
            while (--value >= 0)
            {
                result = (char)('A' + value % 26) + result;
                value /= 26;
            }
            return result;
        }
    }
}
