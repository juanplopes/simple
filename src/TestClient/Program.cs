
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
            Expression<Func<Customer, bool>>. e;
            

            var expr = Customer.Expr(true);
            expr = Expression.And(expr, Customer.Expr(x => x.Address == "2"));
        }
    }
}

