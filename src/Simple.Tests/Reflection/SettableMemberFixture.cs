using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class SettableMemberFixture
    {
        [Test]
        public void CanGetUsingTestProperty()
        {
            var obj = new Sample { TestProp = 42 };
            var prop = typeof(Sample).GetProperty("TestProp").ToSettable();

            prop.Get(obj).Should().Be(42);
        }

        [Test]
        public void CanSetUsingTestProperty()
        {
            var obj = new Sample();
            var prop = typeof(Sample).GetProperty("TestProp").ToSettable();
            prop.Set(obj, 43);

            prop.Get(obj).Should().Be(43);
        }

        [Test]
        public void PropertyWrapperShouldReturnInformation()
        {
            var prop = typeof(Sample).GetProperty("TestProp");
            var set = prop.ToSettable();

            set.Member.Should().Be(prop);
            set.Type.Should().Be(prop.PropertyType);
            set.DeclaringType.Should().Be(prop.DeclaringType);
            set.Name.Should().Be(prop.Name);
            set.CanWrite.Should().Be.True();
            set.CanRead.Should().Be.True();
        }

        [Test]
        public void CanGetFromListIndexed()
        {
            var obj = new List<int> { 1, 2, 3, 42, 5 };
            var prop = typeof(List<int>).GetProperty("Item").ToSettable();

            prop.Get(obj, 3).Should().Be(42);
            prop.Set(obj, 3, 43);
            obj.Should().Have.SameSequenceAs(1, 2, 3, 43, 5);
        }

        [Test]
        public void CanGetFromSampleIndexed()
        {
            var obj = new Sample();
            var prop = typeof(Sample).GetProperty("Item").ToSettable();

            prop.Get(obj, 42).Should().Be(42);
            prop.CanWrite.Should().Be.False();
            prop.CanRead.Should().Be.True();
        }


        [Test]
        public void CanGetUsingTestField()
        {
            var obj = new Sample { TestField = 42 };
            var prop = typeof(Sample).GetField("TestField").ToSettable();

            prop.Get(obj).Should().Be(42);
        }

        [Test]
        public void CanSetUsingTestField()
        {
            var obj = new Sample();
            var prop = typeof(Sample).GetField("TestField").ToSettable();
            prop.Set(obj, 43);

            prop.Get(obj).Should().Be(43);
        }
        [Test]
        public void FieldWrapperShouldReturnInformation()
        {
            var field = typeof(Sample).GetField("TestField");
            var set = field.ToSettable();

            set.Member.Should().Be(field);
            set.Type.Should().Be(field.FieldType);
            set.DeclaringType.Should().Be(field.DeclaringType);
            set.Name.Should().Be(field.Name);
            set.CanWrite.Should().Be.True();
            set.CanRead.Should().Be.True();
        }
        [Test]
        public void CanGetUsingTestFieldUsingIndexesWithoutProblems()
        {
            var obj = new Sample { TestField = 42 };
            var prop = typeof(Sample).GetField("TestField").ToSettable();

            prop.Get(obj, 1, 2, 3).Should().Be(42);
        }

        [Test]
        public void CanSetUsingTestFieldUsingIndexesWithoutProblems()
        {
            var obj = new Sample();
            var prop = typeof(Sample).GetField("TestField").ToSettable();
            prop.Set(obj, 43);

            prop.Get(obj, 1, 2, 3).Should().Be(43);
        }

        [Test]
        public void CanCreateCompositeSetterWithPropertyChain()
        {
            var outer = typeof(Sample).GetProperty("TestInner");
            var inner = typeof(Inner).GetProperty("TestInt");
            var props = new[] { outer, inner }.Select(x => x.ToSettable());

            var set = props.ToSettable();

            set.Member.Should().Be(inner);
            set.Name.Should().Be("TestInner.TestInt");
            set.Type.Should().Be(typeof(int));
            set.CanRead.Should().Be(true);
            set.CanWrite.Should().Be(true);
            set.DeclaringType.Should().Be(typeof(Sample));
        }

        [Test]
        public void CanGetValueFromCompositeSetterWithPropertyChain()
        {
            var outer = typeof(Sample).GetProperty("TestInner");
            var inner = typeof(Inner).GetProperty("TestInt");
            var props = new[] { outer, inner }.Select(x => x.ToSettable());

            var set = props.ToSettable();

            var obj = new Sample() { TestInner = new Inner() { TestInt = 42 } };
            set.Get(obj).Should().Be(42);
        }

        [Test]
        public void CanSetValueFromCompositeSetterWithPropertyChain()
        {
            var outer = typeof(Sample).GetProperty("TestInner");
            var inner = typeof(Inner).GetProperty("TestInt");
            var props = new[] { outer, inner }.Select(x => x.ToSettable());

            var set = props.ToSettable();

            var obj = new Sample();
            set.Set(obj, 42);

            obj.TestInner.TestInt.Should().Be(42);
        }


        class Sample
        {
            public int this[int index] { get { return index; } }
            public int TestProp { get; set; }
            public Inner TestInner { get; set; }
            public int TestField = 0;
        }

        class Inner
        {
            public int TestInt { get; set; }
        }
    }
}
