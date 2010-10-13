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

        public static string ResolveFullPath(this HtmlHelper helper, params string[] components)
        {
            var sb = new StringBuilder("~");

            if (components != null)
                foreach (var component in components)
                    sb.AppendFormat("/{0}", component);

            return ResolveFullPath(helper, sb.ToString());
        }

        public static string ResolveFullPath(this HtmlHelper helper, string url)
        {
            return helper.ViewContext.HttpContext.Request.Url
                .GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped) +
                VirtualPathUtility.ToAbsolute(url);
        }
    }
}
