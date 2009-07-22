using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services.Remoting;
using Simple.ConfigSource;
using Simple.Services;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using Simple.Client;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class SelfRemotingFactoryFixture : BaseFactoryFixture
    {
        protected override Guid GetSource()
        {
            Guid guid = Guid.NewGuid();

            RemotingSimply.Do.Configure(guid,
                XmlConfig.LoadXml<RemotingConfig>(Helper.MakeConfig(9999)));
            Simply.Get(guid).Host(typeof(SimpleService));


            return guid;
        }

        [TearDown]
        public void ReleaseRemoting()
        {
            SourceManager.Do.Clear<RemotingConfig>();
        }

        protected override void ReleaseSource(Guid guid)
        {
            SourceManager.Do.Remove<RemotingConfig>(guid);
            SourceManager.Do.Remove<IServiceHostProvider>(guid);
            SourceManager.Do.Remove<IServiceClientProvider>(guid);
        }
    }
}
