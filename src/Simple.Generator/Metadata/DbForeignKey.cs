using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Metadata
{
    public class DbManyToOne : DbForeignKey
    {
        public bool IsKey { get; set; }

        public DbManyToOne(IDbSchemaProvider provider, string name, IList<DbRelation> columns)
            : base(provider, name, columns)
        {
        }
    }

    public class DbOneToMany : DbForeignKey
    {
        public DbOneToMany(IDbSchemaProvider provider, string name, IList<DbRelation> columns)
            : base(provider, name, columns)
        {
        }
    }

    public abstract class DbForeignKey : DbObject
    {
        public string FkName { get; set; }
        public IList<DbRelation> Columns { get; protected set; }
        public bool SafeNaming { get; set; }

        public DbForeignKey(IDbSchemaProvider provider)
            : base(provider)
        {
            this.SafeNaming = true;
        }

        public DbForeignKey(IDbSchemaProvider provider, string name, IList<DbRelation> columns)
            : this(provider)
        {
            this.Columns = columns;
            this.FkName = name;
        }
    }
}
