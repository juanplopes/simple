using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Common;

namespace Simple.Tests.Common
{
    public class SafeNullableFixture
    {
        [Test]
        public void CanGetValuesWithNoNullableValue()
        {
            SafeNullable.Get(() => 2).Should().Be(2);
        }

        [Test]
        public void CanGetValuesWithNullableValue()
        {
            object a = null;
            SafeNullable.Get(() => a.ToString()).Should().Be(null);
        }

        [Test]
        public void CanGetIntValueWithNullableValue()
        {
            object a = null;
            SafeNullable.Get(() => int.Parse(a.ToString())).Should().Be(0);
        }

        [Test]
        public void CanGetIntValueWithNullableValueAndDefValue()
        {
            object a = null;
            SafeNullable.Get(() => int.Parse(a.ToString()), 42).Should().Be(42);
        }

        [Test]
        public void CanGetStringValueWithNullableValueAndDefValue()
        {
            object a = null;
            SafeNullable.Get(() => a.ToString(), "42").Should().Be("42");
        }
    }
}
