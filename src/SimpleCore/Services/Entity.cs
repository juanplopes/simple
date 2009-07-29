using System;
using System.Collections.Generic;
using System.Text;
using Simple.Filters;
using Simple.DataAccess;
using Simple.Reflection;
using Simple.ConfigSource;

namespace Simple.Services
{
    [Serializable]
    public class Entity<T, R>
        where T : Entity<T, R>
        where R : class, IEntityService<T>
    {
        public static R Do
        {
            get
            {
                return Simply.Get(DefaultConfigAttribute.GetKey(typeof(T))).Resolve<R>();
            }
        }

        protected static void EnsureThisType(Entity<T, R> entity)
        {
            if (!(entity is T)) throw new InvalidOperationException("Assertion failure. No idea what's happening.");
        }

        public static int CountByFilter(Filter filter)
        {
            return Do.CountByFilter(filter);
        }

        public static T Load(object id)
        {
            return Do.Load(id);
        }

        public static T LoadByFilter(Filter filter)
        {
            return Do.LoadByFilter(filter);
        }

        public static T LoadByExample(T example)
        {
            return Do.LoadByExample(example);
        }

        public static IList<T> ListAll()
        {
            return ListAll(OrderBy.None());
        }

        public static IList<T> ListAll(OrderByCollection orderBy)
        {
            return Do.ListAll(orderBy);
        }

        public static IList<T> ListByFilter(Filter filter)
        {
            return ListByFilter(filter, OrderBy.None());
        }

        public static IList<T> ListByFilter(Filter filter, OrderByCollection orderBy)
        {
            return Do.ListByFilter(filter, orderBy);
        }

        public static IList<T> ListByExample(T example)
        {
            return Do.ListByExample(example);
        }

        public static Page<T> PaginateByFilter(Filter filter, OrderByCollection orderBy, int skip, int take)
        {
            return Do.PaginateByFilter(filter, orderBy, skip, take);
        }

        public static Page<T> PaginateByFilter(Filter filter, int skip, int take)
        {
            return Do.PaginateByFilter(filter, OrderBy.None(), skip, take);
        }

        public static Page<T> PaginateAll(OrderByCollection orderBy, int skip, int take)
        {
            return Do.PaginateAll(orderBy, skip, take);
        }

        public static Page<T> PaginateAll(int skip, int take)
        {
            return Do.PaginateAll(OrderBy.None(), skip, take);
        }

        protected virtual T ThisAsT
        {
            get
            {
                EnsureThisType(this);
                return this as T;
            }
        }

        public virtual T Persist()
        {
            return Do.Persist(ThisAsT);
        }

        public virtual T Save()
        {
            return Do.Save(ThisAsT);
        }

        public virtual T Update()
        {
            return Do.Update(ThisAsT);
        }

        public virtual void Delete()
        {
            Do.Delete(ThisAsT);
        }

        public static void DeleteById(object id)
        {
            Do.DeleteById(id);
        }

        public static int DeleteByFilter(Filter filter)
        {
            return Do.DeleteByFilter(filter);
        }


        public virtual T SaveOrUpdate()
        {
            return Do.SaveOrUpdate(ThisAsT);
        }
    }
}
