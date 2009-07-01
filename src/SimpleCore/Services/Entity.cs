using System;
using System.Collections.Generic;
using System.Text;
using Simple.Filters;
using Simple.DataAccess;

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
                return RulesFactory.Create<R>();
            }
        }

        protected static void EnsureThisType(Entity<T, R> entity)
        {
            if (!(entity is T)) throw new InvalidOperationException("Assertion failure. No idea what's happening.");
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

        public static Page<T> PageByFilter(Filter filter, OrderByCollection orderBy, int skip, int take)
        {
            return Rules.PaginateByFilter(filter, orderBy, skip, take);
        }

        public static IList<T> ListByExample(T entity)
        {
            return Rules.ListByExample(entity);
        }

        public static int CountByFilter(Filter filter)
        {
            return Rules.CountByFilter(filter);
        }

        public static void Delete(T entity)
        {
            Rules.Delete(entity);
        }

        public static int DeleteByFilter(Filter filter)
        {
            return Rules.DeleteByFilter(filter);
        }

        public static void DeleteById(object id)
        {
            Rules.DeleteById(id);
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
            return Rules.Persist(ThisAsT);
        }

        public virtual void Save()
        {
            Rules.Save(ThisAsT);
        }

        public virtual void Update()
        {
            Rules.Update(ThisAsT);
        }

        public virtual void Delete()
        {
            Rules.Delete(ThisAsT);
        }

        public virtual void SaveOrUpdate()
        {
            Rules.SaveOrUpdate(ThisAsT);
        }
    }
}
