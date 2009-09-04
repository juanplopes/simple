using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Simple.DataAccess.Context;
using Simple;

namespace Sample.Project.Tests
{
    public class TransactionedFixture : ConfiguredFixture
    {
        ITransaction tx = null;
        IDataContext dx = null;

        public override void FixtureSetup()
        {
            base.FixtureSetup();
            ConfigurationEnsurer.Do.ReMigrate();
        }

        [SetUp]
        public void Setup()
        {
            dx = Simply.Do.EnterContext();
            tx = Simply.Do.GetSession().BeginTransaction();
        }

        [TearDown]
        public void Teardown()
        {
            tx.Rollback();
            dx.Exit();
        }
    }
  
}
