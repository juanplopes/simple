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

        public override void FixtureSetup()
        {
            base.FixtureSetup();
            DataEnsurer.Do.Init();
        }

        [SetUp]
        public void Setup()
        {
            dx = Simply.Do.EnterContext();
            tx = Session.BeginTransaction();
        }

        [TearDown]
        public void Teardown()
        {
            tx.Rollback();
            dx.Exit();
        }
    }

    public class DataEnsurer : Singleton<DataEnsurer>
    {
        public DataEnsurer()
        {
            ConfigurationEnsurer.Do.ReMigrate();
            Samples.Init();
        }
        public void Init() { }
    }

}
