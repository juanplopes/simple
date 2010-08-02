using System;
using System.Collections.Generic;
using System.Reflection;
using Simple.Migrator.Framework;
using Simple.Migrator.Providers;
using Simple.Migrator.Providers.SqlServer;
using Simple.Migrator.Providers.Mysql;
using Simple.Migrator.Providers.PostgreSQL;
using Simple.Migrator.Providers.SQLite;
using Simple.Migrator.Providers.Oracle;

namespace Simple.Migrator
{
    /// <summary>
    /// Handles loading Provider implementations
    /// </summary>
    public class ProviderFactory
    {
        public static ITransformationProvider Create(string providerName, string connectionString)
        {
            Dialect dialectInstance = GetDialect(providerName);
            return dialectInstance.NewProviderForDialect(providerName, connectionString);
        }

        public static Dialect GetDialect(string providerName)
        {
            switch (providerName.ToLower())
            {
                case "system.data.sqlserverce.3.5":
                case "system.data.sqlserverce":
                case "microsoft.sqlserverce.client":
                    return new SqlServerCeDialect();

                case "system.data.sqlclient":
                    return new SqlServerDialect();

                case "mysql.data.mysqlclient":
                    return new MysqlDialect();

                case "npgsql":
                    return new PostgreSQLDialect();

                case "system.data.sqlite":
                    return new SQLiteDialect();

                case "system.data.oracleclient":
                case "oracle.dataaccess.client":
                    return new OracleDialect();

                default:
                    throw new NotImplementedException("The dialect for '" + providerName + "' is not implemented!");

            }
        }
    }
}
