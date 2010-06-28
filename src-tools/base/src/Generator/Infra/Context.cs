using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Environment;

namespace Sample.Project.Generator.Infra
{
    public class Context : MarshalByRefObject
    {
        GeneratorResolver resolver = null;
        public void Init(string name, bool defaultContext)
        {
            resolver = new GeneratorResolver().WithHelp().Define(defaultContext);
            var cfg = new Configurator(name);

            if (!defaultContext)
                cfg.StartServer<ServerStarter>();
            else
                cfg.ConfigClient().ConfigServer();
        }

        public void Execute(string command)
        {
            try
            {
                resolver.Resolve(command).Execute();
            }
            catch (GeneratorException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
