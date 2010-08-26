using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Common
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

   }
}
