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
            c.Should().Not.Be.Null();
            c.CompanyName.Should().Be("Drachenblut Delikatessen");
        }

        [Test]
        public void TestCustomerLoadIdContains()
        {
            var c = Customer.Find(x => x.Id.Contains("DRACD"));
            c.Should().Not.Be.Null();
            c.CompanyName.Should().Be("Drachenblut Delikatessen");
        }

        [Test]
        public void TestCustomerLoadIdContainsFalse()
        {
            var c = Customer.Find(x => x.Id.Contains("DRACDasd"));
            c.Should().Be.Null();
        }

        [Test]
        public void TestCustomerLoadMultipleColumn()
        {
            var c = Customer.Find(x => x.Id.Contains("DRACD") && x.Country == "Germany");
            c.Should().Not.Be.Null();
            c.CompanyName.Should().Be("Drachenblut Delikatessen");
        }

        [Test]
        public void TestProductLoadByIdAndCategory()
        {
            var p = Product.Find(x => x.Id == 2 && x.Category.Name == "Beverages");
            p.Should().Not.Be.Null();
            p.Name.Should().Be("Chang");
        }

        [Test]
        public void TestProductLoadByIdAndCategoryFalse()
        {
            var p = Product.Find(x => x.Id == 2 && x.Category.Name == "OutroNome");
            p.Should().Be.Null();
        }
    }
}
