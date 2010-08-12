using NUnit.Framework;
using SharpTestsEx;
using Simple.Tests.Resources;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class EntityHelperIntegrationFixture
    {
        [Test]
        public void TestEqualityOnNoKey()
        {
            var t1 = new Category();
            t1.Id = 12;
            var t2 = new Category();
            t2.Id = 12;
            var t3 = new Category();
            t3.Id = 13;

            t2.Should().Not.Be(t1);
            t1.Should().Not.Be(t2);

            t3.Should().Not.Be(t1);
            t1.Should().Not.Be(t3);

            t3.Should().Not.Be(t2);
            t2.Should().Not.Be(t3);
        }

        [Test]
        public void TestEqualityOnSimpleKey()
        {
            Territory t1 = new Territory();
            t1.Id = "asd";
            Territory t2 = new Territory();
            t2.Id = "asd";
            Territory t3 = new Territory();
            t3.Id = "asd2";

            t2.Should().Be(t1);
            t1.Should().Be(t2);

            t3.Should().Not.Be(t1);
            t1.Should().Not.Be(t3);

            t3.Should().Not.Be(t2);
            t2.Should().Not.Be(t3);
        }

        [Test]
        public void TestHashCodeOnNoKey()
        {
            var t1 = new Category();
            t1.Id = 12;
            var t2 = new Category();
            t2.Id = 12;
            var t3 = new Category();
            t3.Id = 13;

            t2.GetHashCode().Should().Not.Be(t1.GetHashCode());
            t1.GetHashCode().Should().Not.Be(t2.GetHashCode());

            t3.GetHashCode().Should().Not.Be(t1.GetHashCode());
            t1.GetHashCode().Should().Not.Be(t3.GetHashCode());

            t3.GetHashCode().Should().Not.Be(t2.GetHashCode());
            t2.GetHashCode().Should().Not.Be(t3.GetHashCode());
        }

        [Test]
        public void TestHashCodeOnSimpleKey()
        {
            Territory t1 = new Territory();
            t1.Id = "asd";
            Territory t2 = new Territory();
            t2.Id = "asd";
            Territory t3 = new Territory();
            t3.Id = "asd2";

            t2.GetHashCode().Should().Be(t1.GetHashCode());
            t1.GetHashCode().Should().Be(t2.GetHashCode());

            t3.GetHashCode().Should().Not.Be(t1.GetHashCode());
            t1.GetHashCode().Should().Not.Be(t3.GetHashCode());

            t3.GetHashCode().Should().Not.Be(t2.GetHashCode());
            t2.GetHashCode().Should().Not.Be(t3.GetHashCode());
        }

        [Test]
        public void TestToStringOnNoKey()
        {
            Category t1 = new Category();
            t1.Id = 12;
            Category t2 = new Category();
            t2.Id = 13;

            t1.ToString().Should().Be(t1.GetType().FullName);
            t2.ToString().Should().Be(t2.GetType().FullName);
        }

        [Test]
        public void TestToStringOnSimpleKey()
        {
            Territory t1 = new Territory();
            t1.Id = "asd";
            Territory t2 = new Territory();
            t2.Id = "asd2";

            t1.ToString().Should().Be("(Id=asd)");
            t2.ToString().Should().Be("(Id=asd2)");
        }

        [Test]
        public void TestEqualityOnCompositeKey()
        {
            var t1 = new EmployeeTerritory();
            t1.Employee = new Employee() { Id = 2 };
            t1.Territory = new Territory() { Id = "asd" };

            var t2 = new EmployeeTerritory();
            t2.Employee = new Employee() { Id = 2 };
            t2.Territory = new Territory() { Id = "asd" };

            var t3 = new EmployeeTerritory();
            t3.Employee = new Employee() { Id = 3 };
            t3.Territory = new Territory() { Id = "asd" };

            t2.Should().Be(t1);
            t1.Should().Be(t2);

            t3.Should().Not.Be(t1);
            t1.Should().Not.Be(t3);

            t3.Should().Not.Be(t2);
            t2.Should().Not.Be(t3);
        }
        [Test]
        public void TestHashCodeOnCompositeKey()
        {
            var t1 = new EmployeeTerritory();
            t1.Employee = new Employee() { Id = 2 };
            t1.Territory = new Territory() { Id = "asd" };

            var t2 = new EmployeeTerritory();
            t2.Employee = new Employee() { Id = 2 };
            t2.Territory = new Territory() { Id = "asd" };

            var t3 = new EmployeeTerritory();
            t3.Employee = new Employee() { Id = 3 };
            t3.Territory = new Territory() { Id = "asd" };

            t2.GetHashCode().Should().Be(t1.GetHashCode());
            t1.GetHashCode().Should().Be(t2.GetHashCode());

            t3.GetHashCode().Should().Not.Be(t1.GetHashCode());
            t1.GetHashCode().Should().Not.Be(t3.GetHashCode());

            t3.GetHashCode().Should().Not.Be(t2.GetHashCode());
            t2.GetHashCode().Should().Not.Be(t3.GetHashCode());
        }

        [Test]
        public void TestToStringOnCompositeKey()
        {
            var t1 = new EmployeeTerritory();
            t1.Employee = new Employee() { Id = 2 };
            t1.Territory = new Territory() { Id = "asd" };

            var t2 = new EmployeeTerritory();
            t2.Employee = new Employee() { Id = 2 };
            t2.Territory = new Territory() { Id = "asd" };

            var t3 = new EmployeeTerritory();
            t3.Employee = new Employee() { Id = 3 };
            t3.Territory = new Territory() { Id = "asd" };

            t1.ToString().Should().Be("(Employee=(Id=2) | Territory=(Id=asd))");
            t2.ToString().Should().Be("(Employee=(Id=2) | Territory=(Id=asd))");
            t3.ToString().Should().Be("(Employee=(Id=3) | Territory=(Id=asd))");
        }
    }
}
