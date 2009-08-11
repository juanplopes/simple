using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Linq.Expressions;
using Simple.Expressions;
using Simple.DataAccess;

namespace Simple.Entities
{
    internal static class ExpressionExtensions
    {
        public static EditableExpression ToEditable(this Expression expr)
        {
            return EditableExpression.CreateEditableExpression(expr);
        }
    }

    public class EntityAccessor<T, R> : AggregateFactory<EntityAccessor<T, R>>
        where R : IEntityService<T>
    {
        protected virtual object DefaultKey
        {
            get
            {
                return DefaultConfigAttribute.GetKey(typeof(T));
            }
        }

        protected R Rules
        {
            get { return Simply.Do[ConfigKey ?? DefaultKey].Resolve<R>(); }
        }

        public R GetService()
        {
            return this.Rules;
        }

        public int Count()
        {
            return Rules.Count();
        }

        public int Count(Expression<Func<T, bool>> filter)
        {
            return Rules.CountByFilter(filter.ToEditable());
        }

        public T Load(object id)
        {
            return Rules.Load(id);
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            return Rules.FindByFilter(filter.ToEditable(), OrderBy.None);
        }

        public T Find(Expression<Func<T, bool>> filter, OrderByCollection orderBy)
        {
            return Rules.FindByFilter(filter.ToEditable(), orderBy);
        }


        public IList<T> List()
        {
            return Rules.List(OrderBy.None);
        }

        public IList<T> List(OrderByCollection orderBy)
        {
            return Rules.List(orderBy);
        }

        public IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Rules.ListByFilter(filter.ToEditable(), OrderBy.None);
        }

        public IList<T> List(Expression<Func<T, bool>> filter, OrderByCollection orderBy)
        {
            return Rules.ListByFilter(filter.ToEditable(), orderBy);
        }

        public Page<T> Paginate(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), OrderBy.None, skip, take);
        }

        public Page<T> Paginate(Expression<Func<T, bool>> filter, OrderByCollection orderBy, int skip, int take)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), orderBy, skip, take);
        }

        public Page<T> Paginate(int skip, int take)
        {
            return Rules.Paginate(OrderBy.None, skip, take);
        }
        public Page<T> Paginate(OrderByCollection orderBy, int skip, int take)
        {
            return Rules.Paginate(orderBy, skip, take);
        }

        public void Delete(object id)
        {
            Rules.DeleteById(id);
        }

        public int Delete(Expression<Func<T, bool>> filter)
        {
            return Rules.DeleteByFilter(filter.ToEditable());
        }

        public T Persist(T entity)
        {
            return Rules.Persist(entity);
        }

        public T Save(T entity)
        {
            return Rules.Save(entity);
        }

        public T Update(T entity)
        {
            return Rules.Update(entity);
        }

        public void Delete(T entity)
        {
            Rules.Delete(entity);
        }

        public T SaveOrUpdate(T entity)
        {
            return Rules.SaveOrUpdate(entity);
        }

    }
}
