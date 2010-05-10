using NUnit.Framework;
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

            Assert.AreNotEqual(t1, t2);
            Assert.AreNotEqual(t2, t1);

            Assert.AreNotEqual(t1, t3);
            Assert.AreNotEqual(t3, t1);

            Assert.AreNotEqual(t2, t3);
            Assert.AreNotEqual(t3, t2);
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

            Assert.AreEqual(t1, t2);
            Assert.AreEqual(t2, t1);

            Assert.AreNotEqual(t1, t3);
            Assert.AreNotEqual(t3, t1);

            Assert.AreNotEqual(t2, t3);
            Assert.AreNotEqual(t3, t2);
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

            Assert.AreNotEqual(t1.GetHashCode(), t2.GetHashCode());
            Assert.AreNotEqual(t2.GetHashCode(), t1.GetHashCode());

            Assert.AreNotEqual(t1.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t3.GetHashCode(), t1.GetHashCode());

            Assert.AreNotEqual(t2.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t3.GetHashCode(), t2.GetHashCode());
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

            Assert.AreEqual(t1.GetHashCode(), t2.GetHashCode());
            Assert.AreEqual(t2.GetHashCode(), t1.GetHashCode());

            Assert.AreNotEqual(t1.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t3.GetHashCode(), t1.GetHashCode());

            Assert.AreNotEqual(t2.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t3.GetHashCode(), t2.GetHashCode());
        }

        [Test]
        public void TestToStringOnNoKey()
        {
            Category t1 = new Category();
            t1.Id = 12;
            Category t2 = new Category();
            t2.Id = 13;

            Assert.AreEqual(t1.GetType().FullName, t1.ToString());
            Assert.AreEqual(t2.GetType().FullName, t2.ToString());
        }

        [Test]
        public void TestToStringOnSimpleKey()
        {
            Territory t1 = new Territory();
            t1.Id = "asd";
            Territory t2 = new Territory();
            t2.Id = "asd2";

            Assert.AreEqual("(Id=asd)", t1.ToString());
            Assert.AreEqual("(Id=asd2)", t2.ToString());
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

            Assert.AreEqual(t1, t2);
            Assert.AreEqual(t2, t1);

            Assert.AreNotEqual(t1, t3);
            Assert.AreNotEqual(t3, t1);

            Assert.AreNotEqual(t2, t3);
            Assert.AreNotEqual(t3, t2);
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

            Assert.AreEqual(t1.GetHashCode(), t2.GetHashCode());
            Assert.AreEqual(t2.GetHashCode(), t1.GetHashCode());

            Assert.AreNotEqual(t1.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t3.GetHashCode(), t1.GetHashCode());

            Assert.AreNotEqual(t2.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t3.GetHashCode(), t2.GetHashCode());
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

            Assert.AreEqual("(Employee=(Id=2) | Territory=(Id=asd))", t1.ToString());
            Assert.AreEqual("(Employee=(Id=2) | Territory=(Id=asd))", t2.ToString());
            Assert.AreEqual("(Employee=(Id=3) | Territory=(Id=asd))", t3.ToString());
        }
    }
}
