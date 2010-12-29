using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Simple.Config;
using Simple.Entities.QuerySpec;
using Simple.Expressions.Editable;
using System.Collections;
using Simple.Common;

namespace Simple.Entities
{
    public partial class Entity<T, R>
    {
        public static R Service
        {
            get
            {
                return MySimply.Resolve<R>();
            }
        }

        public static R Do
        {
            get { return Service; }
        }

        protected static Simply MySimply
        {
            get
            {
                var simply = DefaultConfigAttribute.ApplyKey(typeof(T), Simply.Do);
                return simply;
            }
        }
        public static SpecBuilder<T> Query
        {
            get { return new SpecBuilder<T>(); }
        }

        public static T Load(object id)
        {
            return Service.Load(id);
        }

        public static T Load(object id, bool upgradeLock)
        {
            return Service.Load(id, upgradeLock);
        }

        public static IList<T> LoadMany(IEnumerable ids)
        {
            return Service.LoadMany(new LazyEnumerable(ids));
        }

        public static int Count()
        {
            return Service.Count(null);
        }

        public static int Count(Expression<Func<T, bool>> filter)
        {
            return Service.Count(Query.Filter(filter));
        }

        public static T Find(Expression<Func<T, bool>> filter)
        {
            return Service.Find(Query.Filter(filter));
        }

        public static T Find(Expression<Func<T, bool>> filter, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.Find(Query.Filter(filter).ApplyFuncs(options));
        }

        #region List no filter, no options
        public static IList<T> ListAll()
        {
            return Service.List(Query);
        }

        public static IPage<T> ListAll(int top)
        {
            return Service.List(null, Query.Take(top));
        }

        public static IPage<T> ListAll(int skip, int take)
        {
            return Service.List(null, Query.Skip(skip).Take(take));
        }
        #endregion

        #region List no filter, yes options
        public static IList<T> ListAll(Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(Query.ApplyFuncs(options));
        }

        public static IPage<T> ListAll(Func<SpecBuilder<T>, SpecBuilder<T>> options, int top)
        {
            return Service.List(null, Query.ApplyFuncs(options).Take(top));
        }

        public static IPage<T> ListAll(Func<SpecBuilder<T>, SpecBuilder<T>> options, int skip, int take)
        {
            return Service.List(null, Query.ApplyFuncs(options).Skip(skip).Take(take));
        }
        #endregion

        #region List yes filter, no options
        public static IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Service.List(Query.Filter(filter));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top)
        {
            return Service.List(Query.Filter(filter), Query.Take(top));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Service.List(Query.Filter(filter), Query.Skip(skip).Take(take));
        }
        #endregion

        #region List yes filter, yes options
        public static IList<T> List(Expression<Func<T, bool>> filter, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(Query.Filter(filter).ApplyFuncs(options));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, Func<SpecBuilder<T>, SpecBuilder<T>> options, int top)
        {
            return Service.List(Query.Filter(filter), Query.ApplyFuncs(options).Take(top));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, Func<SpecBuilder<T>, SpecBuilder<T>> options, int skip, int take)
        {
            return Service.List(Query.Filter(filter), Query.ApplyFuncs(options).Skip(skip).Take(take));
        }
        #endregion

        public static IList<T> List(SpecBuilder<T> map)
        {
            return Service.List(map);
        }

        public static IPage<T> List(SpecBuilder<T> map, SpecBuilder<T> reduce)
        {
            return Service.List(map, reduce);
        }

        public static IPage<T> Linq(Expression<Func<IQueryable<T>, IQueryable<T>>> map, Expression<Func<IQueryable<T>, IQueryable<T>>> reduce)
        {
            return Service.List(map.ToSpec(), reduce.ToSpec());
        }

        public static int Delete(Expression<Func<T, bool>> filter)
        {
            return Service.Delete(Query.Filter(filter));
        }

        public static int Delete(object id)
        {
            return Service.Delete(id);
        }

        public static int Delete(T entity)
        {
            return Service.Delete(entity);
        }


    }
}
