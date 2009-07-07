using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Simple.Threading
{
    public class ThreadData
    {
        LocalDataStoreSlot store = null;

        object defaultKey = new object();

        public ThreadData()
        {
            store = Thread.AllocateDataSlot();
            Thread.SetData(store, new Dictionary<object, object>());
        }

        public T Get<T>(object key)
        {
            lock (this)
            {
                if (key == null) key = defaultKey;

                try
                {
                    return (T)((Dictionary<object, object>)Thread.GetData(store))[key];
                }
                catch (KeyNotFoundException)
                {
                    return default(T);
                }
            }
        }

        public void Set(object key, object value)
        {
            lock (this)
            {
                if (key == null) key = defaultKey;

                ((Dictionary<object, object>)Thread.GetData(store))[key] = value;
            }
        }

        public void Remove(object key)
        {
            lock (this)
            {
                if (key == null) key = defaultKey;

                ((Dictionary<object, object>)Thread.GetData(store)).Remove(key);
            }
        }
    }
}
