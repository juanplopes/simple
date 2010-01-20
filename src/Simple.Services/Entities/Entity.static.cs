using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions;
using Simple.Config;
using System.Linq.Expressions;
using Simple.DataAccess;

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
            return Service.Count();
        }

        public static int Count(Expression<Func<T, bool>> filter)
        {
            return Service.CountByFilter(filter.ToSerializable());
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
            return entity.Reload();
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
            return Service.FindByFilter(filter.ToSerializable(), new OrderBy<T>());
        }

        public static T Find(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Service.FindByFilter(filter.ToSerializable(), orderBy(new OrderBy<T>()));
        }

        #region List no order, no filter
        public static IList<T> ListAll()
        {
            return Service.List(new OrderBy<T>());
        }

        public static IPage<T> ListAll(int top)
        {
            return Service.Paginate(new OrderBy<T>(), null, top);
        }

        public static IPage<T> ListAll(int skip, int take)
        {
            return Service.Paginate(new OrderBy<T>(), skip, take);
        }
        #endregion


        #region List yes order, no filter
        public static IList<T> ListAll(Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Service.List(orderBy(new OrderBy<T>()));
        }

        public static IPage<T> ListAll(Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Service.Paginate(orderBy(new OrderBy<T>()), null, top);
        }

        public static IPage<T> ListAll(Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Service.Paginate(orderBy(new OrderBy<T>()), skip, take);
        }
        #endregion

        #region List no order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Service.ListByFilter(filter.ToSerializable(), new OrderBy<T>());
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top)
        {
            return Service.PaginateByFilter(filter.ToSerializable(), new OrderBy<T>(), null, top);
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Service.PaginateByFilter(filter.ToSerializable(), new OrderBy<T>(), skip, take);
        }
        #endregion

        #region List yes order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy)
        {
            return Service.ListByFilter(filter.ToSerializable(), orderBy(new OrderBy<T>()));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int top)
        {
            return Service.PaginateByFilter(filter.ToSerializable(), orderBy(new OrderBy<T>()), 0, top);
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, Func<OrderBy<T>, OrderBy<T>> orderBy, int skip, int take)
        {
            return Service.PaginateByFilter(filter.ToSerializable(), orderBy(new OrderBy<T>()), skip, take);
        }


        #endregion


        public static int Delete(Expression<Func<T, bool>> filter)
        {
            return Service.DeleteByFilter(filter.ToSerializable());
        }

        public static void Delete(object id)
        {
            Service.DeleteById(id);
        }

        public static void Delete(T entity)
        {
            Service.Delete(entity);
        }


    }
}
