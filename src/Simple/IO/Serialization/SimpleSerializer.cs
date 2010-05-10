using System;
using System.Runtime.Serialization;

namespace Simple.IO.Serialization
{
    public static class SimpleSerializer
    {
        public static BinarySimpleSerializer Binary()
        {
            return new BinarySimpleSerializer();
        }

        public static BinarySimpleSerializer Binary(ISurrogateSelector selector)
        {
            return new BinarySimpleSerializer(selector);
        }

        public static XmlSimpleSerializer Xml(Type type)
        {
            return new XmlSimpleSerializer(type);
        }

        public static XmlSimpleSerializer Xml<T>()
        {
            return Xml(typeof(T));
        }

        public static DataContractSimpleSerializer DataContract<T>()
        {
            return new DataContractSimpleSerializer(typeof(T));
        }

        public static NetDataContractSimpleSerializer NetDataContract()
        {
            return new NetDataContractSimpleSerializer();
        }

        public static T RoundTrip<T>(this ISimpleSerializer serializer, T graph)
        {
            return (T)serializer.Deserialize(serializer.Serialize(graph));
        }
    }
}
