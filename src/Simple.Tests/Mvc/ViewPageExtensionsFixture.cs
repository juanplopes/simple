using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Moq;
using System.Web.Mvc;
using System.Web.Routing;
using Simple.Web.Mvc;

namespace Simple.Tests.Mvc
{
    [TestFixture]
    public class ViewPageExtensionsFixture
    {
        [Test]
        public void CanReadExistingValueFromRouteData()
        {
            var data = new RouteData();
            data.Values["test"] = 2;

            var view = ViewFor(data);

            view.RouteParam<int>("test").Should().Be(2);
        }

        [Test]
        public void ReadingIsCaseInsensitive()
        {
            var data = new RouteData();
            data.Values["TeSt"] = 2;

            var view = ViewFor(data);

            view.RouteParam<int>("tEsT").Should().Be(2);
        }
        [Test]
        public void CanReadExistingValueFromRouteDataEvenIfsInAnotherType()
        {
            var data = new RouteData();
            data.Values["test"] = "42";

            var view = ViewFor(data);

            view.RouteParam<int>("test").Should().Be(42);
        }

        [Test]
        public void WhenReadingANonExistingObjectReturnDefault()
        {
            var data = new RouteData();
            data.Values["test"] = "42";

            var view = ViewFor(data);

            view.RouteParam<int>("testOther").Should().Be(0);
        }

        private static ViewPage ViewFor(RouteData data)
        {
            var ctx = new Mock<ViewContext>();
            ctx.SetupGet(x => x.RouteData).Returns(data);
            var view = new ViewPage() { ViewContext = ctx.Object };
            return view;
        }
    }
}
