using System;
using System.Linq.Expressions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Simple.Tests.Expressions
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
            public IList<D> DList { get; set; }
        }

        class D
        {
            public D(int a) { Value = a; }
            public int Value { get; set; }
            public int GetValue() { return this.Value; }
        }




        [Test]
        public void TestSimplePropertyName()
        {
            var propName = ExpressionHelper.GetMemberName((A a) => a.BProp);
            Assert.AreEqual("BProp", propName);
        }

        [Test]
        public void TestNestedPropertyName()
        {
            var propName = ExpressionHelper.GetMemberName((A a) => a.BProp.CProp.IntProp);
            Assert.AreEqual("BProp.CProp.IntProp", propName);
        }

        [Test]
        public void TestNestedMethodName()
        {
            var propName = ExpressionHelper.GetMemberName((A a) => a.BProp.CProp.DProp.GetValue());
            Assert.AreEqual("BProp.CProp.DProp.GetValue", propName);
        }

        [Test]
        public void TestSimplePropertySet()
        {
            Expression<Func<A, B>> lambda = x => x.BProp;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, new B() { Diff = 42 });


            Assert.IsNotNull(a.BProp);
            Assert.AreEqual(42, a.BProp.Diff);
        }

        [Test]
        public void TestSimplePropertySetList()
        {
            Expression<Func<A, IList<D>>> lambda = x => x.BProp.CProp.DList;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, new[] { new D(1), new D(2) });

            Assert.IsNotNull(a.BProp.CProp.DList);
        }


        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void FailSettingMethod()
        {
            Expression<Func<A, int>> lambda = x => x.BProp.CProp.DProp.GetValue();

            A a = new A();

            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, 50);
        }


        [Test]
        public void TestSimplePropertySetNewInstance()
        {
            Expression<Func<A, B>> lambda = x => x.BProp;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, new B());


            Assert.IsNotNull(a.BProp);
            Assert.AreEqual(0, a.BProp.Diff);
        }

        [Test]
        public void TestNestedPropertySet()
        {
            Expression<Func<A, int>> lambda = x => x.BProp.CProp.IntProp;

            A a = new A();
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, 42);

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
            ExpressionHelper.SetValue(lambda.Body as MemberExpression, a, 42);

        }

        [Test]
        public void TestGetPropertyFromString()
        {
            var type = typeof(A);
            var str = "BProp.CProp.DProp";

            var prop = str.GetProperty(type);

            Assert.That(prop.Name, Is.EqualTo("DProp"));
            Assert.That(prop.DeclaringType, Is.EqualTo(typeof(C)));
            Assert.That(prop.PropertyType, Is.EqualTo(typeof(D)));
        }

        [Test]
        public void TestGetPropertyFromStringArray()
        {
            var type = typeof(A);
            var str = new[] { "BProp", "CProp", "DProp" };

            var prop = str.GetProperty(type);

            Assert.That(prop.Name, Is.EqualTo("DProp"));
            Assert.That(prop.DeclaringType, Is.EqualTo(typeof(C)));
            Assert.That(prop.PropertyType, Is.EqualTo(typeof(D)));
        }

        [Test]
        public void TestGetPropertyFromStringAndGenericType()
        {
            var str = "BProp.CProp.DProp";

            var prop = str.GetProperty<A>();

            Assert.That(prop.Name, Is.EqualTo("DProp"));
            Assert.That(prop.DeclaringType, Is.EqualTo(typeof(C)));
            Assert.That(prop.PropertyType, Is.EqualTo(typeof(D)));
        }

        [Test]
        public void TestGetPropertyFromStringArrayAndGenericType()
        {
            var str = new[] { "BProp", "CProp", "DProp" };

            var prop = str.GetProperty<A>();

            Assert.That(prop.Name, Is.EqualTo("DProp"));
            Assert.That(prop.DeclaringType, Is.EqualTo(typeof(C)));
            Assert.That(prop.PropertyType, Is.EqualTo(typeof(D)));
        }

        [Test]
        public void TestGetPropertyFromEmptyString()
        {
            var str = "";

            var prop = str.GetProperty<A>();

            Assert.That(prop, Is.Null);
        }

        [Test]
        public void TestTryGetNonExistingPropertyFromString()
        {
            var str = "KProp";

            Assert.Throws<ArgumentException>(() =>
            {
                str.GetProperty<A>();
            });
        }


        [Test]
        public void TestGetPropertyExpressionFromString()
        {
            var str = "BProp.CProp.DProp";

            var expr = Expression.Parameter(typeof(A), "x");
            var prop = str.GetPropertyExpression(expr);

            Assert.AreEqual("x.BProp.CProp.DProp", prop.ToString());
        }

        [Test]
        public void TestGetPropertyExpressionFromStringArray()
        {
            var str = new[] { "BProp", "CProp", "DProp" };

            var expr = Expression.Parameter(typeof(A), "x");
            var prop = str.GetPropertyExpression(expr);

            Assert.AreEqual("x.BProp.CProp.DProp", prop.ToString());

        }

        [Test]
        public void TestGetPropertyLambdaWithObjectReturnTypeFromStringArray()
        {
            var str = new[] { "BProp", "CProp", "DProp" };

            var prop = str.GetPropertyLambda<A>();

            Assert.AreEqual("x => x.BProp.CProp.DProp", prop.ToString());

        }

        [Test]
        public void TestGetPropertyLambdaWithDReturnTypeFromStringArray()
        {
            var str = new[] { "BProp", "CProp", "DProp" };

            var prop = str.GetPropertyLambda<A, D>();

            Assert.AreEqual("x => x.BProp.CProp.DProp", prop.ToString());

        }

        [Test]
        public void TestGetPropertyLambdaWithObjectReturnTypeFromString()
        {
            var str = "BProp.CProp.DProp";

            var prop = str.GetPropertyLambda<A>();

            Assert.AreEqual("x => x.BProp.CProp.DProp", prop.ToString());

        }

        [Test]
        public void TestGetPropertyLambdaWithDReturnTypeFromString()
        {
            var str = "BProp.CProp.DProp";

            var prop = str.GetPropertyLambda<A, D>();

            Assert.AreEqual("x => x.BProp.CProp.DProp", prop.ToString());

        }


        [Test]
        public void TestGetPropertyExpressionFromEmptyString()
        {
            var str = "";
            var expr = Expression.Parameter(typeof(A), "x");
            var prop = str.GetPropertyExpression(expr);

            Assert.AreEqual("x", prop.ToString());
        }

        [Test]
        public void TestTryGetNonExistingPropertyExpressionFromString()
        {
            var str = "KProp";
            var expr = Expression.Parameter(typeof(A), "x");

            Assert.Throws<ArgumentException>(() =>
            {
                str.GetPropertyExpression(expr);
            });
        }
    }

}
