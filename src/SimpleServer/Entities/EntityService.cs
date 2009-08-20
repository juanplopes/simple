using System;
using System.Collections.Generic;

using System.Text;
using Simple.DataAccess;
using NHibernate;
using Simple.ServiceModel;
using Simple.Logging;
using System.Runtime.Serialization;
using log4net;
using System.Linq;
using Simple.ConfigSource;
using System.Runtime.Remoting.Proxies;
using System.Linq.Expressions;
using Simple.Services;
using Simple.Expressions;

namespace Simple.Entities
{
    public class EntityService<T> : EntityService<T, EntityDao<T>>
    {

    }

    [KnownType(typeof(Page<>))]
    public class EntityService<T, D> : MarshalByRefObject, IEntityService<T>
        where D : EntityDao<T>, new()
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

        protected virtual D GetDao()
        {
            D dao = new D();
            dao.Session = Simply.Do[ConfigKey].GetSession();
            return dao;
        }

        protected virtual IOrderedQueryable<Q> Linq<Q>()
        {
            return GetDao().Linq<Q>();
        }

        protected virtual IOrderedQueryable<T> Linq()
        {
            return GetDao().Linq();
        }

        //public bool HeartBeat()
        //{
        //    Logger.Debug("Heartbeat: " + typeof(T).Name);
        //    return true;
        //}

       

        //public virtual T Load(object id)
        //{
        //    return GetDao().Load(id);
        //}

        //public virtual T LoadByExample(T example)
        //{
        //    return GetDao().LoadByExample(example);
        //}

        //public virtual T LoadByFilter(Filter filter)
        //{
        //    return CreateCriteriaByFilter(filter, null).UniqueResult<T>();
        //}

        //public virtual IList<T> ListByExample(T example)
        //{
        //    return GetDao().ListByExample(example);
        //}

        //public virtual IList<T> ListByFilter(Filter filter, OrderByCollection order)
        //{
        //    return CreateCriteriaByFilter(filter, order).List<T>();
        //}

        //public virtual int CountAll()
        //{
        //    return CountByFilter(BooleanExpression.True);
        //}

        //public virtual int CountByFilter(Filter filter)
        //{
        //    return CriteriaTransformer.TransformToRowCount(
        //        CreateCriteriaByFilter(filter, OrderBy.None())).UniqueResult<int>();
        //}

        //public virtual Page<T> PaginateByFilter(Filter filter, OrderByCollection order, int skip, int take)
        //{
        //    ICriteria criteria = CreateCriteriaByFilter(filter, order);
        //    return Extensions.Paginate<T>(criteria, skip, take);
        //}

        //public virtual Page<T> PaginateAll(OrderByCollection order, int skip, int take)
        //{
        //    return this.PaginateByFilter(BooleanExpression.True, order, skip, take);
        //}
        //protected virtual ICriteria CreateCriteriaByFilter(Filter filter, OrderByCollection order)
        //{
        //    D dao = GetDao();
        //    return dao.ToCriteria(filter, order);
        //}

        //public virtual T SaveOrUpdate(T entity)
        //{
        //    GetDao().SaveOrUpdate(entity);
        //    return entity;
        //}

        //public virtual T Save(T entity)
        //{
        //    GetDao().Save(entity);
        //    return entity;
        //}

        //public virtual T Update(T entity)
        //{
        //    GetDao().Update(entity);
        //    return entity;
        //}

        //public virtual T Persist(T entity)
        //{
        //    GetDao().Persist(entity);
        //    return entity;
        //}

        //public IList<T> ListAll(OrderByCollection order)
        //{
        //    return this.ListByFilter(BooleanExpression.True, order);
        //}

        //public virtual void Delete(T entity)
        //{
        //    GetDao().Delete(entity);
        //}

        //public virtual void DeleteById(object id)
        //{
        //    GetDao().DeleteById(id);
        //}

        //public virtual int DeleteByFilter(Filter filter)
        //{
        //    ICriteria criteria = CreateCriteriaByFilter(filter, null);
        //    return GetDao().DeleteByCriteria(criteria);
        //}

        //#region IEntityService<T> Members


        //public T LoadByExpression(Simple.Expressions.EditableExpression expression)
        //{
        //    var whereClause = (Expression<Func<T, bool>>)expression.ToExpression();
        //    return Linq().Where(whereClause).FirstOrDefault();
        //}

        //#endregion

        #region IEntityService<T> Members

        public T Load(object id)
        {
            return GetDao().Load(id);
        }

        protected IQueryable<T> GetDefaultQueriable(EditableExpression filter, OrderBy<T> orderBy, int? skip, int? take)
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

            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public T FindByFilter(Simple.Expressions.EditableExpression filter, OrderBy<T> order)
        {
            return GetDefaultQueriable(filter, order, null, null).FirstOrDefault();
        }

        public IList<T> List(OrderBy<T> order)
        {
            return GetDefaultQueriable(null, order, null, null).ToList();
        }

        public IList<T> ListByFilter(Simple.Expressions.EditableExpression filter, OrderBy<T> order)
        {
            return GetDefaultQueriable(filter, order, null, null).ToList();
        }

        public int Count()
        {
            return GetDefaultQueriable(null, null, null, null).Count();
        }

        public int CountByFilter(Simple.Expressions.EditableExpression filter)
        {
            return GetDefaultQueriable(filter, null, null, null).Count();
        }

        public Page<T> Paginate(OrderBy<T> order, int? skip, int? take)
        {
            IQueryable<T> q = GetDefaultQueriable(null, order, skip, take);

            return new Page<T>(q.ToList(), q.Count());
        }

        public Page<T> PaginateByFilter(Simple.Expressions.EditableExpression filter, OrderBy<T> order, int? skip, int? take)
        {
            IQueryable<T> q = GetDefaultQueriable(filter, order, skip, take);

            return new Page<T>(q.ToList(), q.Count());
        }

        public void DeleteById(object id)
        {
            GetDao().Delete(Load(id));
        }

        public int DeleteByFilter(Simple.Expressions.EditableExpression filter)
        {
            int res = 0;
            foreach (var entity in ListByFilter(filter, null))
            {
                GetDao().Delete(entity);
                res++;
            }
            return res;
        }

        public T SaveOrUpdate(T entity)
        {
            GetDao().SaveOrUpdate(entity);
            return entity;
        }

        public T Save(T entity)
        {
            GetDao().Save(entity);
            return entity;
        }

        public T Update(T entity)
        {
            GetDao().Update(entity);
            return entity;
        }

        public T Persist(T entity)
        {
            GetDao().Persist(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            GetDao().Delete(entity);
        }

        #endregion
    }
}
