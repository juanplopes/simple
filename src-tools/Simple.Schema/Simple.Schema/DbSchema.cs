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

namespace Simple.Schema
{
    public class DbSchema : DbObject
    {
        private DataTable _tableCache = null;
        public DbSchema(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        private DataTable GetSchemaTables()
        {
            if (_tableCache != null) return _tableCache;
            return _tableCache = Provider.GetSchemaTables();
        }

        public IEnumerable<DbTable> GetTables()
        {
            foreach (var row in GetSchemaTables().Select("TABLE_TYPE = 'TABLE' OR TABLE_TYPE = 'BASE TABLE'"))
                yield return new DbTable(Provider, row);
        }

        public IEnumerable<DbTable> GetViews()
        {
            foreach (var row in GetSchemaTables().Select("TABLE_TYPE = 'VIEW'"))
                yield return new DbTable(Provider, row);
        }

        //#region ' Relations '

        private DataTable GetSchemaConstraints()
        {
            DataTable tbl = Provider.GetConstraints();

            return tbl;
        }

        //public DataTable GetTableRelationsOneToMany(string tableSchema, string tableName)
        //{
        //    string CacheKey = "OneToManyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    DataTable otmRelations = GetTableRelationsByPrimaryKey(tableSchema, tableName);
        //    DataTable tbl = otmRelations.Clone();
        //    foreach (DataRow relationRow in otmRelations.Rows)
        //    {
        //        string fkTableSchema = null;
        //        if (relationRow["FK_TABLE_SCHEMA"] != DBNull.Value)
        //            fkTableSchema = relationRow["FK_TABLE_SCHEMA"].ToString();
        //        string fkTableName = relationRow["FK_TABLE_NAME"].ToString();
        //        string fkColumnName = relationRow["FK_COLUMN_NAME"].ToString();

        //        DataTable fkTablePrimaryKeys = GetTablePrimaryKeyColumns(fkTableSchema, fkTableName);
        //        bool fkColumnIsPrimaryKey = false;
        //        foreach (DataRow primarykeyRow in fkTablePrimaryKeys.Rows)
        //        {
        //            string pkColumnName = primarykeyRow["ColumnName"].ToString();
        //            if (fkColumnName.ToLower() == pkColumnName.ToLower())
        //            {
        //                fkColumnIsPrimaryKey = true;
        //                break;
        //            }
        //        }

        //        string tableNameHash = _Provider.QualifiedTableName(fkTableSchema, fkTableName);
        //        if (!dictionaryTablesManyToMany.Contains(tableNameHash) && !fkColumnIsPrimaryKey)
        //            tbl.ImportRow(relationRow);
        //    }

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}

        //public DataTable GetTableRelationsManyToOne(string tableSchema, string tableName)
        //{
        //    string CacheKey = "ManyToOneRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    DataTable mtoRelations = GetTableRelationsByForeignKey(tableSchema, tableName);
        //    DataTable tbl = mtoRelations.Clone();
        //    foreach (DataRow relationRow in mtoRelations.Rows)
        //    {
        //        string pkTableSchema = null;
        //        if (relationRow["PK_TABLE_SCHEMA"] != DBNull.Value)
        //            pkTableSchema = relationRow["PK_TABLE_SCHEMA"].ToString();
        //        string pkTableName = relationRow["PK_TABLE_NAME"].ToString();

        //        string tableNameHash = _Provider.QualifiedTableName(pkTableSchema, pkTableName);
        //        if (!dictionaryTablesManyToMany.Contains(tableNameHash))
        //            tbl.ImportRow(relationRow);
        //    }

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}

        //public DataTable GetTableRelationsManyToMany(string tableSchema, string tableName)
        //{
        //    string CacheKey = "ManyToManyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    DataTable mtmRelations = GetTableRelationsByPrimaryKey(tableSchema, tableName);
        //    DataTable tbl = mtmRelations.Clone();
        //    foreach (DataRow relationRow in mtmRelations.Rows)
        //    {
        //        string fkTableSchema = null;
        //        string fkTableName = null;
        //        if (relationRow["FK_TABLE_SCHEMA"] != DBNull.Value)
        //            fkTableSchema = relationRow["FK_TABLE_SCHEMA"].ToString();
        //        fkTableName = relationRow["FK_TABLE_NAME"].ToString();

