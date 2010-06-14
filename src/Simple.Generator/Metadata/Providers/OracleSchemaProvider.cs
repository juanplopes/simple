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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Simple.Metadata
{
    class OracleSchemaProvider : DbSchemaProvider
    {
        public OracleSchemaProvider(MetaContext context) : base(context) { }

        #region ' IDbProvider Members '

        public override IEnumerable<DbTable> GetTables(IList<string> includedTables, IList<string> excludedTables)
        {
            DataTable tblTables = base.GetDTSchemaTables();
            DataTable tblViews = base.GetDTSchemaTables();

            LoadTableWithCommand(tblTables, "SELECT * FROM ({0}) WHERE TABLE_TYPE!='SYSTEM TABLE'", sqlTables);
            LoadTableWithCommand(tblViews, "SELECT * FROM ({0}) WHERE TABLE_TYPE!='SYSTEM VIEW'", sqlViews);

            foreach (DataRow viewRow in tblViews.Rows)
            {
                tblTables.ImportRow(viewRow);
            }

            return ConstructTables(tblTables.Rows.OfType<DataRow>());
        }
        public override IEnumerable<DbRelation> GetConstraints(IList<string> includedTables, IList<string> excludedTables)
        {
            DataTable tbl = new DataTable("Constraints");
            LoadTableWithCommand(tbl, sqlConstrains);
            return ConstructRelations(tbl.Rows.OfType<DataRow>());
        }

        public override IEnumerable<DbColumn> GetColumns(DbTableName table)
        {
            var columns = base.GetColumns(table).ToList();

            var table2 = GetConnection().GetSchema("Columns", new[] { table.TableSchema, table.TableName })
                .Rows.OfType<DataRow>().ToDictionary(x => (string)x["COLUMN_NAME"]);

            foreach (var column in columns)
            {
                column.DataTypeName = table2[column.ColumnName].GetValue<string>("DATATYPE");
            }

            return columns;
        }

        public override DbType GetDbColumnType(string providerDbType)
        {
            switch (providerDbType)
            {
                case "1":   // BFile
                    return DbType.Binary;

                case "2":   // Blob
                    return DbType.Binary;

                case "3":   // Char
                    return DbType.AnsiStringFixedLength;

                case "4":   // Clob
                    return DbType.AnsiString;

                case "5":   // Cursor
                    return DbType.Object;

                case "6":   // DateTime
                    return DbType.DateTime;

                case "7":   // IntervalDayToSecond
                    return DbType.Object;

                case "8":   // IntervalYearToMonth
                    return DbType.Int32;

                case "9":   // LongRaw
                    return DbType.Binary;

                case "10":  // LongVarChar
                    return DbType.AnsiString;

                case "11":  // NChar
                    return DbType.StringFixedLength;

                case "12":  // NClob
                    return DbType.String;

                case "13":  // Number
                    return DbType.VarNumeric;

                case "14":  // NVarChar
                    return DbType.String;

                case "15":  // Raw
                    return DbType.Binary;

                case "16":  // RowId
                    return DbType.AnsiString;

                case "18":  // Timestamp
                    return DbType.DateTime;

                case "19":  // TimestampLocal
                    return DbType.DateTime;

                case "20":  // TimestampWithTZ
                    return DbType.DateTime;

                case "22":  // VarChar
                    return DbType.AnsiString;

                case "23":  // Byte
                    return DbType.Byte;

                case "24":  // UInt16
                    return DbType.UInt16;

                case "25":  // UInt32
                    return DbType.UInt32;

                case "26":  // SByte
                    return DbType.SByte;

                case "27":  // Int16
                    return DbType.Int16;

                case "28":  // Int32
                    return DbType.Int32;

                case "29":  // Float
                    return DbType.Single;

                case "30":  // Double
                    return DbType.Double;

                default:
                    return DbType.String;
            }


            throw new NotImplementedException();
        }

        public override string QualifiedTableName(DbTableName table)
        {
            if (!string.IsNullOrEmpty(table.TableSchema))
                return string.Format("{0}.{1}", DoubleQuoteIfNeeded(table.TableSchema), DoubleQuoteIfNeeded(table.TableName));
            else
                return string.Format("{0}", DoubleQuoteIfNeeded(table.TableName));
        }

        private string DoubleQuoteIfNeeded(string variable)
        {
            if (variable.IndexOf(' ') > -1)
                return string.Format("\"{0}\"", variable);
            else
                return variable;
        }

        #endregion

        #region ' Oracle SQL code: database objects '
        //
        // Oracle: find tables, views, procedures and database constrains
        //

        const string sqlTables =
            "SELECT " +
            "	NULL AS TABLE_CATALOG," +
            "	OWNER AS TABLE_SCHEMA," +
            "	TABLE_NAME," +
            "	DECODE(OWNER," +
            "		'SYS','SYSTEM TABLE'," +
            "		'SYSTEM','SYSTEM TABLE'," +
            "		'SYSMAN','SYSTEM TABLE'," +
            "		'CTXSYS','SYSTEM TABLE'," +
            "		'MDSYS','SYSTEM TABLE'," +
            "		'OLAPSYS','SYSTEM TABLE'," +
            "		'ORDSYS','SYSTEM TABLE'," +
            "		'OUTLN','SYSTEM TABLE'," +
            "		'WKSYS','SYSTEM TABLE'," +
            "		'WMSYS','SYSTEM TABLE'," +
            "		'XDB','SYSTEM TABLE'," +
            "		'ORDPLUGINS','SYSTEM TABLE'," +
            "	'TABLE') AS TABLE_TYPE," +
            "	NULL AS TABLE_GUID," +
            "	NULL AS DESCRIPTION," +
            "	NULL AS TABLE_PROPID," +
            "	NULL AS DATE_CREATED," +
            "	NULL AS DATE_MODIFIED " +
            "FROM SYS.ALL_TABLES " +
            "ORDER BY TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME";

        const string sqlViews =
            "SELECT " +
            "	NULL AS TABLE_CATALOG," +
            "	OWNER AS TABLE_SCHEMA," +
            "	VIEW_NAME AS TABLE_NAME," +
            "	DECODE(OWNER," +
            "		'SYS','SYSTEM VIEW'," +
            "		'SYSTEM','SYSTEM VIEW'," +
            "		'SYSMAN','SYSTEM VIEW'," +
            "		'CTXSYS','SYSTEM VIEW'," +
            "		'MDSYS','SYSTEM VIEW'," +
            "		'OLAPSYS','SYSTEM VIEW'," +
            "		'ORDSYS','SYSTEM VIEW'," +
            "		'OUTLN','SYSTEM VIEW'," +
            "		'WKSYS','SYSTEM VIEW'," +
            "		'WMSYS','SYSTEM VIEW'," +
            "		'XDB','SYSTEM VIEW'," +
            "		'ORDPLUGINS','SYSTEM VIEW'," +
            "	'VIEW') AS TABLE_TYPE," +
            "	NULL AS TABLE_GUID," +
            "	NULL AS DESCRIPTION," +
            "	NULL AS TABLE_PROPID," +
            "	NULL AS DATE_CREATED," +
            "	NULL AS DATE_MODIFIED " +
            "FROM SYS.ALL_VIEWS " +
            "ORDER BY TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME";

        const string sqlProcedures =
            "SELECT " +
            "	NULL AS SPECIFIC_CATALOG," +
            "	OWNER AS SPECIFIC_SCHEMA," +
            "	OBJECT_NAME AS SPECIFIC_NAME," +
            "	NULL AS ROUTINE_CATALOG," +
            "	OWNER AS ROUTINE_SCHEMA," +
            "	OBJECT_NAME AS ROUTINE_NAME," +
            "	DECODE(OWNER," +
            "		'SYS','SYSTEM PROCEDURE'," +
            "		'SYSTEM','SYSTEM PROCEDURE'," +
            "		'SYSMAN','SYSTEM PROCEDURE'," +
            "		'CTXSYS','SYSTEM PROCEDURE'," +
            "		'MDSYS','SYSTEM PROCEDURE'," +
            "		'OLAPSYS','SYSTEM PROCEDURE'," +
            "		'ORDSYS','SYSTEM PROCEDURE'," +
            "		'OUTLN','SYSTEM PROCEDURE'," +
            "		'WKSYS','SYSTEM PROCEDURE'," +
            "		'WMSYS','SYSTEM PROCEDURE'," +
            "		'XDB','SYSTEM PROCEDURE'," +
            "		'ORDPLUGINS','SYSTEM PROCEDURE'," +
            "	'PROCEDURE') AS ROUTINE_TYPE," +
            "	CREATED AS DATE_CREATED," +
            "	\"TIMESTAMP\" AS LAST_ALTERED " +
            "FROM SYS.ALL_OBJECTS " +
            "WHERE (OBJECT_TYPE = 'PROCEDURE') " +
            "ORDER BY ROUTINE_CATALOG,ROUTINE_SCHEMA,ROUTINE_NAME";

        const string sqlConstrains =
            "SELECT " +
            "	NULL AS PK_TABLE_CATALOG," +
            "	KCUUC.OWNER AS PK_TABLE_SCHEMA," +
            "	KCUUC.TABLE_NAME AS PK_TABLE_NAME," +
            "	KCUUC.COLUMN_NAME AS PK_COLUMN_NAME," +
            "	KCUUC.POSITION AS PK_ORDINAL_POSITION," +
            "	KCUUC.CONSTRAINT_NAME AS PK_NAME," +
            "	NULL AS FK_TABLE_CATALOG," +
            "	KCUC.OWNER AS FK_TABLE_SCHEMA," +
            "	KCUC.TABLE_NAME AS FK_TABLE_NAME," +
            "	KCUC.COLUMN_NAME AS FK_COLUMN_NAME," +
            "	KCUC.POSITION AS FK_ORDINAL_POSITION," +
            "	KCUC.CONSTRAINT_NAME AS FK_NAME " +
            "FROM ALL_CONSTRAINTS RC, ALL_CONS_COLUMNS KCUC, ALL_CONS_COLUMNS KCUUC " +
            "WHERE " +
            "	KCUC.OWNER = RC.OWNER AND" +
            "	KCUC.TABLE_NAME = RC.TABLE_NAME AND" +
            "	KCUC.CONSTRAINT_NAME = RC.CONSTRAINT_NAME AND" +
            "	KCUC.OWNER= RC.OWNER AND" +
            "	KCUC.TABLE_NAME= RC.TABLE_NAME AND" +
            "	KCUUC.OWNER = RC.R_OWNER AND" +
            "	KCUUC.CONSTRAINT_NAME = RC.R_CONSTRAINT_NAME AND" +
            "	RC.CONSTRAINT_TYPE = 'R'";


        #endregion

    }
}
