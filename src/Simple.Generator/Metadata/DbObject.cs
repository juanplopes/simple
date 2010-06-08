using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Simple.Metadata
{
    public abstract class DbObject
    {
        public IDbSchemaProvider Provider { get; protected set; }

        public DbObject(IDbSchemaProvider provider)
        {
            this.Provider = provider;
        }

        public DbObject(string provider, string connectionString) :
            this(GetSchemaProvider(provider, connectionString))
        {

        }

        public T GetValue<T>(DataRow row, string key)
        {
            return GetValue<T>(row, key, false);
        }

        public T GetValue<T>(DataRow row, string key, bool required)
        {
            if (!required && !row.Table.Columns.Contains(key)) return default(T);
            object value = row[key];
            if (value == DBNull.Value) return default(T);
            if (value is IConvertible && !(value is T)) return (T)Convert.ChangeType(value, typeof(T));
            else return (T)value;

        }

        public static DbSchemaProvider GetSchemaProvider(string providerName, string connectionString)
        {
            switch (providerName.ToLower())
            {
                case "system.data.sqlserverce.3.5":
                case "system.data.sqlserverce":
                    return new SqlServerCeSchemaProvider(connectionString, providerName);

                case "system.data.oledb":
                    return new OleDbSchemaProvider(connectionString, providerName);

                case "system.data.sqlclient":
                    return new SqlServerSchemaProvider(connectionString, providerName);

                case "mysql.data.mysqlclient":
                    return new MySqlSchemaProvider(connectionString, providerName);

                case "npgsql":
                    return new PostgreSqlSchemaProvider(connectionString, providerName);

                case "system.data.sqlite":
                    return new SQLiteSchemaProvider(connectionString, providerName);

                case "system.data.oracleclient":
                case "oracle.dataaccess.client":
                    return new OracleSchemaProvider(connectionString, providerName);

                case "vistadb.net20":
                    return new VistaDBSchemaProvider(connectionString, providerName);

                default:
                    throw new NotImplementedException("The provider '" + providerName + "' is not implemented!");

            }
        }
    }
}
