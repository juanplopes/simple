using System;
using System.Linq;
using NUnit.Framework;
using Simple.IO.Serialization;
using Simple.Tests.Resources;
using Simple.Validation;

namespace Simple.Tests.Data
{
    public class ValidationFixture : BaseDataFixture
    {
        [Test]
        public void TestValidateOnSave()
        {
            var c = Customer.ListAll(1).FirstOrDefault();
            Assert.IsNotNull(c);

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            Assert.Throws<SimpleValidationException>(() => c.Save());
            MySimply.GetSession().Clear();
            Assert.Throws<SimpleValidationException>(() => c.Update());
            MySimply.GetSession().Clear();
            Assert.Throws<SimpleValidationException>(() => c.SaveOrUpdate());
            MySimply.GetSession().Clear();
            Assert.Throws<SimpleValidationException>(() => c.Persist());
            MySimply.GetSession().Clear();
        }

        [Test]
        public void TestSerializeValidationException()
        {
            var c = Customer.ListAll(1).FirstOrDefault();
            Assert.IsNotNull(c);

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            var obj = new SimpleValidationException(c.Validate());
            var bytes = SimpleSerializer.Binary().Serialize(obj);
            var obj2 = SimpleSerializer.Binary().Deserialize(bytes) as SimpleValidationException;

            Assert.IsNotNull(obj2.Errors);
            Assert.AreEqual(obj.Errors.Count(), obj2.Errors.Count());
        }

        protected void GenericTest(Func<Customer, ValidationList> func, params string[] props)
        {
            var c = Customer.ListAll(1).FirstOrDefault();
            Assert.IsNotNull(c);

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            var list = func(c);
            Assert.AreEqual(props.Length, list.Count);

            foreach (string s in props)
                Assert.IsTrue(list.Any(x => x.PropertyName == s));
        }

        [Test]
        public void TestServiceValidation()
        {
            GenericTest(x => Customer.Service.Validate(x), "CompanyName", "ContactName");
        }

        [Test]
        public void TestDirectValidation()
        {
            GenericTest(x => x.Validate(), "CompanyName", "ContactName");
        }

        [Test]
        public void TestServicePropertyValidation()
        {
            GenericTest(x => Customer.Service.ValidateProperty(x, "CompanyName"),
                "CompanyName");
        }

        [Test]
        public void TestDirectPropertyValidation()
        {
            GenericTest(x => x.Validate(c => c.CompanyName), "CompanyName");
        }

        [Test]
        public void TestValidateInstanceByProperty()
        {
            Region r = new Region() { Description = new string('a', 101) };
            Assert.Throws<SimpleValidationException>(() => r.Validate().AndThrow());
            new Region() { Description = new string('a', 99) }.Validate().AndThrow();

        }

        [Test]
        public void TestUniquePropertiesPredicate()
        {
            var c = new Customer();
            c.CompanyName = "CompanyName";            

            var expr = c.UniqueProperties("q", x => x.CompanyName);
            Assert.IsTrue(Customer.Count(expr) == 0);
        }

        [Test]
        public void TestUniquePropertiesPredicateFail()
        {
            var c = new Customer();
            c.CompanyName = "Alfreds Futterkiste";

            var expr = c.UniqueProperties("q", x => x.CompanyName);
            Assert.IsFalse(Customer.Count(expr) == 0);
        }

        [Test]
        public void TestMustBeUniqueValidation()
        {
            var c = new Customer();
            c.CompanyName = "CompanyName";

            Assert.IsTrue(c.Validate().Count == 0);
        }

        [Test]
        public void TestMustBeUniqueValidationFail()
        {
            var c = new Customer();
            c.CompanyName = "Alfreds Futterkiste";

            Assert.IsFalse(c.Validate().Count == 0);
        }
    }
}

