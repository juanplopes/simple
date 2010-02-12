using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Simple.DataAccess.Context;
using Simple;
using Sample.Project.SampleData;
using Simple.Patterns;

namespace Sample.Project.Tests
{
    public class TransactionedFixture : ConfiguredFixture
    {
        ITransaction tx = null;
        IDataContext dx = null;

        public ISession Session { get { return Simply.Do.GetSession(); } }

        [SetUp]
        public void Setup()
        {
            dx = Simply.Do.EnterContext();
            tx = Session.BeginTransaction();
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
