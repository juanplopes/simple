
using Simple.Services.Default;
using System;
using Simple;
using Simple.Tests.Service;
using Simple.Services.Remoting;
using Simple.ConfigSource;
using Simplev.Logging;
using System.Linq;
using Simple.IO;
using Simple.Tests.SampleServer;
using Simple.Tests;
using System.Collections.Generic;
namespace TestClient
{
    static class Program
    {
        delegate void AddDelegate(string s);

        interface ITeste<T>
        {
            void Add(T n);
            T Get(int index);
        }

        static void Main(string[] args)
        {
            IList<string> s = new List<string>();
            
            
            IList<object> o = new List<object>();

            AddDelegate d = s.Add;
            AddDelegate d2 = o.Add;
            

            o.Add(2);

        }
    }
}

