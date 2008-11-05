using SimpleLibrary.Threading;
using BasicLibrary.Configuration;
using BasicLibrary.LibraryConfig;
using SimpleLibrary.Config;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            long start = DateTime.Now.Ticks;
            for (int i = 0; i < 3000; i++)
            {
                Console.WriteLine(i);
                SimpleLibraryConfig config = SimpleLibraryConfig.Get();
            }
            TimeSpan end = new TimeSpan(DateTime.Now.Ticks - start);
        }
    }
}

