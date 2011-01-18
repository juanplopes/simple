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
        Dictionary<Tuple<Type, Type>, InterfaceMapping> _maps = new Dictionary<Tuple<Type, Type>, InterfaceMapping>();

        public InterfaceMapping GetMap(Type from, Type to)
        {
            lock (_maps)
            {
                InterfaceMapping res;
                var key = new Tuple<Type, Type>(from, to);
                if (!_maps.TryGetValue(key, out res))
                    _maps[key] = res = from.GetInterfaceMap(to);

                return res;
            }
        }

    }
}
