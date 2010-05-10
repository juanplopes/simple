using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.Serialization
{
    [TestFixture]
    public class DataContractSerializerFixture : BaseSerializerFixture
    {
        protected override ISimpleSerializer GetSerializer<T>()
        {
            return SimpleSerializer.DataContract<T>();
        }

        protected override ISimpleStringSerializer GetStringSerializer<T>()
        {
            return SimpleSerializer.DataContract<T>();
        }
    }
}
