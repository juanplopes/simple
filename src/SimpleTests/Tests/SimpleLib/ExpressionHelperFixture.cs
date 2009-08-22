using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;
using System.Linq.Expressions;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class ExpressionHelperFixture
    {
        class A
        {
            public B BProp { get; set; }
        }

        class B
        {
            public C CProp { get; set; }
            public int Diff { get; set; }
        }

        class C
        {
            public int IntProp { get; set; }
            public D DProp { get; set; }
        }

        class D
        {
            public D(int a) { Value = a; }
            public int Value { get; set; }
        }




        [Test]
        public void TestSimplePropertyName()
        {
            var propName = ExpressionHelper.GetPropertyName((A a) => a.BProp);
            Assert.AreEqual("BProp", propName);
        }

        [Test]
        public void TestNestedPropertyName()
        {
            var propName = ExpressionHelper.GetPropertyName((A a) => a.BProp.CProp.IntProp);
            Assert.AreEqual("BProp.CProp.IntProp", propName);
        }

        [Test]
        public void TestSimplePropertySet()
        {
            Expression<Func<A, B>> lambda = x => x.BProp;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, new B() { Diff = 42 }, false);


            Assert.IsNotNull(a.BProp);
            Assert.AreEqual(42, a.BProp.Diff);
        }

        [Test]
        public void TestSimplePropertySetNewInstance()
        {
            Expression<Func<A, B>> lambda = x => x.BProp;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, null, true);


            Assert.IsNotNull(a.BProp);
            Assert.AreEqual(0, a.BProp.Diff);
        }

        [Test]
        public void TestNestedPropertySet()
        {
            Expression<Func<A, int>> lambda = x => x.BProp.CProp.IntProp;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, 42, false);

            Assert.IsNotNull(a.BProp);
            Assert.AreEqual(0, a.BProp.Diff);
            Assert.IsNotNull(a.BProp.CProp);
            Assert.AreEqual(42, a.BProp.CProp.IntProp);
        }

        [Test, ExpectedException(typeof(MissingMethodException))]
        public void TestNoDefaultConstructorSet()
        {
            Expression<Func<A, int>> lambda = x => x.BProp.CProp.DProp.Value;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, 42, false);

        }

    }

}
