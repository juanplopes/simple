using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.SampleServer;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class EntityServiceFixture : BaseDataFixture
    {
        [Test]
        public void TestLoadProductId1And2()
        {
            Assert.AreEqual("Chai", Product.Do.Load(1).Name);
            Assert.AreEqual("Chang", Product.Do.Load(2).Name);
        }

        [Test]
        public void TestFindFirstCustomerFixture()
        {
            var c = Customer.Do.Find(x => x.CompanyName.StartsWith("B"));
            Assert.AreEqual("BERGS", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerReverseIdOrderFixture()
        {
            var c = Customer.Do.Find(x => x.CompanyName.StartsWith("B"), o => o.Desc(x => x.Id));
            Assert.AreEqual("BSBEV", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerTwoOrderFixture()
        {
            var c = Customer.Do.Find(x => x.City=="Sao Paulo", 
                o => o.Asc(x => x.ContactTitle).Desc(x=>x.ContactName));
            
            Assert.AreEqual("QUEEN", c.Id);
        }


    }
}
