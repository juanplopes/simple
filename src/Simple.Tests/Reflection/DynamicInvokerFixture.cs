using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;
using SharpTestsEx;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class DynamicInvokerFixture
    {
        [Test]
        public void InvokeUsingSingleIntParameter()
        {
            object sample = new Sample<int>();

            var invoker = new DynamicInvoker(sample.GetType());

            invoker.Invoke(sample, "ReturnHashCode", 35)
                .Should().Be(35.GetHashCode());
        }

        [Test]
        public void InvokeUsingTwoParametersAndOverloadedMethods()
        {
            var expected = 35.GetHashCode() + "asd".GetHashCode();
            object sample = new Sample<int>();

            var invoker = new DynamicInvoker(sample.GetType());

            invoker.Invoke(sample, "ReturnHashCode", 35, "asd")
                .Should().Be(expected);
        }

        [Test]
        public void CannotInvokeGenericMethod()
        {
            var expected = 42.GetHashCode() + "qwe".GetHashCode();
            object sample = new Sample<int>();

            var invoker = new DynamicInvoker(sample.GetType());

            invoker.Executing(x => x.Invoke(sample, "ReturnHashCode", "qwe", 42)).Throws<MissingMethodException>();
        }

        [Test]
        public void InvokeUsingTwoParametersAndOverloadedMethodsPassingNullToSecondParameter()
        {
            var expected = 35.GetHashCode();
            object sample = new Sample<int>();

            var invoker = new DynamicInvoker(sample.GetType());

            invoker.Invoke(sample, "ReturnHashCode", 35, null)
                .Should().Be(expected);
        }

        [Test]
        public void InvokeVoidMethodRespectsCounterEffects()
        {
            object sample = new Sample<string>();

            var invoker = new DynamicInvoker(sample.GetType());

            invoker.Invoke(sample, "SetFlag", "true")
                .Should().Be(null);

            sample.Should().Be.OfType<Sample<string>>()
                .And.Value.Flag.Should().Be("true");
        }

        [Test]
        public void InvokeMethodWithOutParameterAndReferenceType()
        {
            object sample = new Sample<string>();
            object[] args = new[] { null, "asd" };

            var invoker = new DynamicInvoker(sample.GetType());

            invoker.Invoke(sample, "SetValue", args).Should().Be(null);
            args.Should().Have.SameSequenceAs("asd", "asd");
        }

        class Sample<T>
        {
            public T Flag = default(T);

            public int ReturnHashCode(T i) { return i.GetHashCode(); }
            public int ReturnHashCode(T i, string s) { return i.GetHashCode() + EqualityComparer<string>.Default.GetHashCode(s); }
            public int ReturnHashCode<Q>(Q value1, T value2) { return value1.GetHashCode() + value2.GetHashCode(); }

            public void SetFlag(T value) { Flag = value; }

            public void SetValue(out T value, T test) { value = test; }
        }
    }
}
