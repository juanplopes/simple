using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions;
using Simple.Config;
using System.Linq.Expressions;

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
            return Service.Count(filter.ToSerializable());
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

        public static T Find(Expression<Func<T, bool>> filter)
        {
            return Service.Find(filter.ToSerializable(), new OrderBy<T>());
        }

        public static T Find(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Service.Find(filter.ToSerializable(), orderBy(new OrderBy<T>()));
        }

        #region List no order, no filter
        public static IList<T> ListAll()
        {
            return Service.List(null, null, null, null);
        }

        public static IPage<T> ListAll(int top)
        {
            return Service.List(null, null, null, top);
        }

        public static IPage<T> ListAll(int skip, int take)
        {
            return Service.List(null, null, skip, take);
        }
        #endregion

        #region List yes order, no filter
        public static IList<T> ListAll(Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Service.List(null, orderBy(new OrderBy<T>()), null, null);
        }

        public static IPage<T> ListAll(Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Service.List(null, orderBy(new OrderBy<T>()), null, top);
        }

        public static IPage<T> ListAll(Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Service.List(null, orderBy(new OrderBy<T>()), skip, take);
        }
        #endregion

        #region List no order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Service.List(filter.ToSerializable(), null, null, null);
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top)
        {
            return Service.List(filter.ToSerializable(), null, null, top);
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Service.List(filter.ToSerializable(), null, skip, take);
        }
        #endregion

        #region List yes order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Service.List(filter.ToSerializable(), orderBy(new OrderBy<T>()), null, null);
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Service.List(filter.ToSerializable(), orderBy(new OrderBy<T>()), null, top);
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Service.List(filter.ToSerializable(), orderBy(new OrderBy<T>()), skip, take);
        }


        #endregion

        public static IPage<T> Linq(Expression<Func<IQueryable<T>, IQueryable<T>>> map, Expression<Func<IQueryable<T>, IQueryable<T>>> reduce)
        {
            return Service.Linq(map.ToSerializable(), reduce.ToSerializable());
        }

        public static int Delete(Expression<Func<T, bool>> filter)
        {
            return Service.Delete(filter.ToSerializable());
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
