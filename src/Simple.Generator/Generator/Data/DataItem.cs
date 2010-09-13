using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Data
{
    public class DataItem<T>
    {
        public static object NullName = new object();

        public object Name { get; protected set; }
        public Action<T> PopulateKeyAction { get; protected set; }
        public Action<T> PreSaveAction { get; protected set; }
        public Action<T> PostSaveAction { get; protected set; }
        public Action<T> FinallyAction { get; protected set; }

        public DataItem()
        {
            this.Name = NullName;
            this.PopulateKeyAction = x => { };
            this.PreSaveAction = x => { };
            this.PostSaveAction = x => { };
            this.FinallyAction = x => { };
        }

        public DataItem(string name) : this()
        {
            this.Name = name ?? NullName;
        }

        public DataItem<T> IdentifiedBy(Action<T> action)
        {
            this.PopulateKeyAction = action;
            return this;
        }

        public DataItem<T> AlsoWith(Action<T> action)
        {
            this.PreSaveAction = action;
            return this;
        }

        public DataItem<T> ThenDo(Action<T> action)
        {
            this.PostSaveAction = action;
            return this;
        }

        public DataItem<T> AndAlwaysDo(Action<T> action)
        {
            this.FinallyAction = action;
            return this;
        }
    }
}
