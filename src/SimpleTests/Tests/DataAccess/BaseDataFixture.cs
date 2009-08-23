using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.DataAccess.Context;
using NHibernate;

namespace Simple.Tests.DataAccess
{
    public class BaseDataFixture
    {
        IDataContext dx = null;
        ITransaction tx = null;

        protected virtual bool OpenOwnTx { get { return true; } }

        [SetUp]
        public void Setup()
        {
            if (OpenOwnTx)
            {
                dx = MySimply.EnterContext();
                tx = MySimply.GetSession().BeginTransaction();
            }
        }

        [TearDown]
        public void Teardown()
        {
            if (OpenOwnTx)
            {
                tx.Dispose();
                dx.Dispose();
            }
        }

        public Simply MySimply
        {
            get
            {
                return NHConfig1.Do.Simply;
            }
        }
    }
}
