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
    public class RemotingFactoryFixture : BaseFactoryFixture
    {
        protected override Guid GetSource()
        {
            Guid guid = Guid.NewGuid();

            RemotingSimply.Do.Configure(guid,
                XmlConfig.LoadXml<RemotingConfig>(RemotingConfigs.SimpleRemotingConfig));

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
                WindowStyle = ProcessWindowStyle.Hidden
            });
            Guid guid = GetSource();

            int i = 0;
            int count = 0;
            while (i++ < 30 && count < 3)
            {
                try
                {
                    ISimpleService service = Simply.Get(guid).Resolve<ISimpleService>();
                    service.GetInt32();
                    count++;
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
        }

        [TestFixtureTearDown]
        public void ClassTeardown()
        {
            process.Kill();
            SourceManager.Do.Clear<RemotingConfig>();
        }
    }
}
