using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Simple.Persistence
{
    [Serializable]
    public class SelfSerializableClass<T>
    {
        public string ToBase64String()
        {
            BinaryFormatter lobjSerializer = new BinaryFormatter();
            MemoryStream lobjStream = new MemoryStream();
            lobjSerializer.Serialize(lobjStream, this);

            return Convert.ToBase64String(lobjStream.GetBuffer());
        }

        public static T FromBase64String(string pstrBase64)
        {
            BinaryFormatter lobjDeserializer = new BinaryFormatter();
            MemoryStream lobjStream = new MemoryStream(Convert.FromBase64String(pstrBase64));
            return (T)lobjDeserializer.Deserialize(lobjStream);
        }
    }
}
