using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Meta.Providers
{
    public class VistaDBSchemaProvider : DbSchemaProvider
    {
        public VistaDBSchemaProvider(string connectionstring, string providername) : base(connectionstring, providername) { }

        #region ' IDbProvider Members '

        public override string GetDatabaseName()
        {
            string DatabaseName = string.Empty;
            using (DbConnection _Connection = GetDBConnection())
            {
                Regex RegExp = new Regex(@"\\(?<db>[^\\]*)?\.vdb3$", RegexOptions.IgnoreCase);
                if (RegExp.IsMatch(_Connection.DataSource))
                {
                    Match found = RegExp.Matches(_Connection.DataSource)[0];
                    DatabaseName = found.Groups[1].Value;
                }
            }
            return DatabaseName;
        }

        public override System.Data.DataTable GetConstraints()
        {
            DataTable tbl = GetDTSchemaConstrains();
            using (DbConnection _Connection = GetDBConnection())
            {
                DataTable Constraints = _Connection.GetSchema("ForeignKeyColumns");
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

        public override System.Data.DbType GetDbColumnType(string providerDbType)
        {
            throw new NotImplementedException();
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