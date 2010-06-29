using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using System.Linq.Expressions;
using Simple;

namespace Sample.Project.Generator.Data
{
    public abstract class DataList<T> : IDataItems
        where T : class, IEntity<T>, new()
    {
        public abstract Expression<Func<T, bool>> FindPredicate(T entity);
        public abstract void DefineItems();
        bool initialized = false;

        public DataList()
        {
            Initialize();
        }

        private IList<DataItem<T>> items = new List<DataItem<T>>();
        private ILookup<object, DataItem<T>> lookup = null;

        public IList<T> GetList(string name)
        {
            return GetEnumeration(name).ToList();
        }

        public T GetFirst(string name)
        {
            return GetEnumeration(name).First();
        }

        public T GetUnique(string name)
        {
            return GetEnumeration(name).Single();
        }

        private IEnumerable<T> GetEnumeration(string name)
        {
            var sample = lookup[name ?? DataItem<T>.NullName];
            var items = sample.SelectMany(x =>
            {
                var t = new T();
                x.PopulateKey(t);
                return Entity<T>.List(FindPredicate(t));
            });
            return items;
        }

        protected virtual DataItem<T> NewItem(string name)
        {
            if (initialized) throw new InvalidOperationException("Already initialized");
            var item = new DataItem<T>(name);
            items.Add(item);
            return item;
        }

        protected virtual DataItem<T> NewItem()
        {
            return NewItem(null);
        }

        public virtual void Initialize()
        {
            if (initialized) return;

            DefineItems();
            lookup = items.ToLookup(x => x.Name);

            initialized = true;
        }

        public virtual void Execute()
        {
            Simply.Do.Log(this).InfoFormat("Executing {0}...", this.GetType().Name);

            Initialize();

            foreach (var item in items)
            {
                var t = new T();
                item.PopulateKey(t);
                if (Entity<T>.Count(FindPredicate(t)) == 0)
                {
                    item.PopulateValues(t);
                    t.Save();
                }
            }
        }
    }
}
