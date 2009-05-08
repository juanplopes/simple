using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;

namespace Simple.DataAccess.Conventions
{
    public class LazyConvention
    {
        public static IConvention ForClass(bool value)
        {
            return Generic(ConventionBuilder.Class, value);
        }

        public static IConvention ForHasOne(bool value)
        {
            return Generic(ConventionBuilder.HasOne, value);
        }

        public static IConvention Generic<T, K>(IConventionBuilder<T, K> builder, bool value)
            where T:IConvention<K>
            where K:IHasAttributes
        {
            return builder.Always(x => x.SetAttribute("lazy", ToAttr(value)));
        }

        protected static string ToAttr(bool value)
        {
            return value ? "true" : "false";
        }
    }
}
