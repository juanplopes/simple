using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.Collections;
using Simple.Threading;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace Simple.Services
{
    [Serializable]
    public class CallHeaders : ILogicalThreadAffinative
    {
        private static ThreadData data = new ThreadData();
        private static object _store = new object();

        public static CallHeaders Do
        {
            get
            {
                var headers = data.Get<CallHeaders>(_store);
                if (headers == null)
                {
                    data.Set(_store, headers = new CallHeaders());
                }
                return headers;
            }
        }

        public Hashtable Data { get; protected set; }

        public void Force(Hashtable headers)
        {
            Data = headers;
        }

        public object Get(object key)
        {
            if (Data.Contains(key))
                return Data[key];
            else
                return null;
        }

        public void Set(object key, object value)
        {
            Data[key] = value;
        }

        public void Clear()
        {
            Data.Clear();
        }

        public object this[object key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        public int Count { get { return Data.Count; } }
    }
}
