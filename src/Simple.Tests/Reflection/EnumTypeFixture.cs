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
                EnumTypeAttribute.IsDefined(SampleEnum1.Value1, obj).Should().Be.True();
            }

            EnumTypeAttribute.IsDefined(SampleEnum1.Value1, "asd").Should().Be.True();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value1, 123).Should().Be.True();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value1, 234).Should().Be.False();
        }

        [Test]
        public void TestValue2()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value2);
            list.Count.Should().Be(1);

            foreach (var obj in list)
            {
                EnumTypeAttribute.IsDefined(SampleEnum1.Value2, obj).Should().Be.True();
            }

            EnumTypeAttribute.IsDefined(SampleEnum1.Value2, "asd").Should().Be.True();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value2, 123).Should().Be.False();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value2, 234).Should().Be.False();
        }

        [Test]
        public void TestValue3()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value3);
            list.Count.Should().Be(1);

            foreach (var obj in list)
            {
                EnumTypeAttribute.IsDefined(SampleEnum1.Value3, obj).Should().Be.True();
            }

            EnumTypeAttribute.IsDefined(SampleEnum1.Value3, "asd").Should().Be.False();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value3, 123).Should().Be.True();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value3, 234).Should().Be.False();
        }

        [Test]
        public void TestValue4()
        {
            var list = EnumTypeAttribute.GetEnumTypes(SampleEnum1.Value4);
            list.Count.Should().Be(0);

            foreach (var obj in list)
            {
                EnumTypeAttribute.IsDefined(SampleEnum1.Value4, obj).Should().Be.True();
            }

            EnumTypeAttribute.IsDefined(SampleEnum1.Value4, "asd").Should().Be.False();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value4, 123).Should().Be.False();
            EnumTypeAttribute.IsDefined(SampleEnum1.Value4, 234).Should().Be.False();
        }

    }
}
