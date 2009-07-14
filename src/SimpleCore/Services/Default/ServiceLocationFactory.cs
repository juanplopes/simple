using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services.Default
{
    public class ServiceLocationFactory : AggregateFactory<ServiceLocationFactory>
    {
        Dictionary<Type, Type> _classes = new Dictionary<Type, Type>();

        public void Set(Type type, Type contract)
        {
            lock (_classes)
            {
                _classes[contract] = type;
            }
        }

        public Type Get(Type contract)
        {
            lock (_classes)
            {
                return _classes[contract];
            }
        }

        public void Clear()
        {
            lock (_classes)
            {
                _classes.Clear();
            }
        }
    }
}
