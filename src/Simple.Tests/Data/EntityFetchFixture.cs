using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.Resources;
using NUnit.Framework;
using NHibernate;

namespace Simple.Tests.Data
{
    public class EntityFetchFixture : BaseDataFixture
    {
        protected override bool OpenOwnTx
        {
            get
            {
                return false;
            }
        }

        [Test]
        public void CanLoadOneProductAndItsCategoryWithLazyLoad()
        {
            Product p = null;
            using (MySimply.EnterContext())
            {
                p = Product.Find(x => true);
            }
            Assert.Throws<LazyInitializationException>(() => p.Category.Name.ToString());
        }

        [Test]
        public void CanLoadOneProductAndItsCategoryWithLazyLoadFetchingCategory()
        {
            Product p = null;
            using (MySimply.EnterContext())
            {
                p = Product.Find(x => true, x => x.Category);
            }
            Assert.AreEqual("Beverages", p.Category.Name);
        }

        [Test]
        public void CanLoadOneProductAndItsCategoryWithLazyLoadFetchingCategoryButNotSupplier()
        {
            Product p = null;
            using (MySimply.EnterContext())
            {
                p = Product.Find(x => true, x => x.Category);
            }
            Assert.AreEqual("Beverages", p.Category.Name);
            Assert.Throws<LazyInitializationException>(() => p.Supplier.ContactName.ToString());

        }

        [Test]
        public void CanLoadOneProductAndItsCategoryWithLazyLoadFetchingCategoryAndSupplier()
        {
            Product p = null;
            using (MySimply.EnterContext())
            {
                p = Product.Find(x => true, x => x.Category, x=>x.Supplier);
            }
            Assert.AreEqual("Beverages", p.Category.Name);
            Assert.AreEqual("Charlotte Cooper", p.Supplier.ContactName);

        }

        [Test]
        public void CanLoadOneProductAndItsCategoryWithLazyLoadFetchingCategoryWhenCategoryIsNull()
        {

            using (MySimply.EnterContext())
            {
                using (var tx = MySimply.BeginTransaction())
                {
                    var p1 = Product.Find(x => true).Clone();
                    p1.Category = null;
                    p1 = p1.Save();

                    Product p = Product.Find(x => x.Id == p1.Id, x => x.Category);

                    Assert.AreEqual(null, p.Category);
                }
            }
        }
    }
}
