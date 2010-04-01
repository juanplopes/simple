using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Environment;
using Simple;
using Schema.Migrator;
using Simple.IO;

namespace Sample.Project.Migrations
{
    public class MigratorProgram
    {
        public static void Main(string[] args)
        {
            var reader = new CommandLineReader(args);

            string env = reader.Get<string>("e", Default.Main);
            long? version = reader.Get<long?>("v", null);

            new Default(env).ConfigServer();

            Migrate(Default.MigrationProvider, version);
        }

        public static void Migrate(string type, long? version)
        {
            new DbMigrator(type, Simply.Do.GetConnectionString(),
                typeof(MigratorProgram).Assembly, true).Migrate(version, "sampleprojectSchemaInfo");
        }
    }
}
