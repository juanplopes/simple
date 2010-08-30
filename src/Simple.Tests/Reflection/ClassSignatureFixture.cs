using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Reflection
{
    public class ClassSignatureFixture
    {
        [Test]
        public void SingleImplementation()
        {
            var sig = new ClassSignature(typeof(SingleImplementationClass));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest1");
        }
        [Test]
        public void DoubleImplementation()
        {
            var sig = new ClassSignature(typeof(DoubleImplementationClass));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest1, ClassSignatureFixture.ITest2");
        }

        [Test]
        public void DoubleImplementationExcept1()
        {
            var sig = new ClassSignature(typeof(DoubleImplementationClass));
            Assert.AreEqual("ClassSignatureFixture.ITest2", 
                sig.MakeImplementingSignature("ClassSignatureFixture.ITest1"));
        }

        [Test]
        public void GenericImplementation()
        {
            var sig = new ClassSignature(typeof(GenericImplementationClass));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest1, ClassSignatureFixture.ITest2, ClassSignatureFixture.ITest3<Int32>");
        }

        [Test]
        public void UnresolvedGenericImplementation()
        {
            var sig = new ClassSignature(typeof(UnresolvedGenericImplementationClass<>));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest3<T>");
        }
        [Test]
        public void ResolvedGenericImplementation()
        {
            var sig = new ClassSignature(typeof(UnresolvedGenericImplementationClass<String>));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest3<String>");
        }

        [Test]
        public void ConstrainedGenericImplementation()
        {
            var sig = new ClassSignature(typeof(ConstrainedGenericClass<>));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest3<T> where T : struct, IConvertible");
        }

        [Test]
        public void ConstrainedGenericImplementationExcept3()
        {
            var sig = new ClassSignature(typeof(ConstrainedGenericClass<>));
            Assert.AreEqual(" where T : struct, IConvertible", 
                sig.MakeImplementingSignature("ClassSignatureFixture.ITest3<T>"));
        }


        [Test]
        public void DoubleConstrainedGenericImplementation()
        {
            var sig = new ClassSignature(typeof(DoubleConstrainedGenericClass<>));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest3<T>, ClassSignatureFixture.ITest4<T> where T : struct, IConvertible");
        }

        [Test]
        public void ResolvedConstrainedGenericImplementation()
        {
            var sig = new ClassSignature(typeof(DoubleConstrainedGenericClass<int>));
            sig.MakeImplementingSignature().Should().Be("ClassSignatureFixture.ITest3<Int32>, ClassSignatureFixture.ITest4<Int32>");
        }

        class SingleImplementationClass : ITest1 { }
        class DoubleImplementationClass : ITest2, ITest1 { }
        class GenericImplementationClass : ITest3<int>, ITest2, ITest1 { }
        class UnresolvedGenericImplementationClass<T> : ITest3<T> { }
        class ConstrainedGenericClass<T> : ITest3<T>
            where T : struct, IConvertible
        {
            public void Test() { }
        }

        class DoubleConstrainedGenericClass<T> : ITest3<T>, ITest4<T>
            where T : struct, IConvertible
        { }


        interface ITest1 { }
        interface ITest2 { }
        interface ITest3<T> { }
        interface ITest4<T> where T : struct { }
    }
}
