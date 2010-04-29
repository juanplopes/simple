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

        protected static SpecBuilder<T> Builder()
        {
            return new SpecBuilder<T>();
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
            return Service.Count(Builder().Filter(filter));
        }


        public static T Find(Expression<Func<T, bool>> filter, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.Find(Builder().Filter(filter).ApplyFuncs(options));
        }

        #region List yes order, no filter
        public static IList<T> ListAll(params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.List(Builder().ApplyFuncs(options));
        }

        public static IPage<T> ListAll(int top, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.Paginate(null, Builder().ApplyFuncs(options).Take(top));
        }

        public static IPage<T> ListAll(int skip, int take, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.Paginate(null, Builder().ApplyFuncs(options).Skip(skip).Take(take));
        }
        #endregion

       
        #region List yes order, yes filter
        public static IList<T> List(Expression<Func<T, bool>> filter, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.List(Builder().Filter(filter).ApplyFuncs(options));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int top, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.Paginate(Builder().Filter(filter), Builder().ApplyFuncs(options).Take(top));
        }

        public static IPage<T> List(Expression<Func<T, bool>> filter, int skip, int take, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return Service.Paginate(Builder().Filter(filter), Builder().ApplyFuncs(options).Skip(skip).Take(take));
        }


        #endregion

        public static IPage<T> Linq(Expression<Func<IQueryable<T>, IQueryable<T>>> map, Expression<Func<IQueryable<T>, IQueryable<T>>> reduce)
        {
            return Service.Linq(map.Funcletize().ToEditableExpression(), reduce.Funcletize().ToEditableExpression());
        }

        public static int Delete(Expression<Func<T, bool>> filter)
        {
            return Service.Delete(Builder().Filter(filter));
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
