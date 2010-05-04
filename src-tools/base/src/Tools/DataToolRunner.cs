using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO;
using Sample.Project.Environment;
using Simple;

namespace Sample.Project.Tools
{
    public abstract class DataToolRunner : IToolRunner
    {
        protected virtual string DefaultEnvironment
        {
            get { return null; }
        }

        public int Execute(CommandLineReader cmd)
        {
            string env = cmd.Get<string>("env", DefaultEnvironment);

            new Configurator(env)
                .StartServer<ServerStarter>(x => x.Configure.DefaultHost());

            using(Simply.Do.EnterContext())
                RunSamples(cmd);

            return 0;
        }

        protected abstract void RunSamples(CommandLineReader cmd);
    }
}
