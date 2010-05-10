using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.Serialization
{
    [TestFixture]
    public class NetDataContractSerializerFixture : BaseSerializerFixture
    {
        protected override ISimpleSerializer GetSerializer<T>()
        {
            return SimpleSerializer.NetDataContract();
        }

        protected override ISimpleStringSerializer GetStringSerializer<T>()
        {
            return SimpleSerializer.NetDataContract();
        }
    }
}
