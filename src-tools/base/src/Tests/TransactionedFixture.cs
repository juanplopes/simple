using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Simple.Data.Context;
using Simple;
using Simple.Patterns;
using NHibernate.Stat;

namespace Example.Project.Tests
{
    public class TransactionedFixture : ConfiguredFixture
    {
        ITransaction tx = null;
        IDataContext dx = null;

        public ISession Session { get { return Simply.Do.GetSession(); } }
        public IStatistics GlobalStats { get { return Session.SessionFactory.Statistics; } }

        [SetUp]
        public void Setup()
        {
            dx = Simply.Do.EnterContext();
            tx = Session.BeginTransaction();
            GlobalStats.Clear();
        }

        [TearDown]
        public void Teardown()
        {
			Session.Clear();
            tx.Rollback();
            dx.Exit();
        }
    }

}
