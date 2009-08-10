using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Simple.IO.Serialization
{
    public class DataContractSimpleSerializer : ISimpleSerializer
    {
        public Type Type { get; set; }
        public DataContractSimpleSerializer(Type type)
        {
            Type = type;
        }


        public byte[] Serialize(object graph)
        {
            return StreamHelper.Serialize(graph,
                (s, g) => new DataContractSerializer(Type).WriteObject(s, g));
        }

        public object Deserialize(byte[] data)
        {
            return StreamHelper.Deserialize(data,
                s => new DataContractSerializer(Type).ReadObject(s));
        }

    }
}
