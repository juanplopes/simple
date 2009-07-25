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
        public static R Service
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
            return Service.CountByFilter(filter);
        }

        public static T Load(object id)
        {
            return Service.Load(id);
        }

        public static T LoadByFilter(Filter filter)
        {
            return Service.LoadByFilter(filter);
        }

        public static T LoadByExample(T example)
        {
            return Service.LoadByExample(example);
        }

        public static IList<T> ListAll()
        {
            return ListAll(OrderBy.None());
        }

        public static IList<T> ListAll(OrderByCollection orderBy)
        {
            return Service.ListAll(orderBy);
        }

        public static IList<T> ListByFilter(Filter filter)
        {
            return ListByFilter(filter, OrderBy.None());
        }

        public static IList<T> ListByFilter(Filter filter, OrderByCollection orderBy)
        {
            return Service.ListByFilter(filter, orderBy);
        }

        public static IList<T> ListByExample(T example)
        {
            return Service.ListByExample(example);
        }

        public static Page<T> PaginateByFilter(Filter filter, OrderByCollection orderBy, int skip, int take)
        {
            return Service.PaginateByFilter(filter, orderBy, skip, take);
        }

        public static Page<T> PaginateByFilter(Filter filter, int skip, int take)
        {
            return Service.PaginateByFilter(filter, OrderBy.None(), skip, take);
        }

        public static Page<T> PaginateAll(OrderByCollection orderBy, int skip, int take)
        {
            return Service.PaginateAll(orderBy, skip, take);
        }

        public static Page<T> PaginateAll(int skip, int take)
        {
            return Service.PaginateAll(OrderBy.None(), skip, take);
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
            return Service.Persist(ThisAsT);
        }

        public T Save()
        {
            return Service.Save(ThisAsT);
        }

        public T Update()
        {
            return Service.Update(ThisAsT);
        }

        public void Delete()
        {
            Service.Delete(ThisAsT);
        }

        public static void DeleteById(object id)
        {
            Service.DeleteById(id);
        }

        public static int DeleteByFilter(Filter filter)
        {
            return Service.DeleteByFilter(filter);
        }


        public T SaveOrUpdate()
        {
            return Service.SaveOrUpdate(ThisAsT);
        }
    }
}
