using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class AttributeCacheFixture
    {
        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class Attribute1Attribute : Attribute { }
        public class Attribute2Attribute : Attribute { }
        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class Attribute3Attribute : Attribute { }

        [Attribute1]
        [Attribute1]
        [Attribute2]
        public class AttributeTest
        {
            [Attribute2]
            [Attribute3]
            [Attribute3]
            public int TestProp { get; set; }
        }

        [Test]
        public void ClassAttributesFirstTest()
        {
            var attr1 = AttributeCache.Do.First<Attribute1Attribute>(typeof(AttributeTest));
            Assert.IsNotNull(attr1);

            var attr2 = AttributeCache.Do.First<Attribute2Attribute>(typeof(AttributeTest));
            Assert.IsNotNull(attr2);

            var attr3 = AttributeCache.Do.First<Attribute3Attribute>(typeof(AttributeTest));
            attr3.Should().Be.Null();
        }

        [Test]
        public void ClassAttributeEnumTest()
        {
            var attr1 = new List<Attribute1Attribute>(
                AttributeCache.Do.Enumerate<Attribute1Attribute>(typeof(AttributeTest)));
            attr1.Count.Should().Be(2);
        }

        [Test]
        public void PropAttributesFirstTest()
        {
            var attr1 = AttributeCache.Do.First<Attribute1Attribute>(typeof(AttributeTest).GetProperty("TestProp"));
            attr1.Should().Be.Null();

            var attr2 = AttributeCache.Do.First<Attribute2Attribute>(typeof(AttributeTest).GetProperty("TestProp"));
            Assert.IsNotNull(attr2);

            var attr3 = AttributeCache.Do.First<Attribute3Attribute>(typeof(AttributeTest).GetProperty("TestProp"));
            Assert.IsNotNull(attr3);
        }

        [Test]
        public void PropAttributeEnumTest()
        {
            var attr3 = new List<Attribute3Attribute>(
                AttributeCache.Do.Enumerate<Attribute3Attribute>(typeof(AttributeTest).GetProperty("TestProp")));
            attr3.Count.Should().Be(2);
        }
    }
}
