using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Entities;
using Simple.Tests.Resources;
using NHibernate.Linq;
namespace Simple.Tests.Data
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

            result.TotalCount.Should().Be(count);
            var comp = q.Skip(skip).Take(take).ToList();

            result.TotalPages(take).Should().Be((int)Math.Ceiling((decimal)q.Count() / take));
            result.Count.Should().Be(comp.Count);
            
            comp.Count.Should().Be.GreaterThan(0);

            for (int i = 0; i < comp.Count; i++)
            {
                result[i].Id.Should().Be(comp[i].Id);
            }
        }

        public void AssertQuery(Func<IQueryable<Customer>, IQueryable<Customer>> func, params Customer[] result)
        {
            var comp = func(_customers.AsQueryable()).ToList();

            result.Length.Should().Be(comp.Count);
            comp.Count.Should().Be.GreaterThan(0);

            for (int i = 0; i < comp.Count; i++)
            {
                result[i].Id.Should().Be(comp[i].Id);
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
            new Customer() { Id = "BERGS" }.Reload().Phone.Should().Not.Be.Null();
            new Customer() { Id = "ANTON" }.Reload().Phone.Should().Not.Be.Null();
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
                Customer.Find(f, q => q.OrderByDesc(x => x.Id)));
        }


        [Test]
        public void TestFindFirstCustomerWithOrExpression()
        {
            var f = Customer.Expr(false);
            f = Customer.Or(f, x => x.CompanyName.StartsWith("a"));
            f = Customer.Or(f, x => x.Country == "Mexico");

            var f2 = Customer.Expr(x => (false || x.CompanyName.StartsWith("A")) || x.Country == "Mexico");


            AssertQuery(x => x.Where(f2).OrderByDescending(o => o.Id).Take(1),
                Customer.Find(f, q => q.OrderByDesc(x => x.Id)));
        }

        [Test]
        public void TestFindFirstCustomerReverseIdOrder()
        {
            var f = Customer.Expr(x => x.CompanyName.StartsWith("B"));
            AssertQuery(x => x.Where(f).OrderByDescending(o => o.Id).Take(1),
                Customer.Find(f, q => q.OrderByDesc(x => x.Id)));

            //Session.Query<Category>().FetchMany(x => x.Products).ThenFetch(x => x.Category);
        }

        [Test]
        public void TestFindFirstCustomerTwoOrder()
        {
            var f = Customer.Expr(x => x.City == "Sao Paulo");
            AssertQuery(x => x.Where(f).OrderBy(o => o.ContactTitle).ThenByDescending(o => o.ContactName).Take(1),
                Customer.Find(f, q => q.OrderBy(x => x.ContactTitle).ThenByDesc(x => x.ContactName)));
        }

        [Test]
        public void TestListProductsByCategoryName()
        {
            var l = Product.List(x => x.Category.Name == "Meat/Poultry");

            l.Count.Should().Be(6);
            l.All(x => x.Category.Id == 6).Should().Be.True();
        }

        public void TestListProductsByCategoryId()
        {
            var c = Category.Find(x => x.Name == "Meat/Poutrly");
            c.Should().Not.Be.Null();

            var p = Product.List(x => x.Category.Id == c.Id);
            p.Count.Should().Be(6);
            p.All(x => x.Category.Id == 6).Should().Be.True();
        }

        [Test]
        public void TestListTop10ProductsAll()
        {
            var list = Product.ListAll(10);
            list.Count.Should().Be(10);
            list.TotalCount.Should().Be(77);
        }

        [Test]
        public void TestFindLastProduct()
        {
            var p = Product.Find(x => true, q => q.OrderByDesc(x => x.Id));
            p.Id.Should().Be(77);
        }

        [Test]
        public void TestListProductsOrderingByOtherField()
        {
            var p = Product.ListAll(q => q.OrderByDesc(x => x.Category.Name));
            var p2 = Product.ListAll().OrderByDescending(x => x.Category.Name);
            p2.Should().Have.SameSequenceAs(p);
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
                Customer.ListAll(q => q.OrderByDesc(x => x.Id).ThenBy(x => x.Fax)).ToArray());
        }

        [Test]
        public void TestListWithOrderTop10()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax), 0, 10,
                Customer.ListAll(q => q.OrderByDesc(x => x.Id).ThenBy(x => x.Fax), 10));
        }

        [Test]
        public void TestListWithOrderSkip20Take10()
        {
            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax), 20, 10,
                Customer.ListAll(q => q.OrderByDesc(x => x.Id).ThenBy(x => x.Fax), 20, 10));
        }

        [Test]
        public void TestListWithOrderAndFilter()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f),
                Customer.List(f, q => q.OrderByDesc(x => x.Id).ThenBy(x => x.Fax)).ToArray());
        }

        [Test]
        public void TestListWithOrderAndFilterTop10()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f), 0, 10,
                Customer.List(f, q => q.OrderByDesc(x => x.Id).ThenBy(x => x.Fax), 10));
        }

        [Test]
        public void TestListWithOrderAndFilterSkip20Take10()
        {
            var f = Customer.Expr(x => x.Region != null);

            AssertQuery(x => x.OrderByDescending(m => m.Id).ThenBy(m => m.Fax).Where(f), 20, 10,
                Customer.List(f, q => q.OrderByDesc(x => x.Id).ThenBy(x => x.Fax), 20, 10));
        }

        [Test]
        public void TestListWithLinqSkip20Take10()
        {
            var f = Customer.Expr(x => x.Region != null);

            var list = Customer.Linq(q => q.Where(x => x.Id.StartsWith("O")), q => q.Skip(1).Take(2));

            AssertQuery(q => q.Where(x => x.Id.StartsWith("O")), 1, 2, list);
        }


        [Test]
        public void TestSmartUpdate()
        {
            var c = Customer.Load("OLDWO");
            c.CompanyName = "WHATEVER";
            c.Update();

            var c3 = Customer.Load("OLDWO");

            c.CompanyName.Should().Be(c.CompanyName);
        }

        [Test]
        public void TestServiceUpdate()
        {
            var c = Customer.Load("OLDWO");
            c.CompanyName = "WHATEVER2";

            Customer.Service.Update(c);

            var c3 = Customer.Load("OLDWO");

            c.CompanyName.Should().Be(c.CompanyName);
        }

        [Test]
        public void TestSaveNew()
        {
            var c = Customer.Load("OLDWO").Clone();
            c.CompanyName = "WHATEVER3";
            c.Id = "AAAAA";
            c.Save();

            var c3 = Customer.Load("AAAAA");

            c.CompanyName.Should().Be(c.CompanyName);
        }

        [Test]
        public void TestSaveWithReferenceAndReload()
        {
            var c = new Product() { Name="test", Category = new Category() { Id =Session.Query<Category>().Select(x=>x.Id).First() }}.Save();
            c.Category.Name.Should().Be.Null();
            c = c.Reload();
            c.Category.Name.Should().Not.Be.Null();
        }


        [Test]
        public void TestDeleteOne()
        {
            Customer.Load("OLDWO").Delete();

            "OLDWO".Executing(x => Customer.Load(x).ToString())
                .Throws<ObjectNotFoundException>();
        }


        [Test]
        public void TestDeleteOneByFilter()
        {
            int count = Customer.Delete(x => x.ContactName == "Yvonne Moncada");
            count.Should().Be(1);

            "OCEAN".Executing(x => Customer.Load(x).ToString())
                .Throws<ObjectNotFoundException>();
        }

        [Test]
        public void TestDeleteOneById()
        {
            Customer.Delete("AROUT");
            "AROUT".Executing(x => Customer.Load(x).ToString())
                .Throws<ObjectNotFoundException>();
        }

        [Test]
        public void TestCountByFilter()
        {
            int c = Customer.Count(x => x.Id == "AROUT");
            c.Should().Be(1);
        }

        [Test]
        public void TestRefreshEntity()
        {
            var c = Customer.Load("BLAUS");
            c.ContactName.Should().Be("Hanna Moos");

            c.ContactName = "WHATEVER";

            c = c.Refresh();
            c.ContactName.Should().Be("Hanna Moos");
        }

        [Test]
        public void TestMergeEntity()
        {
            var c = Customer.Load("BLAUS");

            var c2 = c.Clone();
            c2.CompanyName = "WHATEVER";
            c2.Executing(x => x.SaveOrUpdate()).Throws<NonUniqueObjectException>();

            c2 = c2.Merge();
            c2.SaveOrUpdate();

            var c3 = Customer.Load("BLAUS");
            c3.CompanyName.Should().Be("WHATEVER");

        }

        [Test]
        public void TestCreateIdBySaveProduct()
        {
            var p = Product.Load(1).Clone();
            p.Id = 0;
            p = p.Save();
            p.Id.Should().Not.Be(0);
        }

        [Test]
        public void TestCreateIdBySaveOrUpdateProduct()
        {
            var p = Product.Load(1).Clone();
            p.Id = 0;
            p = p.SaveOrUpdate();
            p.Id.Should().Not.Be(0);
        }



    }
}
