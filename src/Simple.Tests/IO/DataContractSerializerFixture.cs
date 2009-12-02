using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO.Serialization;
using NUnit.Framework;

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
