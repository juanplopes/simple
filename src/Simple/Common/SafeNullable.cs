using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Common;

namespace Simple
{
    public static class SafeNullable
    {
        public static SafeValue<T> Get<T>(Func<T> func)
        {
            return Get(func, default(T));
        }

        public static SafeValue<T> Get<T>(Func<T> func, T defaultValue)
        {
            bool found = false;

            T value = default(T);
            try
            {
                value = func();
                found = true;
            }
            catch (NullReferenceException) { }
            return new SafeValue<T>(found ? value : defaultValue, found);
        }

        public static T With<Q, T>(this Q value, Func<Q, T> func)
        {
            return Get(() => func(value));
        }

        public static T With<Q, T>(this Q value, Func<Q, T> func, T defaultValue)
        {
            return Get(() => func(value), defaultValue);
        }

        public static SafeValue<T> WithSafe<Q, T>(this Q value, Func<Q, T> func)
        {
            return Get(() => func(value));
        }

        public static SafeValue<T> WithSafe<Q, T>(this Q value, Func<Q, T> func, T defaultValue)
        {
            return Get(() => func(value), defaultValue);
        }

    }
}
