using NUnit.Framework;
using SharpTestsEx;
using Simple.Tests.Resources;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class NorthwindCheckFixture : BaseDataFixture
    {
        [Test]
        public void CheckCategories()
        {
            Category.Count().Should().Be(8);
            Category.ListAll(1);
        }

        [Test]
        public void CheckCustomer()
        {
            Customer.Count().Should().Be(91);
            Customer.ListAll(1);
        }

        [Test]
        public void CheckProducts()
        {
            Product.Count().Should().Be(77);
            Product.ListAll(1);

            Product p = Product.Load(2);
            Assert.IsNotNull(p.Category); p.Category.Name.Should().Be("Beverages");
            Assert.IsNotNull(p.Supplier); p.Supplier.CompanyName.Should().Be("Exotic Liquids");
        }

        [Test]
        public void CheckSuppliers()
        {
            Supplier.Count().Should().Be(29);
            Supplier.ListAll(1);
        }

        [Test]
        public void CheckRegions()
        {
            Region.Count().Should().Be(4);
            Region.ListAll(1);
        }

        [Test]
        public void CheckTerritories()
        {
            Territory.Count().Should().Be(53);
            Territory.ListAll(1);
            var t = Territory.Load("03049");
            t.Region.Id.Should().Be(3);
        }

        [Test]
        public void CheckEmployees()
        {
            Employee.Count().Should().Be(9);
            Employee.ListAll(1);
        }

        [Test]
        public void CheckEmployeeTerritories()
        {
            EmployeeTerritory.Count().Should().Be(49);
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
