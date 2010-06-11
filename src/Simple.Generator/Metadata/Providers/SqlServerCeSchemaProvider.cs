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
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Simple.Metadata
{
    public class SqlServerCeSchemaProvider : DbSchemaProvider
    {
        public SqlServerCeSchemaProvider(string connectionstring, string providername) : base(connectionstring, providername) { }

        #region ' IDbProvider Members '

        public override string GetDatabaseName()
        {
            string dbName = GetConnection().Database;
            Regex RegExp = new Regex(@"\\(?<db>[^\\]*)?\.sdf$", RegexOptions.IgnoreCase);
            if (RegExp.IsMatch(dbName))
            {
                Match found = RegExp.Matches(dbName)[0];
                dbName = found.Groups[1].Value;
            }
            return dbName;
        }

        public override DataTable GetSchemaTables()
        {
            DataTable tbl = GetDTSchemaTables();
            var cmd = CreateCommand(sqlTables);
            using (var results = cmd.ExecuteReader())
            {
                while (results.Read())
                {
                    DataRow valuesRow = tbl.NewRow();
                    if (results[0] != DBNull.Value)
                        valuesRow[0] = results.GetString(0);
                    if (results[1] != DBNull.Value)
                        valuesRow[1] = results.GetString(1);
                    if (results[2] != DBNull.Value)
                        valuesRow[2] = results.GetString(2);
                    if (results[3] != DBNull.Value)
                        valuesRow[3] = results.GetString(3);
                    if (results[5] != DBNull.Value)
                        valuesRow[5] = results.GetString(5);
                    if (results[7] != DBNull.Value)
                        valuesRow[7] = results.GetDateTime(7);
                    if (results[8] != DBNull.Value)
                        valuesRow[8] = results.GetDateTime(8);
                    tbl.Rows.Add(valuesRow);
                }
            }

            return tbl;
        }

        public override DataTable GetConstraints()
        {
            DataTable tbl = GetDTSchemaConstrains();
            var cmd = CreateCommand(sqlConstraints);
            using (var results = cmd.ExecuteReader())
            {
                while (results.Read())
                {
                    DataRow valuesRow = tbl.NewRow();
                    if (results[0] != DBNull.Value)
                        valuesRow[0] = results.GetString(0);
                    if (results[1] != DBNull.Value)
                        valuesRow[1] = results.GetString(1);
                    if (results[2] != DBNull.Value)
                        valuesRow[2] = results.GetString(2);
                    if (results[3] != DBNull.Value)
                        valuesRow[3] = results.GetString(3);
                    if (results[4] != DBNull.Value)
                        valuesRow[4] = results.GetInt32(4);
                    if (results[5] != DBNull.Value)
                        valuesRow[5] = results.GetString(5);
                    if (results[6] != DBNull.Value)
                        valuesRow[6] = results.GetString(6);
                    if (results[7] != DBNull.Value)
                        valuesRow[7] = results.GetString(7);
                    if (results[8] != DBNull.Value)
                        valuesRow[8] = results.GetString(8);
                    if (results[9] != DBNull.Value)
                        valuesRow[9] = results.GetString(9);
                    if (results[10] != DBNull.Value)
                        valuesRow[10] = results.GetInt32(10);
                    if (results[11] != DBNull.Value)
                        valuesRow[11] = results.GetString(11);

                    tbl.Rows.Add(valuesRow);
                }
            }

            return tbl;
        }

        public override DbType GetDbColumnType(string providerDbType)
        {
            switch (providerDbType.ToLower())
            {
                case "bigint":
                    return DbType.Int64;

                case "binary":
                case "image":
                case "timestamp":
                case "varbinary":
                case "rowversion":
                    return DbType.Binary;

                case "bit":
                    return DbType.Boolean;

                case "char":
                    return DbType.AnsiStringFixedLength;

                case "datetime":
                case "smalldatetime":
                    return DbType.DateTime;

                case "decimal":
                    return DbType.Decimal;

                case "float":
                    return DbType.Double;

                case "int":
                    return DbType.Int32;

                case "money":
                case "smallmoney":
                    return DbType.Currency;

                case "nchar":
                    return DbType.StringFixedLength;

                case "ntext":
                case "nvarchar":
                    return DbType.String;

                case "real":
                    return DbType.Single;

                case "uniqueidentifier":
                    return DbType.Guid;

                case "smallint":
                    return DbType.Int16;

                case "text":
                case "varchar":
                    return DbType.AnsiString;

                case "tinyint":
                    return DbType.Byte;

                default:
                    return DbType.AnsiString;
            }
        }

        #endregion

        #region ' SqlServerCe code: database constrains '
        //
        // SQL Server Compact: find tables and database constrains
        //
        const string sqlTables =
            "SELECT * " +
            "FROM INFORMATION_SCHEMA.TABLES";

        const string sqlConstraints =
            "SELECT" +
            "	KCUUC.TABLE_CATALOG AS PK_TABLE_CATALOG," +
            "	KCUUC.TABLE_SCHEMA AS PK_TABLE_SCHEMA," +
            "	KCUUC.TABLE_NAME AS PK_TABLE_NAME," +
            "	KCUUC.COLUMN_NAME AS PK_COLUMN_NAME," +
            "	KCUUC.ORDINAL_POSITION AS PK_ORDINAL_POSITION," +
            "	KCUUC.CONSTRAINT_NAME AS PK_NAME," +
            "	KCUC.TABLE_CATALOG AS FK_TABLE_CATALOG," +
            "	KCUC.TABLE_SCHEMA AS FK_TABLE_SCHEMA," +
            "	KCUC.TABLE_NAME AS FK_TABLE_NAME, " +
            "	KCUC.COLUMN_NAME AS FK_COLUMN_NAME, " +
            "	KCUC.ORDINAL_POSITION AS FK_ORDINAL_POSITION, " +
            "	KCUC.CONSTRAINT_NAME AS FK_NAME " +
            "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC " +
            "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCUC" +
            "	ON (RC.CONSTRAINT_NAME = KCUC.CONSTRAINT_NAME)" +
            "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCUUC" +
            "	ON (RC.UNIQUE_CONSTRAINT_NAME = KCUUC.CONSTRAINT_NAME)";

        #endregion

    }
}
