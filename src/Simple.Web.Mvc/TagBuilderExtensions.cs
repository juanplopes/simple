using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Reflection;
using System.Linq.Expressions;

namespace Simple.Web.Mvc
{
    public static class TagBuilderExtensions
    {
        public static HtmlTagBuilder WithAttrs(this HtmlTagBuilder builder, object attrs)
        {
            if (builder == null) return builder;

            var dic = DictionaryHelper.FromAnonymous(attrs);
            builder.MergeAttributes(dic, true);
            return builder;
        }


        public static HtmlTagBuilder WithAttrs(this HtmlTagBuilder builder, params Expression<Func<object, object>>[] attrs)
        {
            if (builder == null) return builder;

            var dic = DictionaryHelper.FromExpressions(attrs);
            builder.MergeAttributes(dic, true);
            return builder;
        }

        public static HtmlTagBuilder WithClasses(this HtmlTagBuilder builder, params string[] classes)
        {
            if (builder == null) return builder;

            return builder.WithAttrs(@class => string.Join(" ", classes));
        }

        public static HtmlTagBuilder AddClasses(this HtmlTagBuilder builder, params string[] classes)
        {
            if (builder == null) return builder;

            foreach (var s in classes)
                builder.AddCssClass(s);
            return builder;
        }

        public static HtmlTagBuilder WithId(this HtmlTagBuilder builder, string newId)
        {
            if (builder == null) return builder;

            return builder.WithAttrs(id => newId);
        }

        public static HtmlTagBuilder WithText(this HtmlTagBuilder builder, string text)
        {
            if (builder == null) return builder;
            builder.SetInnerText(text);
            return builder;
        }
        public static HtmlTagBuilder WithHtml(this HtmlTagBuilder builder, string html)
        {
            if (builder == null) return builder;
            builder.InnerHtml = html;
            return builder;
        }
    }
}
