using System;
using System.Collections.Generic;

using System.Text;
using NUnit.Framework;
using SimpleLibrary.Rules;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;
using System.Reflection;
using NHibernate;

namespace SimpleLibrary.NUnit
{
    public class BaseTests<E> : IEntityProvider<E>
        where E : new()
    {
        protected virtual int CreationNumber
        {
            get
            {
                return 10;
            }
        }

        protected virtual bool DefaultSkipID
        {
            get
            {
                return true;
            }
        }


        private IEntityProvider<E> CreateEntityProvider()
        {
            return new BaseEntityProvider<E>(DefaultSkipID);
        }

        [Test]
        public void InsertScript1()
        {
            SessionManager.ReleaseThreadSessions();
            InsertionSetup();
            TestGetAllAndCompare();
            DeleteAll(true);
        }

        [Test]
        public void UpdateScript1()
        {
            SessionManager.ReleaseThreadSessions();
            InsertionSetup();
            TestGetAllAndUpdate();
            TestGetAllAndCompare();
            DeleteAll(true);
        }

        protected void InsertionSetup()
        {
            DeleteAll(false);
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                rules.Persist(Populate(i));
            }
        }

        protected void TestGetAllAndUpdate()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                E e = Populate(i);
                e = rules.LoadByExample(e);
                rules.Update(e);
            }
        }

        protected void TestGetAllAndCompare()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                var e = Populate(i);
                IList<E> list = rules.ListByExample(e);
                Assert.AreEqual(list.Count, 1);
            }
        }

        protected void DeleteAll(bool assert)
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                var e = Populate(i);
                int deleted = rules.DeleteByCriteria(rules.CreateCriteria().Add(
                    CriteriaHelper.GetCriterion(Expression.Example(e)))
                );
                if (assert)
                    Assert.AreEqual(deleted, 1);
            }
        }

        #region IEntityProvider<E> Members

        public virtual E Populate(int seed)
        {
            return CreateEntityProvider().Populate(seed);
        }

        public virtual bool Compare(E e1, E e2)
        {
            return CreateEntityProvider().Compare(e1, e1);
        }

        #endregion
    }
}
