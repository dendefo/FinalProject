using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Components
{
    [Serializable]
    public class CustomComponent : TileComponent
    {
        public CustomComponent():base() { }
    }
}
