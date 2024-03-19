using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Components
{
    [Serializable]
    public class CharacterRenderer :RenderingComponent<char>
    {

        [JsonConstructor]
        public CharacterRenderer(TileObject tileObject = null) : base(tileObject)
        {
        }
    }
}
