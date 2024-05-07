using Core.Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tile_Components
{
    public class TileComponent : ICloneable, IDisposable
    {
        public Tile Tile = null;
        public Position2D Position => Tile == null ? default : Tile.Position;

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

        public void Dispose()
        {
            Tile = null;
        }
    }
}
