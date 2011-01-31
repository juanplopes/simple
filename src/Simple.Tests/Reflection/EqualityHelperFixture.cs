using NUnit.Framework;
using SharpTestsEx;
using Simple.Entities;
using Simple.Reflection;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

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

        struct Sample4
        {
            private int IntProp { get; set; }
            private string StringProp { get; set; }
            public Sample4(int a, string b) : this() { this.IntProp = a; this.StringProp = b; }
        }

        struct Sample5
        {
            private int IntProp;
            private string StringProp;
            public Sample5(int a, string b) : this() { this.IntProp = a; this.StringProp = b; }
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
        public void ShouldServeAsNonGenericIEqualityComparer()
        {
            Sample1 obj1 = new Sample1();

            EqualityHelper<Sample1> helper = new EqualityHelper<Sample1>();
            helper.Add(x => x.IntProp);
            helper.Add(x => x.StringProp);

            var hash = new Hashtable(helper);
            hash.Add(new Sample1() { IntProp = 2, StringProp = "asd" }, "doesn't matter");
            
            hash.ContainsKey(new Sample1() { IntProp = 2, StringProp = "asd" }).Should().Be.True();
            hash.ContainsKey(new Sample1() { IntProp = 3, StringProp = "asd" }).Should().Be.False();
        }

        [Test]
        public void ShouldServeAsGenericIEqualityComparer()
        {
            Sample1 obj1 = new Sample1();

            EqualityHelper<Sample1> helper = new EqualityHelper<Sample1>();
            helper.Add(x => x.IntProp);
            helper.Add(x => x.StringProp);

            var hash = new Dictionary<Sample1, string>(helper);
            hash.Add(new Sample1() { IntProp = 2, StringProp = "asd" }, "doesn't matter");

            hash.ContainsKey(new Sample1() { IntProp = 2, StringProp = "asd" }).Should().Be.True();
            hash.ContainsKey(new Sample1() { IntProp = 3, StringProp = "asd" }).Should().Be.False();
        }

        [Test]
        public void TestBasicTypesEqualityOuter()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));
            helper.AddAllProperties();

            helper.ObjectEquals(obj1, obj2).Should().Be.True();

            obj1.IntProp = obj2.IntProp = 42;
            helper.ObjectEquals(obj1, obj2).Should().Be.True();
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));

            obj1.StringProp = "A";
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj2.StringProp = "B";
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj1.StringProp = "B";
            helper.ObjectEquals(obj1, obj2).Should().Be.True();
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));
        }

        [Test]
        public void TestBasicTypesEqualityOuterUsingPrivateProperties()
        {
            Sample4 obj1 = new Sample4();
            Sample4 obj2 = new Sample4();

            EqualityHelper helper = new EqualityHelper<Sample4>();
            helper.Add("IntProp").Add("StringProp");

            helper.ObjectEquals(obj1, obj2).Should().Be.True();
        }

        [Test]
        public void TestBasicTypesEqualityOuterUsingPrivateFields()
        {
            Sample5 obj1 = new Sample5();
            Sample5 obj2 = new Sample5();

            EqualityHelper helper = new EqualityHelper(typeof(Sample5));
            helper.Add("IntProp").Add("StringProp");

            helper.ObjectEquals(obj1, obj2).Should().Be.True();
        }


        [Test]
        public void TestBasicTypesEqualityOuterWithCustomComparer()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            var helper = new EqualityHelper<Sample1>();
            helper.Add(x => x.StringProp, StringComparer.InvariantCultureIgnoreCase);

            helper.ObjectEquals(obj1, obj2).Should().Be.True();

            obj1.StringProp = "A";
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj2.StringProp = "A";
            helper.ObjectEquals(obj1, obj2).Should().Be.True();

            obj1.StringProp = "a";
            helper.ObjectEquals(obj1, obj2).Should().Be.True();
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

            helper.ObjectEquals(obj1, obj2).Should().Be.True();

            obj1.IntProp = obj2.IntProp = 42;
            helper.ObjectEquals(obj1, obj2).Should().Be.True();
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));

            obj1.StringProp = "A";
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj2.StringProp = "B";
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj1.StringProp = "B";
            helper.ObjectEquals(obj1, obj2).Should().Be.True();
            helper.ObjectGetHashCode(obj2).Should().Be(helper.ObjectGetHashCode(obj1));
        }

        [Test]
        public void TestDifferentTypes()
        {
            Sample1 obj1 = new Sample1();
            int obj2 = 42;

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));
            helper.ObjectEquals(obj1, obj2).Should().Be.False();
        }

        [Test]
        public void TestNullrefs()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EqualityHelper helper = new EqualityHelper(typeof(Sample1));

            helper.ObjectEquals(obj1, obj2).Should().Be.True();

            obj2 = null;
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj2 = obj1; obj1 = null;
            helper.ObjectEquals(obj1, obj2).Should().Be.False();

            obj1 = obj2 = null;
            helper.ObjectEquals(obj1, obj2).Should().Be.True();
        }
    }
}
