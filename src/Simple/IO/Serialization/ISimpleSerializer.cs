using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.IO.Serialization
{
    public interface ISimpleSerializer
    {
        byte[] Serialize(object graph);
        object Deserialize(byte[] data);
    }

    public interface ISimpleStringSerializer
    {
        string SerializeToString(object graph);
        object DeserializeFromString(string data);
    }
}
