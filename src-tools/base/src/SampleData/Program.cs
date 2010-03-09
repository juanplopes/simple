using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Sample.Project.Environment;
using Simple.IO;

namespace Sample.Project.SampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new CommandLineReader(args);
            string env = reader.Get("e", Default.Main);

            new Default(env).StartServer<ServerStarter>(x => x.Configure.DefaultHost());

            Samples.Init();
        }
    }
}
