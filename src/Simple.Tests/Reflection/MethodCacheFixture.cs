using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class MethodCacheFixture
    {
        public class Sample1
        {
            public int Prop { get; set; }
            public int Method() { return 1; }
        }

        [Test]
        public void TestMethodNonCachedResult()
        {
            Type t = typeof(Sample1);

            MethodInfo m1 = t.GetMethod("Method");
            InvocationDelegate d1 = InvokerFactory.Do.Create(m1);

            MethodInfo m2 = t.GetMethod("Method");
            InvocationDelegate d2 = InvokerFactory.Do.Create(m1);

            Assert.AreEqual(m1, m2);
            Assert.AreNotEqual(d1, d2);

        }

        [Test]
        public void TestPropertyNonCachedResult()
        {
            Type t = typeof(Sample1);

            MethodInfo m1 = t.GetProperty("Prop").GetSetMethod();
            InvocationDelegate d1 = InvokerFactory.Do.Create(m1);

            MethodInfo m2 = t.GetProperty("Prop").GetSetMethod();
            InvocationDelegate d2 = InvokerFactory.Do.Create(m1);

            Assert.AreEqual(m1, m2);
            Assert.AreNotEqual(d1, d2);
        }

        [Test]
        public void TestMethodCachedResult()
        {
            Type t = typeof(Sample1);
            MethodCache cache = new MethodCache();

            MethodInfo m1 = t.GetMethod("Method");
            InvocationDelegate d1 = cache.GetInvoker(m1);

            MethodInfo m2 = t.GetMethod("Method");
            InvocationDelegate d2 = cache.GetInvoker(m2);

            Assert.AreEqual(m1, m2);
            Assert.AreEqual(d1, d2);

        }

        [Test]
        public void TestPropertyGetCachedResult()
        {
            Type t = typeof(Sample1);
            MethodCache cache = new MethodCache();

            PropertyInfo p1 = t.GetProperty("Prop");
            InvocationDelegate d1 = cache.GetGetter(p1);

            PropertyInfo p2 = t.GetProperty("Prop");
            InvocationDelegate d2 = cache.GetGetter(p2);

            Assert.AreEqual(p1, p2);
            Assert.AreEqual(d1, d2);
        }

        [Test]
        public void TestPropertySetCachedResult()
        {
            Type t = typeof(Sample1);
            MethodCache cache = new MethodCache();

            PropertyInfo p1 = t.GetProperty("Prop");
            InvocationDelegate d1 = cache.GetSetter(p1);

            PropertyInfo p2 = t.GetProperty("Prop");
            InvocationDelegate d2 = cache.GetSetter(p2);

            Assert.AreEqual(p1, p2);
            Assert.AreEqual(d1, d2);
        }
    }
}
