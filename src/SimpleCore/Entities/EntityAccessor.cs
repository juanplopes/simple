using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.DataAccess;
using Simple.Services;
using NHibernate.Validator.Engine;
using Simple.Expressions;

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

        protected virtual R Rules
        {
            get { return 
                Simply.Do[DefaultKey].Resolve<R>(); }
        }

        public virtual R GetService()
        {
            return this.Rules;
        }

        public virtual int Count()
        {
            return Rules.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> filter)
        {
            return Rules.CountByFilter(filter.ToEditable());
        }

        public virtual T Load(object id)
        {
            return Rules.Load(id);
        }

        public virtual T Refresh(T entity)
        {
            return Rules.Refresh(entity);
        }

        public virtual T Merge(T entity)
        {
            return Rules.Merge(entity);
        }

        public virtual T Find(Expression<Func<T, bool>> filter)
        {
            return Rules.FindByFilter(filter.ToEditable(), new OrderBy<T>());
        }

        public virtual T Find(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Rules.FindByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()));
        }

        #region List no order, no filter
        public virtual IList<T> List()
        {
            return Rules.List(new OrderBy<T>());
        }

        public virtual IPage<T> List(int top)
        {
            return Rules.Paginate(new OrderBy<T>(), null, top);
        }

        public virtual IPage<T> List(int skip, int take)
        {
            return Rules.Paginate(new OrderBy<T>(), skip, take);
        }
        #endregion

        #region List yes order, no filter
        public virtual IList<T> List(Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Rules.List(orderBy(new OrderBy<T>()));
        }

        public virtual IPage<T> List(Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Rules.Paginate(orderBy(new OrderBy<T>()), null, top);
        }

        public virtual IPage<T> List(Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Rules.Paginate(orderBy(new OrderBy<T>()), skip, take);
        }
        #endregion

        #region List no order, yes filter
        public virtual IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Rules.ListByFilter(filter.ToEditable(), new OrderBy<T>());
        }

        public virtual IPage<T> List(Expression<Func<T, bool>> filter, int top)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), new OrderBy<T>(), null, top);
        }

        public virtual IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), new OrderBy<T>(), skip, take);
        }
        #endregion

        #region List yes order, yes filter
        public virtual IList<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Rules.ListByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()));
        }

        public virtual IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()), 0, top);
        }

        public virtual IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Rules.PaginateByFilter(filter.ToEditable(), orderBy(new OrderBy<T>()), skip, take);
        }

       
        #endregion

        public virtual void Delete(object id)
        {
            Rules.DeleteById(id);
        }

        public virtual int Delete(Expression<Func<T, bool>> filter)
        {
            return Rules.DeleteByFilter(filter.ToEditable());
        }

        public virtual T Persist(T entity)
        {
            return Rules.Persist(entity);
        }

        public virtual T Save(T entity)
        {
            return Rules.Save(entity);
        }

        public virtual T Update(T entity)
        {
            return Rules.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            Rules.Delete(entity);
        }

        public virtual T SaveOrUpdate(T entity)
        {
            return Rules.SaveOrUpdate(entity);
        }

        public virtual IList<InvalidValue> Validate(T entity)
        {
            return Rules.Validate(entity);
        }

        public virtual IList<InvalidValue> Validate(string propName, object value)
        {
            return Rules.ValidateProperty(propName, value);
        }

        public virtual IList<InvalidValue> Validate<P>(Expression<Func<T, P>> expr, P value)
        {
            return Rules.ValidateProperty(ExpressionHelper.GetMemberName(expr), value);
        }

    }
}
