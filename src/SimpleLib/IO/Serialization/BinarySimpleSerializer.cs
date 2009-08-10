using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Simple.IO.Serialization
{
    public class BinarySimpleSerializer : ISimpleSerializer
    {
        public byte[] Serialize(object graph)
        {
            return StreamHelper.Serialize(graph,
                (s, g) => new BinaryFormatter().Serialize(s, g));
        }

        public object Deserialize(byte[] data)
        {
            return StreamHelper.Deserialize(data,
                s => new BinaryFormatter().Deserialize(s));
        }
    }
}
