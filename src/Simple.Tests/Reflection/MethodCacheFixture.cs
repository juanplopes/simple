using System;
using System.Reflection;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class MethodCacheFixture
    {
        public class Sample1
        {
            public Sample1() { }
            public Sample1(string a) { Prop = int.Parse(a); }
            protected Sample1(int a, int? b) { Prop = a + (b ?? 0); }

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

            Assert.AreSame(m1, m2);
            Assert.AreNotSame(d1, d2);

        }

        [Test]
        public void TestPropertyNonCachedResult()
        {
            Type t = typeof(Sample1);

            MethodInfo m1 = t.GetProperty("Prop").GetSetMethod();
            InvocationDelegate d1 = InvokerFactory.Do.Create(m1);

            MethodInfo m2 = t.GetProperty("Prop").GetSetMethod();
            InvocationDelegate d2 = InvokerFactory.Do.Create(m1);

            Assert.AreSame(m1, m2);
            Assert.AreNotSame(d1, d2);
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

            Assert.AreSame(m1, m2);
            Assert.AreSame(d1, d2);

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

            Assert.AreSame(p1, p2);
            Assert.AreSame(d1, d2);
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

            Assert.AreSame(p1, p2);
            Assert.AreSame(d1, d2);
        }

        [Test]
        public void TestConstructorResolutionWithNoParams()
        {
            MethodCache cache = new MethodCache();
            var obj1 = cache.CreateInstance<Sample1>();

            Assert.AreEqual(0, obj1.Prop);
        }

        [Test]
        public void TestConstructorResolutionWith1StringParam()
        {
            MethodCache cache = new MethodCache();
            var obj1 = cache.CreateInstance<Sample1>("1");

            Assert.AreEqual(1, obj1.Prop);
        }

        [Test]
        public void TestConstructorResolutionWith1StringWrongParam()
        {
            MethodCache cache = new MethodCache();
            Assert.Throws<FormatException>(() =>
                    cache.CreateInstance<Sample1>("a"));
        }

        [Test]
        public void TestConstructorResolutionWith2IntParam()
        {
            MethodCache cache = new MethodCache();
            var obj1 = cache.CreateInstance<Sample1>(BindingFlags.NonPublic | BindingFlags.Instance, 1, 2);

            Assert.AreEqual(3, obj1.Prop);
        }

        [Test]
        public void TestConstructorResolutionWith2IntParamWhenOneIsNull()
        {
            MethodCache cache = new MethodCache();
            var obj = cache.CreateInstance<Sample1>(BindingFlags.NonPublic | BindingFlags.Instance, 1, null);
            Assert.AreEqual(1, obj.Prop);
        }

        [Test]
        public void TestConstructorThatDoesntExist()
        {
            MethodCache cache = new MethodCache();
            Assert.Throws<MissingMethodException>(()=>
                    cache.CreateInstance<Sample1>("asd", null, 1));
        }
    }
}
