using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Core.Components
{
    [Serializable]
    [JsonDerivedType(typeof(CustomComponent))]
    [JsonDerivedType(typeof(RenderingComponent<char>))]

    public class TileComponent
    {
        [NonSerialized]
        public TileObject TileObject;

        public TileComponent()
        {
            TileObject = Engine.Instantiate<TileObject>(new(), default);
            TileObject.components.Add(this);
        }
        public TileComponent(TileObject tileObject = null)
        {
            if (tileObject == null)
            {
                TileObject = Engine.Instantiate<TileObject>(new(), default);
                TileObject.components.Add(this);
            }
            else TileObject = tileObject;
        }
        public T Copy<T>(T origin) where T : TileComponent
        {
            //    BinaryFormatter binaryFormatter = new();
            //    Stream stream = new MemoryStream();
            //    binaryFormatter.Serialize(stream, origin);
            //    return binaryFormatter.Deserialize(stream) as T;
            var lol = JsonSerializer.Serialize(origin, origin.GetType());
            JsonSerializerOptions options = new() { IncludeFields = true };
            var ret = JsonSerializer.Deserialize(lol, origin.GetType(), options) as T;
            origin.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase).ToList().ForEach(x => origin.GetType().GetProperty(x.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public).SetValue(ret, x.GetValue(origin)));
            foreach (var field in origin.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase))
            {
                var fields = origin.GetType().GetField(field.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public);
                fields.SetValue(ret, field.GetValue(origin));
            }
            return ret;
        }
        public T AddComponent<T>() where T : TileComponent => TileObject.AddComponent<T>();
        public T GetComponent<T>() where T : TileComponent => TileObject.GetComponent<T>();

        public T Clone<T>() where T : TileComponent
        {
            return MemberwiseClone() as T;
        }
    }
}
