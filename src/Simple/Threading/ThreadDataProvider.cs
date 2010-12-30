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
        [ThreadStatic]
        static IDictionary internalStorage;
        

        /// <summary>
        /// Gets the thread storage.
        /// </summary>
        /// <returns>The thread storage.</returns>
        public IDictionary GetStorage()
        {
            if (internalStorage == null)
                internalStorage = new Hashtable();

            return internalStorage;
        }

        public void SetStorage(IDictionary storage)
        {
            internalStorage = storage;
        }
    }
}
