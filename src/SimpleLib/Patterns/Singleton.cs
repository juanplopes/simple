using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Patterns
{
    public class Singleton<T> : MarshalByRefObject
        where T : class, new()
    {
        protected static T _instance = null;
        protected static object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                    return _instance;
                }
            }
        }

        public static void ForceInstance(T instance)
        {
            lock (_lock)
            {
                _instance = instance;
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
