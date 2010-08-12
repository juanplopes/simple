using NUnit.Framework;
using SharpTestsEx;
using Simple.Entities;
using Simple.Reflection;
using System.Linq;
using System;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class EqualityHelperFixture
    {
        class Sample1
        {
            public int IntProp { get; set; }
            public string StringProp { get; set; }
        }

        class Sample3 : Entity<Sample3>
        {
            static Sample3()
            {
                
                Identifiers.Add(x => x.IntProp).Add(x => x.StringProp);
            }

            public int IntProp { get; set; }
            public string StringProp { get; set; }
        }

        class Sample2 : Sample1
        {
            public string Other { get; set; }
        }

        [Test]
        public void TestBasicTypesEqualityInner()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EqualityHelper helper = new EqualityHelper(obj1);
            helper.Add((Sample1 x) => x.IntProp);
            helper.Add((Sample1 x) => x.StringProp);

            helper.ObjectEquals(obj2).Should().Be.True();

            obj1.IntProp = obj2.IntProp = 42;
            helper.ObjectEquals(obj2).Should().Be.True();
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode());

            obj1.StringProp = "A";
            helper.ObjectEquals(obj2).Should().Be.False();

            obj2.StringProp = "B";
            helper.ObjectEquals(obj2).Should().Be.False();

            obj1.StringProp = "B";
            helper.ObjectEquals(obj2).Should().Be.True();
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode());
        }

        [Test]
        public void TestBasicTypesEqualityOuter()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));
            helper.AddAllProperties();

            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj1.IntProp = obj2.IntProp = 42;
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));

            obj1.StringProp = "A";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj2.StringProp = "B";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj1.StringProp = "B";
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));
        }

        [Test]
        public void TestBasicTypesEqualityOuterWithCustomComparer()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            var helper = new EqualityHelper<Sample1>();
            helper.Add(x => x.StringProp, StringComparer.InvariantCultureIgnoreCase);

            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj1.StringProp = "A";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj2.StringProp = "A";
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj1.StringProp = "a";
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));
        }

        [Test]
        public void TestToStringSingleKey()
        {
            var obj1 = new Sample1();
            var helper = new EqualityHelper<Sample1>(x => x.IntProp);
            obj1.IntProp = 123;

            helper.ObjectToString(obj1).Should().Be("(IntProp=123)");
        }

        [Test]
        public void TestToStringMultipleKey()
        {
            var obj1 = new Sample1();
            var helper = new EqualityHelper<Sample1>(x => x.IntProp, x => x.StringProp);
            obj1.IntProp = 123;
            obj1.StringProp = "asd";

            helper.ObjectToString(obj1).Should().Be("(IntProp=123 | StringProp=asd)");
        }

        [Test]
        public void TestIdentifierListMultipleKey()
        {
            var helper = new EqualityHelper<Sample1>(x => x.IntProp, x => x.StringProp);
            CollectionAssert.AreEquivalent(
                new[] { "IntProp", "StringProp" }, 
                helper.IdentifierNamesList);
        }

        [Test]
        public void TestIdentifierListMultipleKeyUsingEntity()
        {
            new Sample3(); //Workaround TODO
            CollectionAssert.AreEquivalent(
                new[] { "IntProp", "StringProp" },
                Sample3.Identifiers.IdentifierNamesList);
        }

        [Test]
        public void TestToStringMultipleKeyWithNullKey()
        {
            var obj1 = new Sample1();
            var helper = new EqualityHelper<Sample1>(x => x.IntProp, x => x.StringProp);
            obj1.IntProp = 123;
            obj1.StringProp = null;

            helper.ObjectToString(obj1).Should().Be("(IntProp=123 | StringProp=<null>)");
        }


        [Test]
        public void TestInheritanceOuter()
        {
            Sample1 obj1 = new Sample1();
            Sample2 obj2 = new Sample2();

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));
            helper.AddAllProperties();

            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj1.IntProp = obj2.IntProp = 42;
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));

            obj1.StringProp = "A";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj2.StringProp = "B";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj1.StringProp = "B";
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));
        }

        [Test]
        public void TestDifferentTypes()
        {
            Sample1 obj1 = new Sample1();
            int obj2 = 42;

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));
        }

        [Test]
        public void TestNullrefs()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));

            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj2 = null;
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj2 = obj1; obj1 = null;
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj1 = obj2 = null;
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
        }
    }
}
