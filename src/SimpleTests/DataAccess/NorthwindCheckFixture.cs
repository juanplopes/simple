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
            Assert.AreEqual(8, Category.Count());
            Category.ListAll(1);
        }

        [Test]
        public void CheckCustomer()
        {
            Assert.AreEqual(91, Customer.Count());
            Customer.ListAll(1);
        }

        [Test]
        public void CheckProducts()
        {
            Assert.AreEqual(77, Product.Count());
            Product.ListAll(1);

            Product p = Product.Load(2);
            Assert.IsNotNull(p.Category); Assert.AreEqual("Beverages", p.Category.Name);
            Assert.IsNotNull(p.Supplier); Assert.AreEqual("Exotic Liquids", p.Supplier.CompanyName);
        }

        [Test]
        public void CheckSuppliers()
        {
            Assert.AreEqual(29, Supplier.Count());
            Supplier.ListAll(1);
        }

        [Test]
        public void CheckRegions()
        {
            Assert.AreEqual(4, Region.Count());
            Region.ListAll(1);
        }

        [Test]
        public void CheckTerritories()
        {
            Assert.AreEqual(53, Territory.Count());
            Territory.ListAll(1);
            var t = Territory.Load("03049");
            Assert.AreEqual(3, t.Region.Id);
        }

        [Test]
        public void CheckEmployees()
        {
            Assert.AreEqual(9, Employee.Count());
            Employee.ListAll(1);
        }

        [Test]
        public void CheckEmployeeTerritories()
        {
            Assert.AreEqual(49, EmployeeTerritory.Count());
            EmployeeTerritory.Find(x => x.Territory.Id == "85014" && x.Employee.Id == 6);
        }

        [Test]
        public void CheckEmployeeTerritoriesEquality()
        {
            var t = EmployeeTerritory.ListAll(1)[0];
            var t2 = t.Clone();

            Assert.IsFalse(object.ReferenceEquals(t, t2));
            Assert.IsTrue(t.Equals(t2));
        }
    }
}
