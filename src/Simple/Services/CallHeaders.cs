using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.Collections;
using Simple.Common;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace Simple.Services
{
    [Serializable]
    public class CallHeaders : Hashtable, ILogicalThreadAffinative
    {
        [NonSerialized]
        private static ThreadData data = new ThreadData();
        [NonSerialized]
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
        public static void Force(CallHeaders headers)
        {
            data.Set(_store, headers);
        }

        public CallHeaders() : base() { }
        public CallHeaders(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override object this[object key]
        {
            get
            {
                if (this.ContainsKey(key))
                    return base[key];
                else
                    return null;
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
