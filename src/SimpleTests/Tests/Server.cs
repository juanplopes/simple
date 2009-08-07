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
using Simple.Threading;
using Simple.Tests.DataAccess;
using Simple.Tests.SampleServer;


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
            {
                if (args[0] == RemotingTest)
                {
                    var ev = NamedEvents.OpenOrCreate(RemotingTest, false, EventResetMode.ManualReset);

                    Guid guid = Guid.NewGuid();

                    Simply.Get(guid).Configure.Remoting()
                        .FromXml(Helper.MakeConfig(new Uri(args[1])));

                    Simply.Get(guid).Host(typeof(SimpleService));

                    ev.Set();
                    Console.ReadLine();
                }
                else if (args[0] == RemotingServerInterceptorTest)
                {
                    var ev = NamedEvents.OpenOrCreate(RemotingServerInterceptorTest, false, EventResetMode.ManualReset);

                    Guid guid = Guid.NewGuid();

                    Simply.Get(guid).Configure
                        .Remoting().FromXml(Helper.MakeConfig(new Uri(args[1])));

                    BaseInterceptorFixture.ConfigureSvcs(guid);

                    ev.Set();

                    Console.ReadLine();
                }
                else if (args[0] == RemotingClientInterceptorTest)
                {
                    var ev = NamedEvents.OpenOrCreate(RemotingClientInterceptorTest, false, EventResetMode.ManualReset);

                    Guid guid = Guid.NewGuid();

                    Simply.Get(guid).Configure
                        .Remoting().FromXml(Helper.MakeConfig(new Uri(args[1])));

                    BaseInterceptorFixture.ConfigureSvcsWithoutHooks(guid);

                    ev.Set();

                    Console.ReadLine();
                }
            }
            else
            {
                Simply simply = Simply.Get(NHConfig1.ConfigKey);

                simply.Configure.Log4netToConsole();
                simply.Configure
                   .NHibernate().FromXml(NHConfigurations.NHConfig1)
                   .MappingFromAssemblyOf<Category.Map>()
                   .RemotingDefault();

                simply.AddServerHook(x => new DefaultCallHook(x, NHConfig1.ConfigKey));
                simply.HostAssemblyOf(typeof(Category.Map));
                Console.ReadLine();
            }
            //            NUnit.Gui.AppEntry.Main(new string[] { Assembly.GetExecutingAssembly().Location });

            return 0;
        }
    }
}
