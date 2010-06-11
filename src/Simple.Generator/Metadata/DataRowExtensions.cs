using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Simple.Metadata
{
    public static class DataRowExtensions
    {
        public static T GetValue<T>(this DataRow row, string key)
        {
            return GetValue<T>(row, key, false);
        }

        public static T GetValue<T>(this DataRow row, string key, bool required)
        {
            if (!required && !row.Table.Columns.Contains(key)) return default(T);
            object value = row[key];
            if (value == DBNull.Value) return default(T);
            if (value is IConvertible && !(value is T)) return (T)Convert.ChangeType(value, typeof(T));
            else return (T)value;

        }
    }
}
