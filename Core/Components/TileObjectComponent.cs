using Core;
using Renderer;
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
    public class TileObjectComponent : ICloneable, IDisposable
    {
        // Reference to the TileObject that this component is attached to
        public TileObject TileObject { get; set; }
        public Position2D Position => TileObject.Position;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc/></typeparam>
        /// <returns><inheritdoc/></returns>
        public T AddComponent<T>() where T : TileObjectComponent, new() => TileObject.AddComponent<T>();

        public object Clone()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase;
            var ret = GetType().GetConstructor(flags, types: new Type[0]).Invoke(new object[0]);


            foreach (var property in ret.GetType().GetProperties(flags))
            {
                var properties = ret.GetType().GetProperty(property.Name, flags);
                if (property.SetMethod == null) continue;
                properties.SetValue(ret, property.GetValue(this));
            }
            foreach (var field in ret.GetType().GetFields(flags))
            {
                var fields = ret.GetType().GetField(field.Name, flags);
                fields.SetValue(ret, field.GetValue(this));
            }
            return ret;
        }

        public virtual void Dispose()
        {
            TileObject = null;
        }

        /// <summary>
        /// <see cref="TileObject.GetComponent{T}(Type)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"><inheritdoc/></param>
        /// <returns></returns>
        public T GetComponent<T>(Type type) where T : TileObjectComponent => TileObject.GetComponent<T>(type);
    }
}
