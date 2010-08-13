using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Specialized;
using SharpTestsEx;
using System.Web.Mvc;

namespace Simple.Tests.Mvc.ModelBinder
{
    public abstract class BaseCollectionFixture<C, T>
        where C : Company<T>, new()
        where T : IEnumerable<Address>
    {
        protected virtual bool AllowNulls
        {
            get { return true; }
        }

        [Test]
        public void BindZeroAddressToList()
        {
            var obj = Util.TestBind<C>(new NameValueCollection { });

            obj.Should().Be.OfType<C>()
                .And.Value.Places.Should().Have.SameSequenceAs(new C().Places);
        }


        [Test]
        public void BindOneAddressToList()
        {
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "4" } });

            var asserter = obj.Should().Be.OfType<C>().And.Value;

            asserter.Places.Select(x => x.Id)
                .Should().Have.SameSequenceAs(4);
        }

        [Test]
        public void BindTwoAddressesToList()
        {
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "4" }, { "Places", "3" } });

            var asserter = obj.Should().Be.OfType<C>().And.Value;

            asserter.Places.Select(x => x.Id)
                .Should().Have.SameSequenceAs(4, 3);
        }

        [Test, Ignore("Will cause problems with ISet. Must fix it now!")]
        public void BindTwoAddressesToWithinPropertyList()
        {
            var obj = Util.TestBind<Holding<C, T>>(
                new NameValueCollection { { "MyCompany.Places", "4" }, { "MyCompany.Places", "3" } });

            var asserter = obj.Should().Be.OfType<Holding<C, T>>().And.Value;

            asserter.MyCompany.Places.Select(x => x.Id)
                .Should().Have.SameSequenceAs(4, 3);
        }

        [Test]
        public void BindNullValue()
        {
            ModelBindingContext context;
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "" } }, out context);

            var asserter = obj.Should().Be.OfType<C>().And.Value;
            if (AllowNulls)
            {
                asserter.Places.Should().Have.SameSequenceAs((Address)null);
            }
            else
            {
                asserter.Places.Count().Should().Be(0);
                context.ModelState["Places"].Errors.Count().Should().Be(1);
            }
        }

        [Test]
        public void BindInvalidValue()
        {
            ModelBindingContext context;
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "asd" } }, out context);

            var asserter = obj.Should().Be.OfType<C>().And.Value;

            if (AllowNulls)
            {
                asserter.Places.Should().Have.SameSequenceAs((Address)null);
                context.ModelState["Places"].Errors.Count.Should().Be(1);
            }
            else
            {
                asserter.Places.Count().Should().Be(0);
                context.ModelState["Places"].Errors.Count().Should().Be(2);
            }
        }

        [Test]
        public void BindInvalidValueBetweenCorrectOnes()
        {
            ModelBindingContext context;
            var obj = Util.TestBind<C>(new NameValueCollection { { "Places", "1" }, { "Places", "asd" }, { "Places", "3" } }, out context);

            var asserter = obj.Should().Be.OfType<C>().And.Value;

            if (AllowNulls)
            {
                asserter.Places.Select(x => x == null ? -1 : x.Id)
                    .Should().Have.SameSequenceAs(1, -1, 3);

                context.ModelState["Places"].Errors.Count.Should().Be(1);
            }
            else
            {
                asserter.Places.Select(x => x.Id)
                   .Should().Have.SameSequenceAs(1, 3);
                context.ModelState["Places"].Errors.Count().Should().Be(2);
            }

        }
    }
}
