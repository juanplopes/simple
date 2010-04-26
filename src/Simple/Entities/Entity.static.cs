using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions;
using Simple.Config;
using System.Linq.Expressions;
using Simple.Expressions.Editable;

namespace Simple.Entities
{
    public partial class Entity<T, R>
    {
        public static R Service
        {
            get
            {
                return
                    Simply.Do[DefaultKey].Resolve<R>();
            }
        }

        protected static object DefaultKey
        {
            get
            {
                return SourceManager.Do.BestKeyOf(DefaultConfigAttribute.GetKey(typeof(T)));
            }
        }

        public static int Count()
        {
            return Service.Count(null);
        }

        public static int Count(Expression<Func<T, bool>> filter)
        {
            return Service.Count(filter.Funcletize().ToEditableExpression());
        }

        public static T Load(object id)
        {
            return Service.Load(id);
        }

        public static T Refresh(T entity)
        {
            return Service.Refresh(entity);
        }

        public static T Reload(T entity)
        {
            return Service.Reload(entity);
        }

        public static T Merge(T entity)
        {
            return Service.Merge(entity);
        }

        public static T Evict(T entity)
        {
            return Service.Evict(entity);
        }

        public static T Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] fetch)
        {
            return Service.Find(filter.Funcletize().ToEditableExpression(), new OrderBy<T>(), fetch.ToEditable());
        }

        public static T Find(Expression<Func<T, bool>> filter, OrderBy<T> orderBy, params Expression<Func<T, object>>[] fetch)
        {
            return Service.Find(filter.Funcletize().ToEditableExpression(), orderBy, fetch.ToEditable());
        }

        public static OrderBy<T> OrderBy(Expression<Func<T, object>> expr)
        {
            return new OrderBy<T>().Asc(expr);
        }

        public static OrderBy<T> OrderByDesc(Expression<Func<T, object>> expr)
        {
            return new OrderBy<T>().Desc(expr);
        }

        #region List no order, no filter
        public static IList<T> ListAll(params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(null, null, null, null, fetch.ToEditable());
        }

        public static IPage<T> ListAll(int top, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(null, null, null, top, fetch.ToEditable());
        }

        public static IPage<T> ListAll(int skip, int take, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(null, null, skip, take, fetch.ToEditable());
        }
        #endregion

        #region List yes order, no filter
        public static IList<T> ListAll(OrderBy<T> orderBy, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(null, orderBy, null, null, fetch.ToEditable());
        }

        public static IPage<T> ListAll(OrderBy<T> orderBy, int top, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(null, orderBy, null, top, fetch.ToEditable());
        }

        public static IPage<T> ListAll(OrderBy<T> orderBy, int skip, int take, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(null, orderBy, skip, take, fetch.ToEditable());
        }
        #endregion

        #region List no order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(filter.Funcletize().ToEditableExpression(), null, null, null, fetch.ToEditable());
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(filter.Funcletize().ToEditableExpression(), null, null, top, fetch.ToEditable());
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(filter.Funcletize().ToEditableExpression(), null, skip, take, fetch.ToEditable());
        }
        #endregion

        #region List yes order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter, OrderBy<T> orderBy, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(filter.Funcletize().ToEditableExpression(), orderBy, null, null, fetch.ToEditable());
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, OrderBy<T> orderBy, int top, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(filter.Funcletize().ToEditableExpression(), orderBy, null, top, fetch.ToEditable());
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, OrderBy<T> orderBy, int skip, int take, params Expression<Func<T, object>>[] fetch)
        {
            return Service.List(filter.Funcletize().ToEditableExpression(), orderBy, skip, take, fetch.ToEditable());
        }


        #endregion

        public static IPage<T> Linq(Expression<Func<IQueryable<T>, IQueryable<T>>> map, Expression<Func<IQueryable<T>, IQueryable<T>>> reduce)
        {
            return Service.Linq(map.Funcletize().ToEditableExpression(), reduce.Funcletize().ToEditableExpression());
        }

        public static int Delete(Expression<Func<T, bool>> filter)
        {
            return Service.Delete(filter.Funcletize().ToEditableExpression());
        }

        public static void Delete(object id)
        {
            Service.Delete(id);
        }

        public static void Delete(T entity)
        {
            Service.Delete(entity);
        }


    }
}
