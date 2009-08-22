using System.Linq;
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
        public void TestFindFirstCustomer()
        {
            var c = Customer.Do.Find(x => x.CompanyName.StartsWith("B"));
            Assert.AreEqual("BERGS", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerWithOUTAndExpression()
        {
            var f = Customer.Expr(x => (true && x.CompanyName.StartsWith("a")) && x.Country == "Mexico");

            var c = Customer.Do.Find(f, o => o.Desc(x => x.Id));

            Assert.AreEqual("ANTON", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerWithAndExpression()
        {
            var f = Customer.Expr(true);

            f = Customer.And(f, x => x.CompanyName.StartsWith("a"));
            f = Customer.And(f, x => x.Country == "Mexico");

            var c = Customer.Do.Find(f, o => o.Desc(x => x.Id));

            Assert.AreEqual("ANTON", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerWithOUTOrExpression()
        {
            var f = Customer.Expr(x => (false || x.CompanyName.StartsWith("a")) || x.Country == "Mexico");

            var c = Customer.Do.Find(f, o => o.Desc(x => x.Id));

            Assert.AreEqual("TORTU", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerWithOrExpression()
        {
            var f = Customer.Expr(false);

            f = f.Or(x => x.CompanyName.StartsWith("a"));
            f = f.Or(x => x.Country == "Mexico");

            var c = Customer.Do.Find(f, o => o.Desc(x => x.Id));

            Assert.AreEqual("TORTU", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerReverseIdOrder()
        {
            var c = Customer.Do.Find(x => x.CompanyName.StartsWith("B"), o => o.Desc(x => x.Id));
            Assert.AreEqual("BSBEV", c.Id);
        }

        [Test]
        public void TestFindFirstCustomerTwoOrder()
        {
            var c = Customer.Do.Find(x => x.City == "Sao Paulo",
                o => o.Asc(x => x.ContactTitle).Desc(x => x.ContactName));

            Assert.AreEqual("QUEEN", c.Id);
        }

        [Test]
        public void TestListProductsByCategoryName()
        {
            var l = Product.Do.List(x => x.Category.Name == "Meat/Poultry");
            
            Assert.AreEqual(6, l.Count);
            Assert.IsTrue(l.All(x => x.Category.Id == 6));
        }

        [Test]
        public void TestListTop10ProductsAll()
        {
            var list = Product.Do.List(10);
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual(77, list.TotalCount);
        }


        [Test]
        public void TestFindByTrueFielter()
        {
            var c = Customer.Do.Find(x => true);
            Assert.AreEqual("ALFKI", c.Id);
        }
    }
}
