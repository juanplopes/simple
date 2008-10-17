using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace BasicLibrary.Persistence
{
    public class ExecutionContext<T> where T : ExecutionContext<T>, new()
    {
        public static T Get()
        {
            T lobjT;
            if ((lobjT = (T)CallContext.GetData(typeof(T).GUID.ToString()))==null)
            {
                lobjT = new T();
                CallContext.SetData(typeof(T).GUID.ToString(), lobjT);
            }
            return lobjT;
        }
    }
}
