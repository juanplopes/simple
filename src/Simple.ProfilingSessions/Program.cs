using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;
using System.Threading;

namespace Simple.ProfilingSessions
{
    class Program
    {
        static void Main(string[] args)
        {
            var simply = Simply.Do;
            simply.Configure.DefaultHost();
            simply.Host(typeof(TestService));
            simply.StartServer();

            var svc = simply.Resolve<ITestService>();

            var start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                if (i % 10000 == 0) Console.WriteLine(i);
                svc.A30();
            }
            Console.WriteLine(DateTime.Now - start);

        }
    }
}
