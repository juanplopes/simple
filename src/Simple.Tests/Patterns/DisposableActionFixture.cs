using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Patterns;
using Simple.Reflection;
using System.Threading;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class DisposableActionFixture
    {
        [Test]
        public void TestNormalPath()
        {
            int x = 20;

            using (new DisposableAction(() => x++))
            {
                Assert.AreEqual(20, x);
            }

            Assert.AreEqual(21, x);
        }
        [Test]
        public void TestNormalPathEnsuring()
        {
            int x = 20;

            using (new DisposableAction(() => x++, true))
            {
                Assert.AreEqual(20, x);
            }

            Assert.AreEqual(21, x);
        }



        [Test]
        public void TestDisposeAsManyTimesAsNeeded()
        {
            int x = 20;

            using (var d = new DisposableAction(() => x++))
            {
                using (d)
                    Assert.AreEqual(20, x);
            }

            Assert.AreEqual(22, x);
        }

        [Test]
        public void TestDisposeOnlyOneTime()
        {
            int x = 20;

            using (var d = new DisposableAction(() => x++, true))
            {
                using(d)
                    Assert.AreEqual(20, x);
            }

            Assert.AreEqual(21, x);
        }

       

    }


}
