using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple.Site.Helpers
{
    public static class HtmlHelperExtensions
    {
        public const string ScriptsPath = "Scripts";
        public const string StylesheetsPath = "Content";
        public const string ImagesPath = "Content/images";

        public static TagBuilder Script(this HtmlHelper helper, string file)
        {
            var tag = new TagBuilder("script");
            tag.MergeAttribute("type", "text/javascript");
            tag.MergeAttribute("src", helper.ResolveFullPath(ScriptsPath, file));
            return tag;
        }

        public static TagBuilder Image(this HtmlHelper helper, string file, string alt)
        {
            var tag = new TagBuilder("img");
            tag.MergeAttribute("src", helper.ResolveFullPath(ImagesPath, file));
            tag.MergeAttribute("alt", alt);
            return tag;
        }

        public static TagBuilder With(this TagBuilder tag, string attr, object value)
        {
            tag.MergeAttribute(attr, string.Format("{0}", value));
            return tag;
        }

        public static TagBuilder Stylesheet(this HtmlHelper helper, string file)
        {
            var tag = new TagBuilder("link");
            tag.MergeAttribute("type", "text/css");
            tag.MergeAttribute("rel", "Stylesheet");
            tag.MergeAttribute("href", helper.ResolveFullPath(StylesheetsPath, file));
            return tag;
        }

        public static string ResolveFullPath(this HtmlHelper helper, string directory, string file)
        {
            string model = string.Format("~/{0}/{1}", directory, file);
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
