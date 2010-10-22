using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;
using SharpTestsEx;
using Simple.IO.Serialization;

namespace Simple.Tests.Expressions
{
    [TestFixture]
    public class ExpressionWrapperFixture
    {
        [Test]
        public void CanGetExpressionFromSingleProperty()
        {
            var expr = ExprTyped(x => x.TestProp).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.True();
            expr.Type.Should().Be<int>();
            expr.Name.Should().Be("x.TestProp");

            var obj = new Sample();
            expr.Set(obj, 42);
            obj.TestProp.Should().Be(42);
            expr.Get(obj).Should().Be(42);
        }

        [Test]
        public void CanGetExpressionFromSinglePropertyNonTyped()
        {
            var expr = Expr(x => x.TestProp).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.True();
            expr.Type.Should().Be<int>();
            expr.Name.Should().Be("Convert(x.TestProp)");

            var obj = new Sample();
            expr.Set(obj, 42);
            obj.TestProp.Should().Be(42);
            expr.Get(obj).Should().Be(42);
        }

        [Test]
        public void CanGetExpressionFromCompositeProperty()
        {
            var expr = ExprTyped(x => x.TestInner.TestInt).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.True();
            expr.Type.Should().Be<int>();
            expr.Name.Should().Be("x.TestInner.TestInt");

            var obj = new Sample();
            expr.Set(obj, 42);
            obj.TestInner.TestInt.Should().Be(42);
            expr.Get(obj).Should().Be(42);
        }

        [Test]
        public void CanGetExpressionFromCompositePropertyNonTyped()
        {
            var expr = Expr(x => x.TestInner.TestInt).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.True();
            expr.Type.Should().Be<int>();
            expr.Name.Should().Be("Convert(x.TestInner.TestInt)");

            var obj = new Sample();
            expr.Set(obj, 42);
            obj.TestInner.TestInt.Should().Be(42);
            expr.Get(obj).Should().Be(42);
        }


        [Test]
        public void CanGetExpressionFromCompositePropertyWithMethodCall()
        {
            var expr = ExprTyped(x => x.TestInner.TestInt.ToString()).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.False();
            expr.Type.Should().Be<string>();
            expr.Name.Should().Be("x.TestInner.TestInt.ToString()");

            var obj = new Sample();
            expr.Executing(x => x.Set(obj, 42)).Throws<NotSupportedException>();

            obj.TestInner = new Inner() { TestInt = 50 };

            expr.Get(obj).Should().Be("50");
        }

        [Test]
        public void CanGetCrazyExpressions()
        {
            var expr = ExprTyped(x => 2 + 2.0f).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.False();
            expr.Type.Should().Be<float>();
            expr.Name.Should().Be("4");


            var obj = new Sample();
            expr.Executing(x => x.Set(obj, 42)).Throws<NotSupportedException>();

            expr.Get(obj).Should().Be(4.0f);
        }

        [Test]
        public void CanGetCrazyExpressionsNonTyped()
        {
            var expr = Expr(x => 2 + 2.0f).ToSettable();
            expr.CanRead.Should().Be.True();
            expr.CanWrite.Should().Be.False();
            expr.Type.Should().Be<object>();
            expr.Name.Should().Be("Convert(4)");


            var obj = new Sample();
            expr.Executing(x => x.Set(obj, 42)).Throws<NotSupportedException>();

            expr.Get(obj).Should().Be(4.0f);
        }


        [Test]
        public void ShouldBeSerializable()
        {
            var expr = ExprTyped(x => x.TestProp).ToSettable();

            var expr2 = SimpleSerializer.Binary().RoundTrip(expr);
            expr2.Expression.ToString().Should().Be(expr.Expression.ToString());
        }


        private Expression<Func<Sample, T>> ExprTyped<T>(Expression<Func<Sample, T>> expr)
        {
            return expr;
        }

        private Expression<Func<Sample, object>> Expr(Expression<Func<Sample, object>> expr)
        {
            return expr;
        }

        class Sample
        {
            public int this[int index] { get { return index; } }
            public int TestProp { get; set; }
            public Inner TestInner { get; set; }
            public Inner TestInnerField = null;
            public int TestField = 0;
        }

        class Inner
        {
            public int TestInt { get; set; }

            public int Whatever()
            {
                return 42;
            }
        }
    }
}
