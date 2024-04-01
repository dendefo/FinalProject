using Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Asset Manager is a class that is used to load and save assets.
    /// It works as Prefabs in Unity.
    /// </summary>
    public static class AssetManager
    {
        /// <summary>
        /// Load an asset from a file.
        /// </summary>
        /// <typeparam name="T"> Type of TileComponent you want to get in return after Loading the asset</typeparam>
        /// <param name="path"> Path to the Asset is "Assets" Folder</param>
        /// <returns></returns>
        public static T LoadAsset<T>(string path) where T : TileComponent
        {
            path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Assets/" + path + ".json";
            TextReader reader = new StringReader(File.ReadAllText(path));
            Newtonsoft.Json.JsonReader jsonReader = new Newtonsoft.Json.JsonTextReader(reader);
            Newtonsoft.Json.JsonSerializerSettings options = new();
            options.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            options.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.File);
            var ser = Newtonsoft.Json.JsonSerializer.Create(options);
            var obj = ser.Deserialize<TileObject>(jsonReader);
            foreach (var component in obj.components)
            {
                if (component.TileObject != null) component.TileObject.Dispose();
                component.TileObject = obj;
            }
            return obj.GetComponent<T>(typeof(T));
        }

        /// <summary>
        /// Save an asset to a file.
        /// </summary>
        /// <param name="asset"> Asset as Component, that you want to save</param>
        /// <param name="path"> Path to save an asset there (Must include file name)</param>
        public static void SaveAsset(TileComponent asset, string path)
        {
            TextWriter writer = new StringWriter();
            Newtonsoft.Json.JsonSerializerSettings options = new();
            options.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            options.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.Ignore;
            options.Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.File);
            var ser = Newtonsoft.Json.JsonSerializer.Create(options);
            path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Assets/" + path + ".json";
            ser.Serialize(writer, asset.TileObject);
            File.WriteAllText(path, writer.ToString());
        }
        /// <summary>
        /// Save an asset to a file.
        /// </summary>
        /// <param name="asset"> Asset is TileObject to save</param>
        /// <param name="path"> Path to save an asset there (Must include file name)</param>
        public static void SaveAsset(TileObject asset, string path)
        {
            TextWriter writer = new StringWriter();
            Newtonsoft.Json.JsonSerializerSettings options = new();
            options.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            options.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.Ignore;
            options.Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.File);
            var ser = Newtonsoft.Json.JsonSerializer.Create(options);
            path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Assets/" + path + ".json";
            ser.Serialize(writer, asset);
            File.WriteAllText(path, writer.ToString());
        }
    }
}
