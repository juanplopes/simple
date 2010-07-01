using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Migrator.Framework;

namespace Simple.Migrator
{
    public class MigratorOptions
    {
        public string Provider { get; set; }
        public string ConnectionString { get; set; }
        public List<Type> MigrationTypes { get; set; }
        public Action<string> Commands { get; set; }
        public string SchemaInfoTable { get; set; }

        public MigratorOptions(string provider, string connectionString)
        {
            this.Provider = provider;
            this.ConnectionString = connectionString;
            this.MigrationTypes = new List<Type>();
            this.SchemaInfoTable = "SchemaInfo";
        }

        /// <summary>
        /// Collect migrations in one <c>Assembly</c>.
        /// </summary>
        /// <param name="asm">The <c>Assembly</c> to browse.</param>
        /// <returns>The migrations collection</returns>
        public MigratorOptions FromAssembly(Assembly asm)
        {
            foreach (Type t in asm.GetExportedTypes())
            {
                MigrationAttribute attrib =
                    (MigrationAttribute)Attribute.GetCustomAttribute(t, typeof(MigrationAttribute));

                if (attrib != null && typeof(IMigration).IsAssignableFrom(t) && !attrib.Ignore)
                {
                    MigrationTypes.Add(t);
                }
            }

            MigrationTypes.Sort(new MigrationTypeComparer(true));
            return this;
        }

        public MigratorOptions AddTypes(IEnumerable<Type> types)
        {
            MigrationTypes.AddRange(types);
            return this;
        }

        public MigratorOptions WriteWith(Action<string> writer)
        {
            this.Commands = writer;
            return this;
        }

        public MigratorOptions WithSchemaTable(string table)
        {
            this.SchemaInfoTable = table;
            return this;
        }
    }
}
