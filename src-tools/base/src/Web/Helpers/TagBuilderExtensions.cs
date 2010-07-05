using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple.Reflection;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace Sample.Project.Web.Helpers
{
    public static class TagBuilderExtensions
    {
        public static TagBuilder WithAttrs(this TagBuilder builder, object attrs)
        {
            if (builder == null) return builder;

            var dic = DictionaryHelper.FromAnonymous(attrs);
            builder.MergeAttributes(dic, true);
            return builder;
        }


        public static TagBuilder WithAttrs(this TagBuilder builder, params Expression<Func<object, object>>[] attrs)
        {
            if (builder == null) return builder;

            var dic = DictionaryHelper.FromExpressions(attrs);
            builder.MergeAttributes(dic, true);
            return builder;
        }

        public static TagBuilder WithClasses(this TagBuilder builder, params string[] classes)
        {
            if (builder == null) return builder;

            return builder.WithAttrs(@class => string.Join(" ", classes));
        }

        public static TagBuilder AddClasses(this TagBuilder builder, params string[] classes)
        {
            if (builder == null) return builder;

            foreach (var s in classes)
                builder.AddCssClass(s);
            return builder;
        }

        public static TagBuilder WithId(this TagBuilder builder, string newId)
        {
            if (builder == null) return builder;

            return builder.WithAttrs(id => newId);
        }
    }

}
