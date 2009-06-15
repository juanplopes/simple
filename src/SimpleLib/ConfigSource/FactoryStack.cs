using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Common;

namespace Simple.ConfigSource
{
    public static class FactoryStack
    {
        static OrderedItemsList<object> factories;
        static FactoryStack()
        {
            factories = new OrderedItemsList<object>();
        }

        public static void ForceFirst(object item)
        {
            lock(factories)
                factories.ForceFirst(item);
        }

        public static T Find<T>(T item)
        {
            lock(factories)
                try
                {
                    return (T)factories.First(x => x is T);
                }
                catch (InvalidOperationException)
                {
                    ForceFirst(item);
                    return item;
                }
        }
    }
}
