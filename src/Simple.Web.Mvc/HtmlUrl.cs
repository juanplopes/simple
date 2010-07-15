using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Simple.Web.Mvc
{
    public static class HtmlUrl
    {

        public static UrlHelper UrlHelper(this HtmlHelper helper)
        {
            return new UrlHelper(helper.ViewContext.RequestContext);
        }

        public static string ResolveFullPath(this HtmlHelper helper, string directory, string file)
        {
            string model = "~/{0}/{1}".AsFormat(directory, file);
            return ResolveFullPath(helper, model);
        }

        public static string ResolveFullPath(this HtmlHelper helper, string url)
        {
            return helper.ViewContext.HttpContext.Request.Url
                .GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped) +
                VirtualPathUtility.ToAbsolute(url);
        }
    }
}
