using System;
using System.Collections.Generic;

using System.Text;
using NUnit.Framework;
using SimpleLibrary.Rules;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;

namespace SimpleLibrary.NUnit
{
    [TestFixture]
    public class BaseTests<E, P>
        where P : IEntityProvider<E>, new()
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

        [TestFixtureSetUp]
        public void InsertionSetup()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                rules.SaveOrUpdate(EntityProvider.Populate(i));
            }
        }

        [Test]
        public void TestGetAllAndCompare()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                E e = EntityProvider.Populate(i);
                IList<E> list = rules.ListByExample(e);
                Assert.GreaterOrEqual(list.Count, 1);
            }
        }

        [TestFixtureTearDown]
        public void DeleteAllAfterThat()
        {
            BaseDao<E> rules = new BaseDao<E>();
            for (int i = 0; i < CreationNumber; i++)
            {
                E e = EntityProvider.Populate(i);
                int deleted = rules.DeleteByCriteria(rules.CreateCriteria().Add(
                    CriteriaHelper.GetCriterion(Expression.Example(e)))
                );

                Assert.GreaterOrEqual(deleted, 1);
            }
        }
    }
}
