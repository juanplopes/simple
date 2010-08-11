using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web.Routing;

namespace Simple.Tests.Mvc.Mocks
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(p => p.Request).Returns(request.Object);
            context.Setup(p => p.Response).Returns(response.Object);
            context.Setup(p => p.Session).Returns(session.Object);
            context.Setup(p => p.Server).Returns(server.Object);

            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url)
        {
            HttpContextBase context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }

        public static void SetFakeControllerContext(this Controller controller)
        {
            var httpContext = FakeHttpContext();
            ControllerContext context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        public static void SetHttpBrowserCapabilities(this HttpRequestBase request, HttpBrowserCapabilitiesBase httpBrowserCapabilities)
        {
            Mock.Get(request)
                .Setup(p => p.Browser)
                .Returns(httpBrowserCapabilities);
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request)
                .Setup(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (!url.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Sorry, we Setup a virtual url starting with \"~/\".");
            }

            var mock = Mock.Get(request);

            mock.Setup(req => req.QueryString)
                .Returns(GetQueryStringParameters(url));
            mock.Setup(req => req.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            mock.Setup(req => req.PathInfo)
                .Returns(string.Empty);
        }

        private static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
            {
                return url.Substring(0, url.IndexOf("?", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                return url;
            }
        }

        private static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                NameValueCollection parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }
    }
}
