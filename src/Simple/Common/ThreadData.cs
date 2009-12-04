using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Simple.Common
{
    /// <summary>
    /// Controls thread-based data related to the enclosing instance.
    /// </summary>
    public class ThreadData
    {
        object defaultKey = new object();

        /// <summary>
        /// Gets the thread storage.
        /// </summary>
        /// <returns>The thread storage.</returns>
        public Dictionary<object, object> GetStorage()
        {
            var store = Thread.GetNamedDataSlot(this.GetType().GUID.ToString());
            var dic = (Dictionary<object, object>)Thread.GetData(store);
            if (dic == null)
            {
                dic = new Dictionary<object, object>();
                Thread.SetData(store, dic);
            }
            return dic;
        }

        /// <summary>
        /// Gets an specific value from thread data using some object key.
        /// </summary>
        /// <typeparam name="T">The type of the value to be cast.</typeparam>
        /// <param name="key">The key used to get the value.</param>
        /// <returns>The value.</returns>
        public T Get<T>(object key)
        {
            if (key == null) key = defaultKey;

            try
            {
                return (T)GetStorage()[key];
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Sets some object into an slot indexed by a key. 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(object key, object value)
        {
            if (key == null) key = defaultKey;

            GetStorage()[key] = value;

        }

        /// <summary>
        /// Removes specific key and its value from the thread data.
        /// </summary>
        /// <param name="key">The key to be removed.</param>
        public void Remove(object key)
        {
            if (key == null) key = defaultKey;

            GetStorage().Remove(key);
        }
    }
}
