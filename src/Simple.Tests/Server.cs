using System;
using System.Threading;
using Simple.Common;
using Simple.Tests.Services;


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
