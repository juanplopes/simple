using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple.Metadata;
using Simple;
using Sample.Project.Config;
using Simple.NVelocity;

namespace Sample.Project.Generator.Runners
{
    public class EntityGenerator : BaseTableGenerator
    {
        public string FilePath(DbTable table)
        {
            return string.Format("Domain/Generated/{0}.cs", Default.Convention.NameFor(table));
        }

        public override void Create(DbTable table)
        {
            var template = Templates.EntityGenerator.ToTemplate().SetDefaults(table);
            template["idlist"] = MakeIdList(table);

            using (var project = Default.ContractsProject.Writer())
                project.AddNewCompile(FilePath(table), template.Render());
        }

        public override void Delete(DbTable table)
        {
            using (var project = Default.ContractsProject.Writer())
                project.RemoveAndDeleteFile(FilePath(table));
        }

        private static string MakeIdList(DbTable table)
        {
            var re = Default.Convention;
            return string.Join(", ", 
                table.PrimaryKeysExceptFk.Select(x => "{0} {1}".AsFormat(re.TypeFor(x), re.NameFor(x))).Union(
                table.KeyManyToOneRelations.Select(x => "{0} {1}".AsFormat(re.TypeFor(x), re.NameFor(x)))).ToArray());
        }

     
    }
}
