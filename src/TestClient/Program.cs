
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
            Simply.Do[NHConfig1.ConfigKey].Configure.RemotingDefault();
            Category.Do.List(x => 
                x.Name == "asd" 
                || x.Picture.Length > 100 
                && x.Description.Contains("mycat"));
        }
    }
}

