using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    public class CharacterRenderer :RenderingComponent<char>
    {
        
        public CharacterRenderer(TileObject tileObject = null) : base(tileObject)
        {
        }
    }
}