        //        string tableNameHash = _Provider.QualifiedTableName(fkTableSchema, fkTableName);
        //        if (dictionaryTablesManyToMany.Contains(tableNameHash))
        //            tbl.ImportRow(relationRow);
        //    }

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}

        //public DataTable GetTableRelationsOneToOne(string tableSchema, string tableName)
        //{
        //    string CacheKey = "OneToOneRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    DataTable pkRelations = GetTableRelationsByPrimaryKey(tableSchema, tableName);
        //    DataTable tbl = pkRelations.Clone();
        //    foreach (DataRow relationRow in pkRelations.Rows)
        //    {
        //        string fkTableSchema = null;
        //        if (relationRow["FK_TABLE_SCHEMA"] != DBNull.Value)
        //            fkTableSchema = relationRow["FK_TABLE_SCHEMA"].ToString();
        //        string fkTableName = relationRow["FK_TABLE_NAME"].ToString();
        //        string fkColumnName = relationRow["FK_COLUMN_NAME"].ToString();

        //        DataTable fkTablePrimaryKeys = GetTablePrimaryKeyColumns(fkTableSchema, fkTableName);
        //        bool fkColumnIsPrimaryKey = false;
        //        foreach (DataRow primarykeyRow in fkTablePrimaryKeys.Rows)
        //        {
        //            string pkColumnName = primarykeyRow["ColumnName"].ToString();
        //            if (fkColumnName.ToLower() == pkColumnName.ToLower())
        //            {
        //                fkColumnIsPrimaryKey = true;
        //                break;
        //            }
        //        }

        //        string tableNameHash = _Provider.QualifiedTableName(fkTableSchema, fkTableName);
        //        if (!dictionaryTablesManyToMany.Contains(tableNameHash) && fkColumnIsPrimaryKey)
        //            tbl.ImportRow(relationRow);
        //    }

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}

        //public DataTable GetTableRelationsByPrimaryKey(string tableSchema, string tableName)
        //{
        //    string CacheKey = "PrimaryKeyRelations:" + _Provider.QualifiedTableName(tableSchema, tableName);
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    string WhereClause;
        //    if (!string.IsNullOrEmpty(tableSchema))
        //        WhereClause = string.Format("PK_TABLE_SCHEMA = '{0}' AND PK_TABLE_NAME = '{1}'", tableSchema, tableName);
        //    else
        //        WhereClause = string.Format("PK_TABLE_NAME = '{0}'", tableName);

        //    DataTable relations = GetSchemaConstraints();
        //    DataRow[] pkRelations = (DataRow[])relations.Select(WhereClause);
        //    DataTable tbl = relations.Clone();
        //    foreach (DataRow relationRow in pkRelations)
        //    {
        //        tbl.ImportRow(relationRow);
        //    }

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}

        public DataTable GetTableRelationsByForeignKey(string tableSchema, string tableName)
        {
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

            return tbl;
        }

        //#endregion

        //#region ' Store Procedures '

        //public DataTable GetProcedures()
        //{
        //    string CacheKey = "Procedures";
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    DataTable tblProcedures = _Provider.GetProcedures();
        //    DataTable tbl = new DataTable("Procedures");
        //    tbl = tblProcedures.Clone();
        //    if (tblProcedures.Rows.Count > 0)
        //    {
        //        string WhereClause = "ROUTINE_TYPE = 'PROCEDURE'";
        //        foreach (DataRow tblRow in tblProcedures.Select(WhereClause))
        //        {
        //            tbl.ImportRow(tblRow);
        //        }
        //    }

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}

        //public DataTable GetProcedureParameters(string procedureSchema, string procedureName)
        //{
        //    string CacheKey = "ProcedureParameters:" + (string.IsNullOrEmpty(procedureSchema) ? procedureName : procedureSchema + "." + procedureName);
        //    if (Cache.Keys.Contains(CacheKey))
        //        return Cache[CacheKey];

        //    DataTable tbl = _Provider.GetProcedureParameters(procedureSchema, procedureName);

        //    Cache.Add(CacheKey, tbl);
        //    return tbl;
        //}


        #region ' Helpers '

        public DbType GetDbColumnType(string providerDbType)
        {
            return Provider.GetDbColumnType(providerDbType);
        }

        public string QualifiedTableName(string tableSchema, string tableName)
        {
            return Provider.QualifiedTableName(tableSchema, tableName);
        }

        public string GetDatabaseName()
        {
            return Provider.GetDatabaseName();
        }

        #endregion

    }
}
