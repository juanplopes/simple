using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.SampleServer;
using NUnit.Framework;
using NHibernate;
using NHibernate.Validator.Engine;
using Simple.Validation;

namespace Simple.Tests.DataAccess
{
    public class ValidationFixture : BaseDataFixture
    {
        [Test]
        public void TestValidateOnSave()
        {
            var c = Customer.Do.List(1).FirstOrDefault();
            Assert.IsNotNull(c);

            c.CompanyName = new string('0', 42);
            c.ContactName = new string('0', 42);

            Assert.Throws<ValidationException>(() => c.Save());
            MySimply.GetSession().Clear();
            Assert.Throws<ValidationException>(() => c.Update());
            MySimply.GetSession().Clear();
            Assert.Throws<ValidationException>(() => c.SaveOrUpdate());
            MySimply.GetSession().Clear();
            Assert.Throws<ValidationException>(() => c.Persist());
            MySimply.GetSession().Clear();
        }

        protected void GenericTest(Func<Customer, IList<InvalidValue>> func, params string[] props)
        {
            var c = Customer.Do.List(1).FirstOrDefault();
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
        public void TestAccessorValidation()
        {
            GenericTest(x => Customer.Do.Validate(x), "CompanyName", "ContactName");
        }

        [Test]
        public void TestServicePropertyValidation()
        {
            GenericTest(x => Customer.Service.ValidateProperty("CompanyName", x.CompanyName),
                "CompanyName");
        }

        [Test]
        public void TestDirectPropertyValidation()
        {
            GenericTest(x => x.Validate(c => c.CompanyName), "CompanyName");
        }

        [Test]
        public void TestDirectAccessorValidation()
        {
            GenericTest(x => Customer.Do.Validate(c => c.CompanyName, x.CompanyName),
                "CompanyName");
        }
    }
}

