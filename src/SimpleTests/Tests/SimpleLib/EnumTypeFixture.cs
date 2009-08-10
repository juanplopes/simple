using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Common;
using NUnit.Framework;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class EnumTypeFixture
    {
        public enum SampleEnum1
        {
            [EnumType("asd"), EnumType(123)] Value1,
            [EnumType("asd")] Value2,
            [EnumType(123)] Value3,
            Value4
        }

        [Test]
        public void TestValue1()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value1);
            Assert.AreEqual(2, list.Count);

            foreach (var obj in list)
            {
                Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value1, obj));
            }

            Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value1, "asd"));
            Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value1, 123));
            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value1, 234));
        }

        [Test]
        public void TestValue2()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value2);
            Assert.AreEqual(1, list.Count);

            foreach (var obj in list)
            {
                Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value2, obj));
            }

            Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value2, "asd"));
            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value2, 123));
            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value2, 234));
        }

        [Test]
        public void TestValue3()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value3);
            Assert.AreEqual(1, list.Count);

            foreach (var obj in list)
            {
                Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value3, obj));
            }

            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value3, "asd"));
            Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value3, 123));
            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value3, 234));
        }

        [Test]
        public void TestValue4()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value4);
            Assert.AreEqual(0, list.Count);

            foreach (var obj in list)
            {
                Assert.IsTrue(EnumTypeAttribute.IsDefined(SampleEnum1.Value4, obj));
            }

            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value4, "asd"));
            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value4, 123));
            Assert.IsFalse(EnumTypeAttribute.IsDefined(SampleEnum1.Value4, 234));
        }

    }
}
