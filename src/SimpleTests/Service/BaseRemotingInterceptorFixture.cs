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
using Simple.Common;

namespace Simple.Tests.Service
{
    public abstract class BaseRemotingServerInterceptorFixture : BaseRemotingInterceptorFixture
    {
        public override string ExecutionKey
        {
            get { return Server.RemotingServerInterceptorTest; }
        }

        protected override Guid Configure()
        {
            Guid guid = base.Configure();
            ConfigureClientServerHooks(guid);
            return guid;
        }
    }

    public abstract class BaseRemotingClientInterceptorFixture : BaseRemotingInterceptorFixture
    {

        protected override Guid Configure()
        {
            Guid guid = base.Configure();
            ConfigureClientHooks(guid);
            return guid;
        }

        public override string ExecutionKey
        {
            get { return Server.RemotingClientInterceptorTest; }
        }
    }

    public abstract class BaseRemotingInterceptorFixture : BaseInterceptorFixture
    {
        public abstract Uri Uri { get; }
        public abstract string ExecutionKey { get; }

        Process process;

        [TestFixtureSetUp]
        public void ClassSetup()
        {
            process = Process.Start(new ProcessStartInfo()
            {
                FileName = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath,
                Arguments = string.Join(" ", new string[] {
                    ExecutionKey,
                    Uri.ToString()}),
                WindowStyle = ProcessWindowStyle.Minimized
            });
            NamedEvents.OpenOrWait(ExecutionKey).WaitOne();
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

            Simply.Do[guid].Configure
                .Remoting().FromXml(Helper.MakeConfig(Uri));

            return guid;
        }

        protected override void Release(Guid guid)
        {

        }
    }
}
