using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Specialized;
using SharpTestsEx;

namespace Simple.Tests.Mvc.ModelBinder
{
    public abstract class BaseCollectionFixture<C, T>
        where C : Company<T>, new()
        where T : IEnumerable<Address>
    {
        [Test]
        public void BindOneAddressToList()
        {
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "4" } });

            var asserter = obj.Should().Be.OfType<C>().And.Value;
            asserter.Places.Count().Should().Be(1);
            asserter.Places.Select(x => x.ID).Should().Have.SameSequenceAs(4);
        }

        [Test]
        public void BindTwoAddressesToList()
        {
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "4" }, { "Places", "3" } });

            var asserter = obj.Should().Be.OfType<C>().And.Value;
            asserter.Places.Count().Should().Be(2);
            asserter.Places.Select(x => x.ID).Should().Have.SameSequenceAs(4, 3);
        }

        //[Test]
        //public void CanBindMultipleIntArrayProperty()
        //{
        //    var obj = TestBind<CompanyArray>(new NameValueCollection { { "Children", "4" }, { "Children", "3" } });

        //    var asserter = obj.Should().Be.OfType<CompanyArray>().And.Value;
        //    asserter.Places.Length.Should().Be(2);
        //    asserter.Places.Select(x => x.ID).Should().Have.SameSequenceAs(4, 3);
        //}

        //[Test]
        //public void CanBindMultipleIntSetProperty()
        //{
        //    var obj = TestBind<CompanyISet>(new NameValueCollection { { "Children", "3" }, { "Children", "4" } });

        //    var asserter = obj.Should().Be.OfType<CompanyISet>().And.Value;
        //    asserter.Places.Count.Should().Be(2);
        //    asserter.Places.Select(x => x.ID).Should().Have.SameSequenceAs(3, 4);
        //}

        //[Test]
        //public void CanBindMultipleIntSetPropertyWithNullValues()
        //{
        //    ModelBindingContext context;
        //    var obj = TestBind<CompanyISet>(new NameValueCollection { { "Children", "" } }, out context);

        //    var asserter = obj.Should().Be.OfType<CompanyISet>().And.Value;
        //    asserter.Places.Count.Should().Be(0);

        //    context.ModelState["Children"].Errors.Count.Should().Be(1);
        //}

        //[Test]
        //public void CanBindMultipleIntArrayPropertyWithNullValues()
        //{
        //    ModelBindingContext context;
        //    var obj = TestBind<CompanyArray>(new NameValueCollection { { "Children", "" } }, out context);

        //    var asserter = obj.Should().Be.OfType<CompanyArray>().And.Value;
        //    asserter.Places.Length.Should().Be(1);
        //    asserter.Places.Should().Have.SameSequenceAs((Address)null);

        //    context.ModelState["Children"].Errors.Count.Should().Be(0);
        //}

        //[Test]
        //public void CanBindMultipleIntSetPropertyWithInvalidValues()
        //{
        //    ModelBindingContext context;
        //    var obj = TestBind<CompanyISet>(new NameValueCollection { { "Children", "asd" } }, out context);

        //    var asserter = obj.Should().Be.OfType<CompanyISet>().And.Value;
        //    asserter.Places.Count.Should().Be(0);

        //    context.ModelState["Children"].Errors.Count.Should().Be(2);
        //}
    }
}
