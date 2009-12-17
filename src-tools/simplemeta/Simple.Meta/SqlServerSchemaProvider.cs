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

namespace Simple.Meta
{
    class SqlServerSchemaProvider : DbSchemaProvider
    {
        public SqlServerSchemaProvider(string connectionstring, string providername) : base(connectionstring, providername) { }

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

        public override DbType GetDbColumnType(string providerDbType)
        {
            switch (providerDbType)
            {
                case "0":   // SqlDbType.BigInt
                    return DbType.Int64;

                case "1":   // SqlDbType.Binary
                case "7":   // SqlDbType.Image
                case "19":  // SqlDbType.Timestamp
                case "21":  // SqlDbType.VarBinary
                    return DbType.Binary;

                case "2":   // SqlDbType.Bit
                    return DbType.Boolean;

                case "3":   // SqlDbType.Char
                    return DbType.AnsiStringFixedLength;

                case "4":   // SqlDbType.DateTime
                case "15":  // SqlDbType.SmallDateTime
                    return DbType.DateTime;

                case "5":   // SqlDbType.Decimal
                    return DbType.Decimal;

                case "6":   // SqlDbType.Float
                    return DbType.Double;

                case "8":   // SqlDbType.Int
                    return DbType.Int32;

                case "9":   // SqlDbType.Money
                case "17":  // SqlDbType.SmallMoney
                    return DbType.Currency;

                case "10":  // SqlDbType.NChar
                    return DbType.StringFixedLength;

                case "11":  // SqlDbType.NText
                case "12":  // SqlDbType.NVarChar 
                    return DbType.String;

                case "13":  // SqlDbType.Real:
                    return DbType.Single;

                case "14":  // SqlDbType.UniqueIdentifier:
                    return DbType.Guid;

                case "16":  // SqlDbType.SmallInt:
                    return DbType.Int16;

                case "18":  // SqlDbType.Text:
                case "22":  // SqlDbType.VarChar:
                    return DbType.AnsiString;

                case "20":  // SqlDbType.TinyInt:
                    return DbType.Byte;

                default:
                    return DbType.AnsiString;
            }

        }

        #endregion

        #region ' SQL code: database constrains '
        //
        // SQL Server: find database constrains
        //
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
