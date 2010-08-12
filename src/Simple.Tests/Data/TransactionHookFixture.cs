using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Tests.Resources;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class TransactionHookFixture : BaseDataFixture
    {
        protected override bool OpenOwnTx
        {
            get
            {
                return false;
            }
        }

        [Test]
        public void TestDeleteWithinTransaction()
        {
            using (MySimply.EnterContext())
            using (var tx = MySimply.BeginTransaction())
            {
                int c = Customer.Count();
                Assert.Throws<Exception>(() => Customer.Service.DeleteTwoCustomers());
                int c2 = Customer.Count();
                (c - c2).Should().Be(2);
                tx.Rollback();

                int c3 = Customer.Count();
                (c - c3).Should().Be(0);
            }
        }

        [Test]
        public void TestDeleteWithoutTransaction()
        {
            using (MySimply.EnterContext())
            {
                int c = Customer.Count();
                Assert.Throws<Exception>(() => Customer.Service.DeleteTwoCustomers());
                int c2 = Customer.Count();

                (c - c2).Should().Be(0);
            }
        }


    }
}
