using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Common;

namespace Simple.ConfigSource
{
    public abstract class AggregateFactory<T> : Singleton<T>
        where T : AggregateFactory<T>, new()
    {
        public static T Get()
        {
            return FactoryStack.Find(Instance);
        }

        public AggregateFactory()
        {
            Configure();
        }

        public abstract void Configure();
    }
}
