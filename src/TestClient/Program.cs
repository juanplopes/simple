using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Rules;
using Simple.Tests.Contracts;
using Simple.Filters;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using Simple.Remoting;
using Simple.Client;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Simply.Log<Program>().Debug("Teste");
        }
    }
}

