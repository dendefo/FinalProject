using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    /// <summary>
    /// Add this Attribute to a TileComponent to ensure that only one instance of the component can appear on a TileObject.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed public class AppearOnlyOnceAttribute : Attribute
    {
    }
}
