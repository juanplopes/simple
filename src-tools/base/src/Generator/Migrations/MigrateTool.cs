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

namespace Sample.Project.Generator.Migrations
{
    public class MigrateTool : ICommand
    {
        public long? Version { get; set; }

        #region ICommand Members

        public void Execute()
        {
            new DbMigrator(
                Simply.Do.GetConfig<ApplicationConfig>().ADOProvider,
                Simply.Do.GetConnectionString(),
                GetType().Assembly, true).Migrate(Version, "SchemaInfo");
        }

        #endregion
    }
}
