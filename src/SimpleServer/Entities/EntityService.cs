using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using log4net;
using NHibernate.Linq;
using Simple.ConfigSource;
using Simple.DataAccess;
using Simple.Expressions.Editable;
using Simple.Services;
using NHibernate;

namespace Simple.Entities
{
    [KnownType(typeof(Page<>))]
    public class EntityService<T> : MarshalByRefObject, IEntityService<T>
    {
        private ILog _logger = null;
        protected ILog Logger
        {
            get
            {
                if (_logger == null)
                    _logger = Simply.Do.Log(this);
                return _logger;
            }
        }

        protected object ConfigKey
        {
            get
            {
                return SourceManager.Do.BestKeyOf(SimpleContext.Get().ConfigKey, DefaultConfigAttribute.GetKey(typeof(T)));
            }
        }

        protected ISession Session
        {
            get
            {
                return Simply.Do[ConfigKey].GetSession();
            }
        }

        protected virtual IOrderedQueryable<Q> Linq<Q>()
        {
            return Session.Linq<Q>();
        }

        protected virtual IOrderedQueryable<T> Linq()
        {
            return Linq<T>();
        }

        #region IEntityService<T> Members

        public T Load(object id)
        {
            return Session.Load<T>(id);
        }

        public T Refresh(T entity)
        {
            Session.Refresh(entity);
            return entity;
        }

        public T Merge(T entity)
        {
            return (T)Session.Merge(entity);
        }

        protected IQueryable<T> GetDefaultQueriable(EditableExpression filter, OrderBy<T> orderBy)
        {
            IQueryable<T> query = Linq();

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

        protected IQueryable<T> SkipAndTake(IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);
            return query;
        }

        public T FindByFilter(Simple.Expressions.Editable.EditableExpression filter, OrderBy<T> order)
        {
            return GetDefaultQueriable(filter, order).FirstOrDefault();
        }

        public IList<T> List(OrderBy<T> order)
        {
            return GetDefaultQueriable(null, order).ToList();
        }

        public IList<T> ListByFilter(Simple.Expressions.Editable.EditableExpression filter, OrderBy<T> order)
        {
            return GetDefaultQueriable(filter, order).ToList();
        }

        public int Count()
        {
            return GetDefaultQueriable(null, null).Count();
        }

        public int CountByFilter(Simple.Expressions.Editable.EditableExpression filter)
        {
            return GetDefaultQueriable(filter, null).Count();
        }

        public IPage<T> Paginate(OrderBy<T> order, int? skip, int? take)
        {
            IQueryable<T> q = GetDefaultQueriable(null, order);

            return new Page<T>(SkipAndTake(q, skip, take).ToList(), q.Count());
        }

        public IPage<T> PaginateByFilter(Simple.Expressions.Editable.EditableExpression filter, OrderBy<T> order, int? skip, int? take)
        {
            IQueryable<T> q = GetDefaultQueriable(filter, order);

            return new Page<T>(SkipAndTake(q, skip, take).ToList(), q.Count());
        }

        public void DeleteById(object id)
        {
            Session.Delete(Load(id));
            Session.Flush();
        }

        public int DeleteByFilter(Simple.Expressions.Editable.EditableExpression filter)
        {
            int res = 0;
            foreach (var entity in ListByFilter(filter, null))
            {
                Session.Delete(entity);
                res++;
            }
            Session.Flush();
            return res;
        }

        public T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            Session.Flush();
            return entity;
        }

        public T Save(T entity)
        {
            Session.Save(entity);
            Session.Flush();
            return entity;
        }

        public T Update(T entity)
        {
            Session.Update(entity);
            Session.Flush();
            return entity;
        }

        public T Persist(T entity)
        {
            Session.Persist(entity);
            Session.Flush();
            return entity;
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
            Session.Flush();
        }

        #endregion
    }
}
