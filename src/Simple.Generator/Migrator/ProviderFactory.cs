#region License

//The contents of this file are subject to the Mozilla Public License
//Version 1.1 (the "License"); you may not use this file except in
//compliance with the License. You may obtain a copy of the License at
//http://www.mozilla.org/MPL/
//Software distributed under the License is distributed on an "AS IS"
//basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
//License for the specific language governing rights and limitations
//under the License.

#endregion

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
