using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Common;

namespace Simple.Tests.Config
{
    public class AggregateFactoryFixture
    {
        [Test]
        public void SameKeyReturnsSameFactory()
        {
            var fac1 = TestFactory.Do["asd"];
            var fac2 = TestFactory.Do["asd"];
            fac2.Should().Be(fac1);
        }

        [Test]
        public void DiffKeyReturnsDifferentFactory()
        {
            var fac1 = TestFactory.Do["asd"];
            var fac2 = TestFactory.Do["asd2"];
            Assert.AreNotEqual(fac1, fac2);
        }

        [Test]
        public void DefaultKeyReturnsNullFactory()
        {
            var fac1 = TestFactory.Do;
            var fac2 = TestFactory.Do[null];
            var fac3 = TestFactory.Do[SourceManager.Do.DefaultKey];
            fac2.Should().Be(fac1);
            fac3.Should().Be(fac2);
        }

        [Test]
        public void DefaultKeyInsideContextReturnsContextKeyFactory()
        {
            var fac1 = TestFactory.Do;
            var fac2 = TestFactory.Do[null];
            using (TestFactory.KeyContext("qwe"))
            {
                var fac3 = TestFactory.Do;
                var fac4 = TestFactory.Do["qwe"];
                fac2.Should().Be(fac1);
                fac4.Should().Be(fac3);
                Assert.AreNotEqual(fac1, fac3);
            }
            var fac5 = TestFactory.Do;
            fac5.Should().Be(fac2);
        }

        [Test]
        public void DefaultKeyInsideContextReturnsContextKeyFactoryEvenWithThreadDataModification()
        {
            using (TestFactory.KeyContext("qwe"))
            {
                new ThreadData().Set("defaultKey", "asd");
                var fac1 = TestFactory.Do;
                var fac2 = TestFactory.Do["qwe"];
                fac2.Should().Be(fac1);
            }
        }

        public class TestFactory : AggregateFactory<TestFactory>
        {
            public string Value { get; set; }
        }
    }
}
