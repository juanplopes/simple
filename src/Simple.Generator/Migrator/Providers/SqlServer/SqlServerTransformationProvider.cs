using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Providers.SqlServer
{
    /// <summary>
    /// Migration transformations provider for Microsoft SQL Server.
    /// </summary>
    public class SqlServerTransformationProvider : TransformationProvider
    {
        public SqlServerTransformationProvider(Dialect dialect, string invariantProvider, string connectionString)
            : base(dialect, invariantProvider, connectionString)
        { }


        // FIXME: We should look into implementing this with INFORMATION_SCHEMA if possible
        // so that it would be usable by all the SQL Server implementations
        public override bool ConstraintExists(string table, string name)
        {
            using (IDataReader reader =
                ExecuteQuery(string.Format("SELECT TOP 1 * FROM sysobjects WHERE id = object_id('{0}')", name)))
            {
                return reader.Read();
            }
        }

        public override void AddColumn(string table, string sqlColumn)
        {
            ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD {1}", table, sqlColumn));
        }

        public override bool ColumnExists(string table, string column)
        {
            if (!TableExists(table))
                return false;

            using (IDataReader reader =
                ExecuteQuery(String.Format("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{0}' AND COLUMN_NAME='{1}'", table, column)))
            {
                return reader.Read();
            }
        }

        public override bool TableExists(string table)
        {
            using (IDataReader reader =
                ExecuteQuery(String.Format("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='{0}'", table)))
            {
                return reader.Read();
            }
        }

        public override void RemoveColumn(string table, string column)
        {
            var varName = "n" + Guid.NewGuid().ToString("N");
            ExecuteNonQuery(string.Format(
@"DECLARE @{2} VARCHAR(1000);
DECLARE NAME_CURSOR CURSOR FOR
SELECT CONT.NAME FROM SYSOBJECTS CONT, SYSCOLUMNS COL, SYSCONSTRAINTS CNT 
WHERE CONT.PARENT_OBJ = COL.ID AND CNT.CONSTID = CONT.ID AND CNT.COLID=COL.COLID
AND COL.NAME = '{1}' AND COL.ID = OBJECT_ID('{0}');
OPEN NAME_CURSOR
FETCH NEXT FROM NAME_CURSOR INTO @{2}; 
WHILE @@FETCH_STATUS = 0
BEGIN
    EXEC ('ALTER TABLE {0} DROP CONSTRAINT ' + @{2})
FETCH NEXT FROM NAME_CURSOR INTO @{2}; 
END
CLOSE NAME_CURSOR
DEALLOCATE NAME_CURSOR", table, column, varName));


           //DeleteColumnConstraints(table, column);
            base.RemoveColumn(table, column);
        }

        public override void RenameColumn(string tableName, string oldColumnName, string newColumnName)
        {
            ExecuteNonQuery(String.Format("EXEC sp_rename '{0}.{1}', '{2}', 'COLUMN'", tableName, oldColumnName, newColumnName));
        }

        public override void RenameTable(string oldName, string newName)
        {
            ExecuteNonQuery(String.Format("EXEC sp_rename {0}, {1}", oldName, newName));
        }

     

    }
}
