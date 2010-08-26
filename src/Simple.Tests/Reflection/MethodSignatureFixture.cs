using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using NUnit.Framework;
using SharpTestsEx;
using System.Collections;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    public class MethodSignatureFixture
    {
        [Test]
        public void StringEndsWithMethod()
        {
            var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string), typeof(bool), typeof(CultureInfo) });
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("Boolean EndsWith(String value, Boolean ignoreCase, CultureInfo culture)");
            sig.MakeCall().Should().Be("EndsWith(value, ignoreCase, culture)");
        }

        [Test]
        public void SingleStringMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringParameter");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void SingleStringParameter(String a)");
            sig.MakeCall().Should().Be("SingleStringParameter(a)");
        }

        [Test]
        public void TwoIntMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoIntParameters");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void TwoIntParameters(Int32 a, Int32 b)");
            sig.MakeCall().Should().Be("TwoIntParameters(a, b)");
        }

        [Test]
        public void TwoIntMethodSkippingFirst()
        {
            var method = typeof(Sample1).GetMethod("TwoIntParameters");
            var sig = new MethodSignature(method);
            sig.MakeSignature(1).Should().Be("void TwoIntParameters(Int32 b)");
            sig.MakeCall("this").Should().Be("TwoIntParameters(this, b)");
        }

        [Test]
        public void SingleStringOutMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringOutParameter");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void SingleStringOutParameter(out String a)");
            sig.MakeCall().Should().Be("SingleStringOutParameter(out a)");
        }

        [Test]
        public void SingleStringRefMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringRefParameter");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void SingleStringRefParameter(ref String a)");
            sig.MakeCall().Should().Be("SingleStringRefParameter(ref a)");
        }


        [Test]
        public void NamespacesForSingleStringOutMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringOutParameter");
            var sig = new MethodSignature(method);
            sig.MakeSignature();

            var expected = new[] { "System" };


            CollectionAssert.AreEquivalent(expected, sig.Namespaces.ToArray());
        }

        [Test]
        public void NamespacesForSingleSelfMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleSelfParameter");
            var sig = new MethodSignature(method);
            sig.MakeSignature();

            var expected = new[] { "System", "Simple.Tests.Reflection" };

            CollectionAssert.AreEquivalent(expected, sig.Namespaces.ToArray());
        }

        [Test]
        public void NamespacesForSingleSelfReturnMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleSelfReturn");
            var sig = new MethodSignature(method);
            sig.MakeSignature();

            var expected = new[] { "Simple.Tests.Reflection" };

            CollectionAssert.AreEquivalent(expected, sig.Namespaces.ToArray());
        }

        [Test]
        public void TwoGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoGenericParameters");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void TwoGenericParameters<T>(T a, T b)");
            sig.MakeCall().Should().Be("TwoGenericParameters<T>(a, b)");
        }

        [Test]
        public void OneGenericParamsParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("OneGenericParamsParameters");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void OneGenericParamsParameters<T>(params T[] a)");
            sig.MakeCall().Should().Be("OneGenericParamsParameters<T>(a)");
        }

        [Test]
        public void TwoConstrainedGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedGenericParameters");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void TwoConstrainedGenericParameters<T>(T a, T b) where T : class, IConvertible, new()");
            sig.MakeCall().Should().Be("TwoConstrainedGenericParameters<T>(a, b)");
        }

        [Test]
        public void TwoConstrainedStructGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedStructGenericParameters");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void TwoConstrainedStructGenericParameters<T>(T a, T b) where T : struct, IConvertible");
            sig.MakeCall().Should().Be("TwoConstrainedStructGenericParameters<T>(a, b)");
        }


        [Test]
        public void TwoConstrainedRefGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedRefGenericParameters");
            var sig = new MethodSignature(method);

            sig.ReturnsVoid.Should().Be.True();
            sig.MakeSignature().Should().Be("void TwoConstrainedRefGenericParameters<T>(ref T a, T b) where T : struct, IConvertible");
            sig.MakeCall().Should().Be("TwoConstrainedRefGenericParameters<T>(ref a, b)");
        }

        [Test]
        public void GenericParameterInsideGenericClass()
        {
            var method = typeof(Sample2<int>).GetMethod("GenericParameter");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void GenericParameter<Q>(Q a)");
            sig.MakeCall().Should().Be("GenericParameter<Q>(a)");
        }

        [Test]
        public void GenericParameterInsideGenericClassWithConstraint()
        {
            var method = typeof(Sample2<int>).GetMethod("GenericParameterInheriting");
            var sig = new MethodSignature(method);
            sig.MakeSignature().Should().Be("void GenericParameterInheriting<Q>(Q a) where Q : MethodSignatureFixture.Sample1, IConvertible, new()");
            sig.MakeCall().Should().Be("GenericParameterInheriting<Q>(a)");
        }

        [Test]
        public void SeveralGenericParameters()
        {
            var method = typeof(Sample2<int>).GetMethod("SeveralClasses");
            var sig = new MethodSignature(method);
            Assert.AreEqual(
                "A SeveralClasses<A, B, C>(B b, C c) where A : struct where B : class, ICollection, IEnumerable, IList, new() where C : List<B>, ICollection, ICollection<B>, IEnumerable, IEnumerable<B>, IList, IList<B>, new()", sig.MakeSignature());
            sig.MakeCall().Should().Be("SeveralClasses<A, B, C>(b, c)");
            sig.ReturnsVoid.Should().Be.False();
        }

        [Test]
        public void NamespacesForSeveralGenericParameters()
        {
            var method = typeof(Sample2<int>).GetMethod("SeveralClasses");
            var sig = new MethodSignature(method);
            sig.MakeSignature();

            var expected = new[] {
                "Simple.Tests.Reflection",
                "System.Collections",
                "System.Collections.Generic"
            };

            CollectionAssert.AreEquivalent(expected, sig.Namespaces.ToArray());
        }

        class Sample1
        {
            public void SingleStringParameter(string a) { }
            public void SingleStringOutParameter(out string a) { a = ""; }
            public void SingleStringRefParameter(ref string a) { a = ""; }
            public void TwoIntParameters(int a, int b) { }
            public void SingleSelfParameter(Sample1 a) { }
            public Sample1 SingleSelfReturn() { return null; }
            public void TwoGenericParameters<T>(T a, T b) { }
            public void OneGenericParamsParameters<T>(params T[] a) { }
            public void TwoConstrainedGenericParameters<T>(T a, T b)
                where T : class, IConvertible, new() { }
            public void TwoConstrainedStructGenericParameters<T>(T a, T b)
                where T : struct, IConvertible { }

            public void TwoConstrainedRefGenericParameters<T>(ref T a, T b)
               where T : struct, IConvertible { }
        }

        class Sample2<T>
        {
            public void GenericParameter<Q>(Q a) { }
            public void GenericParameterInheriting<Q>(Q a)
                where Q : Sample1, IConvertible, new() { }

            public A SeveralClasses<A, B, C>(B b, C c)
                where A : struct
                where B : class, IList, new()
                where C : List<B>, new()
            {
                return default(A);
            }

        }
    }
}
