using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.DataAccess.Context;
using Simple.DataAccess;
using NHibernate.Cfg;
using NHibernate;

namespace Simple.Tests.DataAccess
{
    public class BaseDataFixture
    {
        public virtual bool OpenDataContext { get { return true; } }

        public Simply GetSimply()
        {
            return Simply.Get(typeof(DBEnsurer));
        }

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            DBEnsurer.Ensure(typeof(DBEnsurer));
        }

        IDataContext dtx = null;
        [SetUp]
        public void TestSetup()
        {
            if (OpenDataContext)
                dtx = GetSimply().EnterContext();

        }

        [TearDown]
        public void TestTearDown()
        {
            if (OpenDataContext)
                dtx.Exit();

            DeleteFromAllTables(typeof(DBEnsurer));
        }

        public static int DeleteFromAllTables(object key)
        {
            int res = 0;
            using (Simply.Get(key).EnterContext())
            {
                Configuration config = Simply.Get(key).GetNHibernateConfig();
                foreach (var mapping in config.ClassMappings)
                {
                    string deleteSql = string.Format("delete from {0}", mapping.Table.Name);
                    res += Simply.Get(key).GetSession().CreateSQLQuery(deleteSql).ExecuteUpdate();
                }
                Assert.AreEqual(4, config.ClassMappings.Count);
            }
            return res;
        }
    }
}
