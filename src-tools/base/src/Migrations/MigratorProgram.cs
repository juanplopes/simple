using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Environment;
using Simple;

namespace Conspirarte.Migrations
{
    public class MigratorProgram
    {
        public static void Main(string[] args)
        {
            string env = Default.Main;

            if (args.Length > 0) env = args[0];

            long? version = null;
            if (args.Length > 1) version = long.Parse(args[1]);

            new Default(env).ConfigServer();

            Migrate(Default.MigrationProvider, version);
        }

        public static void Migrate(string type, long? version)
        {
            var connectionString = Simply.Do.GetConnectionString();

            var mig = new Migrator.Migrator(type, connectionString, typeof(MigratorProgram).Assembly, true);
            if (version != null)
                mig.MigrateTo(version ?? 0);
            else
                mig.MigrateToLastVersion();
        }
    }
}
