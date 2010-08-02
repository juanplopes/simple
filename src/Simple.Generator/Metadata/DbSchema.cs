using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Simple.Metadata
{
    public class DbSchema
    {
        public string Provider { get; set; }
        public string ConnectionString { get; set; }

        protected static DbSchemaProvider GetSchemaProvider(MetaContext context)
        {
            switch (context.Provider.ToLower())
            {
                case "system.data.sqlserverce.3.5":
                case "system.data.sqlserverce":
                    return new SqlServerCeSchemaProvider(context);

                //case "system.data.oledb":
                //    return new OleDbSchemaProvider(connectionString, providerName);

                case "system.data.sqlclient":
                    return new SqlServerSchemaProvider(context);

                case "mysql.data.mysqlclient":
                    return new MySqlSchemaProvider(context);

                case "npgsql":
                    return new PostgreSqlSchemaProvider(context);

                case "system.data.sqlite":
                    return new SQLiteSchemaProvider(context);

                case "system.data.oracleclient":
                case "oracle.dataaccess.client":
                    return new OracleSchemaProvider(context);

                //case "vistadb.net20":
                //    return new VistaDBSchemaProvider(connectionString, providerName);

                default:
                    throw new NotImplementedException("The provider '" + context.Provider + "' is not implemented!");

            }
        }

        public DbSchema(string provider, string connectionString)
        {
            this.Provider = provider;
            this.ConnectionString = connectionString;
        }

        public IEnumerable<DbTable> GetTables()
        {
            return GetTables("%");
        }

        public IEnumerable<DbTable> GetTables(params string[] included)
        {
            return GetTables(included, new string[] { });
        }

        public IEnumerable<DbTable> GetTables(IList<string> included, IList<string> excluded)
        {
            using (var provider = GetSchemaProvider(new MetaContext(ConnectionString, Provider)))
            {
                var tables = provider.GetTables(included, excluded).ToList();
                var relations = provider.GetConstraints(included, excluded).ToList();
                var columns = tables.SelectMany(x => provider.GetColumns(x)).ToList();

                provider.Context
                    .InjectTables(tables)
                    .InjectRelactions(relations)
                    .InjectTableColumns(columns)
                    .ExecuteCache();

                foreach (var table in tables)
                    table.ExecuteCache();

                return tables;
            }
        }



    }
}
