using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ProfilingSessions
{
    class Program
    {
        static void Main(string[] args)
        {
            var simply = Simply.Do;
            simply.Configure.RemotingDefault();
            simply.Host(typeof(TestService));
            simply.StartServer();

            var svc = simply.Resolve<ITestService>();

            var start = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                if (i % 1000 == 0) Console.WriteLine(i);
                svc.A00();
            }
            Console.WriteLine(DateTime.Now - start);

        }
    }
}
