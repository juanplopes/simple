using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Generator.Data
{
    public static class DataManager
    {
        static IDictionary<Type, IDataList> items = new Dictionary<Type, IDataList>();
        public static IDataList Get(Type type)
        {
            IDataList item;
            if (!items.TryGetValue(type, out item))
                items[type] = item = (IDataList)Activator.CreateInstance(type);

            return item;
        }

        public static T Get<T>()
            where T:IDataList
        {
            return (T)Get(typeof(T));
        }

        public static T Execute<T>()
            where T:IDataList
        {
            var t = Get<T>();
            t.Execute();
            return t;
        }
    }
}
