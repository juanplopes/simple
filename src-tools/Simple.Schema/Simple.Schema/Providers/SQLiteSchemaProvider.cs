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

namespace Simple.Meta.Providers
{
    class SQLiteSchemaProvider : DbSchemaProvider
    {

        public SQLiteSchemaProvider(string connectionstring, string providername) : base(connectionstring, providername) { }

        #region ' IDbProvider Members '

        public override DataTable GetConstraints()
        {
            DataTable tbl = GetDTSchemaConstrains();
            using (DbConnection _Connection = GetDBConnection())
            {
                DataTable Constraints = _Connection.GetSchema("ForeignKeys");
                foreach (DataRow contrainRow in Constraints.Rows)
                {
                    DataRow constrain = tbl.NewRow();
                    if (contrainRow["FKEY_TO_CATALOG"] != DBNull.Value)
                        constrain["PK_TABLE_CATALOG"] = contrainRow["FKEY_TO_CATALOG"];
                    if (contrainRow["FKEY_TO_SCHEMA"] != DBNull.Value)
                        constrain["PK_TABLE_SCHEMA"] = contrainRow["FKEY_TO_SCHEMA"];
                    constrain["PK_TABLE_NAME"] = contrainRow["FKEY_TO_TABLE"];
                    constrain["PK_COLUMN_NAME"] = contrainRow["FKEY_TO_COLUMN"];
                    if (contrainRow["TABLE_CATALOG"] != DBNull.Value)
                        constrain["FK_TABLE_CATALOG"] = contrainRow["TABLE_CATALOG"];
                    if (contrainRow["TABLE_SCHEMA"] != DBNull.Value)
                        constrain["FK_TABLE_SCHEMA"] = contrainRow["TABLE_SCHEMA"];
                    constrain["FK_TABLE_NAME"] = contrainRow["TABLE_NAME"];
                    constrain["FK_COLUMN_NAME"] = contrainRow["FKEY_FROM_COLUMN"];
                    constrain["FK_ORDINAL_POSITION"] = contrainRow["FKEY_FROM_ORDINAL_POSITION"];
                    constrain["FK_NAME"] = contrainRow["CONSTRAINT_NAME"];

                    tbl.Rows.Add(constrain);
                }
            }

            return tbl;
        }

        /// <summary>
        /// For a given database type, returns a closest-match DbType.
        /// According SQLite SQLiteConvert :: internal static DbType TypeNameToDbType(string Name)
        /// </summary>
        /// <param name="providerDbType">The name of the type to match</param>
        /// <returns>DbType the text evaluates to</returns>
        public override DbType GetDbColumnType(string providerDbType)
        {
            switch (providerDbType.ToUpper())
            {
                case "1":
                    return DbType.Binary;

                case "2":
                    return DbType.Byte;

                case "3":
                    return DbType.Boolean;

                case "4":
                    return DbType.Guid;

                case "6":
                    return DbType.DateTime;

                case "7":
                    return DbType.Decimal;

                case "8":
                    return DbType.Double;

                case "10":
                    return DbType.Int16;

                case "11":
                    return DbType.Int32;

                case "12":
                    return DbType.Int64;

                case "15":
                    return DbType.Single;

                case "16":
                    return DbType.String;

                default:
                    return DbType.Object;
            }
        }

        public override DataTable GetProcedures()
        {
            return GetDTSchemaProcedures();
        }

        public override DataTable GetProcedureParameters(string procedureSchema, string procedureName)
        {
            return GetDTSchemaProcedureParameters();
        }

        public override DataTable GetSchemaTables()
        {
            DataTable tbl = new DataTable("SchemaTables");
            using (DbConnection _Connection = GetDBConnection())
            {
                tbl = _Connection.GetSchema("Tables");
            }
            using (DbConnection _Connection = GetDBConnection())
            {
                DataTable tblViews = _Connection.GetSchema("Views");
                foreach (DataRow viewRow in tblViews.Rows)
                {
                    DataRow tblRow = tbl.NewRow();
                    if (viewRow["TABLE_CATALOG"] != DBNull.Value)
                        tblRow["TABLE_CATALOG"] = viewRow["TABLE_CATALOG"];
                    if (viewRow["TABLE_SCHEMA"] != DBNull.Value)
                        tblRow["TABLE_SCHEMA"] = viewRow["TABLE_SCHEMA"];
                    tblRow["TABLE_NAME"] = viewRow["TABLE_NAME"];
                    tblRow["TABLE_TYPE"] = "VIEW";

                    tbl.Rows.Add(tblRow);
                }
            }

            return tbl;
        }

        #endregion

    }
}
