using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web;
using System.Globalization;

namespace Simple.Web.Mvc
{

    /// <summary>
    /// Represents a view engine for rendering a Web Forms page in MVC with support for discover views for mobile devices.
    /// </summary>
    public class MobileCapableWebFormViewEngine : WebFormViewEngine
    {
        private StringDictionary deviceFolders;

        /// <summary>
        /// Initializes a new instance of the MobileCapableWebFormViewEngine class.
        /// </summary>
        public MobileCapableWebFormViewEngine()
        {
            this.deviceFolders = new StringDictionary
            {
                { "MSIE Mobile", "WindowsMobile" },
                { "Mozilla", "iPhone" }
            };
        }

        /// <summary>
        /// Get the "browser/folder" mapping dictionary.
        /// </summary>
        public StringDictionary DeviceFolders
        {
            get
            {
                return this.deviceFolders;
            }
        }

        /// <summary>
        /// Finds the view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="masterName">Name of the master.</param>
        /// <param name="useCache">if set to true [use cache].</param>
        /// <returns>The page view.</returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            ViewEngineResult result = null;
            HttpRequestBase request = controllerContext.HttpContext.Request;

            if (request.Browser.IsMobileDevice)
            {
                string mobileViewName = string.Empty;

                mobileViewName = string.Format(
                                        CultureInfo.InvariantCulture,
                                        "Mobile/{0}/{1}",
                                        this.RetrieveDeviceFolderName(request.Browser.Browser),
                                        viewName);

                result = this.ResolveView(controllerContext, mobileViewName, masterName, useCache);

                if (result == null || result.View == null)
                {
                    mobileViewName = string.Format(
                                            CultureInfo.InvariantCulture,
                                            "Mobile/{0}",
                                            viewName);

                    result = this.ResolveView(controllerContext, mobileViewName, masterName, useCache);
                }
            }

            if (result == null || result.View == null)
            {
                result = this.ResolveView(controllerContext, viewName, masterName, useCache);
            }

            return result;
        }

        protected virtual ViewEngineResult ResolveView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        /// <summary>
        /// Get the device folder associated with the name of the browser.
        /// </summary>
        /// <param name="browser">Name of the browser.</param>
        /// <returns>The associated folder name.</returns>
        private string RetrieveDeviceFolderName(string browser)
        {
            if (this.deviceFolders.ContainsKey(browser))
            {
                return this.deviceFolders[browser.Trim()];
            }
            else
            {
                return "unknown";
            }
        }
    }
}
