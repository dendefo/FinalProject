using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Components
{
    /// <summary>
    /// Basic component for rendering characters
    /// </summary>
    public class CharacterRenderer : RenderingComponent<char>
    {
        public CharacterRenderer() : base()
        {
        }
    }
}
