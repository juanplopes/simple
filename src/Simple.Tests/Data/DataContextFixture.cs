using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Data.Context;
using NHibernate;
using Simple.Tests.Resources;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class DataContextFixture : BaseDataFixture
    {
        protected override bool OpenOwnTx
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
            using (var dx = MySimply.EnterContext())
            {
                s1 = dx.Session;
                Assert.IsTrue(s1.IsOpen);

                using (var dx2 = MySimply.EnterContext())
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
                using (var dx = MySimply.EnterContext())
                {
                    var s1 = dx.Session;
                    s1.Close();
                }
            }
            catch (InvalidOperationException) { }

            using (var dx = MySimply.EnterContext())
            {
                var s1 = dx.Session;
                Assert.IsTrue(dx.IsOpen);
            }
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void TryCloseMainSessionTest()
        {
            using (var dx = MySimply.EnterContext())
            {
                using (var dx2 = MySimply.EnterContext())
                {
                    dx2.Session.Close();
                }
            }
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void NestedTransactions()
        {
            using (var dx = MySimply.EnterContext())
            {
                Category c = Category.Load(1);

                string saveOldName = c.Name;

                using (var tx1 = dx.Session.BeginTransaction())
                {
                    using (var tx2 = dx.Session.BeginTransaction())
                    {
                        c.Name = "NewName";
                        c.SaveOrUpdate();

                        c = Category.Load(1);
                        Assert.AreEqual("NewName", c.Name);

                        tx2.Commit();
                    }

                    c = Category.Load(1);
                    Assert.AreEqual("NewName", c.Name);

                    using (var tx2 = dx.Session.BeginTransaction())
                    {
                        c = Category.Load(1);
                        c.Name = saveOldName;
                        c.Update();

                        tx2.Commit();
                    }

                    tx1.Rollback();
                }

                Assert.AreNotEqual("NewName", c.Name);

            }
        }
    }
}
