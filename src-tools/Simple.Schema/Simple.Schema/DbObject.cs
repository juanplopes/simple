using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schema.Metadata.Providers;

namespace Schema.Metadata
{
    public abstract class DbObject
    {
        public IDbSchemaProvider Provider { get; protected set; }

        public DbObject(IDbSchemaProvider provider)
        {
            this.Provider = provider;
        }

        public DbObject(string connectionString, string provider) :
            this(GetSchemaProvider(connectionString, provider))
        {

        }

        private static DbSchemaProvider GetSchemaProvider(string connectionString, string providerName)
        {
            switch (providerName.ToLower())
            {
                case "system.data.sqlserverce.3.5":
                case "system.data.sqlserverce":
                    return new SqlServerCeSchemaProvider(connectionString, providerName);

                case "system.data.oledb":
                    return new OleDbSchemaProvider(connectionString, providerName);

                case "system.data.sqlclient":
                    return new SqlServerSchemaProvider(connectionString, providerName);

                case "mysql.data.mysqlclient":
                    return new MySqlSchemaProvider(connectionString, providerName);

                case "npgsql":
                    return new PostgreSqlSchemaProvider(connectionString, providerName);

                case "system.data.sqlite":
                    return new SQLiteSchemaProvider(connectionString, providerName);

                case "system.data.oracleclient":
                case "oracle.dataaccess.client":
                    return new OracleSchemaProvider(connectionString, providerName);

                case "vistadb.net20":
                    return new VistaDBSchemaProvider(connectionString, providerName);

                default:
                    throw new NotImplementedException("The provider '" + providerName + "' is not implemented!");

            }
        }
    }
}
