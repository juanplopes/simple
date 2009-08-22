
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
using System.Linq.Expressions;
using Simple.Expressions;
namespace TestClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var f = Customer.Expr(true);

            f = f.And(x => x.CompanyName == "Living Consultoria");
            f = f.And(x => x.Address == "Novo endereço");

            f = Customer.And(f, x => x.City == "rio de janeiro");



        }



    }
}

