using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Common;

namespace Simple.Tests.Common
{
    public class SafeNullableFixture
    {
        [Test]
        public void CanGetValuesWithNoNullableValue()
        {
            Assert.AreEqual(2, SafeNullable.Get(() => 2));
        }

        [Test]
        public void CanGetValuesWithNullableValue()
        {
            object a = null;
            Assert.AreEqual(null, SafeNullable.Get(() => a.ToString()));
        }

        [Test]
        public void CanGetIntValueWithNullableValue()
        {
            object a = null;
            Assert.AreEqual(0, SafeNullable.Get(() => int.Parse(a.ToString())));
        }

        [Test]
        public void CanGetIntValueWithNullableValueAndDefValue()
        {
            object a = null;
            Assert.AreEqual(42, SafeNullable.Get(() => int.Parse(a.ToString()), 42));
        }

        [Test]
        public void CanGetStringValueWithNullableValueAndDefValue()
        {
            object a = null;
            Assert.AreEqual("42", SafeNullable.Get(() => a.ToString(), "42"));
        }
    }
}
