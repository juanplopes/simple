using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Simple.Migrator.Fluent
{
    public abstract partial class TableAction
    {
		public ColumnAddAction AddAnsiString(string name) 
		{
			return AddColumn(name, DbType.AnsiString);
		}
		public ColumnAddAction AddBinary(string name) 
		{
			return AddColumn(name, DbType.Binary);
		}
		public ColumnAddAction AddByte(string name) 
		{
			return AddColumn(name, DbType.Byte);
		}
		public ColumnAddAction AddBoolean(string name) 
		{
			return AddColumn(name, DbType.Boolean);
		}
		public ColumnAddAction AddCurrency(string name) 
		{
			return AddColumn(name, DbType.Currency);
		}
		public ColumnAddAction AddDate(string name) 
		{
			return AddColumn(name, DbType.Date);
		}
		public ColumnAddAction AddDateTime(string name) 
		{
			return AddColumn(name, DbType.DateTime);
		}
		public ColumnAddAction AddDecimal(string name) 
		{
			return AddColumn(name, DbType.Decimal);
		}
		public ColumnAddAction AddDouble(string name) 
		{
			return AddColumn(name, DbType.Double);
		}
		public ColumnAddAction AddGuid(string name) 
		{
			return AddColumn(name, DbType.Guid);
		}
		public ColumnAddAction AddInt16(string name) 
		{
			return AddColumn(name, DbType.Int16);
		}
		public ColumnAddAction AddInt32(string name) 
		{
			return AddColumn(name, DbType.Int32);
		}
		public ColumnAddAction AddInt64(string name) 
		{
			return AddColumn(name, DbType.Int64);
		}
		public ColumnAddAction AddObject(string name) 
		{
			return AddColumn(name, DbType.Object);
		}
		public ColumnAddAction AddSByte(string name) 
		{
			return AddColumn(name, DbType.SByte);
		}
		public ColumnAddAction AddSingle(string name) 
		{
			return AddColumn(name, DbType.Single);
		}
		public ColumnAddAction AddString(string name) 
		{
			return AddColumn(name, DbType.String);
		}
		public ColumnAddAction AddTime(string name) 
		{
			return AddColumn(name, DbType.Time);
		}
		public ColumnAddAction AddUInt16(string name) 
		{
			return AddColumn(name, DbType.UInt16);
		}
		public ColumnAddAction AddUInt32(string name) 
		{
			return AddColumn(name, DbType.UInt32);
		}
		public ColumnAddAction AddUInt64(string name) 
		{
			return AddColumn(name, DbType.UInt64);
		}
		public ColumnAddAction AddVarNumeric(string name) 
		{
			return AddColumn(name, DbType.VarNumeric);
		}
		public ColumnAddAction AddAnsiStringFixedLength(string name) 
		{
			return AddColumn(name, DbType.AnsiStringFixedLength);
		}
		public ColumnAddAction AddStringFixedLength(string name) 
		{
			return AddColumn(name, DbType.StringFixedLength);
		}
		public ColumnAddAction AddXml(string name) 
		{
			return AddColumn(name, DbType.Xml);
		}
		public ColumnAddAction AddDateTime2(string name) 
		{
			return AddColumn(name, DbType.DateTime2);
		}
		public ColumnAddAction AddDateTimeOffset(string name) 
		{
			return AddColumn(name, DbType.DateTimeOffset);
		}
    }
}
