using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.IO.Serialization
{
    public class StreamHelper
    {
        public static byte[] Serialize(object graph, Action<Stream, object> action)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                action(stream, graph);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return new BinaryReader(stream).ReadBytes((int)stream.Length); ;
            }
        }

        public static object Deserialize(byte[] data, Func<Stream, object> action)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                return action(stream);
            }
        }
    }
}
