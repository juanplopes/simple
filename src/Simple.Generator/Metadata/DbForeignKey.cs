using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple.Metadata
{
    public class DbManyToOne : DbForeignKey
    {
        public bool IsKey { get; set; }

        public DbManyToOne(MetaContext context, IList<DbRelation> columns)
            : base(context, columns)
        {
        }
    }

    public class DbOneToMany : DbForeignKey
    {
        public DbOneToMany(MetaContext context, IList<DbRelation> columns)
            : base(context, columns)
        {
        }
    }

    public class DbForeignKeyName : ContextualizedObject
    {
        public DbTableName PkTableRef { get; set; }
        public DbTableName FkTableRef { get; set; }
        public string Name { get; set; }

        public DbForeignKeyName(MetaContext context)
            : base(context)
        { }

        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbForeignKeyName>()
                .Add(x => x.PkTableRef)
                .Add(x => x.FkTableRef)
                .Add(x => x.Name);
        }
    }

    public abstract class DbForeignKey : DbForeignKeyName
    {
        public IList<DbRelation> Columns { get; protected set; }
        public bool SafeNaming { get; set; }

        public DbForeignKey(MetaContext context, IList<DbRelation> columns) : base(context)
        {
            this.Columns = columns;
            
            var first = columns.First();
            this.Name = first.Name;
            this.PkTableRef = first.PkColumnRef.TableRef;
            this.FkTableRef = first.FkColumnRef.TableRef;
            this.SafeNaming = true;
        }

        EqualityHelper _helper = new EqualityHelper<DbForeignKey>()
            .Add(x => x.Name);

        public override bool Equals(object obj)
        {
            return _helper.ObjectEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return _helper.ObjectGetHashCode(this);
        }
    }
}
