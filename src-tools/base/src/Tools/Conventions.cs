using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;
using Sample.Project.Tools.Infra;
using Simple.Generator.Strings;

namespace Sample.Project.Tools
{
    public class Conventions
    {
        protected IPluralizer Pluralizer { get; set; }
        public Conventions(IPluralizer pluralizer)
        {
            this.Pluralizer = pluralizer;
        }
        public Conventions()
            : this(new EnglishPluralizer())
        {

        }

        public string NameFor(DbTableName table)
        {
            return table.Name.CleanUp().ToSingular(Pluralizer);
        }

        public string TypeFor(DbColumn column)
        {
            return column.GetDisplayTypeName(column.IsKey);
        }

        public string NameFor(DbColumn column)
        {
            return column.Name.CleanUp();
        }

        public string TypeFor(DbManyToOne fk)
        {
            return NameFor(fk.PkTableRef);
        }

        public string NameFor(DbManyToOne fk)
        {
            if (fk.Columns.Count == 1)
                return fk.Columns[0].FkColumnRef.Name.ReplaceId().CleanUp();
            else
                return NameFor(fk.PkTableRef);
        }

        public string TypeFor(DbOneToMany fk)
        {
            return "IList<" + NameFor(fk.FkTableRef) + ">";
        }

        public string ConcreteTypeFor(DbOneToMany fk)
        {
            return "List<" + NameFor(fk.FkTableRef) + ">";
        }


        public string NameFor(DbOneToMany fk)
        {
            var name = NameFor(fk.FkTableRef).ToPlural(Pluralizer);
            if (!fk.SafeNaming)
                name += "At" + fk.Columns[0].Name.ReplaceId().CleanUp();
            return name;
        }
    }
}
