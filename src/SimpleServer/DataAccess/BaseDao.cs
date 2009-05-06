using System;
using System.Collections.Generic;

using System.Text;
using NHibernate;
using NHibernate.Impl;
using NHibernate.Engine;
using System.Collections;
using NHibernate.Criterion;
using Simple.Config;
using Simple.Filters;


namespace Simple.DataAccess
{
    /// <summary>
    /// Untyped base DAO class. Session holder.
    /// </summary>
    public class BaseDao
    {
        public ISession Session { get; private set; }

        public BaseDao(ISession session)
        {
            Session = session;
        }

        /// <summary>
        /// Creates a DAO instance getting session from pool.
        /// </summary>
        public BaseDao() : this(SessionManager.GetSession()) { }
        public BaseDao(string factoryName) : this(SessionManager.GetSession(factoryName)) { }
        public BaseDao(bool forceNewSession) : this(SessionManager.GetSession(forceNewSession)) { }
        public BaseDao(string factoryName, bool forceNewSession) : this(SessionManager.GetSession(factoryName, forceNewSession)) { }


        /// <summary>
        /// Creates a DAO instance using session from another DAO instance.
        /// </summary>
        /// <param name="previousDao">DAO reference</param>
        public BaseDao(BaseDao previousDao) : this(previousDao.Session) { }
    }

    public class BaseDao<T> : BaseDao
    {
        public BaseDao() : base() { }
        public BaseDao(ISession session) : base(session) { }
        public BaseDao(BaseDao previousDao) : base(previousDao) { }
        public BaseDao(string factoryName) : base(factoryName) { }
        public BaseDao(bool forceNewSession) : base(forceNewSession) { }
        public BaseDao(string factoryName, bool forceNewSession) : base(factoryName, forceNewSession) { }

        protected static class Nested
        {
            public static SimpleConfig Config = SimpleConfig.Get();
        }

        //public T Unproxy(T entity)
        //{
        //    return (T)EntityHelper.Unproxy(entity, Session.GetSessionImplementation().PersistenceContext);
        //}

        //public IList<T> UnproxyList(ICollection<T> colInput)
        //{
        //    IList<T> colOutput = new List<T>(colInput.Count);

        //    foreach (T t in colInput)
        //    {
        //        colOutput.Add(Unproxy(t));
        //    }

        //    return colOutput;
        //}

        public ICriteria ToCriteria(Filter filter, OrderByCollection order)
        {
            ICriteria criteria = this.CreateCriteria();
            criteria.Add(CriteriaHelper.GetCriterion(filter));

            if (order != null)
                foreach (OrderBy o in order)
                    criteria.AddOrder(CriteriaHelper.GetOrder(o));
            return criteria;
        }

        protected virtual bool DefaultFlush
        {
            get { return true; }
        }

        protected virtual bool DefaultMergeBeforeUpdate
        {
            get { return Nested.Config.DataConfig.Options.MergeBeforeUpdate; }
        }

        public T Load(object id)
        {
            return Load(id, LockMode.None);
        }

        public T Load(object id, LockMode lockMode)
        {
            return Session.Load<T>(id, lockMode);
        }

        public ICriteria CreateCriteria()
        {
            return Session.CreateCriteria(typeof(T));
        }

        public IMultiCriteria CreateMultiCriteria()
        {
            return Session.CreateMultiCriteria();
        }

        public T LoadByExample(T example)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Example.Create(example));
            return criteria.UniqueResult<T>();
        }

        public IList<T> ListByExample(T example)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Example.Create(example));
            return criteria.List<T>();
        }

        public void Persist(T entity)
        {
            Persist(entity, DefaultFlush);
        }

        public void Persist(T entity, bool flush)
        {
            Session.Persist(entity);
            if (flush) Session.Flush();
        }

        public void Update(T entity)
        {
            Update(entity, DefaultFlush);
        }

        public void Update(T entity, bool flush)
        {
            if (DefaultMergeBeforeUpdate)
                entity = (T)Session.Merge(entity);
            Session.Update(entity);
            if (flush) Session.Flush();
        }

        public void Save(T entity)
        {
            Save(entity, DefaultFlush);
        }

        public void Save(T entity, bool flush)
        {
            if (DefaultMergeBeforeUpdate)
                entity = (T)Session.Merge(entity);

            Session.Save(entity);
            if (flush) Session.Flush();
        }

        public void SaveOrUpdate(T entity)
        {
            SaveOrUpdate(entity, DefaultFlush);
        }

        public void SaveOrUpdate(T entity, bool flush)
        {
            if (DefaultMergeBeforeUpdate)
                entity = (T)Session.Merge(entity);

            Session.SaveOrUpdate(entity);
            if (flush) Session.Flush();
        }

        public void Delete(T entity)
        {
            Delete(entity, DefaultFlush);
        }

        public void Delete(T entity, bool flush)
        {
            Session.Delete(entity);
            if (flush) Session.Flush();
        }

        public void DeleteById(object id)
        {
            DeleteById(id, true);
        }

        public void DeleteById(object id, bool flush)
        {
            T entity = Load(id, LockMode.Upgrade);
            Delete(entity, flush);
        }

        public int DeleteByCriteria(ICriteria criteria)
        {
            return DeleteByCriteria(criteria, DefaultFlush);
        }

        public int DeleteByCriteria(ICriteria criteria, bool flush)
        {
            IList<T> list = criteria.List<T>();

            foreach (T entity in list)
            {
                Delete(entity, false);
            }
            if (flush) Session.Flush();
            return list.Count;
        }

        public virtual object TestMethod(object obj)
        {
            return null;
        }
    }
}
