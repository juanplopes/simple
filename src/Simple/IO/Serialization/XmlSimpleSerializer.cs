using System;
using System.Text;
using System.Xml.Serialization;

namespace Simple.IO.Serialization
{
    public class XmlSimpleSerializer : ISimpleSerializer, ISimpleStringSerializer
    {
        public Type Type { get; protected set; }
        public XmlSimpleSerializer(Type type)
        {
            Type = type;
        }

        public byte[] Serialize(object graph)
        {
            return StreamHelper.Serialize(graph,
                (s, g) => new XmlSerializer(Type).Serialize(s, g));
        }

        public object Deserialize(byte[] data)
        {
            return StreamHelper.Deserialize(data,
                s => new XmlSerializer(Type).Deserialize(s));
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
