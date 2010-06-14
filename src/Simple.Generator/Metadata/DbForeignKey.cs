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
        public DbTableName PkTableName { get; set; }
        public DbTableName FkTableName { get; set; }
        public string FkName { get; set; }

        public DbForeignKeyName(MetaContext context)
            : base(context)
        { }

        public override EqualityHelper CreateHelper()
        {
            return new EqualityHelper<DbForeignKeyName>()
                .Add(x => x.PkTableName)
                .Add(x => x.FkTableName)
                .Add(x => x.FkName);
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
            this.FkName = first.FkName;
            this.PkTableName = first.PkColumnName.TableName;
            this.FkTableName = first.FkColumnName.TableName;
            this.SafeNaming = true;
        }

        EqualityHelper _helper = new EqualityHelper<DbForeignKey>()
            .Add(x => x.FkName);

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
