using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple;

namespace Sample.Project.Web.Helpers
{
    public static class HeaderExtensions
    {
        public const string ScriptsPath = "Scripts";
        public const string StylesheetsPath = "Content";
        public const string ImagesPath = "Content/images";

        public static TagBuilder Script(this HtmlHelper helper, string file)
        {
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            builder.MergeAttribute("src", helper.ResolveFullPath(ScriptsPath, file));
            return builder;
        }

        public static TagBuilder Image(this HtmlHelper helper, string file, string alt)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttribute("src", helper.ResolveFullPath(ImagesPath, file));
            builder.MergeAttribute("alt", alt);
            return builder;
        }

        public static TagBuilder Stylesheet(this HtmlHelper helper, string file)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("type", "text/css");
            builder.MergeAttribute("rel", "Stylesheet");
            builder.MergeAttribute("href", helper.ResolveFullPath(StylesheetsPath, file));
            return builder;
        }

        public static string ResolveFullPath(this HtmlHelper helper, string directory, string file)
        {
            string model = "~/{0}/{1}".AsFormat(directory, file);
            return helper.ViewContext.HttpContext.Request.Url
                .GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped) +
                VirtualPathUtility.ToAbsolute(model);
        }

        public static string ResolveUrl(this HtmlHelper helper, string path)
        {
            return VirtualPathUtility.ToAbsolute(path);
        }
    }
}
