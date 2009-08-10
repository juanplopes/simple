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
            Assert.AreEqual(8, Category.CountAll());
            Category.PaginateAll(0, 1);
        }

        [Test]
        public void CheckCustomer()
        {
            Assert.AreEqual(91, Customer.CountAll());
            Customer.PaginateAll(0, 1);
        }

        [Test]
        public void CheckProducts()
        {
            Assert.AreEqual(77, Product.CountAll());
            Product.PaginateAll(0, 1);

            Product p = Product.Load(2);
            Assert.IsNotNull(p.Category); Assert.AreEqual("Beverages", p.Category.Name);
            Assert.IsNotNull(p.Supplier); Assert.AreEqual("Exotic Liquids", p.Supplier.CompanyName);
        }

        [Test]
        public void CheckSuppliers()
        {
            Assert.AreEqual(29, Supplier.CountAll());
            Supplier.PaginateAll(0, 1);
        }

    }
}
