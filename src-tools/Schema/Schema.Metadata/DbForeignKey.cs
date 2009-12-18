using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schema.Metadata
{
    public class DbForeignKey : DbObject
    {
        public string FkName { get; set; }
        public IList<DbRelation> Columns { get; protected set; }

        public DbForeignKey(IDbSchemaProvider provider) : base(provider) { }

        public DbForeignKey(IDbSchemaProvider provider, string name, IList<DbRelation> columns)
            : this(provider)
        {
            this.Columns = columns;
            this.FkName = name;
        }
    }
}
