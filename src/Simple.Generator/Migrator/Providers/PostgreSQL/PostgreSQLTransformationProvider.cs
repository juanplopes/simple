using System;
using System.Collections.Generic;
using System.Data;
using Simple.Migrator.Framework;
using System.Data.Common;

namespace Simple.Migrator.Providers.PostgreSQL
{
    /// <summary>
    /// Migration transformations provider for Microsoft SQL Server.
    /// </summary>
    public class PostgreSQLTransformationProvider : TransformationProvider
    {
        public PostgreSQLTransformationProvider(Dialect dialect, string invariantProvider, string connectionString)
            : base(dialect, invariantProvider, connectionString)
        { }

        public override void RemoveTable(string name)
        {
            ExecuteNonQuery(String.Format("DROP TABLE IF EXISTS {0} CASCADE", name));
        }

        public override bool ConstraintExists(string table, string name)
        {
            using (IDataReader reader =
                ExecuteQuery(string.Format("SELECT constraint_name FROM information_schema.table_constraints WHERE table_schema = 'public' AND constraint_name = lower('{0}')", name)))
            {
                return reader.Read();
            }
        }

        public override bool ColumnExists(string table, string column)
        {
            if (!TableExists(table))
                return false;

            using (IDataReader reader =
                ExecuteQuery(String.Format("SELECT column_name FROM information_schema.columns WHERE table_schema = 'public' AND table_name = lower('{0}') AND column_name = lower('{1}')", table, column)))
            {
                return reader.Read();
            }
        }

        public override bool TableExists(string table)
        {

            using (IDataReader reader =
                ExecuteQuery(String.Format("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_name = lower('{0}')", table)))
            {
                return reader.Read();
            }
        }

        public override void ChangeColumn(string table, Column column)
        {
            if (!ColumnExists(table, column.Name))
            {
                Logger.Warn("Column {0}.{1} does not exist", table, column.Name);
                return;
            }

            string tempColumn = "temp_" + column.Name;
            RenameColumn(table, column.Name, tempColumn);
            AddColumn(table, column);
            ExecuteQuery(String.Format("UPDATE {0} SET {1}={2}", table, column.Name, tempColumn));
            RemoveColumn(table, tempColumn);
        }

        public override string[] GetTables()
        {
            List<string> tables = new List<string>();
            using (IDataReader reader = ExecuteQuery("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'"))
            {
                while (reader.Read())
                {
                    tables.Add((string)reader[0]);
                }
            }
            return tables.ToArray();
        }

        public override Column[] GetColumns(string table)
        {
            List<Column> columns = new List<Column>();
            using (
                IDataReader reader =
                    ExecuteQuery(
                        String.Format("select COLUMN_NAME, IS_NULLABLE from information_schema.columns where table_schema = 'public' AND table_name = lower('{0}');", table)))
            {
                // FIXME: Mostly duplicated code from the Transformation provider just to support stupid case-insensitivty of Postgre
                while (reader.Read())
                {
                    Column column = new Column(reader[0].ToString(), DbType.String);
                    bool isNullable = reader.GetString(1) == "YES";
                    column.ColumnProperty |= isNullable ? ColumnProperty.Null : ColumnProperty.NotNull;

                    columns.Add(column);
                }
            }

            return columns.ToArray();
        }

        public override Column GetColumnByName(string table, string columnName)
        {
            // Duplicate because of the lower case issue
            return Array.Find(GetColumns(table),
                delegate(Column column)
                {
                    return column.Name == columnName.ToLower();
                });
        }
    }
}
