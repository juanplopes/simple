using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Simple.Generator.Interfaces;
using NUnit.Framework;

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

        [Test, Ignore]
        public void CanGetSignatureForTwoConstrainedGenericParametersMethod()
        {
            var method = typeof(Sample1).GetMethod("TwoConstrainedGenericParameters");
            var sig = new MethodSignature(method);
            Assert.AreEqual("void TwoConstrainedGenericParameters<T>(T a, T b) where T : class, IConvertible, new()", sig.MakeSignature(0));
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
            public void TwoConstrainedGenericParameters<T>(T a, T b)
                where T : class, IConvertible, new() { }
        }
    }
}
