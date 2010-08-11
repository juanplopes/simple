using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Web.Mvc;
using System.Web.Mvc;

namespace Simple.Tests.Mvc.Mocks
{
    public class MockMobileCapableWebFormViewEngine : MobileCapableWebFormViewEngine
    {
        private IView view;
        private IViewEngine viewEngine;
        private string viewPathExpected;

        public MockMobileCapableWebFormViewEngine(IView view, IViewEngine viewEngine, string viewPathExpected)
        {
            this.view = view;
            this.viewEngine = viewEngine;
            this.viewPathExpected = viewPathExpected;
        }

        protected override ViewEngineResult ResolveView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (this.viewPathExpected.Equals(viewName, StringComparison.OrdinalIgnoreCase))
            {
                return new ViewEngineResult(this.view, this.viewEngine);
            }

            return new ViewEngineResult(new List<string>(0));
        }
    }
}
