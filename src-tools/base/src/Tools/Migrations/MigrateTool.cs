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
using System.IO;

namespace Sample.Project.Tools.Migrations
{
    public class MigrateTool : ICommand
    {
        public long? Version { get; set; }
        public bool WithTest { get; set; }
        public bool WithDevelopment { get; set; }
        public string FilePath { get; set; }
        public bool DryRun { get; set; }

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

        private void Migrate(long? version)
        {
            Action<string> action = null;
            var builder = new StringBuilder();
            if (FilePath != null || DryRun) 
               action = x=>builder.AppendLine(x);


            var config = Simply.Do.GetConfig<ApplicationConfig>();

            var options = new MigratorOptions(config.ADOProvider, Simply.Do.GetConnectionString())
                .FromAssembly(typeof(MigrateTool).Assembly)
                .WithSchemaTable(config.SchemaInfoTable)
                .WriteWith(action);

            new DbMigrator(options).Migrate(version);

            if (FilePath != null)
                File.WriteAllText(FilePath, builder.ToString());
        }
       

        #endregion
    }
}
