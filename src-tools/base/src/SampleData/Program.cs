using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Environment.Development;
using Simple;

namespace Sample.Project.SampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            Default.ConfigureServer();
            Simply.Do.Configure.DefaultHost();
            Simply.Do.InitServer(typeof(ServerStarter).Assembly, false);

            Samples.Init();
        }
    }
}
