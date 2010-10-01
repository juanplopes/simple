using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Collections;

namespace Simple.Threading
{
    /// <summary>
    /// Controls thread-based data related to the enclosing instance.
    /// </summary>
    public class ThreadDataProvider : IContextProvider
    {
        string key = Guid.NewGuid().ToString();
        

        /// <summary>
        /// Gets the thread storage.
        /// </summary>
        /// <returns>The thread storage.</returns>
        public IDictionary GetStorage()
        {
            LocalDataStoreSlot store = Thread.GetNamedDataSlot(key);
            var dic = (IDictionary)Thread.GetData(store);
            if (dic == null)
            {
                dic = new Dictionary<object, object>();
                Thread.SetData(store, dic);
            }
            return dic;
        }

        public void SetStorage(IDictionary storage)
        {
            LocalDataStoreSlot store = Thread.GetNamedDataSlot(key);
            Thread.SetData(store, storage);
        }
    }
}
