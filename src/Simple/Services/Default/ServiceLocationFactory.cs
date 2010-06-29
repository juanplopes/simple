using System;
using System.Collections.Generic;
using Simple.Config;

namespace Simple.Services.Default
{
    public interface IServiceLocationFactory
    {
        void Set(object server, Type contract);
        object Get(Type contract);
        object TryGet(Type contract);
        void Clear();
    }

    public class ServiceLocationFactory : AggregateFactory<ServiceLocationFactory>, IServiceLocationFactory
    {
        Dictionary<Type, object> _classes = new Dictionary<Type, object>();

        public void Set(object server, Type contract)
        {
            lock (_classes)
            {
                Simply.Do.Log(this).DebugFormat("Setting server object for contract {0}...", contract.Name);
                _classes[contract] = server;
            }
        }

        public object TryGet(Type contract)
        {
            lock (_classes)
            {
                object obj = null;
                Simply.Do.Log(this).DebugFormat("Trying to retrieving server object for contract {0}...", contract.Name);
                _classes.TryGetValue(contract, out obj);
                return obj;
            }
        }

        public object Get(Type contract)
        {
            try
            {
                lock (_classes)
                {
                    Simply.Do.Log(this).DebugFormat("Retrieving server object for contract {0}...", contract.Name);
                    return _classes[contract];
                }
            }
            catch (KeyNotFoundException e)
            {
                lock (_classes)
                {
                    throw new ServiceConnectionException("Service not found: {0}".AsFormat(contract.GetRealClassName()), e);
                }
            }
        }

        public void Clear()
        {
            lock (_classes)
            {
                Simply.Do.Log(this).DebugFormat("Clearing server objects...");
                _classes.Clear();
            }
        }
    }
}
