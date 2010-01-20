using System.Linq;
using NUnit.Framework;
using Simple.Tests.Resources;
using Simple.Expressions;
using System.Collections.Generic;
using System;
using Simple.DataAccess;
using NHibernate;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class EntityServiceFixture : BaseDataFixture
    {
        IList<Customer> _customers = null;
        [TestFixtureSetUp]
        public void FixtureSetup2()
        {
            using (MySimply.EnterContext())
                _customers = MySimply.GetSession().CreateCriteria<Customer>().List<Customer>();
        }

        public void AssertQuery(Func<IQueryable<Customer>, IQueryable<Customer>> func, int skip, int take, IPage<Customer> result)
        {
            var q = func(_customers.AsQueryable());

            int count = q.Count();

            Assert.AreEqual(count, result.TotalCount);
            var comp = q.Skip(skip).Take(take).ToList();

            Assert.AreEqual((int)Math.Ceiling((decimal)q.Count() / take), result.TotalPages(take));
            Assert.AreEqual(comp.Count, result.Count);
            Assert.Greater(comp.Count, 0);

            for (int i = 0; i < comp.Count; i++)
            {
                Assert.AreEqual(comp[i].Id, result[i].Id);
            }
        }

        public void AssertQuery(Func<IQueryable<Customer>, IQueryable<Customer>> func, params Customer[] result)
        {
            var comp = func(_customers.AsQueryable()).ToList();

            Assert.AreEqual(comp.Count, result.Length);
            Assert.Greater(comp.Count, 0);

            for (int i = 0; i < comp.Count; i++)
            {
                Assert.AreEqual(comp[i].Id, result[i].Id);
            }
        }

        [Test]
        public void TestLoadById()
        {
            AssertQuery(x => x.Where(o => o.Id == "BERGS"), Customer.Load("BERGS"));
            AssertQuery(x => x.Where(o => o.Id == "ANTON"), Customer.Load("ANTON"));
        }

        [Test]
        public void TestReloadById()
        {
            Assert.IsNotNull(new Customer() { Id = "BERGS" }.Reload().Phone);
            Assert.IsNotNull(new Customer() { Id = "ANTON" }.Reload().Phone);
        }

        [Test]
        public void TestFindFirstByInsideId()
        {
            var c = new Customer() { Id = "BERGS" };
            var f = Customer.Expr(x => x.Id == c.Id);
            AssertQuery(x => x.Where(f).Take(1), Customer.Find(f));
        }

        [Test]
        public void TestFindFirstByStartsWithFilter()
        {
            var f = Customer.Expr(x => x.CompanyName.StartsWith("B"));
            AssertQuery(x => x.Where(f).Take(1), Customer.Find(f));
        }

        [Test]
        public void TestFindFirstCustomerWithAndExpression()
        {
            var f = Customer.Expr(true);
            f = Customer.And(f, x => x.CompanyName.StartsWith("a"));
            f = Customer.And(f, x => x.Country == "Mexico");

            var f2 = Customer.Expr(x => (true && x.CompanyName.StartsWith("A")) && x.Country == "Mexico");


            AssertQuery(x => x.Where(f2).OrderByDescending(o => o.Id).Take(1),
                Customer.Find(f, o => o.Desc(x => x.Id)));
        }


        [Test]
        public void TestFindFirstCustomerWithOrExpression()
        {
            var f = Customer.Expr(false);
            f = Customer.Or(f, x => x.CompanyName.StartsWith("a"));
            f = Customer.Or(f, x => x.Country == "Mexico");

            var f2 = Customer.Expr(x => (false || x.CompanyName.StartsWith("A")) || x.Country == "Mexico");


            AssertQuery(x => x.Where(f2).OrderByDescending(o => o.Id).Take(1),
                Customer.Find(f, o => o.Desc(x => x.Id)));
        }

        [Test]
        public void TestFindFirstCustomerReverseIdOrder()
        {
            var f = Customer.Expr(x => x.CompanyName.StartsWith("B"));
            AssertQuery(x => x.Where(f).OrderByDescending(o => o.Id).Take(1),
                Customer.Find(f, o => o.Desc(x => x.Id)));
        }

        [Test]
        public void TestFindFirstCustomerTwoOrder()
        {
            var f = Customer.Expr(x => x.City == "Sao Paulo");
            AssertQuery(x => x.Where(f).OrderBy(o => o.ContactTitle).ThenByDescending(o => o.ContactName).Take(1),
                Customer.Find(f, o => o.Asc(x => x.ContactTitle).Desc(x => x.ContactName)));
        }

        [Test]
        public void TestListProductsByCategoryName()
        {
            var l = Product.List(x => x.Category.Name == "Meat/Poultry");

            Assert.AreEqual(6, l.Count);
            Assert.IsTrue(l.All(x => x.Category.Id == 6));
        }

        public void TestListProductsByCategoryId()
        {
            var c = Category.Find(x=>x.Name == "Meat/Poutrly");
            Assert.IsNotNull(c);

            var p = Product.List(x=>x.Category.Id == c.Id);
            Assert.AreEqual(6, p.Count);
            Assert.IsTrue(p.All(x=>x.Category.Id == 6));
        }

        [Test]
        public void TestListTop10ProductsAll()
        {
            var list = Product.ListAll(10);
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual(77, list.TotalCount);
        }

        [Test]
        public void TestFindLastProduct()
        {
            var p = Product.Find(x => true, o => o.Desc(x => x.Id));
            Assert.AreEqual(77, p.Id);
        }

        [Test]
        public void TestListProductsOrderingByOtherField()
        {
            var p = Product.ListAll(o=>o.Desc(x=>x.Category.Name));
            var p2 = Product.ListAll().OrderByDescending(x => x.Category.Name);
            Assert.IsTrue(p.SequenceEqual(p2));
        }

        [Test]
        public void TestFindByTrueFilter()
        {
            AssertQuery(x => x.Take(1), Customer.Find(x => true));
        }

        [Test]
        public void TestListAll()
        {
            AssertQuery(x => x, Customer.ListAll().ToArray());
        }

        [Test]
        public void TestListAllTop10()
        {
            AssertQuery(x => x, 0, 10, Customer.ListAll(10));
        }

        [Test]
        public void TestListAllSkip20Take10()
        {
            AssertQuery(x => x, 20, 10, Customer.ListAll(20, 10));
        }

        [Test]
        public void TestListByFilter()
        {
            var f = Customer.Expr(x => x.Region != null);
            AssertQuery(x => x.Where(f), Customer.List(f).ToArray());
        }

        [Test]
        public void TestListByFilterTop10()
        {
            var f = Customer.Expr(x => x.Region != null);
            AssertQuery(x => x.Where(f), 0, 10, Customer.List(f, 10));
        }

        [Test]
        public void TestListByFilterSkip20Take10()
        {
            var f = Customer.Expr(x => x.Region != null);
            AssertQuery(x => x.Where(f), 20, 10, Customer.List(f, 20, 10));
        }

        [Test]
        public void TestListWithOrder()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax),
                Customer.ListAll(o => o.Desc(x => x.Id).Asc(x => x.Fax)).ToArray());
        }

        [Test]
        public void TestListWithOrderTop10()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax), 0, 10,
                Customer.ListAll(o => o.Desc(x => x.Id).Asc(x => x.Fax), 10));
        }

        [Test]
        public void TestListWithOrderSkip20Take10()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax), 20, 10,
                Customer.ListAll(o => o.Desc(x => x.Id).Asc(x => x.Fax), 20, 10));
        }

        [Test]
        public void TestListWithOrderAndFilter()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f),
                Customer.List(f, o => o.Desc(x => x.Id).Asc(x => x.Fax)).ToArray());
        }

        [Test]
        public void TestListWithOrderAndFilterTop10()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f), 0, 10,
                Customer.List(f, o => o.Desc(x => x.Id).Asc(x => x.Fax), 10));
        }

        [Test]
        public void TestListWithOrderAndFilterSkip20Take10()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f), 20, 10,
                Customer.List(f, o => o.Desc(x => x.Id).Asc(x => x.Fax), 20, 10));
        }

        [Test]
        public void TestSmartUpdate()
        {
            var c = Customer.Load("OLDWO");
            c.CompanyName = "WHATEVER";
            c.Update();

            var c3 = Customer.Load("OLDWO");

            Assert.AreEqual(c.CompanyName, c.CompanyName);
        }

        [Test]
        public void TestServiceUpdate()
        {
            var c = Customer.Load("OLDWO");
            c.CompanyName = "WHATEVER2";

            Customer.Service.Update(c);

            var c3 = Customer.Load("OLDWO");

            Assert.AreEqual(c.CompanyName, c.CompanyName);
        }

        [Test]
        public void TestSaveNew()
        {
            var c = Customer.Load("OLDWO").Clone();
            c.CompanyName = "WHATEVER3";
            c.Id = "AAAAA";
            c.Save();

            var c3 = Customer.Load("AAAAA");

            Assert.AreEqual(c.CompanyName, c.CompanyName);
        }

        [Test]
        public void TestDeleteOne()
        {
            Customer.Load("OLDWO").Delete();
            Assert.Throws<ObjectNotFoundException>(() => Customer.Load("OLDWO"));
        }

       
        [Test]
        public void TestDeleteOneByFilter()
        {
            int count = Customer.Delete(x => x.ContactName == "Yvonne Moncada");
            Assert.AreEqual(1, count);
            Assert.Throws<ObjectNotFoundException>(() => Customer.Load("OCEAN"));
        }

        [Test]
        public void TestDeleteOneById()
        {
            Customer.Delete("AROUT");
            Assert.Throws<ObjectNotFoundException>(() => Customer.Load("AROUT"));
        }

        [Test]
        public void TestCountByFilter()
        {
            int c = Customer.Count(x => x.Id == "AROUT");
            Assert.AreEqual(1, c);
        }

        [Test]
        public void TestRefreshEntity()
        {
            var c = Customer.Load("BLAUS");
            Assert.AreEqual("Hanna Moos", c.ContactName);

            c.ContactName = "WHATEVER";

            c = c.Refresh();
            Assert.AreEqual("Hanna Moos", c.ContactName);
        }

        [Test]
        public void TestMergeEntity()
        {
            var c = Customer.Load("BLAUS");

            var c2 = c.Clone();
            c2.CompanyName = "WHATEVER";
            Assert.Throws<NonUniqueObjectException>(() => c2.SaveOrUpdate());

            c2 = c2.Merge();
            c2.SaveOrUpdate();

            var c3 = Customer.Load("BLAUS");
            Assert.AreEqual("WHATEVER", c3.CompanyName);

        }

        [Test]
        public void TestCreateIdBySaveProduct()
        {
            var p = Product.Load(1).Clone();
            p.Id = 0;
            p = p.Save();
            Assert.AreNotEqual(0, p.Id);
        }

        [Test]
        public void TestCreateIdBySaveOrUpdateProduct()
        {
            var p = Product.Load(1).Clone();
            p.Id = 0;
            p = p.SaveOrUpdate();
            Assert.AreNotEqual(0, p.Id);
        }



    }
}
