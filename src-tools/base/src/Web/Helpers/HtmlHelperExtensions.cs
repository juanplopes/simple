using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple;
using Simple.Web.Mvc;
using MvcContrib.FluentHtml.Elements;
using System.Collections;
using MvcContrib.FluentHtml;
using MvcContrib.FluentHtml.Expressions;
using System.Linq.Expressions;
using Simple.Common;
using Simple.Entities;
using Simple.Validation;
namespace Sample.Project.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public const string ScriptsPath = "Scripts";
        public const string StylesheetsPath = "Content";
        public const string ImagesPath = "Content/images";

        public static TagBuilder Script(this HtmlHelper helper, string file)
        {
            return new TagBuilder("script").WithAttrs(new
            {
                type = "text/javascript",
                src = helper.ResolveFullPath(ScriptsPath, file)
            });
        }

        public static TagBuilder Image(this HtmlHelper helper, string file, string alt)
        {
            return new TagBuilder("img").WithAttrs(new
            {
                src = helper.ResolveFullPath(ImagesPath, file),
                alt = alt
            });

        }

        public static TagBuilder Stylesheet(this HtmlHelper helper, string file)
        {
            return new TagBuilder("link").WithAttrs(new
            {
                type = "text/css",
                rel = "Stylesheet",
                href = helper.ResolveFullPath(StylesheetsPath, file)
            });
        }


        public static string ButtonLink(this HtmlHelper helper, string text, ActionResult action)
        {
            return helper.ActionLink(text, action, new { @class = "button" }).ToString();
        }

        public static ViewResult DeleteView(this Controller controller, object itemName, params object[] objs)
        {
            return ConfirmView(controller, "Do you want to remove '{0}'?", (itemName??"").ToString().AsFormat(objs));
        }

        public static ViewResult ConfirmView(this Controller controller, object text, params object[] objs)
        {
            var view = new ViewResult() { ViewName = "Confirm", ViewData = controller.ViewData, TempData = controller.TempData }; 
            view.ViewData["confirm-message"] = (text??"").ToString().AsFormat(objs);
            return view;
        }

        public static Select<T> AutoSelect<T, P>(this IViewModelContainer<T> html, Expression<Func<T, P>> member, string viewDataKey)
            where T : class
        {
            var expr = Expression.Lambda<Func<T, object>>(member.Body, member.Parameters);
            return html
                .Select(expr)
                .Options(html.FindSelectList(member, viewDataKey));
        }

     

    }
}
