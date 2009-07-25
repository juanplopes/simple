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
    public class RemotingInterceptorFixture : BaseInterceptorFixture
    {
        Process process;

        [TestFixtureSetUp]
        public void ClassSetup()
        {
            process = Process.Start(new ProcessStartInfo()
            {
                FileName = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath,
                Arguments = Server.RemotingInterceptorTest,
                WindowStyle = ProcessWindowStyle.Normal
            });
            NamedEvents.OpenOrWait(Server.RemotingInterceptorTest);
        }
        
        [TestFixtureTearDown]
        public void ClassTeardown()
        {
            process.Kill();
            SourceManager.Do.Clear<RemotingConfig>();
        }

        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();

            RemotingSimply.Do.Configure(guid,
                XmlConfig.LoadXml<RemotingConfig>(Helper.MakeConfig(4012)));

            return guid;
        }

        protected override void Release(Guid guid)
        {
            
        }
    }
}
