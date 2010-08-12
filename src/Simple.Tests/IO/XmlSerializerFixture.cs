using NUnit.Framework;
using SharpTestsEx;
using Simple.IO.Serialization;

namespace Simple.Tests.Serialization
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
