using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Simple.Web.Mvc
{
    public static class HtmlNotice
    {
        public static string PageTitle(this HtmlHelper helper, string title, string description, Action<StringBuilder> overrides)
        {
            var builder = new TagBuilder("div").WithClasses("form-title");
            builder.InnerHtml = string.Format("{0}{1}",
                new TagBuilder("h2").FluentlyDo(x => x.SetInnerText(title)),
                new TagBuilder("p").FluentlyDo(x => x.SetInnerText(description)));

            var str = new StringBuilder();
            str.Append(builder);
            overrides(str);

            return str.ToString();
        }

        public static TagBuilder SimpleValidationSummary(this HtmlHelper helper, string message)
        {
            var contents = helper.ValidationSummary(message);
            if (contents == null) return null;

            TagBuilder builder = new TagBuilder("div");
            builder.AddCssClass("simple-notification-error");
            builder.InnerHtml = contents.ToString();
            return builder;
        }

        public static string DefaultNotificationFormat = "simple-notification-{0}";
        public static string DefaultSucessClass = "success";
        public static string DefaultErrorClass = "error";

        public static bool HasNotice(this IDictionary<string, object> data, string key)
        {
            return data.ContainsKey(DefaultNotificationFormat.AsFormat(key));
        }

        public static NoticeDefinition Notify(this IDictionary<string, object> data, string key)
        {
            var not = new NoticeDefinition();
            data[DefaultNotificationFormat.AsFormat(key)] = not;
            return not;
        }

        public static NoticeDefinition NotifySuccess(this IDictionary<string, object> data, string text)
        {
            return data.Notify(DefaultSucessClass).WithTitle(text);
        }

        public static NoticeDefinition NotifyError(this IDictionary<string, object> data, string text)
        {
            return data.Notify(DefaultErrorClass).WithTitle(text);
        }

        public static NoticeActionResult NotifySuccess(this ViewResultBase result, string text)
        {
            return new NoticeActionResult(result, x => x.Controller.ViewData.NotifySuccess(text));
        }

        public static NoticeActionResult NotifyError(this ViewResultBase result, string text)
        {
            return new NoticeActionResult(result, x => x.Controller.ViewData.NotifyError(text));
        }

        public static NoticeActionResult NotifySuccess(this RedirectToRouteResult result, string text)
        {
            return new NoticeActionResult(result, x => x.Controller.TempData.NotifySuccess(text));
        }

        public static NoticeActionResult NotifyError(this RedirectToRouteResult result, string text)
        {
            return new NoticeActionResult(result, x => x.Controller.TempData.NotifyError(text));
        }

        public static NoticeActionResult NotifySuccess(this RedirectResult result, string text)
        {
            return new NoticeActionResult(result, x => x.Controller.TempData.NotifySuccess(text));
        }

        public static NoticeActionResult NotifyError(this RedirectResult result, string text)
        {
            return new NoticeActionResult(result, x => x.Controller.TempData.NotifyError(text));
        }

        public static string NoticeAll(this HtmlHelper helper, Func<TagBuilder, TagBuilder> func)
        {
            var builder = new StringBuilder();
            builder.Append(func(helper.ViewData.NoticeSuccess()));
            builder.Append(func(helper.ViewData.NoticeError()));
            builder.Append(func(helper.ViewContext.TempData.NoticeSuccess()));
            builder.Append(func(helper.ViewContext.TempData.NoticeError()));
            return builder.ToString();
        }

        public static TagBuilder NoticeSuccess(this IDictionary<string, object> data)
        {
            return data.Notice(DefaultSucessClass);
        }

        public static TagBuilder NoticeError(this IDictionary<string, object> data)
        {
            return data.Notice(DefaultErrorClass);
        }

        public static TagBuilder Notice(this IDictionary<string, object> data, string key)
        {
            key = DefaultNotificationFormat.AsFormat(key);
            var definition = data[key] as NoticeDefinition;
            return RenderNotice(key, definition);
        }

        public static TagBuilder RenderNotice(string key, NoticeDefinition definition)
        {
            if (definition != null)
            {
                var notice = new TagBuilder("div").WithClasses(key);

                if (definition.Title != null)
                {
                    var span = new TagBuilder("span").WithText(definition.Title);
                    notice.InnerHtml += span.ToString();
                }

                if (definition.Items != null && definition.Items.Count > 0)
                {
                    var ul = new TagBuilder("ul").WithHtml(
                      definition.Items.Aggregate(new StringBuilder(),
                        (str, y) => str.Append(new TagBuilder("li").WithText(y))).ToString());

                    notice.InnerHtml += ul.ToString();
                }
                return notice;
            }
            else return null; ;
        }

        public static TagBuilder RenderNotice(this HtmlHelper helper, string key, NoticeDefinition definition)
        {
            return RenderNotice(key, definition);
        }

    }
}
