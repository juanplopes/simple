﻿using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Filters;
using NHibernate;
using SimpleLibrary.ServiceModel;
using BasicLibrary.Logging;
using System.Runtime.Serialization;
using log4net;

namespace SimpleLibrary.Rules
{
    public class BaseRules<T> : BaseRules<T, BaseDao<T>>
    {

    }

    [KnownType(typeof(Page<>))]
    public class BaseRules<T, D> : IBaseRules<T>
        where D : BaseDao<T>, new()
    {
        private ILog _logger = null;
        protected ILog Logger
        {
            get
            {
                if (_logger == null)
                    _logger = MainLogger.Get(this);
                return _logger;
            }
        }

        bool ITestableService.HeartBeat()
        {
            Logger.Debug("Heartbeat: " + typeof(T).Name);
            return true;
        }

        protected virtual D GetDao()
        {
            return new D();
        }

        public virtual T Load(object id)
        {
            return GetDao().Load(id);
        }

        public virtual T LoadByExample(T example)
        {
            return GetDao().LoadByExample(example);
        }

        public virtual T LoadByFilter(Filter filter)
        {
            return CreateCriteriaByFilter(filter, null).UniqueResult<T>();
        }

        public virtual IList<T> ListByExample(T example)
        {
            return GetDao().ListByExample(example);
        }

        public virtual IList<T> ListByFilter(Filter filter, OrderByCollection order)
        {
            return CreateCriteriaByFilter(filter, order).List<T>();
        }

        public virtual int CountByFilter(Filter filter)
        {
            return CriteriaTransformer.TransformToRowCount(
                CreateCriteriaByFilter(filter, OrderBy.None())).UniqueResult<int>();
        }

        public virtual Page<T> PaginateByFilter(Filter filter, OrderByCollection order, int skip, int take)
        {
            ICriteria criteria = CreateCriteriaByFilter(filter, order);
            return Extensions.Paginate<T>(criteria, skip, take);
        }

        protected virtual ICriteria CreateCriteriaByFilter(Filter filter, OrderByCollection order)
        {
            D dao = GetDao();
            return dao.ToCriteria(filter, order);
        }

        public virtual void SaveOrUpdate(T entity)
        {
            GetDao().SaveOrUpdate(entity);
        }

        public virtual void Save(T entity)
        {
            GetDao().Save(entity);
        }

        public virtual void Update(T entity)
        {
            GetDao().Update(entity);
        }

        public virtual T Persist(T entity)
        {
            GetDao().Persist(entity);
            return entity;
        }

        public IList<T> ListAll(OrderByCollection order)
        {
            return this.ListByFilter(BooleanExpression.True, order);
        }

        public Page<T> PaginateAll(OrderByCollection order, int skip, int take)
        {
            return this.PaginateByFilter(BooleanExpression.True, order, skip, take);
        }

        public virtual void Delete(T entity)
        {
            GetDao().Delete(entity);
        }

        public virtual void DeleteById(object id)
        {
            GetDao().DeleteById(id);
        }

        public virtual int DeleteByFilter(Filter filter)
        {
            ICriteria criteria = CreateCriteriaByFilter(filter, null);
            return GetDao().DeleteByCriteria(criteria);
        }

    }
}
