using Core.Components;
using Core.Rendering;
using Core.Tile_Components;
using Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Tile : IRenderable,IDisposable
    {
        public static Action<Tile, TileObject> ObjectEntered;
        public static Action<Tile, TileObject> ObjectPassedOver;
        public List<TileComponent> Components { get; set; } = new List<TileComponent>();
        public Position2D Position { get; set; }
        public bool isHighLighted;
        public Color HighlightColor { get; set; }
        private TileObject tileObject;
        public TileObject TileObject
        {
            get
            {
                return tileObject;
            }
            set
            {
                tileObject = value;
            }
        }
        public VisualRepresentation Visuals { get; set; }
        public Tile(Position2D position, VisualRepresentation visuals)
        {
            Position = position;
            Visuals = visuals;
        }
        public T AddComponent<T>(TileComponent origin = null) where T : TileComponent,new()
        {
            T component;
            if (origin == null)
            {
                component = origin.Clone() as T;
                Components.Add(component);
                component.Tile = this;
                return component;
            }
            component = new T();
            component.Tile = this;
            Components.Add(component);
            return component;
        }
        public T GetTileComponent<T>(Type type) where T : TileComponent
        {
            foreach (var component in Components)
            {
                var t1 = component.GetType();
                if (type.IsInstanceOfType(component) || t1 == type)
                {
                    return component as T;
                }
            }
            return null;
        }
        public bool TryGetTileComponent<T>(Type type, out T component) where T : TileComponent
        {
            foreach (var c in Components)
            {
                if (type.IsInstanceOfType(c) || c.GetType() == type)
                {
                    component = c as T;
                    return true;
                }
            }
            component = null;
            return false;
        }
        public bool RemoveComponent<T>(Type type) where T : TileComponent
        {
            foreach (var component in Components)
            {
                var t1 = component.GetType();
                if (t1 == type)
                {
                    Components.Remove(component);
                    component.Tile = null;
                    component.Dispose();
                    return true;
                }
            }
            return false;
        }

        internal void Dispose()
        {
            Components.ForEach(c => c.Dispose());
            Components.Clear();

        }
        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}
