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
        public static HtmlTagBuilder SimpleValidationSummary(this HtmlHelper helper, string message)
        {
            var contents = helper.ValidationSummary(message);
            if (contents == null) return null;

            HtmlTagBuilder builder = new HtmlTagBuilder("div");
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

    
        public static HtmlTagBuilder NoticeSuccess(this IDictionary<string, object> data)
        {
            return data.Notice(DefaultSucessClass);
        }

        public static HtmlTagBuilder NoticeError(this IDictionary<string, object> data)
        {
            return data.Notice(DefaultErrorClass);
        }

        public static HtmlTagBuilder Notice(this IDictionary<string, object> data, string key)
        {
            key = DefaultNotificationFormat.AsFormat(key);
            var definition = data[key] as NoticeDefinition;
            return RenderNotice(key, definition);
        }

        public static HtmlTagBuilder RenderNotice(string key, NoticeDefinition definition)
        {
            if (definition != null)
            {
                var notice = new HtmlTagBuilder("div").WithClasses(key);

                if (definition.Title != null)
                {
                    var span = new HtmlTagBuilder("span").WithText(definition.Title);
                    notice.InnerHtml += span.ToString();
                }

                if (definition.Items != null && definition.Items.Count > 0)
                {
                    var ul = new HtmlTagBuilder("ul").WithHtml(
                      definition.Items.Aggregate(new StringBuilder(),
                        (str, y) => str.Append(new HtmlTagBuilder("li").WithText(y))).ToString());

                    notice.InnerHtml += ul.ToString();
                }
                return notice;
            }
            else return null; ;
        }

        public static HtmlTagBuilder RenderNotice(this HtmlHelper helper, string key, NoticeDefinition definition)
        {
            return RenderNotice(key, definition);
        }

    }
}
