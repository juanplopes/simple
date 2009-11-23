using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Simple.IO.Serialization
{
    public class BinarySimpleSerializer : ISimpleSerializer, ISimpleStringSerializer
    {
        [Serializable]
        public class NullValueType { }

        public byte[] Serialize(object graph)
        {
            if (graph == null) graph = new NullValueType();
            
            return StreamHelper.Serialize(graph,
                (s, g) => new BinaryFormatter().Serialize(s, g));
        }

        public object Deserialize(byte[] data)
        {
            object ret = StreamHelper.Deserialize(data,
                s => new BinaryFormatter().Deserialize(s));
            if (ret is NullValueType) ret = null;
            return ret;
        }

        #region ISimpleStringSerializer Members

        public string SerializeToString(object graph)
        {
            return Convert.ToBase64String(Serialize(graph));
        }

        public object DeserializeFromString(string data)
        {
            return Deserialize(Convert.FromBase64String(data));
        }

        #endregion
    }
}
