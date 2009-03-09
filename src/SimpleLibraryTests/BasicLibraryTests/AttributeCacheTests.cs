using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BasicLibrary.Reflection;
using SimpleLibrary.BasicLibraryTests.TestClasses;

namespace SimpleLibrary.BasicLibraryTests
{
    [TestFixture]
    public class AttributeCacheTests
    {
        [Test]
        public void ClassAttributesFirstTest()
        {
            var attr1 = AttributeCache.Instance.First<Attribute1Attribute>(typeof(AttributeTest));
            Assert.IsNotNull(attr1);

            var attr2 = AttributeCache.Instance.First<Attribute2Attribute>(typeof(AttributeTest));
            Assert.IsNotNull(attr2);

            var attr3 = AttributeCache.Instance.First<Attribute3Attribute>(typeof(AttributeTest));
            Assert.IsNull(attr3);
        }

        [Test]
        public void ClassAttributeEnumTest()
        {
            var attr1 = new List<Attribute1Attribute>(
                AttributeCache.Instance.Enumerate<Attribute1Attribute>(typeof(AttributeTest)));
            Assert.AreEqual(2, attr1.Count);
        }

        [Test]
        public void PropAttributesFirstTest()
        {
            var attr1 = AttributeCache.Instance.First<Attribute1Attribute>(typeof(AttributeTest).GetProperty("TestProp"));
            Assert.IsNull(attr1);

            var attr2 = AttributeCache.Instance.First<Attribute2Attribute>(typeof(AttributeTest).GetProperty("TestProp"));
            Assert.IsNotNull(attr2);

            var attr3 = AttributeCache.Instance.First<Attribute3Attribute>(typeof(AttributeTest).GetProperty("TestProp"));
            Assert.IsNotNull(attr3);
        }

        [Test]
        public void PropAttributeEnumTest()
        {
            var attr3 = new List<Attribute3Attribute>(
                AttributeCache.Instance.Enumerate<Attribute3Attribute>(typeof(AttributeTest).GetProperty("TestProp")));
            Assert.AreEqual(2, attr3.Count);
        }
    }
}
