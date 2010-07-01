using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Environment;
using Simple;
using Simple.IO;
using Sample.Project.Config;
using Simple.Migrator;
using Simple.Generator;
using Sample.Project.Tools.Infra;

namespace Sample.Project.Tools.Migrations
{
    public class MigrateTool : ICommand
    {
        public long? Version { get; set; }
        public bool WithTest { get; set; }
        public bool WithDevelopment { get; set; }

        public MigrateTool()
        {
            WithDevelopment = true;
            WithTest = false;
        }


        #region ICommand Members

        public void Execute()
        {
            if (!Configurator.IsProduction)
            {
                if (WithDevelopment)
                    using (Context.Development)
                        Migrate(Version);

                if (WithTest)
                    using (Context.Test)
                        Migrate(Version);
            }
            else
            {
                Migrate(Version);
            }

        }

        private static void Migrate(long? version)
        {
            var config = Simply.Do.GetConfig<ApplicationConfig>();
            new DbMigrator(config.ADOProvider, Simply.Do.GetConnectionString(), typeof(MigrateTool).Assembly, false)
                .Migrate(version, config.SchemaInfoTable);
        }

        #endregion
    }
}
