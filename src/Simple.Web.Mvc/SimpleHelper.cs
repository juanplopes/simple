using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Simple.Web.Mvc
{
    public class SimpleHelper : System.Web.WebPages.HelperPage
    {
        public static new WebViewPage Page
        {
            get { return (WebViewPage)WebPageContext.Current.Page; }
        }

        public static new HtmlHelper Html
        {
            get { return Page.Html; }
        }

        public static UrlHelper Url
        {
            get { return Page.Url; }
        }
    }
}
