using System;
using System.Collections.Generic;
using System.Text;
using Simple.Filters;
using Simple.DataAccess;
using Simple.Reflection;
using Simple.ConfigSource;
using Simple.Client;

namespace Simple.Services
{
    [Serializable]
    public class Entity<T, R>
        where T : Entity<T, R>
        where R : class, IEntityService<T>
    {
        public static R Rules
        {
            get
            {
                return Simply.Get(DefaultConfigAttribute.GetKey(typeof(T))).Connect<R>();
            }
        }

        protected static void EnsureThisType(Entity<T, R> entity)
        {
            if (!(entity is T)) throw new InvalidOperationException("Assertion failure. No idea what's happening.");
        }

        public static int CountByFilter(Filter filter)
        {
            return Rules.CountByFilter(filter);
        }

        public static T Load(object id)
        {
            return Rules.Load(id);
        }

        public static T LoadByFilter(Filter filter)
        {
            return Rules.LoadByFilter(filter);
        }

        public static T LoadByExample(T example)
        {
            return Rules.LoadByExample(example);
        }

        public static IList<T> ListAll()
        {
            return ListAll(OrderBy.None());
        }

        public static IList<T> ListAll(OrderByCollection orderBy)
        {
            return Rules.ListAll(orderBy);
        }

        public static IList<T> ListByFilter(Filter filter)
        {
            return ListByFilter(filter, OrderBy.None());
        }

        public static IList<T> ListByFilter(Filter filter, OrderByCollection orderBy)
        {
            return Rules.ListByFilter(filter, orderBy);
        }

        public static IList<T> ListByExample(T example)
        {
            return Rules.ListByExample(example);
        }

        public static Page<T> PaginateByFilter(Filter filter, OrderByCollection orderBy, int skip, int take)
        {
            return Rules.PaginateByFilter(filter, orderBy, skip, take);
        }

        public static Page<T> PaginateByFilter(Filter filter, int skip, int take)
        {
            return Rules.PaginateByFilter(filter, OrderBy.None(), skip, take);
        }

        public static Page<T> PaginateAll(OrderByCollection orderBy, int skip, int take)
        {
            return Rules.PaginateAll(orderBy, skip, take);
        }

        public static Page<T> PaginateAll(int skip, int take)
        {
            return Rules.PaginateAll(OrderBy.None(), skip, take);
        }

        protected T ThisAsT
        {
            get
            {
                EnsureThisType(this);
                return this as T;
            }
        }

        public T Persist()
        {
            return Rules.Persist(ThisAsT);
        }

        public T Save()
        {
            return Rules.Save(ThisAsT);
        }

        public T Update()
        {
            return Rules.Update(ThisAsT);
        }

        public void Delete()
        {
            Rules.Delete(ThisAsT);
        }

        public static void DeleteById(object id)
        {
            Rules.DeleteById(id);
        }

        public static int DeleteByFilter(Filter filter)
        {
            return Rules.DeleteByFilter(filter);
        }


        public T SaveOrUpdate()
        {
            return Rules.SaveOrUpdate(ThisAsT);
        }
    }
}
