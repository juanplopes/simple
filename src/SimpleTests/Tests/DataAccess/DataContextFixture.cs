using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.DataAccess.Context;
using NHibernate;
using Simple.Tests.Contracts;
using Simple.Tests.DataAccess.Samples;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class DataContextFixture : BaseDataFixture
    {
        public override bool OpenDataContext
        {
            get
            {
                return false;
            }
        }


        [Test]
        public void SimpleDataContextTest()
        {
            ISession s1, s2, s3, s4;
            using (var dx = GetSimply().EnterContext())
            {
                s1 = dx.Session;
                Assert.IsTrue(s1.IsOpen);

                using (var dx2 = GetSimply().EnterContext())
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

        [Test]
        public void TestReopenDataContext()
        {
            try
            {
                using (var dx = GetSimply().EnterContext())
                {
                    var s1 = dx.Session;
                    s1.Close();
                }
            }
            catch (InvalidOperationException) { }

            using (var dx = GetSimply().EnterContext())
            {
                var s1 = dx.Session;
                Assert.IsTrue(dx.IsOpen);
            }
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void TryCloseMainSessionTest()
        {
            using (var dx = GetSimply().EnterContext())
            {
                using (var dx2 = GetSimply().EnterContext())
                {
                    dx2.Session.Close();
                }
            }
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void NestedTransactions()
        {
            using (var dx = GetSimply().EnterContext())
            {
                var sample = new SimpleFourEntityInsert();

                Empresa e = Empresa.Load(sample.Empresa.Id);

                using (var tx1 = dx.Session.BeginTransaction())
                {
                    using (var tx2 = dx.Session.BeginTransaction())
                    {
                        e.Nome = this.GetType().GUID.ToString();
                        e.SaveOrUpdate();

                        e = Empresa.Load(sample.Empresa.Id);
                        Assert.AreEqual(this.GetType().GUID.ToString(), e.Nome);

                        tx2.Commit();
                    }

                    e = Empresa.Load(sample.Empresa.Id);
                    Assert.AreEqual(this.GetType().GUID.ToString(), e.Nome);

                    tx1.Rollback();
                }

                Assert.AreNotEqual(this.GetType().GUID.ToString(), e.Nome);
            }
        }
    }
}
