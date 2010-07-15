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
            builder.AddCssClass("validation-summary-errors");
            builder.InnerHtml = contents.ToString();
            return builder;
        }



        //public static TagBuilder Notice(this HtmlHelper helper)
        //{
        //    return helper.ViewData.GenericNotice(BaseController.NoticeKey);
        //}

        //public static TagBuilder TempNotice(this HtmlHelper helper)
        //{
        //    return helper.ViewContext.TempData
        //        .GenericNotice(BaseController.TempNoticeKey).WithClasses("noticeMessage", "autoFadeOut");
        //}
        //private static TagBuilder GenericNotice(this IDictionary<string, object> data, string key)
        //{
        //    object defObj = null;
        //    data.TryGetValue(key, out defObj);
        //    var def = defObj as BaseController.NoticeDefinition;
        //    return GenericNotice(def);
        //}

        //private static TagBuilder GenericNotice(BaseController.NoticeDefinition noticeDef)
        //{
        //    if (noticeDef != null && noticeDef.Title != null)
        //    {
        //        TagBuilder notice = new TagBuilder("div");
        //        notice.AddCssClass("noticeMessage");

        //        TagBuilder noticeH3 = new TagBuilder("h3");
        //        noticeH3.AddCssClass("noticeH3");
        //        noticeH3.SetInnerText(noticeDef.Title);

        //        notice.InnerHtml += noticeH3.ToString();

        //        if (noticeDef.Message != null)
        //        {
        //            TagBuilder noticeP = new TagBuilder("p");
        //            noticeP.AddCssClass("noticeP");
        //            noticeP.SetInnerText(noticeDef.Message);

        //            notice.InnerHtml += noticeP.ToString();
        //        }

        //        return notice;
        //    }
        //    else return null;
        //}
    }
}
