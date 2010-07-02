using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Generator.Data
{
    public static class DataManager
    {
        //Take a look at InsertDataCommand. It's more interesting.

        static IDictionary<Type, IDataList> items = new Dictionary<Type, IDataList>();
        public static IDataList Get(Type type)
        {
            IDataList item;
            if (!items.TryGetValue(type, out item))
                items[type] = item = (IDataList)Activator.CreateInstance(type);

            return item;
        }

        public static T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public static IList<IDataList> FromAssembly(Assembly asm)
        {
            return asm.GetTypes()
                .Where(x => x.CanAssign(typeof(IDataList)))
                .Select(x => Get(x)).ToList();
        }

        public static void ExecuteAllThatMatches(this IList<IDataList> samples, string env)
        {
            foreach (var sample in samples.Where(x => x.Matches(env)))
                sample.Execute();
        }
    }
}
