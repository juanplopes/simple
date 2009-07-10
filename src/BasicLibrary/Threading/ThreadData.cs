using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BasicLibrary.Threading
{
    public class ThreadData<T>
    {
        public object this[string type, int id]
        {
            get
            {
                return Thread.GetData(Thread.GetNamedDataSlot(typeof(T).GUID.ToString() + GetKeyFromPair(type,id)));
            }
            set
            {
                Thread.SetData(Thread.GetNamedDataSlot(typeof(T).GUID.ToString() + GetKeyFromPair(type,id)), value);
            }
        }

        protected string GetKeyFromPair(string type, int id)
        {
            return type + "&&&" + id;
        }
    }
}
