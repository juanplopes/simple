using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using log4net;
using NHibernate.Linq;
using Simple.Config;
using Simple.Data;
using Simple.Expressions.Editable;
using Simple.Services;
using NHibernate;
using Simple.Validation;
using NHibernate.Metadata;
using FluentValidation.Results;

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


        protected virtual object ConfigKey
        {
            get
            {
                return DefaultConfigAttribute.GetKey(typeof(T));
            }
        }

        protected virtual Simply MySimply
        {
            get
            {
                return Simply.Do[ConfigKey];
            }
        }

        protected virtual ISession Session
        {
            get
            {
                return MySimply.GetSession();
            }
        }

        protected virtual IQueryable<T> GetDefaultQueriable(EditableExpression<Func<T, bool>> filter, OrderBy<T> orderBy, IList<EditableExpression<Func<T, object>>> fetch)
        {
            IQueryable<T> query = Query();

            if (filter != null)
                query = query.Where(filter.ToTypedLambda());

            if (fetch != null)
                foreach (var fetchItem in fetch)
                    query = query.Fetch(fetchItem.ToTypedLambda());

            if (orderBy != null && orderBy.Count > 0)
            {
                IOrderedQueryable<T> tempQuery;
                if (orderBy[0].Backwards)
                    tempQuery = query.OrderByDescending(orderBy[0].ToExpression());
                else
                    tempQuery = query.OrderBy(orderBy[0].ToExpression());

                for (int i = 1; i < orderBy.Count; i++)
                {
                    if (orderBy[i].Backwards)
                        tempQuery = tempQuery.ThenByDescending(orderBy[i].ToExpression());
                    else
                        tempQuery = tempQuery.ThenBy(orderBy[i].ToExpression());

                }
                query = tempQuery;
            }

            return query;
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
            MySimply.Validate(obj).AndThrow();
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
            return Load(NHMetadata.GetIdentifier(entity, Session.GetSessionImplementation().EntityMode));
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

        protected virtual IQueryable<T> SkipAndTake(IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);
            return query;
        }

        public virtual T Find(EditableExpression<Func<T, bool>> filter, OrderBy<T> order, IList<EditableExpression<Func<T, object>>> fetch)
        {
            return GetDefaultQueriable(filter, order, fetch).FirstOrDefault();
        }

        public virtual int Count(EditableExpression<Func<T, bool>> filter)
        {
            return GetDefaultQueriable(filter, null, null).Count();
        }

        public virtual IPage<T> Linq(EditableExpression<Func<IQueryable<T>, IQueryable<T>>> mapExpression, EditableExpression<Func<IQueryable<T>, IQueryable<T>>> reduceExpression)
        {
            var map = mapExpression.ToTypedLambda().Compile();
            var reduce = reduceExpression.ToTypedLambda().Compile();

            var linq = Query();

            linq = map(linq);
            var count = linq.Count();

            linq = reduce(linq);
            var list = linq.ToList();

            return new Page<T>(list, count);
        }

        public virtual IPage<T> List(EditableExpression<Func<T, bool>> filter, OrderBy<T> order, int? skip, int? take, IList<EditableExpression<Func<T, object>>> fetch)
        {
            IQueryable<T> q = GetDefaultQueriable(filter, order, fetch);

            return new Page<T>(SkipAndTake(q, skip, take).ToList(), q.Count());
        }

        public virtual void Delete(object id)
        {
            Session.Delete(Load(id));
            Session.Flush();
        }

        [RequiresTransaction]
        public virtual int Delete(EditableExpression<Func<T, bool>> filter)
        {
            int res = 0;
            foreach (var entity in List(filter, null, null, null, null))
            {
                Session.Delete(entity);
                res++;
            }
            Session.Flush();
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

        public virtual T Persist(T entity)
        {
            if (ValidateOnSave) ValidateAndThrow(entity);
            Session.Persist(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
            Session.Flush();
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
