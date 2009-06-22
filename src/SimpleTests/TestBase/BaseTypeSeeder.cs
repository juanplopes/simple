using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.TestBase
{
    public class BaseTypeSeeder : ITypeSeeder
    {
        #region ITypeSeeder Members

        public object GetValue(Type type, int seed)
        {
            if (CheckType<int>(type))
                return seed;

            else if (CheckType<long>(type))
                return (long)seed;

            else if (CheckType<string>(type))
                return seed.ToString();

            else if (CheckType<DateTime>(type))
                return new DateTime(2000, 1, 1).AddDays(seed);

            else if (CheckType<char>(type))
                return (seed%10).ToString()[0];

            else
                return null;
        }

        protected bool CheckType<T>(Type type)
        {
            return type.IsAssignableFrom(typeof(T));
        }

        #endregion
    }
}
