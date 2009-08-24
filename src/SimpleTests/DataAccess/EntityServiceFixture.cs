using System.Linq;
using NUnit.Framework;
using Simple.Tests.SampleServer;
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
            AssertQuery(x => x.Where(o => o.Id == "BERGS"), Customer.Do.Load("BERGS"));
            AssertQuery(x => x.Where(o => o.Id == "ANTON"), Customer.Do.Load("ANTON"));
        }

        [Test]
        public void TestFindFirstByStartsWithFilter()
        {
            var f = Customer.Expr(x => x.CompanyName.StartsWith("B"));
            AssertQuery(x => x.Where(f).Take(1), Customer.Do.Find(f));
        }

        [Test]
        public void TestFindFirstCustomerWithAndExpression()
        {
            var f = Customer.Expr(true);
            f = Customer.And(f, x => x.CompanyName.StartsWith("a"));
            f = Customer.And(f, x => x.Country == "Mexico");

            var f2 = Customer.Expr(x => (true && x.CompanyName.StartsWith("A")) && x.Country == "Mexico");


            AssertQuery(x => x.Where(f2).OrderByDescending(o => o.Id).Take(1),
                Customer.Do.Find(f, o => o.Desc(x => x.Id)));
        }


        [Test]
        public void TestFindFirstCustomerWithOrExpression()
        {
            var f = Customer.Expr(false);
            f = Customer.Or(f, x => x.CompanyName.StartsWith("a"));
            f = Customer.Or(f, x => x.Country == "Mexico");

            var f2 = Customer.Expr(x => (false || x.CompanyName.StartsWith("A")) || x.Country == "Mexico");


            AssertQuery(x => x.Where(f2).OrderByDescending(o => o.Id).Take(1),
                Customer.Do.Find(f, o => o.Desc(x => x.Id)));
        }

        [Test]
        public void TestFindFirstCustomerReverseIdOrder()
        {
            var f = Customer.Expr(x => x.CompanyName.StartsWith("B"));
            AssertQuery(x => x.Where(f).OrderByDescending(o => o.Id).Take(1),
                Customer.Do.Find(f, o => o.Desc(x => x.Id)));
        }

        [Test]
        public void TestFindFirstCustomerTwoOrder()
        {
            var f = Customer.Expr(x => x.City == "Sao Paulo");
            AssertQuery(x => x.Where(f).OrderBy(o => o.ContactTitle).ThenByDescending(o => o.ContactName).Take(1),
                Customer.Do.Find(f, o => o.Asc(x => x.ContactTitle).Desc(x => x.ContactName)));
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
        public void TestFindLastProduct()
        {
            var p = Product.Do.Find(x => true, o => o.Desc(x => x.Id));
            Assert.AreEqual(77, p.Id);
        }

        [Test]
        public void TestFindByTrueFilter()
        {
            AssertQuery(x => x.Take(1), Customer.Do.Find(x => true));
        }

        [Test]
        public void TestListAll()
        {
            AssertQuery(x => x, Customer.Do.List().ToArray());
        }

        [Test]
        public void TestListAllTop10()
        {
            AssertQuery(x => x, 0, 10, Customer.Do.List(10));
        }

        [Test]
        public void TestListAllSkip20Take10()
        {
            AssertQuery(x => x, 20, 10, Customer.Do.List(20, 10));
        }

        [Test]
        public void TestListByFilter()
        {
            var f = Customer.Expr(x => x.Region != null);
            AssertQuery(x => x.Where(f), Customer.Do.List(f).ToArray());
        }

        [Test]
        public void TestListByFilterTop10()
        {
            var f = Customer.Expr(x => x.Region != null);
            AssertQuery(x => x.Where(f), 0, 10, Customer.Do.List(f, 10));
        }

        [Test]
        public void TestListByFilterSkip20Take10()
        {
            var f = Customer.Expr(x => x.Region != null);
            AssertQuery(x => x.Where(f), 20, 10, Customer.Do.List(f, 20, 10));
        }

        [Test]
        public void TestListWithOrder()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax),
                Customer.Do.List(o => o.Desc(x => x.Id).Asc(x => x.Fax)).ToArray());
        }

        [Test]
        public void TestListWithOrderTop10()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax), 0, 10,
                Customer.Do.List(o => o.Desc(x => x.Id).Asc(x => x.Fax), 10));
        }

        [Test]
        public void TestListWithOrderSkip20Take10()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax), 20, 10,
                Customer.Do.List(o => o.Desc(x => x.Id).Asc(x => x.Fax), 20, 10));
        }

        [Test]
        public void TestListWithOrderAndFilter()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f),
                Customer.Do.List(f, o => o.Desc(x => x.Id).Asc(x => x.Fax)).ToArray());
        }

        [Test]
        public void TestListWithOrderAndFilterTop10()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f), 0, 10,
                Customer.Do.List(f, o => o.Desc(x => x.Id).Asc(x => x.Fax), 10));
        }

        [Test]
        public void TestListWithOrderAndFilterSkip20Take10()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f), 20, 10,
                Customer.Do.List(f, o => o.Desc(x => x.Id).Asc(x => x.Fax), 20, 10));
        }

        [Test]
        public void TestSmartUpdate()
        {
            var c = Customer.Do.Load("OLDWO");
            c.CompanyName = "WHATEVER";
            c.Update();

            var c3 = Customer.Do.Load("OLDWO");

            Assert.AreEqual(c.CompanyName, c.CompanyName);
        }

        [Test]
        public void TestServiceUpdate()
        {
            var c = Customer.Do.Load("OLDWO");
            c.CompanyName = "WHATEVER2";

            Customer.Service.Update(c);

            var c3 = Customer.Do.Load("OLDWO");

            Assert.AreEqual(c.CompanyName, c.CompanyName);
        }

        [Test]
        public void TestSaveNew()
        {
            var c = Customer.Do.Load("OLDWO").Clone();
            c.CompanyName = "WHATEVER3";
            c.Id = "AAAAA";
            c.Save();

            var c3 = Customer.Do.Load("AAAAA");

            Assert.AreEqual(c.CompanyName, c.CompanyName);
        }

        [Test]
        public void TestDeleteOne()
        {
            Customer.Do.Load("OLDWO").Delete();
            Assert.Throws<ObjectNotFoundException>(() => Customer.Do.Load("OLDWO"));
        }

        [Test]
        public void TestDeleteOneByFilter()
        {
            int count = Customer.Do.Delete(x => x.ContactName == "Yvonne Moncada");
            Assert.AreEqual(1, count);
            Assert.Throws<ObjectNotFoundException>(() => Customer.Do.Load("OCEAN"));
        }

        [Test]
        public void TestDeleteOneById()
        {
            Customer.Do.Delete("AROUT");
            Assert.Throws<ObjectNotFoundException>(() => Customer.Do.Load("AROUT"));
        }

        [Test]
        public void TestCountByFilter()
        {
            int c = Customer.Do.Count(x => x.Id == "AROUT");
            Assert.AreEqual(1, c);
        }

        [Test]
        public void TestRefreshEntity()
        {
            var c = Customer.Do.Load("BLAUS");
            Assert.AreEqual("Hanna Moos", c.ContactName);

            c.ContactName = "WHATEVER";

            c = c.Refresh();
            Assert.AreEqual("Hanna Moos", c.ContactName);
        }

        [Test]
        public void TestMergeEntity()
        {
            var c = Customer.Do.Load("BLAUS");

            var c2 = c.Clone();
            c2.CompanyName = "WHATEVER";
            Assert.Throws<NonUniqueObjectException>(() => c2.SaveOrUpdate());

            c2 = c2.Merge();
            c2.SaveOrUpdate();

            var c3 = Customer.Do.Load("BLAUS");
            Assert.AreEqual("WHATEVER", c3.CompanyName); 

        }

        [Test]
        public void TestCreateIdBySaveProduct()
        {
            var p = Product.Do.Load(1).Clone();
            p.Id = 0;
            p = p.Save();
            Assert.AreNotEqual(0, p.Id);
        }

        [Test]
        public void TestCreateIdBySaveOrUpdateProduct()
        {
            var p = Product.Do.Load(1).Clone();
            p.Id = 0;
            p = p.SaveOrUpdate();
            Assert.AreNotEqual(0, p.Id);
        }
    }
}
