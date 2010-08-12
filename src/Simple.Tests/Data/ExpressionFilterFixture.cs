using NUnit.Framework;
using SharpTestsEx;
using Simple.Tests.Resources;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class ExpressionFilterFixture : BaseDataFixture
    {
        [Test]
        public void TestCustomerLoadById()
        {
            var c = Customer.Find(x => x.Id == "DRACD");
            Assert.IsNotNull(c);
            c.CompanyName.Should().Be("Drachenblut Delikatessen");
        }

        [Test]
        public void TestCustomerLoadIdContains()
        {
            var c = Customer.Find(x => x.Id.Contains("DRACD"));
            Assert.IsNotNull(c);
            c.CompanyName.Should().Be("Drachenblut Delikatessen");
        }

        [Test]
        public void TestCustomerLoadIdContainsFalse()
        {
            var c = Customer.Find(x => x.Id.Contains("DRACDasd"));
            Assert.IsNull(c);
        }

        [Test]
        public void TestCustomerLoadMultipleColumn()
        {
            var c = Customer.Find(x => x.Id.Contains("DRACD") && x.Country == "Germany");
            Assert.IsNotNull(c);
            c.CompanyName.Should().Be("Drachenblut Delikatessen");
        }

        [Test]
        public void TestProductLoadByIdAndCategory()
        {
            var p = Product.Find(x => x.Id == 2 && x.Category.Name == "Beverages");
            Assert.IsNotNull(p);
            p.Name.Should().Be("Chang");
        }

        [Test]
        public void TestProductLoadByIdAndCategoryFalse()
        {
            var p = Product.Find(x => x.Id == 2 && x.Category.Name == "OutroNome");
            Assert.IsNull(p);
        }
    }
}
