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


using System.Data;
using System.Data.Common;
using System.Reflection;
using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Metadata
{
    abstract public class DbSchemaProvider : IDbSchemaProvider
    {
        private string _connectionString = null;
        private string _providerName = null;
        private DbConnection _connection = null;

        protected DbConnection GetConnection()
        {
            if (_connection == null)
            {
                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(_providerName);
                _connection = providerFactory.CreateConnection();
                _connection.ConnectionString = _connectionString;
                _connection.Open();
            }

            return _connection;
        }

        public DbSchemaProvider(string connectionstring, string providername)
        {
            _connectionString = connectionstring;
            _providerName = providername;
        }

        #region ' IDbProvider Members '

        virtual public string GetDatabaseName()
        {
            return GetConnection().Database;
        }

        private string GetRelationsClause(IList<string> included, IList<string> excluded, string type)
        {
            return GetGenericClause(
                new[] { type + "_TABLE_NAME", type + "_TABLE_SCHEMA", type + "_TABLE_CATALOG" },
                included, excluded) + " AND PK_ORDINAL_POSITION = FK_ORDINAL_POSITION";
        }

        private string GetTablesClause(IList<string> included, IList<string> excluded)
        {
            return GetGenericClause(
                new[] { "TABLE_NAME", "TABLE_SCHEMA", "TABLE_CATALOG" },
                included, excluded);
        }

        public string GetGenericClause(IList<string> columns, IList<string> included, IList<string> excluded)
        {
            var incString = included.Count > 0 ?
                string.Join(" OR ", included.Select(x => GetTableWhereClause(columns, "LIKE", x)).ToArray()) :
                "1=1";

            var excString = excluded.Count > 0 ?
                string.Join(" AND ", excluded.Select(x => GetTableWhereClause(columns, "NOT LIKE", x)).ToArray()) :
                "1=1";

            return string.Format("({0}) AND ({1})", incString, excString);
        }


        protected string GetTableWhereClause(IList<string> columns, string op, string tableName)
        {
            var names = tableName.Split('.').Reverse().ToList();

            var clauses = new List<string>();
            for (int i = 0; i < columns.Count && i < names.Count; i++)
                clauses.Add(string.Format("{0} {1} '{2}'", columns[i], op, names[i]));

            return "(" + string.Join(" AND ", clauses.ToArray()) + ")";
        }

        virtual public IEnumerable<DbTable> GetTables(IList<string> includedTables, IList<string> excludedTables)
        {
            return GetConnection().GetSchema("Tables")
                .Select(GetTablesClause(includedTables, excludedTables))
                .Select(x=>new DbTable(x));
        }

        virtual protected DbCommand CreateCommand(string sql, params object[] parameters)
        {
            var cmd = GetConnection().CreateCommand();
            cmd.CommandText = string.Format(sql, parameters);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        virtual protected void LoadTableWithCommand(DataTable table, string sql, params object[] parameters)
        {
            var cmd = CreateCommand(sql, parameters);
            using (var reader = cmd.ExecuteReader())
                table.Load(reader);
        }

        virtual public IEnumerable<DbColumn> GetColumns(string table)
        {
            using (var cmd = CreateCommand("SELECT * FROM {0}", QualifiedTableName(tableSchema, tableName)))
            using (var reader = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                return reader.GetSchemaTable();
        }

        abstract public DataTable GetConstraints();
        abstract public DbType GetDbColumnType(string providerDbType);

        virtual public string QualifiedTableName(string tableSchema, string tableName)
        {
            if (!string.IsNullOrEmpty(tableSchema))
                return string.Format("[{0}].[{1}]", tableSchema, tableName);
            else
                return string.Format("[{0}]", tableName);
        }



        #endregion

        #region ' Helper functions '

        protected DataTable GetDTSchemaTables()
        {
            DataTable tbl = new DataTable("SchemaTables");
            tbl.Columns.Add("TABLE_CATALOG", typeof(System.String));
            tbl.Columns.Add("TABLE_SCHEMA", typeof(System.String));
            tbl.Columns.Add("TABLE_NAME", typeof(System.String));
            tbl.Columns.Add("TABLE_TYPE", typeof(System.String));
            tbl.Columns.Add("TABLE_GUID", typeof(System.Guid));
            tbl.Columns.Add("DESCRIPTION", typeof(System.String));
            tbl.Columns.Add("TABLE_PROPID", typeof(System.Int32));
            tbl.Columns.Add("DATE_CREATED", typeof(System.DateTime));
            tbl.Columns.Add("DATE_MODIFIED", typeof(System.DateTime));

            return tbl;
        }

        protected DataTable GetDTSchemaConstrains()
        {
            DataTable tbl = new DataTable("Constraints");
            tbl.Columns.Add("PK_TABLE_CATALOG", typeof(System.String));
            tbl.Columns.Add("PK_TABLE_SCHEMA", typeof(System.String));
            tbl.Columns.Add("PK_TABLE_NAME", typeof(System.String));
            tbl.Columns.Add("PK_COLUMN_NAME", typeof(System.String));
            tbl.Columns.Add("PK_ORDINAL_POSITION", typeof(System.String));
            tbl.Columns.Add("PK_NAME", typeof(System.String));
            tbl.Columns.Add("FK_TABLE_CATALOG", typeof(System.String));
            tbl.Columns.Add("FK_TABLE_SCHEMA", typeof(System.String));
            tbl.Columns.Add("FK_TABLE_NAME", typeof(System.String));
            tbl.Columns.Add("FK_COLUMN_NAME", typeof(System.String));
            tbl.Columns.Add("FK_ORDINAL_POSITION", typeof(System.Int32));
            tbl.Columns.Add("FK_NAME", typeof(System.String));

            return tbl;
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
                _connection.Dispose();
            }
        }

        #endregion
    }
}
