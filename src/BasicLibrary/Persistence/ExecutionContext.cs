using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace BasicLibrary.Persistence
{
    [Serializable]
    public class ExecutionContext<T> where T : ExecutionContext<T>, new()
    {
        public static T Get()
        {
            T lobjT;
            if ((lobjT = (T)CallContext.GetData(typeof(T).GUID.ToString()))==null)
            {
                lobjT = new T();
                lobjT.Init();
                Set(lobjT);
            }
            return lobjT;
        }

        public static void Set(T obj)
        {
            CallContext.SetData(typeof(T).GUID.ToString(), obj);
        }

        public virtual void Init() { }
    }
}
