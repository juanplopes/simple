
using Simple.Services.Default;
using System;
using Simple;
using Simple.Tests.Service;
using Simple.Services.Remoting;
using Simple.ConfigSource;
using Simple.Logging;
using System.Linq;
using Simple.IO;
namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Simply.Do.Host(typeof(SimpleService));

            for (int i = 0; i < 1000; i++)
            {
                var client = Simply.Do.Resolve<ISimpleService>();
                client.GetInt32();
            }
        }
    }
}

