using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.SimpleLib.Serializers
{
    [TestFixture] 
    public class XmlSerializerFixture : BaseSerializerFixture
    {
        protected override ISimpleSerializer GetSerializer<T>()
        {
            return SimpleSerializer.Xml<T>();
        }

        protected override ISimpleStringSerializer GetStringSerializer<T>()
        {
            return SimpleSerializer.Xml<T>();
        }
    }
}
