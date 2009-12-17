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

namespace Simple.Meta
{
    public class DbSchema
    {
        private string _ConnectionName;
        private string _ConnectionString;
        private string _ProviderName;
        private DbSchemaProvider _Provider;
        private Dictionary<string, DataTable> Cache = new Dictionary<string, DataTable>();
        private ArrayList dictionaryTablesManyToMany = new ArrayList();
        private ArrayList dictionaryTablesOneToOne = new ArrayList();

        #region ' Properties '

        public string ConnectionName
        {
            get { return _ConnectionName; }
        }

        public string ConnectionString
        {
            get { return _ConnectionString; }
        }

        public string ProviderName
        {
            get { return _ProviderName; }
        }

        #endregion

        #region ' Constractors '

        public DbSchema() :
            this("LocalSqlServer")
        { }

        public DbSchema(string ConnectionName) :
            this(ConnectionName, System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString, System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionName].ProviderName)
        { }

        public DbSchema(string ConnectionString, string ProviderName) :
            this(null, ConnectionString, ProviderName)
        { }

        private DbSchema(string ConnectionName, string ConnectionString, string ProviderName)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
                _ConnectionName = ConnectionName;

            _ConnectionString = ConnectionString;
            _ProviderName = ProviderName;
            _Provider = GetSchemaProvider(_ConnectionString, _ProviderName);
            DiscoverTableRelations();
        }

        #endregion

        #region ' Methods '

        #region ' Tables and Views '

        private DataTable GetSchemaTables()
        {
            string CacheKey = "SchemaTables";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable tbl = _Provider.GetSchemaTables();

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTables()
        {
            string CacheKey = "Tables";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable schemaTables = GetSchemaTables();
            DataTable tbl = new DataTable("Tables");
            if (schemaTables.Rows.Count > 0)
            {
                string WhereClause = "TABLE_TYPE = 'TABLE' OR TABLE_TYPE = 'BASE TABLE'";
                tbl = schemaTables.Clone();
                foreach (DataRow tblRow in schemaTables.Select(WhereClause))
                {
                    tbl.ImportRow(tblRow);
                }
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetViews()
        {
            string CacheKey = "Views";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable schemaTables = GetSchemaTables();
            DataTable tbl = new DataTable("Views");
            if (schemaTables.Rows.Count > 0)
            {
                string WhereClause = "TABLE_TYPE = 'VIEW'";
                tbl = schemaTables.Clone();
                foreach (DataRow tblRow in schemaTables.Select(WhereClause))
                {
                    tbl.ImportRow(tblRow);
                }
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTablesAndViews()
        {
            string CacheKey = "TablesAndViews";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable schemaTables = GetSchemaTables();
            DataTable tbl = new DataTable("TablesAndViews");
            if (schemaTables.Rows.Count > 0)
            {
                string WhereClause = "TABLE_TYPE = 'TABLE' OR TABLE_TYPE = 'BASE TABLE' OR TABLE_TYPE = 'VIEW'";
                tbl = schemaTables.Clone();
                foreach (DataRow tblRow in schemaTables.Select(WhereClause))
                {
                    tbl.ImportRow(tblRow);
                }
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTablesLogical()
        {
            string CacheKey = "LogicalTables";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DiscoverTableRelations();

            return GetTablesLogical();
        }

        public DataTable GetTablesManyToMany()
        {
            string CacheKey = "ManyToManyTables";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DiscoverTableRelations();

            return GetTablesManyToMany();
        }

        private void DiscoverTableRelations()
        {
            DataTable schemaTables = GetTables();
            DataTable tblManyToMany = new DataTable("ManyToManyTables");
            tblManyToMany = schemaTables.Clone();
            DataTable tblLogical = new DataTable("LogicalTables");
            tblLogical = schemaTables.Clone();

            dictionaryTablesManyToMany.Clear();
            foreach (DataRow tableRow in schemaTables.Rows)
            {
                string tableSchema = null;
                if (tableRow["TABLE_SCHEMA"] != DBNull.Value)
                    tableSchema = tableRow["TABLE_SCHEMA"].ToString();
                string tableName = tableRow["TABLE_NAME"].ToString();

                // Discover Many-to-Many relations
                int foreignKeyRelationsCount = GetTableRelationsByForeignKey(tableSchema, tableName).Rows.Count;
                int tableColumnsCount = GetTableColumns(tableSchema, tableName).Rows.Count;
                int primaryKeyColumnsCount = GetTablePrimaryKeyColumns(tableSchema, tableName).Rows.Count;

                if ((foreignKeyRelationsCount == tableColumnsCount) || (primaryKeyColumnsCount + foreignKeyRelationsCount == tableColumnsCount))
                {
                    dictionaryTablesManyToMany.Add(_Provider.QualifiedTableName(tableSchema, tableName));
                    tblManyToMany.ImportRow(tableRow);
                }
                else
                    tblLogical.ImportRow(tableRow);


            }

            Cache.Add("ManyToManyTables", tblManyToMany);
            Cache.Add("LogicalTables", tblLogical);
        }

        #endregion

        #region ' Columns and Primary Keys '

        public DataTable GetTableColumns(string tableSchema, string tableName)
        {
            string CacheKey = "Columns:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable tbl = new DataTable();
            DataTable tableColumns = _Provider.GetTableColumns(tableSchema, tableName);
            if (tableColumns.Columns.Contains("IsHidden"))
            {
                if (tableColumns.Select("IsHidden = 1").Length > 0)
                {
                    tbl = tableColumns.Clone();
                    foreach (DataRow columnRow in tableColumns.Select("IsHidden = 0 OR IsHidden IS NULL"))
                    {
                        tbl.ImportRow(columnRow);
                    }
                }
                else
                    tbl = tableColumns;
            }
            else
                tbl = tableColumns;

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTablePrimaryKeyColumns(string tableSchema, string tableName)
        {
            DataTable TableColumns = GetTableColumns(tableSchema, tableName);
            string WhereClause = "IsKey = true";
            DataTable tbl = TableColumns.Clone();
            foreach (DataRow primaryKeyRow in TableColumns.Select(WhereClause))
                tbl.ImportRow(primaryKeyRow);

            return tbl;
        }

        public DataTable GetTableFields(string tableSchema, string tableName)
        {
            List<string> filteredcolumns = new List<string>();

            foreach (DataRow primarykey in GetTablePrimaryKeyColumns(tableSchema, tableName).Rows)
            {
                string columnname = "'" + primarykey["ColumnName"].ToString() + "'";
                if (!filteredcolumns.Contains(columnname))
                    filteredcolumns.Add(columnname);
            }

            foreach (DataRow pkRelationRow in GetTableRelationsByPrimaryKey(tableSchema, tableName).Rows)
            {
                string columnname = "'" + pkRelationRow["PK_COLUMN_NAME"].ToString() + "'";
                if (!filteredcolumns.Contains(columnname))
                    filteredcolumns.Add(columnname);
            }
            foreach (DataRow fkRelationRow in GetTableRelationsByForeignKey(tableSchema, tableName).Rows)
            {
                string columnname = "'" + fkRelationRow["FK_COLUMN_NAME"].ToString() + "'";
                if (!filteredcolumns.Contains(columnname))
                    filteredcolumns.Add(columnname);
            }

            string WhereClause = "ColumnName NOT IN ( " + string.Join(", ", filteredcolumns.ToArray()) + " )";
            DataTable TableColumns = GetTableColumns(tableSchema, tableName);
            DataTable tbl = TableColumns.Clone();
            foreach (DataRow columnRow in TableColumns.Select(WhereClause))
            {
                tbl.ImportRow(columnRow);
            }

            return tbl;
        }

        #endregion

        #region ' Relations '

        private DataTable GetSchemaConstraints()
        {
            string CacheKey = "Constraints";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable tbl = _Provider.GetConstraints();

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTableRelationsOneToMany(string tableSchema, string tableName)
        {
            string CacheKey = "OneToManyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable otmRelations = GetTableRelationsByPrimaryKey(tableSchema, tableName);
            DataTable tbl = otmRelations.Clone();
            foreach (DataRow relationRow in otmRelations.Rows)
            {
                string fkTableSchema = null;
                if (relationRow["FK_TABLE_SCHEMA"] != DBNull.Value)
                    fkTableSchema = relationRow["FK_TABLE_SCHEMA"].ToString();
                string fkTableName = relationRow["FK_TABLE_NAME"].ToString();
                string fkColumnName = relationRow["FK_COLUMN_NAME"].ToString();

                DataTable fkTablePrimaryKeys = GetTablePrimaryKeyColumns(fkTableSchema, fkTableName);
                bool fkColumnIsPrimaryKey = false;
                foreach (DataRow primarykeyRow in fkTablePrimaryKeys.Rows)
                {
                    string pkColumnName = primarykeyRow["ColumnName"].ToString();
                    if (fkColumnName.ToLower() == pkColumnName.ToLower())
                    {
                        fkColumnIsPrimaryKey = true;
                        break;
                    }
                }

                string tableNameHash = _Provider.QualifiedTableName(fkTableSchema, fkTableName);
                if (!dictionaryTablesManyToMany.Contains(tableNameHash) && !fkColumnIsPrimaryKey)
                    tbl.ImportRow(relationRow);
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTableRelationsManyToOne(string tableSchema, string tableName)
        {
            string CacheKey = "ManyToOneRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable mtoRelations = GetTableRelationsByForeignKey(tableSchema, tableName);
            DataTable tbl = mtoRelations.Clone();
            foreach (DataRow relationRow in mtoRelations.Rows)
            {
                string pkTableSchema = null;
                if (relationRow["PK_TABLE_SCHEMA"] != DBNull.Value)
                    pkTableSchema = relationRow["PK_TABLE_SCHEMA"].ToString();
                string pkTableName = relationRow["PK_TABLE_NAME"].ToString();

                string tableNameHash = _Provider.QualifiedTableName(pkTableSchema, pkTableName);
                if (!dictionaryTablesManyToMany.Contains(tableNameHash))
                    tbl.ImportRow(relationRow);
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTableRelationsManyToMany(string tableSchema, string tableName)
        {
            string CacheKey = "ManyToManyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable mtmRelations = GetTableRelationsByPrimaryKey(tableSchema, tableName);
            DataTable tbl = mtmRelations.Clone();
            foreach (DataRow relationRow in mtmRelations.Rows)
            {
                string fkTableSchema = null;
                string fkTableName = null;
                if (relationRow["FK_TABLE_SCHEMA"] != DBNull.Value)
                    fkTableSchema = relationRow["FK_TABLE_SCHEMA"].ToString();
                fkTableName = relationRow["FK_TABLE_NAME"].ToString();

                string tableNameHash = _Provider.QualifiedTableName(fkTableSchema, fkTableName);
                if (dictionaryTablesManyToMany.Contains(tableNameHash))
                    tbl.ImportRow(relationRow);
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTableRelationsOneToOne(string tableSchema, string tableName)
        {
            string CacheKey = "OneToOneRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable pkRelations = GetTableRelationsByPrimaryKey(tableSchema, tableName);
            DataTable tbl = pkRelations.Clone();
            foreach (DataRow relationRow in pkRelations.Rows)
            {
                string fkTableSchema = null;
                if (relationRow["FK_TABLE_SCHEMA"] != DBNull.Value)
                    fkTableSchema = relationRow["FK_TABLE_SCHEMA"].ToString();
                string fkTableName = relationRow["FK_TABLE_NAME"].ToString();
                string fkColumnName = relationRow["FK_COLUMN_NAME"].ToString();

                DataTable fkTablePrimaryKeys = GetTablePrimaryKeyColumns(fkTableSchema, fkTableName);
                bool fkColumnIsPrimaryKey = false;
                foreach (DataRow primarykeyRow in fkTablePrimaryKeys.Rows)
                {
                    string pkColumnName = primarykeyRow["ColumnName"].ToString();
                    if (fkColumnName.ToLower() == pkColumnName.ToLower())
                    {
                        fkColumnIsPrimaryKey = true;
                        break;
                    }
                }

                string tableNameHash = _Provider.QualifiedTableName(fkTableSchema, fkTableName);
                if (!dictionaryTablesManyToMany.Contains(tableNameHash) && fkColumnIsPrimaryKey)
                    tbl.ImportRow(relationRow);
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTableRelationsByPrimaryKey(string tableSchema, string tableName)
        {
            string CacheKey = "PrimaryKeyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            string WhereClause;
            if (!string.IsNullOrEmpty(tableSchema))
                WhereClause = string.Format("PK_TABLE_SCHEMA = '{0}' AND PK_TABLE_NAME = '{1}'", tableSchema, tableName);
            else
                WhereClause = string.Format("PK_TABLE_NAME = '{0}'", tableName);

            DataTable relations = GetSchemaConstraints();
            DataRow[] pkRelations = (DataRow[])relations.Select(WhereClause);
            DataTable tbl = relations.Clone();
            foreach (DataRow relationRow in pkRelations)
            {
                tbl.ImportRow(relationRow);
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetTableRelationsByForeignKey(string tableSchema, string tableName)
        {
            string CacheKey = "ForeignKeyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            string WhereClause;
            if (!string.IsNullOrEmpty(tableSchema))
                WhereClause = string.Format("FK_TABLE_SCHEMA = '{0}' AND FK_TABLE_NAME = '{1}'", tableSchema, tableName);
            else
                WhereClause = string.Format("FK_TABLE_NAME = '{0}'", tableName);

            DataTable relations = GetSchemaConstraints();
            DataRow[] fkRelations = (DataRow[])relations.Select(WhereClause);
            DataTable tbl = relations.Clone();
            foreach (DataRow relationRow in fkRelations)
            {
                tbl.ImportRow(relationRow);
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        #endregion

        #region ' Store Procedures '

        public DataTable GetProcedures()
        {
            string CacheKey = "Procedures";
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable tblProcedures = _Provider.GetProcedures();
            DataTable tbl = new DataTable("Procedures");
            tbl = tblProcedures.Clone();
            if (tblProcedures.Rows.Count > 0)
            {
                string WhereClause = "ROUTINE_TYPE = 'PROCEDURE'";
                foreach (DataRow tblRow in tblProcedures.Select(WhereClause))
                {
                    tbl.ImportRow(tblRow);
                }
            }

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        public DataTable GetProcedureParameters(string procedureSchema, string procedureName)
        {
            string CacheKey = "ProcedureParameters:" + (string.IsNullOrEmpty(procedureSchema) ? procedureName : procedureSchema + "." + procedureName);
            if (Cache.Keys.Contains(CacheKey))
                return Cache[CacheKey];

            DataTable tbl = _Provider.GetProcedureParameters(procedureSchema, procedureName);

            Cache.Add(CacheKey, tbl);
            return tbl;
        }

        #endregion

        #endregion

        #region ' Helpers '

        private DbSchemaProvider GetSchemaProvider(string connectionString, string providerName)
        {
            DbSchemaProvider dbProvider;
            switch (providerName.ToLower())
            {
                case "system.data.sqlserverce.3.5":
                case "system.data.sqlserverce":
                    dbProvider = new SqlServerCeSchemaProvider(connectionString, providerName);
                    break;

                case "system.data.oledb":
                    dbProvider = new OleDbSchemaProvider(connectionString, providerName);
                    break;

                case "system.data.sqlclient":
                    dbProvider = new SqlServerSchemaProvider(connectionString, providerName);
                    break;

                case "mysql.data.mysqlclient":
                    dbProvider = new MySqlSchemaProvider(connectionString, providerName);
                    break;

                case "npgsql":
                    dbProvider = new PostgreSqlSchemaProvider(connectionString, providerName);
                    break;

                case "system.data.sqlite":
                    dbProvider = new SQLiteSchemaProvider(connectionString, providerName);
                    break;

                case "system.data.oracleclient":
                case "oracle.dataaccess.client":
                    dbProvider = new OracleSchemaProvider(connectionString, providerName);
                    break;

                case "vistadb.net20":
                    dbProvider = new VistaDBSchemaProvider(connectionString, providerName);
                    break;

                default:
                    throw new NotImplementedException("The provider '" + providerName + "' is not implemented!");

            }
            return dbProvider;
        }

        public string GetPropertyType(string SystemType)
        {
            return this.GetPropertyType(SystemType, false);
        }

        public string GetPropertyType(string SystemType, bool IsNullable)
        {
            return _Provider.GetPropertyType(SystemType, IsNullable);
        }

        public DbType GetDbColumnType(string providerDbType)
        {
            return _Provider.GetDbColumnType(providerDbType);
        }

        public string QualifiedTableName(string tableSchema, string tableName)
        {
            return _Provider.QualifiedTableName(tableSchema, tableName);
        }

        public string GetDatabaseName()
        {
            return _Provider.GetDatabaseName();
        }

        #endregion

    }
}
