using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.SampleServer;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class NorthwindCheckFixture
    {
        [Test]
        public void CheckCategories()
        {
            Assert.AreEqual(8, Category.Do.Count());
            Category.Do.List(1);
        }

        [Test]
        public void CheckCustomer()
        {
            Assert.AreEqual(91, Customer.Do.Count());
            Customer.Do.List(1);
        }

        [Test]
        public void CheckProducts()
        {
            Assert.AreEqual(77, Product.Do.Count());
            Product.Do.List(1);

            Product p = Product.Do.Load(2);
            Assert.IsNotNull(p.Category); Assert.AreEqual("Beverages", p.Category.Name);
            Assert.IsNotNull(p.Supplier); Assert.AreEqual("Exotic Liquids", p.Supplier.CompanyName);
        }

        [Test]
        public void CheckSuppliers()
        {
            Assert.AreEqual(29, Supplier.Do.Count());
            Supplier.Do.List(1);
        }

        [Test]
        public void CheckRegions()
        {
            Assert.AreEqual(4, Region.Do.Count());
            Region.Do.List(1);
        }

        [Test]
        public void CheckTerritories()
        {
            Assert.AreEqual(53, Territory.Do.Count());
            Territory.Do.List(1);
            var t = Territory.Do.Load("03049");
            Assert.AreEqual(3, t.Region.Id);
        }

        [Test]
        public void CheckEmployees()
        {
            Assert.AreEqual(9, Employee.Do.Count());
            Employee.Do.List(1);
        }

        [Test]
        public void CheckEmployeeTerritories()
        {
            Assert.AreEqual(49, EmployeeTerritory.Do.Count());
            EmployeeTerritory.Do.Find(x => x.Territory.Id == "85014" && x.Employee.Id == 6);
        }

        [Test]
        public void CheckEmployeeTerritoriesEquality()
        {
            var t = EmployeeTerritory.Do.List(1)[0];
            var t2 = t.Clone();

            Assert.IsFalse(object.ReferenceEquals(t, t2));
            Assert.IsTrue(t.Equals(t2));
        }
    }
}
