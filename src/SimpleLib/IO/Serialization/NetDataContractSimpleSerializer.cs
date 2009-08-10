using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace Simple.IO.Serialization
{
    public class NetDataContractSimpleSerializer : ISimpleSerializer
    {

        public byte[] Serialize(object graph)
        {
            return StreamHelper.Serialize(graph,
                (s, g) => new NetDataContractSerializer().Serialize(s, g));
        }

        public object Deserialize(byte[] data)
        {
            return StreamHelper.Deserialize(data,
                s => new NetDataContractSerializer().Deserialize(s));
        }

    }
}
