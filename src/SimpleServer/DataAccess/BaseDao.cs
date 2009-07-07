using System;
using System.Collections.Generic;

using System.Text;
using NHibernate;
using NHibernate.Impl;
using NHibernate.Engine;
using System.Collections;
using NHibernate.Criterion;
using Simple.Filters;
using System.Linq;
using NHibernate.Linq;

namespace Simple.DataAccess
{
    /// <summary>
    /// Untyped base DAO class. Session holder.
    /// </summary>
    public class Dao
    {
        private ISession _session = null;
        public ISession Session
        {
            get
            {
                if (_session == null)
                    throw new InvalidOperationException("This Dao has no session attached");
                
                return _session;
            }
            set
            {
                _session = value;
            }
        }

        public Dao(ISession session)
        {
            _session = session;
        }


        /// <summary>
        /// Creates a DAO instance getting session from pool.
        /// </summary>
        public Dao() { }
        public Dao(object key) : this(SessionManager.GetSession(key)) { }
        public Dao(object key, bool forceNew) : this(SessionManager.GetSession(key, forceNew)) { }
     
    }

    public class EntityDao<T> : Dao
    {
        public EntityDao() : base() { }
        public EntityDao(ISession session) : base(session) { }
        public EntityDao(object key) : base(key) { }
        public EntityDao(object key, bool forceNew) : base(key, forceNew) { }


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

        public IOrderedQueryable<Q> Linq<Q>()
        {
            return Session.Linq<Q>();
        }

        public IOrderedQueryable<T> Linq()
        {
            return Linq<T>();
        }

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
            Session.Update(entity);
            if (flush) Session.Flush();
        }

        public void Save(T entity)
        {
            Save(entity, DefaultFlush);
        }

        public void Save(T entity, bool flush)
        {
            object obj = Session.Save(entity);
            if (flush) Session.Flush();
        }

        public void SaveOrUpdate(T entity)
        {
            SaveOrUpdate(entity, DefaultFlush);
        }

        public void SaveOrUpdate(T entity, bool flush)
        {
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
