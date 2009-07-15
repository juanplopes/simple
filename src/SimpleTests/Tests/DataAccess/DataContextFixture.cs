using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.DataAccess.Context;
using Simple.Server;
using NHibernate;
using Simple.Tests.Contracts;

namespace Simple.Tests.DataAccess
{
    [TestClass]
    public class DataContextFixture
    {
        [TestInitialize]
        public void TestSetup()
        {
            using(var dx = Simply.Get(typeof(DBEnsurer)).EnterContext())
                DBEnsurer.Ensure(typeof(DBEnsurer));
        }


        [TestMethod]
        public void SimpleDataContextTest()
        {
            ISession s1, s2, s3, s4;
            using (var dx = Simply.Get(typeof(DBEnsurer)).EnterContext())
            {
                s1 = dx.Session;
                Assert.IsTrue(s1.IsOpen);

                using (var dx2 = Simply.Get(typeof(DBEnsurer)).EnterContext())
                {
                    s2 = dx2.Session;
                    Assert.AreEqual(s1, s2);

                    s3 = dx2.NewSession();
                    Assert.IsTrue(s3.IsOpen);

                    s4 = dx.NewSession();
                }

                Assert.IsFalse(s3.IsOpen);
                
                Assert.IsTrue(s2.IsOpen);
                Assert.IsTrue(s4.IsOpen);
            }

            Assert.IsFalse(s2.IsOpen);
            Assert.IsFalse(s4.IsOpen);
        }

        [TestMethod]
        public void NestedTransactions()
        {
            using (var dx = Simply.Get(typeof(DBEnsurer)).EnterContext())
            {
                Empresa e = Empresa.Load(DBEnsurer.E1.Id);

                using (var tx1 = dx.Session.BeginTransaction())
                {
                    using (var tx2 = dx.Session.BeginTransaction())
                    {
                        e.Nome = this.GetType().GUID.ToString();
                        e.SaveOrUpdate();

                        e = Empresa.Load(DBEnsurer.E1.Id);
                        Assert.AreEqual(this.GetType().GUID.ToString(), e.Nome);

                        tx2.Commit();
                    }

                    e = Empresa.Load(DBEnsurer.E1.Id);
                    Assert.AreEqual(this.GetType().GUID.ToString(), e.Nome);

                    tx1.Rollback();
                }

                Assert.AreNotEqual(this.GetType().GUID.ToString(), e.Nome);
            }
        }
    }
}
