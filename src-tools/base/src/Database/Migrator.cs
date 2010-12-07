using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator;
using System.Reflection;

namespace Example.Project.Database
{
    public class Migrator
    {
        private MigratorOptions options;

        public Migrator(string provider, string cs, Action<MigratorOptions> optionsFactory)
        {
            options = new MigratorOptions(provider, cs);
            optionsFactory(options);
            options = options.FromAssembly(this.GetType().Assembly);
        }

        public void Migrate(long? version)
        {
            new DbMigrator(options).Migrate(version);
        }
    }
}
