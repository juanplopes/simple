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
            simply.Host(typeof(TestService));

            var start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                if (i % 10000 == 0) Console.WriteLine(i);
                simply.Resolve<ITestService>().A30();
            }
            Console.WriteLine(DateTime.Now - start);

        }
    }
}
