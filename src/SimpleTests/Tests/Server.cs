using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using Sample.BusinessServer.Rules;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Simple.Remoting;
using Simple.ConfigSource;
using Simple.Tests.Service;
using Simple.Services;
using Simple.Server;
using System.Threading;


namespace Simple.Tests
{
    public class Server
    {
        public const string RemotingTest="remotingtest";

        static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == RemotingTest)
                {
                    Guid guid = Guid.NewGuid();

                    RemotingSimply.Do.Configure(guid,
                        XmlConfig.LoadXml<RemotingConfig>(RemotingConfigs.SimpleRemotingConfig));

                    Simply.Get(guid).Host(typeof(SimpleService));
                    Thread.Sleep(-1);
                }
            }
            return 0;
        }
    }
}
