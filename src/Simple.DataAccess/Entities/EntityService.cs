using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using log4net;
using NHibernate.Linq;
using Simple.Config;
using Simple.DataAccess;
using Simple.Expressions.Editable;
using Simple.Services;
using NHibernate;
using NHibernate.Validator.Engine;
using Simple.Validation;
using NHibernate.Metadata;

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
                return SourceManager.Do.BestKeyOf(SimpleContext.Get().ConfigKey, DefaultConfigAttribute.GetKey(typeof(T)));
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

        #region IEntityService<T> Members

        public virtual T Load(object id)
        {
            return Session.Load<T>(id);
        }

        public virtual T Refresh(T entity)
        {
            Session.SessionFactory.GetClassMetadata(typeof(T)).GetIdentifier(entity, Session.GetSessionImplementation().EntityMode);
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

        protected virtual IQueryable<T> GetDefaultQueriable(EditableExpression filter, OrderBy<T> orderBy)
        {
            IQueryable<T> query = Query();

            if (filter != null)
                query = query.Where((Expression<Func<T, bool>>)filter.ToExpression());

            if (orderBy != null && orderBy.Count > 0)
            {
                IOrderedQueryable<T> tempQuery;
                if (orderBy[0].Backwards)
                    tempQuery = query.OrderByDescending(orderBy[0].ToExpression<T>());
                else
                    tempQuery = query.OrderBy(orderBy[0].ToExpression<T>());

                for (int i = 1; i < orderBy.Count; i++)
                {
                    if (orderBy[i].Backwards)
                        tempQuery = tempQuery.ThenByDescending(orderBy[i].ToExpression<T>());
                    else
                        tempQuery = tempQuery.ThenBy(orderBy[i].ToExpression<T>());

                }
                query = tempQuery;
            }

            return query;
        }

        protected virtual IQueryable<T> SkipAndTake(IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);
            return query;
        }

        public virtual T FindByFilter(Simple.Expressions.Editable.EditableExpression filter, OrderBy<T> order)
        {
            return GetDefaultQueriable(filter, order).FirstOrDefault();
        }

        public virtual IList<T> List(OrderBy<T> order)
        {
            return GetDefaultQueriable(null, order).ToList();
        }

        public virtual IList<T> ListByFilter(Simple.Expressions.Editable.EditableExpression filter, OrderBy<T> order)
        {
            return GetDefaultQueriable(filter, order).ToList();
        }

        public virtual int Count()
        {
            return GetDefaultQueriable(null, null).Count();
        }

        public virtual int CountByFilter(Simple.Expressions.Editable.EditableExpression filter)
        {
            return GetDefaultQueriable(filter, null).Count();
        }

        public virtual IPage<T> Paginate(OrderBy<T> order, int? skip, int? take)
        {
            IQueryable<T> q = GetDefaultQueriable(null, order);

            return new Page<T>(SkipAndTake(q, skip, take).ToList(), q.Count());
        }

        public virtual IPage<T> PaginateByFilter(Simple.Expressions.Editable.EditableExpression filter, OrderBy<T> order, int? skip, int? take)
        {
            IQueryable<T> q = GetDefaultQueriable(filter, order);

            return new Page<T>(SkipAndTake(q, skip, take).ToList(), q.Count());
        }

        public virtual void DeleteById(object id)
        {
            Session.Delete(Load(id));
        }

        [RequiresTransaction]
        public virtual int DeleteByFilter(Simple.Expressions.Editable.EditableExpression filter)
        {
            int res = 0;
            foreach (var entity in ListByFilter(filter, null))
            {
                Session.Delete(entity);
                res++;
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

        public virtual T Persist(T entity)
        {
            if (ValidateOnSave) ValidateAndThrow(entity);
            Session.Persist(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }

        #endregion

        #region IEntityService<T> Members


        public virtual IList<InvalidValue> Validate(T entity)
        {
            return MySimply.Validate(entity);
        }

        public virtual IList<InvalidValue> ValidateProperty(string propName, object value)
        {
            return MySimply.Validate(typeof(T), propName, value);
        }

        #endregion
    }
}
