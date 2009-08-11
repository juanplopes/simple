using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.SampleServer;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class EntityServiceLoadFixture
    {
        [Test]
        public void TestLoadProductId1And2()
        {
            Assert.AreEqual("Chai", Product.Do.Load(1).Name);
            Assert.AreEqual("Chang", Product.Do.Load(2).Name);
        }
    }
}
