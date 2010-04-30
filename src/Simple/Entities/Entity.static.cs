using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions;
using Simple.Config;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Entities.QuerySpec;

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

        public static SpecBuilder<T> Do
        {
            get { return new SpecBuilder<T>(); }
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

        public static int Count()
        {
            return Service.Count(null);
        }

        public static int Count(Expression<Func<T, bool>> filter)
        {
            return Service.Count(Do.Filter(filter));
        }

        public static T Find(Expression<Func<T, bool>> filter)
        {
            return Service.Find(Do.Filter(filter));
        }

        public static T Find(Expression<Func<T, bool>> filter, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.Find(Do.Filter(filter).ApplyFuncs(options));
        }

        #region List no filter, no options
        public static IList<T> ListAll()
        {
            return Service.List(Do);
        }

        public static IPage<T> ListAll(int top)
        {
            return Service.List(null, Do.Take(top));
        }

        public static IPage<T> ListAll(int skip, int take)
        {
            return Service.List(null, Do.Skip(skip).Take(take));
        }
        #endregion

        #region List no filter, yes options
        public static IList<T> ListAll(Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(Do.ApplyFuncs(options));
        }

        public static IPage<T> ListAll(int top, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(null, Do.ApplyFuncs(options).Take(top));
        }

        public static IPage<T> ListAll(int skip, int take, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(null, Do.ApplyFuncs(options).Skip(skip).Take(take));
        }
        #endregion

        #region List yes filter, no options
        public static IList<T> List(Expression<Func<T, bool>> filter)
        {
            return Service.List(Do.Filter(filter));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top)
        {
            return Service.List(Do.Filter(filter), Do.Take(top));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take)
        {
            return Service.List(Do.Filter(filter), Do.Skip(skip).Take(take));
        }
        #endregion

        #region List yes filter, yes options
        public static IList<T> List(Expression<Func<T, bool>> filter, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(Do.Filter(filter).ApplyFuncs(options));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(Do.Filter(filter), Do.ApplyFuncs(options).Take(top));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take, Func<SpecBuilder<T>, SpecBuilder<T>> options)
        {
            return Service.List(Do.Filter(filter), Do.ApplyFuncs(options).Skip(skip).Take(take));
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
            return Service.Linq(map.Funcletize().ToLazyExpression(), reduce.Funcletize().ToLazyExpression());
        }

        public static int Delete(Expression<Func<T, bool>> filter)
        {
            return Service.Delete(Do.Filter(filter));
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
