using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Common
{
    public class Singleton<T>
        where T : new()
    {
        class Nested
        {
            public static T Instance = new T();
        }
        public static T Instance
        {
            get
            {
                return Nested.Instance;
            }
        }
    }
}
