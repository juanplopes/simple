using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Reflection
{
    public static class DictionaryHelper
    {
        public static IDictionary<string, object> FromAnonymous(object obj)
        {
            var value = new Dictionary<string, object>();
            if (obj == null) return value;

            foreach (var prop in obj.GetType().GetProperties())
            {
                value[prop.Name] = MethodCache.Do.GetGetter(prop)(obj, null);
            }

            return value;
        }
    }
}
