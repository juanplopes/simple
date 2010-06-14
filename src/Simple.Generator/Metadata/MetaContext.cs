using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Metadata
{
    public class MetaContext
    {
        public string ConnectionString { get; private set; }
        public string Provider { get; private set; }

        public Dictionary<DbColumnName, DbColumn> AllColumns { get; private set; }
        public Dictionary<DbTableName, DbTable> AllTables { get; private set; }
        public Dictionary<DbRelationName, DbRelation> AllRelations { get; private set; }

        public ILookup<DbTableName, DbColumn> ColumnsByTable { get; private set; }
        public ILookup<DbTableName, DbRelation> OutRelationsByTable { get; private set; }
        public ILookup<DbTableName, DbRelation> InRelationsByTable { get; private set; } 

        public MetaContext(string connectionString, string provider)
        {
            ConnectionString = connectionString;
            Provider = provider;

            AllColumns = new Dictionary<DbColumnName,DbColumn>();
            AllTables = new Dictionary<DbTableName,DbTable>();
            AllRelations = new Dictionary<DbRelationName,DbRelation>();
        }

        public MetaContext InjectTables(IEnumerable<DbTable> items)
        {
            foreach (var item in items)
                AllTables[item] = item;
            return this;
        }

        public MetaContext InjectRelactions(IEnumerable<DbRelation> items)
        {
            foreach (var item in items)
            {
                AllRelations[item] = item;
            }

            return this;
        }

        public MetaContext InjectTableColumns(IEnumerable<DbColumn> items)
        {
            foreach (var item in items)
                AllColumns[item] = item;

            return this;
        }

        public MetaContext ExecuteCache()
        {
            OutRelationsByTable = AllRelations.Values.ToLookup(x => x.FkColumnName.TableName);
            InRelationsByTable = AllRelations.Values.ToLookup(x => x.PkColumnName.TableName);
            ColumnsByTable = AllColumns.Values.ToLookup(x => x.TableName);

            return this;
        }
    }
}
