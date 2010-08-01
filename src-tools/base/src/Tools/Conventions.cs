using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;
using Example.Project.Tools.Infra;
using Simple.Generator.Strings;
using Simple;
using Simple.Generator;

namespace Example.Project.Tools
{
    public class Conventions : ITableConventions
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
            return column.GetDisplayTypeName(false);
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
            return "ISet<" + NameFor(fk.FkTableRef) + ">";
        }

        public string ConcreteTypeFor(DbOneToMany fk)
        {
            return "HashedSet<" + NameFor(fk.FkTableRef) + ">";
        }


        public string NameFor(DbOneToMany fk)
        {
            var name = NameFor(fk.FkTableRef).ToPlural(Pluralizer);
            if (!fk.SafeNaming)
                name += "At" + fk.Columns[0].Name.ReplaceId().CleanUp();
            return name;
        }

        public string Pluralize(string entity)
        {
            return Pluralizer.ToPlural(entity);
        }
    }
}
