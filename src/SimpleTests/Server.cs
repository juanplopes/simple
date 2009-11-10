using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Simple.Services.Remoting;
using Simple.ConfigSource;
using Simple.Tests.Service;
using Simple.Services;
using System.Threading;
using Simple.Common;
using Simple.Tests.DataAccess;
using Simple.Tests.SampleServer;
using System.IO;
using NHibernate.Tool.hbm2ddl;


namespace Simple.Tests
{
    public class Server
    {
        public const string RemotingTest = "remotingtest";
        public const string RemotingServerInterceptorTest = "remotingserverinterceptortest";
        public const string RemotingClientInterceptorTest = "remotingclientinterceptortest";

        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length > 0)
                ExecuteSamples(args);

            Console.ReadLine();
            return 0;
        }

        static void ExecuteSamples(string[] args)
        {
            SampleGenericPart(Server.RemotingTest, args,
                g => Simply.Do[g].Host(typeof(SimpleService)));

            SampleGenericPart(Server.RemotingServerInterceptorTest, args,
                g => BaseInterceptorFixture.ConfigureSvcs(g));

            SampleGenericPart(Server.RemotingClientInterceptorTest, args,
                g => BaseInterceptorFixture.ConfigureSvcsWithoutHooks(g));
        }

        static void SampleGenericPart(string sampleName, string[] args, Action<Guid> configure)
        {
            if (args.Length > 0 && args[0] == sampleName)
            {
                var ev = NamedEvents.OpenOrCreate(sampleName, false, EventResetMode.ManualReset);

                Guid guid = Guid.NewGuid();

                Simply.Do[guid].Configure.Remoting()
                    .FromXmlString(Helper.MakeConfig(new Uri(args[1])));

                configure(guid);

                ev.Set();
            }
        }
    }
}
