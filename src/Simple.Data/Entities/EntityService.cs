using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using log4net;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Metadata;
using Simple.Config;
using Simple.Entities.QuerySpec;
using Simple.Expressions.Editable;
using Simple.Services;
using Simple.Validation;

namespace Simple.Entities
{
    [KnownType(typeof(Page<>))]
    public class EntityService<T> : MarshalByRefObject, IEntityService<T>
    {
        private ILog _logger = null;
        protected virtual ILog Logger
        {
            get
            {
                if (_logger == null)
                    _logger = Simply.Do.Log(this);
                return _logger;
            }
        }

        private IClassMetadata _NHMetadata;
        protected virtual IClassMetadata NHMetadata
        {
            get
            {
                if (_NHMetadata == null)
                    _NHMetadata = Session.SessionFactory.GetClassMetadata(typeof(T));

                return _NHMetadata;
            }
        }


        protected virtual Simply MySimply
        {
            get
            {
                var simply = DefaultConfigAttribute.ApplyKey(typeof(T), Simply.Do);
                return simply;
            }
        }

        protected virtual ISession Session
        {
            get
            {
                return MySimply.GetSession();
            }
        }

        protected virtual SpecBuilder<T> Builder
        {
            get { return new SpecBuilder<T>(); }
        }

        protected virtual IQueryable<Q> Query<Q>()
        {
            return Session.Query<Q>();
        }

        protected virtual IQueryable<T> Query()
        {
            return Query<T>();
        }

        protected virtual bool ValidateOnSave { get { return true; } }
        protected virtual void ValidateAndThrow(object obj)
        {
            var result = MySimply.Validate(obj);
            if (!result.IsValid)
            {
                Session.Evict(obj);
                result.AndThrow();
            }
        }

        public virtual T Load(object id)
        {
            return Session.Load<T>(id);
        }

        public virtual T Refresh(T entity)
        {
            Session.Refresh(entity);
            return entity;
        }

        public virtual T Reload(T entity)
        {
            Session.Flush();
            Session.Evict(entity);
            var id = NHMetadata.GetIdentifier(entity, Session.GetSessionImplementation().EntityMode);
            return Refresh(Load(id));
        }

        public virtual T Merge(T entity)
        {
            return (T)Session.Merge(entity);
        }

        public virtual T Evict(T entity)
        {
            Session.Evict(entity);
            return entity;
        }


        public virtual T Find(SpecBuilder<T> map)
        {
            return Query().ApplySpecs(map).FirstOrDefault();
        }

        public virtual int Count(SpecBuilder<T> map)
        {
            return Query().ApplySpecs(map).Count();
        }

        public virtual IList<T> List(SpecBuilder<T> map)
        {
            return Query().ApplySpecs(map).ToList();
        }

        public virtual IPage<T> List(SpecBuilder<T> map, SpecBuilder<T> reduce)
        {
            var mapped = Query().ApplySpecs(map);

            return new Page<T>(mapped.ApplySpecs(reduce).ToList(), mapped.Count());
        }

        public virtual IPage<T> Linq(LazyExpression<Func<IQueryable<T>, IQueryable<T>>> mapExpression, LazyExpression<Func<IQueryable<T>, IQueryable<T>>> reduceExpression)
        {
            var map = mapExpression.Real.Compile();
            var reduce = reduceExpression.Real.Compile();

            var linq = Query();

            linq = map(linq);
            var count = linq.Count();

            linq = reduce(linq);
            var list = linq.ToList();

            return new Page<T>(list, count);
        }


        public virtual int Delete(object id)
        {
            return Delete((T)Load(id));
        }

        [RequiresTransaction]
        public virtual int Delete(SpecBuilder<T> map)
        {
            int res = 0;
            foreach (var entity in List(map))
            {
                res += Delete((T)entity);
            }
            return res;
        }

        public virtual T SaveOrUpdate(T entity)
        {
            if (ValidateOnSave) ValidateAndThrow(entity);
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public virtual T Save(T entity)
        {
            if (ValidateOnSave) ValidateAndThrow(entity);
            Session.Save(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            if (ValidateOnSave) ValidateAndThrow(entity);
            Session.Update(entity);
            return entity;
        }

        public virtual int Delete(T entity)
        {
            Session.Delete(entity);
            return 1;
        }


        public virtual ValidationList Validate(T entity)
        {
            return MySimply.Validate(entity);
        }

        public virtual ValidationList ValidateProperty(T entity, params string[] props)
        {
            return MySimply.Validate(entity, props);
        }
    }
}
