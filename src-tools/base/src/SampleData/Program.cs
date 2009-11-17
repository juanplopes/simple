using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Sample.Project.Environment;

namespace Sample.Project.SampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            new Default(Default.Main).StartServer(
                typeof(ServerStarter).Assembly, false,
                x => x.Configure.DefaultHost());

            Samples.Init();
        }
    }
}
