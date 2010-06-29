using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Project.Generator.Data
{
    public class DataItem<T>
    {
        public static object NullName = new object();

        public object Name { get; protected set; }
        public Action<T> PopulateKey { get; protected set; }
        public Action<T> PopulateValues { get; protected set; }

        public DataItem()
        {
            this.Name = NullName;
            this.PopulateKey = x => { };
            this.PopulateValues = x => { };
        }

        public DataItem(string name) : this()
        {
            this.Name = name ?? NullName;
        }

        public DataItem<T> Key(Action<T> action)
        {
            this.PopulateKey = action;
            return this;
        }

        public DataItem<T> Value(Action<T> action)
        {
            this.PopulateValues = action;
            return this;
        }


    }
}
