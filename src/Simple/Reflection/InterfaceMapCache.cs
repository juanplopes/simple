using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.Reflection;

namespace Simple.Reflection
{
    public class InterfaceMapCache : Singleton<InterfaceMapCache>
    {
        struct Map
        {
            public Type From { get; set; }
            public Type To { get; set; }
        }

        Dictionary<Map, InterfaceMapping> _maps = new Dictionary<Map, InterfaceMapping>();

        public InterfaceMapping GetMap(Type from, Type to)
        {
            lock (_maps)
            {
                InterfaceMapping res;
                var key = new Map { From = from, To = to };
                if (!_maps.TryGetValue(key, out res))
                    _maps[key] = res = from.GetInterfaceMap(to);

                return res;
            }
        }

    }
}
