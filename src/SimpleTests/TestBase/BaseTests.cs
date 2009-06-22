using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Rules;
using Simple.DataAccess;
using System.Reflection;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace Simple.TestBase
{
    public class BaseTests : IEntityProvider
    {
        public Type EntityType { get; set; }
        public bool DefaultSkipID { get; set; }
        public BaseTests(Type entityType, bool defaultSkipId)
        {
            this.EntityType = entityType;
            this.DefaultSkipID = defaultSkipId;
        }

        protected virtual int CreationNumber
        {
            get
            {
                return 10;
            }
        }

        private IEntityProvider CreateEntityProvider()
        {
            return new BaseEntityProvider(EntityType, DefaultSkipID);
        }

        [TestMethod]
        public void InsertScript1()
        {
            using (DataContext.Enter())
            {
                InsertionSetup();
                TestGetAllAndCompare();
                DeleteAll(true);
            }
        }

        [TestMethod]
        public void UpdateScript1()
        {
            using (DataContext.Enter())
            {
                InsertionSetup();
                TestGetAllAndUpdate();
                TestGetAllAndCompare();
                DeleteAll(true);
            }
        }

        protected void InsertionSetup()
        {
            DeleteAll(false);
            ISession session = SessionManager.GetSession();
            for (int i = 0; i < CreationNumber; i++)
            {
                session.Persist(Populate(i));
            }
        }

        protected void TestGetAllAndUpdate()
        {
            ISession session = SessionManager.GetSession();
            for (int i = 0; i < CreationNumber; i++)
            {
                object e = Populate(i);
                e = session.CreateCriteria(EntityType).Add(Example.Create(e)).UniqueResult();
                session.Update(e);
            }
        }

        protected void TestGetAllAndCompare()
        {
            ISession session = SessionManager.GetSession();
            for (int i = 0; i < CreationNumber; i++)
            {
                object e = Populate(i);
                IList list = session.CreateCriteria(EntityType).Add(Example.Create(e)).List();
                Assert.AreEqual(list.Count, 1);
            }
        }

        protected void DeleteAll(bool assert)
        {
            ISession session = SessionManager.GetSession();
            for (int i = 0; i < CreationNumber; i++)
            {
                object e = Populate(i);
                IList list = session.CreateCriteria(EntityType).Add(Example.Create(e)).List();
                int deleted = 0;
                foreach (object o in list)
                {
                    session.Delete(o);
                    deleted++;
                }
                if (assert)
                    Assert.AreEqual(deleted, 1);
            }
        }

        #region IEntityProvider<E> Members

        public virtual object Populate(int seed)
        {
            return CreateEntityProvider().Populate(seed);
        }

        public virtual bool Compare(object e1, object e2)
        {
            return CreateEntityProvider().Compare(e1, e1);
        }

        #endregion
    }
}
