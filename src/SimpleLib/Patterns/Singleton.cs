using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Patterns
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

        public static T Do
        {
            get
            {
                return Instance;
            }

        }
    }
}
