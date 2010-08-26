using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Common;
using SharpTestsEx;

namespace Simple.Tests.Common
{
    [TestFixture]
    public class SafeValueFixture
    {
        [Test]
        public void CanUseObjectMethodsOnItWhenItsFound()
        {
            var safe = new SafeValue<int>(42, true);
            
            safe.Equals(42).Should().Be(true);
            safe.Equals("42").Should().Be(false);
            safe.GetHashCode().Should().Be.EqualTo(42.GetHashCode());
        }

        [Test]
        public void CanUseObjectMethodsOnItWhenItsNotFound()
        {
            var safe = new SafeValue<int>(42, false);

            safe.Equals(42).Should().Be(true);
            safe.Equals("42").Should().Be(false);
            safe.GetHashCode().Should().Be.EqualTo(42.GetHashCode());
        }

        [Test]
        public void CanUseBasicStaticOperatorsOnIt()
        {
            var safe = new SafeValue<int>(42, false);

            (safe == 42).Should().Be.True();
            (safe == 43).Should().Be.False();

            (safe != 42).Should().Be.False();
            (safe != 43).Should().Be.True();

            (safe > 41).Should().Be.True();
            (safe < 43).Should().Be.True();

        }

        [Test]
        public void WhenItsNotFoundObjectValueShouldReturnNull()
        {
            var safe = new SafeValue<int>(42, false);

            safe.Value.Should().Be(42);
            safe.ObjectValue.Should().Be.Null();
        }
    }
}
