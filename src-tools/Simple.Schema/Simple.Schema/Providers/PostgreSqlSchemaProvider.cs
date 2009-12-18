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

namespace Simple.Meta.Providers
{
    class PostgreSqlSchemaProvider : DbSchemaProvider
    {
        public PostgreSqlSchemaProvider(string connectionstring, string providername) : base(connectionstring, providername) { }

        #region ' IDbProvider Members '

        public override DataTable GetConstraints()
        {
            DataTable tbl = new DataTable("Constraints");
            using (DbConnection _Connection = this.GetDBConnection())
            {
                DbCommand _Command = _Connection.CreateCommand();
                _Command.CommandText = sqlConstraints;
                _Command.CommandType = CommandType.Text;
                tbl.Load(_Command.ExecuteReader());
            }

            return tbl;
        }

        public override DataTable GetSchemaTables()
        {
            DataTable tbl = GetDTSchemaTables();
            using (DbConnection _Connection = GetDBConnection())
            {
                DbCommand _Command = _Connection.CreateCommand();
                _Command.CommandText = sqlTables;
                _Command.CommandType = CommandType.Text;
                tbl.Load(_Command.ExecuteReader());
            }
            return tbl;
        }

        public override DataTable GetProcedures()
        {
            DataTable tbl = GetDTSchemaProcedures();
            using (DbConnection _Connection = GetDBConnection())
            {
                DbCommand _Command = _Connection.CreateCommand();
                _Command.CommandText = sqlProcedures;
                _Command.CommandType = CommandType.Text;
                tbl.Load(_Command.ExecuteReader());
            }

            return tbl;
        }

        public override DbType GetDbColumnType(string providerDbType)
        {
            switch (providerDbType)
            {
                case "1":	// NpgsqlDbType.Bigint
                    return DbType.Int64;

                case "2":	// NpgsqlDbType.Boolean
                    return DbType.Boolean;

                case "3":	// NpgsqlDbType.Box
                case "5":	// NpgsqlDbType.Circle
                case "10":	// NpgsqlDbType.Line
                case "11":	// NpgsqlDbType.LSeg
                case "14":	// NpgsqlDbType.Path                            
                case "15":	// NpgsqlDbType.Point
                case "16":	// NpgsqlDbType.Polygon
                case "24":	// NpgsqlDbType.Inet
                case "25":	// NpgsqlDbType.Bit
                case "30":	// NpgsqlDbType.Interval
                case "-2147483648":	// NpgsqlDbType.Array
                    return DbType.Object;

                case "4":	// NpgsqlDbType.Bytea
                    return DbType.Binary;

                case "6":	// NpgsqlDbType.Char
                case "29":	// NpgsqlDbType.Oidvector
                    return DbType.String;

                case "7":	// NpgsqlDbType.Date
                    return DbType.Date;

                case "8":	// NpgsqlDbType.Double
                    return DbType.Double;

                case "9":	// NpgsqlDbType.Integer
                    return DbType.Int32;

                case "12":	// NpgsqlDbType.Money
                    return DbType.Currency;

                case "13":	// NpgsqlDbType.Numeric
                    return DbType.Decimal;

                case "17":	// NpgsqlDbType.Real
                    return DbType.Single;

                case "18":	// NpgsqlDbType.Smallint
                    return DbType.Int16;

                case "19":	// NpgsqlDbType.Text
                case "22":	// NpgsqlDbType.Varchar
                case "23":	// NpgsqlDbType.Refcursor
                    return DbType.String;

                case "20":	// NpgsqlDbType.Time
                case "31":	// NpgsqlDbType.TimeTZ
                    return DbType.Time;

                case "21":	// NpgsqlDbType.Timestamp
                case "26":	// NpgsqlDbType.TimestampTZ
                    return DbType.DateTime;

                case "27":	// NpgsqlDbType.Uuid
                    return DbType.Guid;

                case "28":	// NpgsqlDbType.Xml
                    return DbType.Xml;

                default:
                    return DbType.AnsiString;
            }
        }

        public override string QualifiedTableName(string tableSchema, string tableName)
        {
            if (!string.IsNullOrEmpty(tableSchema))
                return string.Format("{0}.{1}", DoubleQuoteIfNeeded(tableSchema), DoubleQuoteIfNeeded(tableName));
            else
                return string.Format("{0}", DoubleQuoteIfNeeded(tableName));
        }

        private string DoubleQuoteIfNeeded(string variable)
        {
            if (variable.IndexOf(' ') > -1)
                return string.Format("\"{0}\"", variable);
            else
                return variable;
        }

        #endregion

        #region ' SQL code: database constrains '
        //
        // PostgreSQL Server: find database constrains
        //
        const string sqlTables =
            "SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE " +
            "FROM INFORMATION_SCHEMA.TABLES " +
            "WHERE (TABLE_SCHEMA <> 'pg_catalog') AND (TABLE_SCHEMA <> 'information_schema') " +
            "ORDER BY TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME";

        const string sqlProcedures =
            "SELECT SPECIFIC_CATALOG, SPECIFIC_SCHEMA, SPECIFIC_NAME, ROUTINE_CATALOG, ROUTINE_SCHEMA, ROUTINE_NAME, ROUTINE_TYPE, CREATED, LAST_ALTERED " +
            "FROM INFORMATION_SCHEMA.ROUTINES " +
            "WHERE (SPECIFIC_SCHEMA <> 'pg_catalog') AND (SPECIFIC_SCHEMA <> 'information_schema') " +
            "ORDER BY SPECIFIC_CATALOG, SPECIFIC_SCHEMA, SPECIFIC_NAME";

        const string sqlConstraints =
            "SELECT" +
            "	KCUUC.TABLE_CATALOG AS PK_TABLE_CATALOG," +
            "	KCUUC.TABLE_SCHEMA AS PK_TABLE_SCHEMA," +
            "	KCUUC.TABLE_NAME AS PK_TABLE_NAME," +
            "	KCUUC.COLUMN_NAME AS PK_COLUMN_NAME," +
            "	KCUUC.ORDINAL_POSITION AS PK_ORDINAL_POSITION," +
            "	KCUUC.CONSTRAINT_NAME AS PK_NAME, " +
            "	KCUC.TABLE_CATALOG AS FK_TABLE_CATALOG," +
            "	KCUC.TABLE_SCHEMA AS FK_TABLE_SCHEMA," +
            "	KCUC.TABLE_NAME AS FK_TABLE_NAME," +
            "	KCUC.COLUMN_NAME AS FK_COLUMN_NAME," +
            "	KCUC.ORDINAL_POSITION AS FK_ORDINAL_POSITION," +
            "	KCUC.CONSTRAINT_NAME AS FK_NAME " +
            "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC " +
            "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCUC" +
            "	ON (RC.CONSTRAINT_CATALOG = KCUC.CONSTRAINT_CATALOG" +
            "		AND RC.CONSTRAINT_SCHEMA = KCUC.CONSTRAINT_SCHEMA" +
            "		AND RC.CONSTRAINT_NAME = KCUC.CONSTRAINT_NAME)" +
            "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCUUC" +
            "	ON (RC.UNIQUE_CONSTRAINT_CATALOG = KCUUC.CONSTRAINT_CATALOG" +
            "		AND RC.UNIQUE_CONSTRAINT_SCHEMA = KCUUC.CONSTRAINT_SCHEMA" +
            "		AND RC.UNIQUE_CONSTRAINT_NAME = KCUUC.CONSTRAINT_NAME)";

        #endregion

    }
}
