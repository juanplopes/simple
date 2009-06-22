using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;

namespace Simple.Remoting
{
    public class ClientFactory
    {

        public ClientFactory(object config)
        {
        }

        public MarshalByRefObject Create(Type type, string endpointKey)
        {
            return (MarshalByRefObject)RemotingServices.Connect(
                type, "tcp://localhost:8080/" + endpointKey);
        }

        public T Create<T>(string endpointKey)
            where T:class
        {
            return Create(typeof(T), endpointKey) as T;
        }
    }
}
