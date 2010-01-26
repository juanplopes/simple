using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Simple.IO.Serialization
{
    public class BinarySimpleSerializer : ISimpleSerializer, ISimpleStringSerializer
    {
        [Serializable]
        public class NullValueType { }

        public ISurrogateSelector CustomSelector { get; protected set; }

        public BinarySimpleSerializer() { }
        public BinarySimpleSerializer(ISurrogateSelector customSelector) { CustomSelector = customSelector; }

        protected BinaryFormatter GetFormatter()
        {
            var formatter = new BinaryFormatter();
            if (CustomSelector != null)
            {
                formatter.SurrogateSelector = new SurrogateSelector();
                formatter.SurrogateSelector.ChainSelector(CustomSelector);
            }
            return formatter;
        }

        public byte[] Serialize(object graph)
        {
            if (graph == null) graph = new NullValueType();
            
            return StreamHelper.Serialize(graph,
                (s, g) => GetFormatter().Serialize(s, g));
        }

        public object Deserialize(byte[] data)
        {
            object ret = StreamHelper.Deserialize(data,
                s => GetFormatter().Deserialize(s));
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
