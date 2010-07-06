using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Simple.Generator.Interfaces;
using NUnit.Framework;
using System.Collections;

namespace Simple.Tests.Generator
{
    public class MethodSignatureFixture
    {
        [Test]
        public void CanGetSignatureForStringEndsWithMethod()
        {
            var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string), typeof(bool), typeof(CultureInfo) });
            var sig = new MethodSignature(method);
            Assert.AreEqual("Boolean EndsWith(String value, Boolean ignoreCase, CultureInfo culture)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForSingleStringMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringParameter");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void SingleStringParameter(String a)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForTwoIntMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoIntParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoIntParameters(Int32 a, Int32 b)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForTwoIntMethodSkippingFirst()
        {
            var method = typeof(Sample1).GetMethod("TwoIntParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoIntParameters(Int32 b)", sig.MakeSignature(1));
        }

        [Test]
        public void CanGetSignatureForSingleStringOutMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringOutParameter");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void SingleStringOutParameter(out String a)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForSingleStringRefMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringRefParameter");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void SingleStringRefParameter(ref String a)", sig.MakeSignature(0));
        }


        [Test]
        public void CanGetInvolvedNamespacesSingleStringOutMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleStringOutParameter");
            var sig = new MethodSignature(method);
            var expected = new[] {
                "System"
            };


            CollectionAssert.AreEquivalent(expected, sig.InvolvedNamespaces.ToArray());
        }

        [Test]
        public void CanGetInvolvedNamespacesForSingleSelfMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleSelfParameter");
            var sig = new MethodSignature(method);
            var expected = new[] {
                "System", 
                "Simple.Tests.Generator"
            };

            CollectionAssert.AreEquivalent(expected, sig.InvolvedNamespaces.ToArray());
        }

        [Test]
        public void CanGetInvolvedNamespacesForSingleSelfReturnMethod()
        {
            var method = typeof(Sample1).GetMethod("SingleSelfReturn");
            var sig = new MethodSignature(method);
            var expected = new[] {
                "Simple.Tests.Generator"
            };

            CollectionAssert.AreEquivalent(expected, sig.InvolvedNamespaces.ToArray());
        }

        [Test]
        public void CanGetSignatureForTwoGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoGenericParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoGenericParameters<T>(T a, T b)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForOneGenericParamsParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("OneGenericParamsParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void OneGenericParamsParameters<T>(params T[] a)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForTwoConstrainedGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedGenericParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoConstrainedGenericParameters<T>(T a, T b) where T : class, IConvertible, new()", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForTwoConstrainedStructGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedStructGenericParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoConstrainedStructGenericParameters<T>(T a, T b) where T : struct, IConvertible", sig.MakeSignature(0));
        }


        [Test]
        public void CanGetSignatureForTwoConstrainedRefGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedRefGenericParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoConstrainedRefGenericParameters<T>(ref T a, T b) where T : struct, IConvertible", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForGenericParameterInsideGenericClass()
        {
            var method = typeof(Sample2<int>).GetMethod("GenericParameter");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void GenericParameter<Q>(Q a)", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForGenericParameterInsideGenericClassWithConstraint()
        {
            var method = typeof(Sample2<int>).GetMethod("GenericParameterInheriting");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void GenericParameterInheriting<Q>(Q a) where Q : MethodSignatureFixture.Sample1, IConvertible, new()", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetSignatureForSeveralGenericParameters()
        {
            var method = typeof(Sample2<int>).GetMethod("SeveralClasses");
            var sig = new MethodSignature(method);
            Assert.AreEqual(
                "A SeveralClasses<A, B, C>(B b, C c) where A : struct where B : class, ICollection, IEnumerable, IList, new() where C : List<B>, ICollection, ICollection<B>, IEnumerable, IEnumerable<B>, IList, IList<B>, new()", sig.MakeSignature(0));
        }

        [Test]
        public void CanGetInvolvedNamespacesForSeveralGenericParameters()
        {
            var method = typeof(Sample2<int>).GetMethod("SeveralClasses");
            var sig = new MethodSignature(method);
            var expected = new[] {
                "Simple.Tests.Generator",
                "System.Collections",
                "System.Collections.Generic",
                "System"
            };

            CollectionAssert.AreEquivalent(expected, sig.InvolvedNamespaces.ToArray());
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
