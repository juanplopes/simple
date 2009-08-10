using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Simple.IO.Serialization
{
    public class XmlSimpleSerializer : ISimpleSerializer
    {
        public Type Type { get; set; }
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

    }
}
