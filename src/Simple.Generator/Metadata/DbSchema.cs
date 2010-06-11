//
//  Data.Common.DbSchema - http://dbschema.codeplex.com
//
//  The contents of this file are subject to the New BSD
//  License (the "License"); you may not use this file
//  except in compliance with the License. You may obtain a copy of
//  the License at http://www.opensource.org/licenses/bsd-license.php
//
//  Software distributed under the License is distributed on an 
//  "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//  implied. See the License for the specific language governing
//  rights and limitations under the License.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Simple.Metadata
{
    public class DbSchema : IDisposable
    {
        protected IDbSchemaProvider Provider { get; private set; } 
        protected static DbSchemaProvider GetSchemaProvider(string providerName, string connectionString)
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

        private DataTable _tableCache = null;
        public DbSchema(string provider, string connectionString)
        {
            Provider = GetSchemaProvider(provider, connectionString);
        }

        private DataTable GetSchemaTables()
        {
            if (_tableCache != null) return _tableCache;
            return _tableCache = Provider.GetSchemaTables();
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
            return GetGeneric(included, excluded, "TABLE", "BASE TABLE");
        }

        public IEnumerable<DbTable> GetViews()
        {
            return GetViews("%");
        }

        public IEnumerable<DbTable> GetViews(params string[] included)
        {
            return GetViews(included, new string[] { });
        }

        public IEnumerable<DbTable> GetViews(IList<string> included, IList<string> excluded)
        {
            return GetGeneric(included, excluded, "VIEW");
        }


        #region ' Helpers '

        public string GetDatabaseName()
        {
            return Provider.GetDatabaseName();
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            if (Provider != null) Provider.Dispose();
        }

        #endregion
    }
}
