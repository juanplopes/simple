using NUnit.Framework;
using SharpTestsEx;
using Simple.Reflection;

namespace Simple.Tests.Reflection
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
            list.Count.Should().Be(2);

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
            list.Count.Should().Be(1);

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
            list.Count.Should().Be(1);

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
            list.Count.Should().Be(0);

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
