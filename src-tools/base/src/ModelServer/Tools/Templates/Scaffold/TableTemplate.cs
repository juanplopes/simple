using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Simple.Metadata;
using Simple;
using Simple.Generator;

namespace Example.Project.Tools.Templates.Scaffold
{
    public class TableTemplate : TableTemplateBase
    {
        public TableTemplate(string template) : base(template) { }

        protected override Simple.Generator.ITableConventions GetConventions()
        {
            return Options.Do.Conventions;
        }

        protected override void SetTemplate(Simple.Metadata.DbTable table)
        {
            var opt = Options.Instance;
            var re = opt.Conventions;
            
            this.SetMany(new
            {
                re = re,
                table = table,
                opt = opt,
                classname = re.NameFor(table),
                count = new Func<IEnumerable, int>(x => x.Cast<object>().Count()),
                idlist = new Func<DbTable, string>(MakeIdList)
            });
        }

        private static string MakeIdList(DbTable table)
        {
            var re = Options.Do.Conventions;
            return string.Join(", ",
                table.PrimaryKeysExceptFk.Select(x => "{0} {1}".AsFormat(re.TypeFor(x), re.NameFor(x))).Union(
                table.KeyManyToOneRelations.Select(x => "{0} {1}".AsFormat(re.TypeFor(x), re.NameFor(x)))).ToArray());
        }
    }
}
