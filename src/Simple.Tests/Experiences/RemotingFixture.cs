using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Runtime.Remoting;
using Simple.Tests.Service;
using Simple.DynamicProxy;

namespace Simple.Tests.Experiences
{
    [TestFixture]
    public class RemotingFixture
    {
        [Test]
        public void TestSameServiceInSameEndpoint()
        {
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SimpleService),
                this.GetType().GUID.ToString(), WellKnownObjectMode.SingleCall);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FailConnectService),
                this.GetType().GUID.ToString(), WellKnownObjectMode.SingleCall);
        }

        [Test, ExpectedException(typeof(InvalidCastException))]
        public void CreateConcreteClassProxy()
        {
            var obj = (SampleConcreteClass)DynamicProxyFactory.Instance.CreateProxy(null, (o, m, p) =>
            {
                return 84;
            });

            Assert.AreEqual(84, obj.GetInt());
        }

        class SampleConcreteClass
        {
            public int GetInt()
            {
                return 42;
            }
        }
    }
}
