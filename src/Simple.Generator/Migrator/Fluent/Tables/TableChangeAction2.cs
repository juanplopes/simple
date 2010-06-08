using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Simple.Migrator.Fluent
{
    public partial class TableChangeAction
    {
		public ColumnChangeAction ChangeAnsiString(string name) 
		{
			return ChangeColumn(name, DbType.AnsiString);
		}
		public ColumnChangeAction ChangeBinary(string name) 
		{
			return ChangeColumn(name, DbType.Binary);
		}
		public ColumnChangeAction ChangeByte(string name) 
		{
			return ChangeColumn(name, DbType.Byte);
		}
		public ColumnChangeAction ChangeBoolean(string name) 
		{
			return ChangeColumn(name, DbType.Boolean);
		}
		public ColumnChangeAction ChangeCurrency(string name) 
		{
			return ChangeColumn(name, DbType.Currency);
		}
		public ColumnChangeAction ChangeDate(string name) 
		{
			return ChangeColumn(name, DbType.Date);
		}
		public ColumnChangeAction ChangeDateTime(string name) 
		{
			return ChangeColumn(name, DbType.DateTime);
		}
		public ColumnChangeAction ChangeDecimal(string name) 
		{
			return ChangeColumn(name, DbType.Decimal);
		}
		public ColumnChangeAction ChangeDouble(string name) 
		{
			return ChangeColumn(name, DbType.Double);
		}
		public ColumnChangeAction ChangeGuid(string name) 
		{
			return ChangeColumn(name, DbType.Guid);
		}
		public ColumnChangeAction ChangeInt16(string name) 
		{
			return ChangeColumn(name, DbType.Int16);
		}
		public ColumnChangeAction ChangeInt32(string name) 
		{
			return ChangeColumn(name, DbType.Int32);
		}
		public ColumnChangeAction ChangeInt64(string name) 
		{
			return ChangeColumn(name, DbType.Int64);
		}
		public ColumnChangeAction ChangeObject(string name) 
		{
			return ChangeColumn(name, DbType.Object);
		}
		public ColumnChangeAction ChangeSByte(string name) 
		{
			return ChangeColumn(name, DbType.SByte);
		}
		public ColumnChangeAction ChangeSingle(string name) 
		{
			return ChangeColumn(name, DbType.Single);
		}
		public ColumnChangeAction ChangeString(string name) 
		{
			return ChangeColumn(name, DbType.String);
		}
		public ColumnChangeAction ChangeTime(string name) 
		{
			return ChangeColumn(name, DbType.Time);
		}
		public ColumnChangeAction ChangeUInt16(string name) 
		{
			return ChangeColumn(name, DbType.UInt16);
		}
		public ColumnChangeAction ChangeUInt32(string name) 
		{
			return ChangeColumn(name, DbType.UInt32);
		}
		public ColumnChangeAction ChangeUInt64(string name) 
		{
			return ChangeColumn(name, DbType.UInt64);
		}
		public ColumnChangeAction ChangeVarNumeric(string name) 
		{
			return ChangeColumn(name, DbType.VarNumeric);
		}
		public ColumnChangeAction ChangeAnsiStringFixedLength(string name) 
		{
			return ChangeColumn(name, DbType.AnsiStringFixedLength);
		}
		public ColumnChangeAction ChangeStringFixedLength(string name) 
		{
			return ChangeColumn(name, DbType.StringFixedLength);
		}
		public ColumnChangeAction ChangeXml(string name) 
		{
			return ChangeColumn(name, DbType.Xml);
		}
		public ColumnChangeAction ChangeDateTime2(string name) 
		{
			return ChangeColumn(name, DbType.DateTime2);
		}
		public ColumnChangeAction ChangeDateTimeOffset(string name) 
		{
			return ChangeColumn(name, DbType.DateTimeOffset);
		}
    }
}
