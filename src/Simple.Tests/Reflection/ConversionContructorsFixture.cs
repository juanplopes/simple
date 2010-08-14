using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class ConversionContructorsFixture
    {
        [Test]
        public void CanFindCorrectConversionConstructor()
        {
            var ctors = new ConversionConstructors();
            var ctor = ctors.MakeConversionPlan(typeof(Type1));

            var obj = ctor.Converter(2);
            obj.Should().Be.OfType<Type1>()
                .And.ValueOf.ok.Should().Be.True();
        }

        [Test]
        public void CanFindCorrectConversionConstructorWith2Levels()
        {
            var ctors = new ConversionConstructors();
            var ctor = ctors.MakeConversionPlan(typeof(Type3));

            var obj = ctor.Converter(2);
            obj.Should().Be.OfType<Type3>()
                .And.ValueOf.a1.ok.Should().Be.True();
        }


        [Test]
        public void ReturnsNullWhenNoGoodConstructorFound()
        {
            var ctors = new ConversionConstructors();
            var ctor = ctors.MakeConversionPlan(typeof(Type2));
            ctor.Should().Be.Null();
        }


        public class Type3
        {
            public Type1 a1 = null;
            public Type3(Type1 type)
            {
                a1 = type;
            }
        }

        public class Type1
        {
            public bool ok = false;
            public Type1() { }
            public Type1(int a) { ok = true; }
            public Type1(int a, int b) { }
        }

        public class Type2
        {
        }




    }
}
