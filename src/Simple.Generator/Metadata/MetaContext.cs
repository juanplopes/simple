using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Metadata
{
    public class MetaContext
    {
        public Dictionary<DbColumnName, DbColumn> AllColumns { get; private set; }
        public Dictionary<DbTableName, DbTable> AllTables { get; private set; }
        public Dictionary<DbRelationName, DbRelation> AllRelations { get; private set; }
        
        public MetaContext()
        {
            AllColumns = new Dictionary<DbColumnName,DbColumn>();
            AllTables = new Dictionary<DbTableName,DbTable>();
            AllRelations = new Dictionary<DbRelationName,DbRelation>();
        }

        public MetaContext InjectTables(IEnumerable<DbTable> tables)
        {
            AllTables.AddRange(tables);
            return this;
        }

        public MetaContext InjectRelactions(IEnumerable<DbRelation> relations)
        {
            AllRelations.AddRange(relations);
            return this;
        }

        public MetaContext InjectTableColumns(DbTable table, 

    }
}
