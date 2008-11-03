using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Config;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleLibraryConfig.Get();
            long start = DateTime.Now.Ticks;
            for (int i = 0; i < 100; i++)
            {
                SimpleLibraryConfig.Get();
            }
            TimeSpan end = new TimeSpan(DateTime.Now.Ticks - start);
        }
    }
}

