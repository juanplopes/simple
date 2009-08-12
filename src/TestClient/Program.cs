
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
    static class Program
    {
        static void Times(this int n, Action<int> action)
        {
            for (int i = 0; i < n; i++)
                action(i);
        }

        static TimeSpan Minutes(this int n)
        {
            return new TimeSpan(0, n, 0);
        }

        static DateTime Ago(this TimeSpan time)
        {
            return DateTime.Now.Subtract(time);
        }

        static void Main(string[] args)
        {
            20.Minutes().Ago();
        }
    }
}

