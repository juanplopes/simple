using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Threading
{
    public class ContextData
    {
        static object defaultKey = new object();

        public Func<IContextProvider> Provider { get; protected set; }

        public ContextData(IContextProvider provider)
            : this(() => provider)
        {
        }

        public ContextData(Func<IContextProvider> provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            this.Provider = provider;
        }

        public object Get(object key)
        {
            return Get<object>(key);
        }

        public T Get<T>(object key)
        {
            if (key == null) key = defaultKey;

            try
            {
                var item = Provider().GetStorage()[key];
                if (item is T)
                    return (T)item;
                else
                    return default(T);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public void Set(object key, object value)
        {
            if (key == null) key = defaultKey;

            Provider().GetStorage()[key] = value;
        }

        public void Remove(object key)
        {
            if (key == null) key = defaultKey;

            Provider().GetStorage().Remove(key);
        }

        public T Singleton<T>()
            where T : new()
        {
            return Singleton(() => new T());
        }

        public void SetSingleton<T>(T obj)
        {
            var key = typeof(T).GUID;
            Set(key, obj);
        }

        public T Singleton<T>(Func<T> creator)
        {
            var key = typeof(T).GUID;
            var obj = Get<T>(key);
            if (obj == null)
            {
                obj = creator();
                SetSingleton(obj);
            }

            return obj;
        }
    }
}
