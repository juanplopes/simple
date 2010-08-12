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
            var ctor = MethodCache.Do.GetInvoker(ctors.GetBest(typeof(Type1)));

            var obj = ctor(null, 2);
            obj.Should().Be.InstanceOf<Type1>();
            ((Type1)obj).ok.Should().Be.True();
        }

        [Test]
        public void ReturnsNullWhenNoGoodConstructorFound()
        {
            var ctors = new ConversionConstructors();
            var ctor = ctors.GetBest(typeof(Type2));
            ctor.Should().Be.Null();
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
