using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Components
{
    /// <summary>
    /// To implement custom components, inherit from this class.
    /// Similar to Unity's Monobehaviour, this class will be used to add custom logic to the game.
    /// It is recommended to use the [JsonProperty] and [JsonIgnore] attributes from the Newtonsoft.Json namespace.
    /// [JsonProperty] is the Equivilant of [SerizalizeField] in Unity.
    /// [JsonIgnore] is the Equivilant of [NonSerialized] in Unity.
    /// 
    /// </summary>
    public class CustomComponent : TileObjectComponent
    {
        //Will Add here calls as Updata, Start, Awake, etc.
    }
}
