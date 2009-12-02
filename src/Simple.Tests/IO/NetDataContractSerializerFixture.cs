using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO.Serialization;
using NUnit.Framework;

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
