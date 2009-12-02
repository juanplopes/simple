using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.Serialization
{
    [TestFixture]
    public class BinarySerializerFixture : BaseSerializerFixture
    {
        protected override ISimpleSerializer GetSerializer<T>()
        {
            return SimpleSerializer.Binary();
        }

        protected override ISimpleStringSerializer GetStringSerializer<T>()
        {
            return SimpleSerializer.Binary();
        }
    }
}
