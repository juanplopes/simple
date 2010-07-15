using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Common;

namespace Simple.Web.Mvc
{
    public static class HtmlButtons
    {
        public static TagBuilder Submit(this HtmlHelper helper, string caption)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.MergeAttribute("type", "submit");
            builder.MergeAttribute("id", string.Format("submit_{0}", caption));
            builder.MergeAttribute("value", caption);
            return builder;
        }

        public static TagBuilder BackLink(this HtmlHelper helper, string defaultUrl, string text)
        {
            string str = SafeNullable.Get(() => helper.ViewContext.HttpContext.Request.UrlReferrer.ToString());
            if (str != null && str.EndsWith(helper.ViewContext.HttpContext.Request.RawUrl)) str = null;
            str = str ?? defaultUrl;

            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("href", str);
            builder.MergeAttribute("class", "backlink");
            builder.InnerHtml = text;

            return builder;
        }
    }
}
