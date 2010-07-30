using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Simple.Web.Mvc;

namespace Sample.Project.Web.Helpers
{
    public static class NoticeExtensions
    {
        public static string PageTitle(this HtmlHelper helper, string title, string description)
        {
         
            return helper.PageTitle(title, description,
                x => x
                    .Append(helper.SimpleValidationSummary())
                    .Append(helper.NoticeAll(n => n.AddClasses("autohide")))
                );
        }

        public static TagBuilder SimpleValidationSummary(this HtmlHelper helper)
        {
            return helper.SimpleValidationSummary("Erro(s) durante o processamento:");
        }

    }
}
