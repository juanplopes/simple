using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.DataAccess;
using Simple.Services;

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
                return BestKeyOf(ConfigKey, DefaultConfigAttribute.GetKey(typeof(T)));
            }
        }

        protected R Rules
        {
            get { return 
                Simply.Do[DefaultKey].Resolve<R>(); }
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
            return Rules.FindByFilter(filter.ToEditable(), new OrderBy<T>());
        }

        public T Find(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Rules.FindByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()));
        }

        #region List no order, no filter
        public IList<T> List()
        {
            return Rules.List(new OrderBy<T>());
        }

        public IPage<T> List(int top)
        {
            return Rules.Paginate(new OrderBy<T>(), null, top);
        }

        public IPage<T> List(int skip, int take)
        {
            return Rules.Paginate(new OrderBy<T>(), skip, take);
        }
        #endregion

        #region List yes order, no filter
        public IList<T> List(Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Rules.List(orderBy(new OrderBy<T>()));
        }

        public IPage<T> List(Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Rules.Paginate(orderBy(new OrderBy<T>()), null, top);
        }

        public IPage<T> List(Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Rules.Paginate(orderBy(new OrderBy<T>()), skip, take);
        }
        #endregion

        #region List no order, yes filter
        public IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Rules.ListByFilter(filter.ToEditable(), new OrderBy<T>());
        }

        public IPage<T> List(Expression<Func<T, bool>> filter, int top)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), new OrderBy<T>(), null, top);
        }

        public IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), new OrderBy<T>(), skip, take);
        }
        #endregion

        #region List yes order, yes filter
        public IList<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Rules.ListByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()));
        }

        public IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()), 0, top);
        }

        public IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()), skip, take);
        }

       
        #endregion

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
