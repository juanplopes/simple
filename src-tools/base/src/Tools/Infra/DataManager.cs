using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator.Data;

namespace Sample.Project.Tools.Infra
{
    public class DataManager
    {
        //Take a look at InsertDataCommand. It's more interesting.

        static IDictionary<Type, IDataList> items = new Dictionary<Type, IDataList>();
        public static T Get<T>()
            where T : IDataList, new()
        {
            IDataList item;
            if (!items.TryGetValue(typeof(T), out item))
                items[typeof(T)] = item = new T();

            return (T)item;
        }

    }
}
