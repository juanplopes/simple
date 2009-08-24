using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class EntityHelperFixture
    {
        class Sample1
        {
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
            
            EntityHelper helper = new EntityHelper(obj1);
            helper.AddID((Sample1 x) => x.IntProp);
            helper.AddID((Sample1 x) => x.StringProp);

            Assert.IsTrue(helper.ObjectEquals(obj2));

            obj1.IntProp = obj2.IntProp = 42;
            Assert.IsTrue(helper.ObjectEquals(obj2));
            Assert.AreEqual(helper.ObjectGetHashCode(), helper.ObjectGetHashCode(obj2));

            obj1.StringProp = "A";
            Assert.IsFalse(helper.ObjectEquals(obj2));

            obj2.StringProp = "B";
            Assert.IsFalse(helper.ObjectEquals(obj2));

            obj1.StringProp = "B";
            Assert.IsTrue(helper.ObjectEquals(obj2));
            Assert.AreEqual(helper.ObjectGetHashCode(), helper.ObjectGetHashCode(obj2));
        }

        [Test]
        public void TestBasicTypesEqualityOuter()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EntityHelper helper = new EntityHelper(typeof(Sample1));
            helper.AddAllProperties();

            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj1.IntProp = obj2.IntProp = 42;
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            Assert.AreEqual(helper.ObjectGetHashCode(obj1), helper.ObjectGetHashCode(obj2));

            obj1.StringProp = "A";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj2.StringProp = "B";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj1.StringProp = "B";
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            Assert.AreEqual(helper.ObjectGetHashCode(obj1), helper.ObjectGetHashCode(obj2));
        }

        [Test]
        public void TestInheritanceOuter()
        {
            Sample1 obj1 = new Sample1();
            Sample2 obj2 = new Sample2();

            EntityHelper helper = new EntityHelper(typeof(Sample1));
            helper.AddAllProperties();

            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));

            obj1.IntProp = obj2.IntProp = 42;
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            Assert.AreEqual(helper.ObjectGetHashCode(obj1), helper.ObjectGetHashCode(obj2));

            obj1.StringProp = "A";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj2.StringProp = "B";
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));

            obj1.StringProp = "B";
            Assert.IsTrue(helper.ObjectEquals(obj1, obj2));
            Assert.AreEqual(helper.ObjectGetHashCode(obj1), helper.ObjectGetHashCode(obj2));
        }

        [Test]
        public void TestDifferentTypes()
        {
            Sample1 obj1 = new Sample1();
            int obj2 = 42;

            EntityHelper helper = new EntityHelper(typeof(Sample1));
            Assert.IsFalse(helper.ObjectEquals(obj1, obj2));
        }

        [Test]
        public void TestNullrefs()
        {
            Sample1 obj1 = new Sample1();
            Sample1 obj2 = new Sample1();

            EntityHelper helper = new EntityHelper(typeof(Sample1));
            
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
