
using Simple.Services.Default;
using System;
using Simple;
using Simple.Tests.Service;
using Simple.Services.Remoting;
using Simple.ConfigSource;
using Simple.Logging;
using System.Linq;
using Simple.IO;
using Simple.Tests.SampleServer;
using Simple.Tests;
namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Simply.Get(NHConfig1.ConfigKey).Configure.RemotingDefault();
            var c = Customer.Load(1);
        }
    }
}

