using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
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
            c.Should().Not.Be.Null();

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            c.Executing(x => x.Save()).Throws<SimpleValidationException>();
            MySimply.GetSession().Clear();
            c.Executing(x => x.Update()).Throws<SimpleValidationException>();
            MySimply.GetSession().Clear();
            c.Executing(x => x.SaveOrUpdate()).Throws<SimpleValidationException>();
            MySimply.GetSession().Clear();
        }

        [Test]
        public void TestSerializeValidationException()
        {
            var c = Customer.ListAll(1).FirstOrDefault();
            c.Should().Not.Be.Null();

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            var obj = new SimpleValidationException(c.Validate());
            var bytes = SimpleSerializer.Binary().Serialize(obj);
            var obj2 = SimpleSerializer.Binary().Deserialize(bytes) as SimpleValidationException;

            obj2.Errors.Count().Should().Be(obj.Errors.Count());
        }

        protected void GenericTest(Func<Customer, ValidationList> func, params string[] props)
        {
            var c = Customer.ListAll(1).FirstOrDefault();
            c.Should().Not.Be.Null();

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            var list = func(c);
            list.Count.Should().Be(props.Length);

            foreach (string s in props)
                list.Any(x => x.PropertyName == s).Should().Be.True();
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
            
            r.Executing(x => x.Validate().AndThrow()).Throws<SimpleValidationException>();

            new Region() { Description = new string('a', 99) }.Validate().AndThrow();

        }

        [Test]
        public void TestUniquePropertiesPredicate()
        {
            var c = new Customer();
            c.CompanyName = "CompanyName";            

            var expr = c.UniqueProperties("q", x => x.CompanyName);
            Customer.Count(expr).Should().Be(0);
        }

        [Test]
        public void TestUniquePropertiesPredicateFail()
        {
            var c = new Customer();
            c.CompanyName = "Alfreds Futterkiste";

            var expr = c.UniqueProperties("q", x => x.CompanyName);
            Customer.Count(expr).Should().Be(1);
        }

        [Test]
        public void TestMustBeUniqueValidation()
        {
            var c = new Customer();
            c.CompanyName = "CompanyName";

            c.Validate().Count.Should().Be(0);
        }

        [Test]
        public void TestMustBeUniqueValidationFail()
        {
            var c = new Customer();
            c.CompanyName = "Alfreds Futterkiste";

            c.Validate().Count.Should().Be(1);
        }
    }
}

