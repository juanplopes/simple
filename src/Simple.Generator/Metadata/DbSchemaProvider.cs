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
        private DbConnection _connection = null;
        public MetaContext Context { get; private set; }

        protected DbConnection GetConnection()
        {
            if (_connection == null)
            {
                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(Context.Provider);
                _connection = providerFactory.CreateConnection();
                _connection.ConnectionString = Context.ConnectionString;
                _connection.Open();
            }

            return _connection;
        }

        public DbSchemaProvider(MetaContext context)
        {
            Context = context;
        }

        #region ' IDbProvider Members '

        protected string GetRelationsClause(IList<string> included, IList<string> excluded, bool forceLower)
        {
            return GetGenericClause(
                new[] { "FK_TABLE_NAME", "FK_TABLE_SCHEMA", "FK_TABLE_CATALOG" },
                included, excluded, forceLower);
        }


        protected string GetTablesClause(IList<string> included, IList<string> excluded, bool forceLower)
        {
            return GetGenericClause(
                new[] { "TABLE_NAME", "TABLE_SCHEMA", "TABLE_CATALOG" },
                included, excluded, forceLower);
        }

        protected string GetGenericClause(IList<string> columns, IList<string> included, IList<string> excluded, bool forceLower)
        {
            var incString = included.Count > 0 ?
                string.Join(" OR ", included.Select(x => GetTableWhereClause(columns, "LIKE", x, forceLower)).ToArray()) :
                "1=1";

            var excString = excluded.Count > 0 ?
                string.Join(" AND ", excluded.Select(x => GetTableWhereClause(columns, "NOT LIKE", x, forceLower)).ToArray()) :
                "1=1";

            return string.Format("({0}) AND ({1})", incString, excString);
        }


        protected string GetTableWhereClause(IList<string> columns, string op, string tableName, bool forceLower)
        {
            var names = tableName.Split('.').Reverse().ToList();

            string format = forceLower ? "lower({0}) {1} '{2}'" : "{0} {1} '{2}'";

            var clauses = new List<string>();
            for (int i = 0; i < columns.Count && i < names.Count; i++)
                clauses.Add(string.Format(format, columns[i], op, names[i].ToLower()));

            return "(" + string.Join(" AND ", clauses.ToArray()) + ")";
        }

        virtual public IEnumerable<DbTable> GetTables(IList<string> includedTables, IList<string> excludedTables)
        {
            return ConstructTables(
                GetConnection().GetSchema("Tables")
                .Select(GetTablesClause(includedTables, excludedTables, false)));
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

        virtual public IEnumerable<DbColumn> GetColumns(DbTableName table)
        {
            using (var cmd = CreateCommand("SELECT * FROM {0}", QualifiedTableName(table)))
            using (var reader = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                return ConstructColumns(table, reader.GetSchemaTable().Rows.OfType<DataRow>());
        }

        virtual protected IEnumerable<DbColumn> ConstructColumns(DbTableName table, IEnumerable<DataRow> columns)
        {
            foreach (var row in columns)
            {
                var column = new DbColumn(Context, table, row);
                column.DbColumnType = GetDbColumnType(column.ProviderType);
                yield return column;
            }
        }

        virtual protected IEnumerable<DbTable> ConstructTables(IEnumerable<DataRow> tables)
        {
            foreach (var row in tables)
            {
                var table = new DbTable(Context, row);
                table.QualifiedTableName = QualifiedTableName(table);
                yield return table;
            }
        }


        virtual protected IEnumerable<DbRelation> ConstructRelations(IEnumerable<DataRow> relations)
        {
            return relations.Select(x => new DbRelation(Context, x)).Where(x=>x.FkOrdinalPosition == x.PkOrdinalPosition);
        }


        abstract public IEnumerable<DbRelation> GetConstraints(IList<string> includedTables, IList<string> excludedTables);
        abstract public DbType GetDbColumnType(string providerDbType);

        virtual public string QualifiedTableName(DbTableName table)
        {
            if (!string.IsNullOrEmpty(table.Schema))
                return string.Format("[{0}].[{1}]", table.Schema, table.Name);
            else
                return string.Format("[{0}]", table.Name);
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
