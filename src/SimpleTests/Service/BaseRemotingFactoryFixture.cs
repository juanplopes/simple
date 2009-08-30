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
    public abstract class BaseRemotingFactoryFixture : BaseFactoryFixture
    {
        public abstract Uri Uri { get; }

        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();

            Simply.Do[guid].Configure.Remoting(Uri.ToString());

            return guid;
        }

        protected override void Release(Guid guid)
        {
            Simply.Do[guid].Release.Remoting();
        }

        Process process;

        [TestFixtureSetUp]
        public void ClassSetup()
        {
            process = Process.Start(new ProcessStartInfo()
            {
                FileName = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath,
                Arguments = string.Join(" ", new string[] {
                    Server.RemotingTest,
                    Uri.ToString()}),
                WindowStyle = ProcessWindowStyle.Minimized
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
