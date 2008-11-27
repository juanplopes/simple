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
    [TestFixture]
    public class BaseTests<E, P>
        where P : IEntityProvider<E>, new()
        where E : new()
    {
        public virtual int CreationNumber
        {
            get
            {
                return 10;
            }
        }

        public P EntityProvider
        {
            get
            {
                return new P();
            }
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
                rules.Persist(EntityProvider.Populate(i));
            }
        }

        protected void TestGetAllAndUpdate()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                E e = EntityProvider.Populate(i);
                e = rules.LoadByExample(e);
                rules.Update(e);
            }
        }

        protected void TestGetAllAndCompare()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                E e = EntityProvider.Populate(i);
                IList<E> list = rules.ListByExample(e);
                Assert.AreEqual(list.Count, 1);
            }
        }

        protected void DeleteAll(bool assert)
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                E e = EntityProvider.Populate(i);
                int deleted = rules.DeleteByCriteria(rules.CreateCriteria().Add(
                    CriteriaHelper.GetCriterion(Expression.Example(e)))
                );
                if (assert)
                    Assert.AreEqual(deleted, 1);
            }
        }
    }
}
