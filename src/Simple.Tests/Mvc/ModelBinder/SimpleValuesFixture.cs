using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Web.Mvc;
using System.Web.Mvc;
using Simple.Tests.Mvc.Mocks;
using System.Web.Routing;
using Moq;
using System.Collections.Specialized;
using Simple.Entities;
using Iesi.Collections.Generic;

namespace Simple.Tests.Mvc.ModelBinder
{
    [TestFixture]
    public class SimpleValuesFixture
    {
       
        [Test]
        public void BindAddressByConstructor()
        {
            var obj = Util.TestBind<ContactInfo>(new NameValueCollection { { "HomeAddress", "2" } });
            
            obj.Should().Be.OfType<ContactInfo>().And
                .Value.HomeAddress.Id.Should().Be(2);
        }

        [Test]
        public void BindAddressByIDProperty()
        {
            var obj = Util.TestBind<ContactInfo>(new NameValueCollection { { "HomeAddress.ID", "2" } });

            obj.Should().Be.OfType<ContactInfo>().And
                .Value.HomeAddress.Id.Should().Be(2);
        }

        [Test]
        public void BindLocationByInnerConstructor()
        {
            var obj = Util.TestBind<ContactInfo>(new NameValueCollection { { "Location", "2" } });

            obj.Should().Be.OfType<ContactInfo>().And
                .Value.Location.Address.Id.Should().Be(2);
        }



        [Test]
        public void BindAddressWithInvalidValueCauseModelStateError()
        {
            ModelBindingContext context;
            var obj = Util.TestBind<ContactInfo>(new NameValueCollection { { "HomeAddress", "2a" } }, out context);

            obj.Should().Be.OfType<ContactInfo>().And
                .Value.HomeAddress.Should().Be.Null();
            context.ModelState["HomeAddress"].Errors.Count.Should().Be(1);
        }

        [Test]
        public void BindAddressWithMultipleValuesChoosesOnlyTheFirst()
        {
            var obj = Util.TestBind<ContactInfo>(new NameValueCollection { { "HomeAddress", "4" }, { "HomeAddress", "3" } });

            obj.Should().Be.OfType<ContactInfo>().And
                .Value.HomeAddress.Id.Should().Be(4);
        }

        [Test]
        public void BindNullByConstructorCauseEntireEntityToBeNull()
        {
            var obj = Util.TestBind<ContactInfo>(new NameValueCollection { { "HomeAddress", "" } });

            obj.Should().Be.OfType<ContactInfo>().And
                .Value.HomeAddress.Should().Be.Null();
        }

        [Test]
        public void BindAddressWithinAnotherPropertyByConstructor()
        {
            var obj = Util.TestBind<Employee>(new NameValueCollection { { "Contact.HomeAddress", "2" } });

            obj.Should().Be.OfType<Employee>().And
                .Value.Contact.HomeAddress.Id.Should().Be(2);
        }


        [Test]
        public void BindHomeAddressWithinAnotherPropertyByIDProperty()
        {
            var obj = Util.TestBind<Employee>(new NameValueCollection { { "Contact.HomeAddress.ID", "2" } });

            obj.Should().Be.OfType<Employee>().And
                .Value.Contact.HomeAddress.Id.Should().Be(2);
        }
    }
}
