using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace Simple.IO.Serialization
{
    public class NetDataContractSimpleSerializer : ISimpleSerializer, ISimpleStringSerializer
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

        #region ISimpleStringSerializer Members

        public string SerializeToString(object graph)
        {
            return Encoding.UTF8.GetString(Serialize(graph));
        }

        public object DeserializeFromString(string data)
        {
            return Deserialize(Encoding.UTF8.GetBytes(data));
        }

        #endregion
    }
}
