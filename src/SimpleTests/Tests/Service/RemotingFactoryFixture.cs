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
using Simple.Threading;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class RemotingFactoryFixture : BaseFactoryFixture
    {
        protected override Guid GetSource()
        {
            Guid guid = Guid.NewGuid();

            RemotingSimply.Do.Configure(guid,
                XmlConfig.LoadXml<RemotingConfig>(Helper.MakeConfig(4002)));

            return guid;
        }

        protected override void ReleaseSource(Guid guid)
        {
            SourceManager.Do.Remove<RemotingConfig>(guid);
            SourceManager.Do.Remove<IServiceHostProvider>(guid);
            SourceManager.Do.Remove<IServiceClientProvider>(guid);
        }

        Process process;

        [TestFixtureSetUp]
        public void ClassSetup()
        {
            process = Process.Start(new ProcessStartInfo()
            {
                FileName = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath,
                Arguments = Server.RemotingTest,
                WindowStyle = ProcessWindowStyle.Normal
            });
            NamedEvents.OpenOrWait(Server.RemotingTest).WaitOne();
        }

        [TestFixtureTearDown]
        public void ClassTeardown()
        {
            process.Kill();
            SourceManager.Do.Clear<RemotingConfig>();
        }
    }
}
