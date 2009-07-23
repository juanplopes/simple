using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services.Default
{
    public class ServiceLocationFactory : AggregateFactory<ServiceLocationFactory>
    {
        Dictionary<Type, object> _classes = new Dictionary<Type, object>();

        public void Set(object server, Type contract)
        {
            lock (_classes)
            {
                _classes[contract] = server;
            }
        }

        public object Get(Type contract)
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
