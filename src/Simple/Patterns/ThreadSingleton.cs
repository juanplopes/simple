using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Common;

namespace Simple.Patterns
{
    public class ThreadSingleton<T> : MarshalByRefObject
        where T : class, new()
    {
        static ThreadData data = new ThreadData();

        public static T Instance
        {
            get
            {
                T obj = data.Get<T>(null);
                if (obj == null)
                {
                    obj = new T();
                    data.Set(null, obj);
                }

                return obj;
            }
        }

        public static void ForceInstance(T instance)
        {
            data.Set(null, instance);
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
