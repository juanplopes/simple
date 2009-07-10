using System;
using System.Collections.Generic;

using System.Text;
using Simple.DataAccess;
using Simple.Filters;
using NHibernate;
using Simple.ServiceModel;
using Simple.Logging;
using System.Runtime.Serialization;
using log4net;
using System.Linq;
using Simple.Server;
using Simple.ConfigSource;

namespace Simple.Services
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
                    _logger = LoggerManager.Get(this);
                return _logger;
            }
        }

        protected object ConfigKey
        {
            get
            {
                return DefaultConfigAttribute.GetKey(typeof(T));
            }
        }

        public bool HeartBeat()
        {
            Logger.Debug("Heartbeat: " + typeof(T).Name);
            return true;
        }

        protected virtual D GetDao()
        {
            D dao = new D();
            dao.Session = Simply.Get(ConfigKey).OpenNHSession();
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

        public virtual T SaveOrUpdate(T entity)
        {
            GetDao().SaveOrUpdate(entity);
            return entity;
        }

        public virtual T Save(T entity)
        {
            GetDao().Save(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            GetDao().Update(entity);
            return entity;
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
