using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Migrator.Fluent
{
    public abstract partial class TableAction
    {
		public ColumnAction AddAnsiString(string name) 
		{
			return AddColumn(name, DbType.AnsiString);
		}
		public ColumnAction AddBinary(string name) 
		{
			return AddColumn(name, DbType.Binary);
		}
		public ColumnAction AddByte(string name) 
		{
			return AddColumn(name, DbType.Byte);
		}
		public ColumnAction AddBoolean(string name) 
		{
			return AddColumn(name, DbType.Boolean);
		}
		public ColumnAction AddCurrency(string name) 
		{
			return AddColumn(name, DbType.Currency);
		}
		public ColumnAction AddDate(string name) 
		{
			return AddColumn(name, DbType.Date);
		}
		public ColumnAction AddDateTime(string name) 
		{
			return AddColumn(name, DbType.DateTime);
		}
		public ColumnAction AddDecimal(string name) 
		{
			return AddColumn(name, DbType.Decimal);
		}
		public ColumnAction AddDouble(string name) 
		{
			return AddColumn(name, DbType.Double);
		}
		public ColumnAction AddGuid(string name) 
		{
			return AddColumn(name, DbType.Guid);
		}
		public ColumnAction AddInt16(string name) 
		{
			return AddColumn(name, DbType.Int16);
		}
		public ColumnAction AddInt32(string name) 
		{
			return AddColumn(name, DbType.Int32);
		}
		public ColumnAction AddInt64(string name) 
		{
			return AddColumn(name, DbType.Int64);
		}
		public ColumnAction AddObject(string name) 
		{
			return AddColumn(name, DbType.Object);
		}
		public ColumnAction AddSByte(string name) 
		{
			return AddColumn(name, DbType.SByte);
		}
		public ColumnAction AddSingle(string name) 
		{
			return AddColumn(name, DbType.Single);
		}
		public ColumnAction AddString(string name) 
		{
			return AddColumn(name, DbType.String);
		}
		public ColumnAction AddTime(string name) 
		{
			return AddColumn(name, DbType.Time);
		}
		public ColumnAction AddUInt16(string name) 
		{
			return AddColumn(name, DbType.UInt16);
		}
		public ColumnAction AddUInt32(string name) 
		{
			return AddColumn(name, DbType.UInt32);
		}
		public ColumnAction AddUInt64(string name) 
		{
			return AddColumn(name, DbType.UInt64);
		}
		public ColumnAction AddVarNumeric(string name) 
		{
			return AddColumn(name, DbType.VarNumeric);
		}
		public ColumnAction AddAnsiStringFixedLength(string name) 
		{
			return AddColumn(name, DbType.AnsiStringFixedLength);
		}
		public ColumnAction AddStringFixedLength(string name) 
		{
			return AddColumn(name, DbType.StringFixedLength);
		}
		public ColumnAction AddXml(string name) 
		{
			return AddColumn(name, DbType.Xml);
		}
		public ColumnAction AddDateTime2(string name) 
		{
			return AddColumn(name, DbType.DateTime2);
		}
		public ColumnAction AddDateTimeOffset(string name) 
		{
			return AddColumn(name, DbType.DateTimeOffset);
		}
    }
}
