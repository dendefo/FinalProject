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
    /// <summary>
    /// A class that represents a Component that can be attached to a TileObject
    /// Similar to Unity's Behavior class
    /// </summary>
    public class TileComponent
    {
        // Reference to the TileObject that this component is attached to
        public TileObject TileObject { get; set; }

        /// <summary>
        /// Copies a TileComponent
        /// Used in Commands as AddComponent and in AssetManager as LoadAsset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        static internal T Copy<T>(T origin) where T : TileComponent, new()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase;
            T ret = origin.GetType().GetConstructor(flags, types: new Type[0]).Invoke(new object[0]) as T;


            foreach (var property in ret.GetType().GetProperties(flags))
            {
                var properties = ret.GetType().GetProperty(property.Name, flags);
                if (property.SetMethod == null) continue;
                properties.SetValue(ret, property.GetValue(origin));
            }
            foreach (var field in ret.GetType().GetFields(flags))
            {
                var fields = ret.GetType().GetField(field.Name, flags);
                fields.SetValue(ret, field.GetValue(origin));
            }
            return ret;
        }
        public T AddComponent<T>() where T : TileComponent, new() => TileObject.AddComponent<T>();
        public T GetComponent<T>(Type type) where T : TileComponent => TileObject.GetComponent<T>(type);
    }
}
