using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simple.Reflection
{
    public static class DictionaryHelper
    {
        public static IDictionary<string, object> FromExpressions(params Expression<Func<object, object>>[] items)
        {
            return FromExpressions(items, false);
        }
        public static IDictionary<string, object> FromExpressions(Expression<Func<object, object>>[] items, bool caseSensitive)
        {
            var value = new Dictionary<string, object>(caseSensitive ? StringComparer.InvariantCulture : StringComparer.InvariantCultureIgnoreCase);
            if (items == null) return value;

            foreach (var item in items)
            {
                var name = item.Parameters[0].Name;
                value[name] = item.Compile()(null);
            }

            return value;
        }

        public static IDictionary<string, object> FromAnonymous(object obj)
        {
            return FromAnonymous(obj, false);
        }

        public static IDictionary<string, object> FromAnonymous(object obj, bool caseSensitive)
        {
            var value = new Dictionary<string, object>(caseSensitive ? StringComparer.InvariantCulture : StringComparer.InvariantCultureIgnoreCase);
            if (obj == null) return value;

            foreach (var prop in obj.GetType().GetProperties())
            {
                value[prop.Name] = MethodCache.Do.GetGetter(prop)(obj, null);
            }

            return value;
        }
    }
}
