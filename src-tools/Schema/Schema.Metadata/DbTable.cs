using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Schema.Metadata
{
    public class DbTable : DbObject
    {
        private IList<DbColumn> _columnCache = null;
        private IList<DbRelation> _fkCache = null;

        public DbTable(IDbSchemaProvider provider)
            : base(provider)
        {
        }

        public DbTable(IDbSchemaProvider provider, DataRow TableRow)
            : this(provider)
        {
            if (TableRow.Table.Columns.Contains("TABLE_CATALOG"))
                if (TableRow["TABLE_CATALOG"] != DBNull.Value)
                    this.TableCatalog = TableRow["TABLE_CATALOG"].ToString();
            if (TableRow.Table.Columns.Contains("TABLE_SCHEMA"))
                if (TableRow["TABLE_SCHEMA"] != DBNull.Value)
                    this.TableSchema = TableRow["TABLE_SCHEMA"].ToString();
            this.TableName = TableRow["TABLE_NAME"].ToString();
            this.TableType = TableRow["TABLE_TYPE"].ToString();
        }

        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }

        #region ' Columns and Primary Keys '

        private IEnumerable<DbColumn> GetSchemaColumns()
        {
            foreach (DataRow row in Provider.GetTableColumns(TableSchema, TableName).Rows)
                yield return new DbColumn(Provider, row);
        }

        private IEnumerable<DbRelation> GetRelations()
        {
            string where;
            if (!string.IsNullOrEmpty(TableSchema))
                where = string.Format("FK_TABLE_SCHEMA = '{0}' AND FK_TABLE_NAME = '{1}'", TableSchema, TableName);
            else
                where = string.Format("FK_TABLE_NAME = '{0}'", TableName);

            foreach (var row in Provider.GetConstraints().Select(where))
                yield return new DbRelation(Provider, row);
        }


        protected IList<DbColumn> AllColumns
        {
            get
            {
                if (_columnCache == null) _columnCache = GetSchemaColumns().ToList();
                return _columnCache;
            }
        }

        public IList<DbRelation> ForeignKeyColumns
        {
            get
            {
                if (_fkCache == null) _fkCache = GetRelations().ToList();
                return _fkCache;
            }
        }


        public IEnumerable<DbColumn> Columns
        {
            get
            {
                return AllColumns.Where(x => !x.IsHidden);
            }
        }

        public IEnumerable<DbColumn> PrimaryKeyColumns
        {
            get
            {
                return Columns.Where(x => x.IsKey);
            }
        }

        
        public IEnumerable<DbColumn> GetFieldColumns()
        {
            return Columns.Except(PrimaryKeyColumns);
        }

        public IEnumerable<DbForeignKey> GetForeignKeys()
        {
            var fks = ForeignKeyColumns;
            return fks.GroupBy(x => x.FkName).Select(x => new DbForeignKey(Provider, x.Key, x.ToList()));
        }

        #endregion

    }
}
