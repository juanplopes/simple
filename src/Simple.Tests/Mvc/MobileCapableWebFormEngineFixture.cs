using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;
using System.Globalization;
using NUnit.Framework;
using SharpTestsEx;
using System.Web;
using System.Web.Routing;
using Simple.Tests.Mvc.Mocks;

namespace Simple.Tests.Mvc
{
    [TestFixture]
    public class MobileCapableWebFormViewEngineFixture
    {
        [Test]
        public void FindViewReturnsSpecificMobileViewPath()
        {
            var supportedBrowserName = "MSIE Mobile";
            var testControllerContext = RetrieveTestControllerContext(true, supportedBrowserName);
            var viewName = "Index";
            var viewPathExpected = string.Format(CultureInfo.InvariantCulture, "Mobile/WindowsMobile/{0}", viewName);

            ExecuteTest(testControllerContext, viewName, viewPathExpected);
        }

        [Test]
        public void FindViewReturnsSharedMobileViewPath()
        {
            var supportedBrowserName = "MSIE Mobile";
            var testControllerContext = RetrieveTestControllerContext(true, supportedBrowserName);
            var viewName = "SharedMobileView";
            var viewPathExpected = string.Format(CultureInfo.InvariantCulture, "Mobile/{0}", viewName);

            ExecuteTest(testControllerContext, viewName, viewPathExpected);
        }

        [Test]
        public void FindViewReturnsDesktopViewPath()
        {
            var supportedBrowserName = "IE";
            var testControllerContext = RetrieveTestControllerContext(false, supportedBrowserName);
            var viewName = "Index";
            var viewPathExpected = viewName;

            ExecuteTest(testControllerContext, viewName, viewPathExpected);
        }

        [Test]
        public void FindViewReturnsSharedMobileViewPathWhenSpecificMobileFolderNotExists()
        {
            var notSupportedBrowserName = "Not Supported Browser";
            var testControllerContext = RetrieveTestControllerContext(true, notSupportedBrowserName);
            var viewName = "Index";
            var viewPathExpected = string.Format(CultureInfo.InvariantCulture, "Mobile/{0}", viewName);

            ExecuteTest(testControllerContext, viewName, viewPathExpected);
        }

        [Test]
        public void FindViewReturnsDesktopViewPathWhenSpecificMobileFolderAndSharedMobileFolderNotExists()
        {
            var notSupportedBrowserName = "Not Supported Browser";
            var testControllerContext = RetrieveTestControllerContext(true, notSupportedBrowserName);
            var viewName = "UndefinedView";
            var viewPathExpected = string.Format(CultureInfo.InvariantCulture, "{0}", viewName);

            ExecuteTest(testControllerContext, viewName, viewPathExpected);
        }

        private static void ExecuteTest(ControllerContext testControllerContext, string viewName, string viewPathExpected)
        {
            var testViewFormView = new WebFormView(viewPathExpected);
            var fakeViewEngine = new Mock<IViewEngine>();
            var webFormViewEngine = new MockMobileCapableWebFormViewEngine(
                                            testViewFormView,
                                            fakeViewEngine.Object,
                                            viewPathExpected);

            var viewEngineResult = webFormViewEngine.FindView(testControllerContext, viewName, string.Empty, false);

            Assert.IsNotNull(viewEngineResult);
            Assert.IsNotNull(viewEngineResult.View);
            Assert.IsNotNull(viewEngineResult.ViewEngine);
            ((WebFormView)viewEngineResult.View).ViewPath.Should().Be(viewPathExpected);
        }

        private static ControllerContext RetrieveTestControllerContext(bool isMobileDevice, string browser)
        {
            var fakeHttpContext = MvcMockHelpers.FakeHttpContext();

            var httpBrowserCapabilities = new Mock<HttpBrowserCapabilitiesBase>();
            httpBrowserCapabilities.Setup(p => p.IsMobileDevice)
                                   .Returns(isMobileDevice);
            httpBrowserCapabilities.Setup(p => p.Browser)
                                   .Returns(browser);
            MvcMockHelpers.SetHttpBrowserCapabilities(fakeHttpContext.Request, httpBrowserCapabilities.Object);

            var fakeController = new Mock<ControllerBase>();
            var testRouteData = new RouteData();
            testRouteData.Values.Add("controller", "TestController");

            return new ControllerContext(fakeHttpContext, testRouteData, fakeController.Object);
        }
    }
}
