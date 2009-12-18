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

namespace Simple.Meta
{
    abstract public class DbSchemaProvider : IDbSchemaProvider
    {
        private string _ConnectionString;
        private string _ProviderName;

        protected string ProviderName
        {
            get { return _ProviderName; }
        }

        public DbSchemaProvider(string connectionstring, string providername)
        {
            _ConnectionString = connectionstring;
            _ProviderName = providername;
        }

        #region ' IDbProvider Members '

        virtual public string GetDatabaseName()
        {
            string DatabaseName = string.Empty;
            using (DbConnection _Connection = GetDBConnection())
            {
                DatabaseName = _Connection.Database;
            }
            return DatabaseName;
        }

        virtual public DataTable GetSchemaTables()
        {
            DataTable tbl = new DataTable("SchemaTables");
            using (DbConnection _Connection = GetDBConnection())
            {
                tbl = _Connection.GetSchema("Tables");
            }

            return tbl;
        }

        virtual public DataTable GetTableColumns(string tableSchema, string tableName)
        {
            DataTable tbl = new DataTable();

            using (DbConnection _Connection = GetDBConnection())
            {
                DbCommand _Command = _Connection.CreateCommand();
                _Command.CommandText = string.Format("SELECT * FROM {0}", QualifiedTableName(tableSchema, tableName));
                _Command.CommandType = CommandType.Text;

                System.Console.WriteLine("SQL: " + _Command.CommandText);
                tbl = _Command.ExecuteReader(CommandBehavior.KeyInfo).GetSchemaTable();
            }

            return tbl;
        }

        abstract public DataTable GetConstraints();

        virtual public string QualifiedTableName(string tableSchema, string tableName)
        {
            if (!string.IsNullOrEmpty(tableSchema))
                return string.Format("[{0}].[{1}]", tableSchema, tableName);
            else
                return string.Format("[{0}]", tableName);
        }

        virtual public DataTable GetProcedures()
        {
            DataTable tbl = new DataTable("Procedures");
            using (DbConnection _Connection = GetDBConnection())
            {
                tbl = _Connection.GetSchema("Procedures");
            }

            return tbl;
        }

        virtual public DataTable GetProcedureParameters(string procedureSchema, string procedureName)
        {
            #region ' Decapated Code '
            //DataTable tbl = new DataTable("ProcedureParameters");
            //using (DbConnection _Connection = GetDBConnection())
            //{
            //    string[] restrictions = new string[4] { null, procedureSchema, procedureName, null };
            //    tbl = _Connection.GetSchema("ProcedureParameters", restrictions);
            //}
            //return tbl;
            #endregion

            DataTable tbl = GetDTSchemaProcedureParameters();
            using (DbConnection _Connection = GetDBConnection())
            {
                DbCommand _Command = _Connection.CreateCommand();
                _Command.CommandText = this.QualifiedTableName(procedureSchema, procedureName);
                _Command.CommandType = CommandType.StoredProcedure;

                DbParameter par = _Command.CreateParameter();

                DbProviderFactory pf = DbProviderFactories.GetFactory(this.ProviderName);
                DbCommandBuilder cb = pf.CreateCommandBuilder();
                MethodInfo theMethod = cb.GetType().GetMethod("DeriveParameters");
                theMethod.Invoke(cb, new object[] { _Command });

                int counter = 1;
                foreach (DbParameter p in _Command.Parameters)
                {
                    if (p.ParameterName != "@RETURN_VALUE")
                    {
                        DataRow parameterRow = tbl.NewRow();
                        if (!string.IsNullOrEmpty(procedureSchema))
                            parameterRow["SPECIFIC_SCHEMA"] = procedureSchema;
                        parameterRow["SPECIFIC_NAME"] = procedureName;
                        parameterRow["PARAMETER_NAME"] = p.ParameterName;
                        parameterRow["ORDINAL_POSITION"] = counter;
                        parameterRow["PARAMETER_MODE"] = p.Direction;
                        parameterRow["IS_RESULT"] = p.Direction == ParameterDirection.ReturnValue;
                        parameterRow["DATA_TYPE"] = p.DbType;
                        parameterRow["CHARACTER_MAXIMUM_LENGTH"] = p.Size;

                        tbl.Rows.Add(parameterRow);
                        counter++;
                    }
                }
            }

            return tbl;
        }

       

        abstract public DbType GetDbColumnType(string providerDbType);

        #endregion

        #region ' Helper functions '

        internal DbConnection GetDBConnection()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(_ProviderName);
            DbConnection _Connection = providerFactory.CreateConnection();
            _Connection.ConnectionString = _ConnectionString;
            _Connection.Open();
            return _Connection;
        }

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

        protected DataTable GetDTSchemaProcedures()
        {
            DataTable tbl = new DataTable("Procedures");
            tbl.Columns.Add(new DataColumn("SPECIFIC_CATALOG", typeof(string)));
            tbl.Columns.Add(new DataColumn("SPECIFIC_SCHEMA", typeof(string)));
            tbl.Columns.Add(new DataColumn("SPECIFIC_NAME", typeof(string)));
            tbl.Columns.Add(new DataColumn("ROUTINE_CATALOG", typeof(string)));
            tbl.Columns.Add(new DataColumn("ROUTINE_SCHEMA", typeof(string)));
            tbl.Columns.Add(new DataColumn("ROUTINE_NAME", typeof(string)));
            tbl.Columns.Add(new DataColumn("ROUTINE_TYPE", typeof(string))); 
            tbl.Columns.Add(new DataColumn("CREATED", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("LAST_ALTERED", typeof(DateTime)));

            return tbl;
        }

        protected DataTable GetDTSchemaProcedureParameters()
        {
            DataTable tbl = new DataTable("ProcedureParameters");
            tbl.Columns.Add("SPECIFIC_CATALOG", typeof(System.String));
            tbl.Columns.Add("SPECIFIC_SCHEMA", typeof(System.String));
            tbl.Columns.Add("SPECIFIC_NAME", typeof(System.String));
            tbl.Columns.Add("PARAMETER_NAME", typeof(System.String));
            tbl.Columns.Add("ORDINAL_POSITION", typeof(System.Int32));
            tbl.Columns.Add("PARAMETER_MODE", typeof(ParameterDirection));
            tbl.Columns.Add("IS_RESULT", typeof(System.Boolean));
            tbl.Columns.Add("DATA_TYPE", typeof(System.Data.DbType));
            tbl.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(System.Int32));
            tbl.Columns.Add("NUMERIC_PRECISION", typeof(System.Int16));
            tbl.Columns.Add("NUMERIC_SCALE", typeof(System.Int32));
            tbl.Columns.Add("DATETIME_PRECISION", typeof(System.Int16));

            return tbl;
        }

        #endregion

    }
}
