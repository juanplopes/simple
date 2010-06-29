using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Project.Generator.Data
{
    public class DataManager
    {
        static IDictionary<Type, IDataItems> items = new Dictionary<Type, IDataItems>();
        public static T Get<T>()
            where T : IDataItems, new()
        {
            IDataItems item;
            if (!items.TryGetValue(typeof(T), out item))
            {
                item = new T();
                item.Initialize();
                items[typeof(T)] = item;
            }
            return (T)item;
        }

    }
}
