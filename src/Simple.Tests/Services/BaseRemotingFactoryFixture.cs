using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using Simple.Common;
using Simple.Config;
using Simple.Services.Remoting;

namespace Simple.Tests.Services
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
                FileName = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath,
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
