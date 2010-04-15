using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Tools.Migrations;
using Sample.Project.Tools.UnitTestData;
using Sample.Project.Tools.DevTestData;
using Sample.Project.Tools.SampleData;
using Simple.IO;

namespace Sample.Project.Tools
{
    public class Program
    {
        static int Main(string[] args)
        {
            var runners = new Dictionary<string, IToolRunner>(StringComparer.InvariantCultureIgnoreCase);
            runners["Migrator"] = new MigratorTool();
            runners["UnitTestData"] = new UnitTestDataTool();
            runners["DevTestData"] = new DevTestDataTool();
            runners["InitialData"] = new InitialDataTool();

            var reader = new CommandLineReader(args);
            var tool = reader.Get<string>("tool");

            if (!runners.ContainsKey(tool))
            {
                Console.WriteLine("Tool not found: {0}.", tool);
                return 1;
            }

            runners[tool].Execute(reader);

            return 0;
        }
    }
}
