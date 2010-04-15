using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Environment;
using Simple;
using Schema.Migrator;
using Simple.IO;
using Sample.Project.Tools;

namespace Sample.Project.Tools.Migrations
{
    public class MigratorTool : IToolRunner
    {
        #region IToolRunner Members

        public int Execute(CommandLineReader cmd)
        {
            string env = cmd.Get<string>("env");
            long? version = cmd.Get<long?>("version");
            
            new Configurator(env).ConfigServer();


            new DbMigrator(
                Configurator.MigrationProvider, 
                Simply.Do.GetConnectionString(),
                GetType().Assembly, true).Migrate(version, "sampleprojectSchemaInfo");

            return 0;
        }

        #endregion
    }
}
