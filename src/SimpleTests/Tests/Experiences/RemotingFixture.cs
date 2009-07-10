using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Remoting;
using Simple.Tests.Service;

namespace Simple.Tests.Experiences
{
    [TestClass]
    public class RemotingFixture
    {
        [TestMethod]
        public void TestSameServiceInSameEndpoint()
        {
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SimpleService),
                this.GetType().GUID.ToString(), WellKnownObjectMode.SingleCall);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FailConnectService),
                this.GetType().GUID.ToString(), WellKnownObjectMode.SingleCall);
        }

    }
}
