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
        public override void ExecuteSingle(DbTable table)
        {
            var re = Default.Convention;
            
            var className = re.NameFor(table);
            var filename = string.Format("Domain/Generated/{0}.cs", className);

            var template = Templates.EntityGenerator.ToTemplate().SetDefaults(table);
            
            template["idlist"] = string.Join(", ",
                table.PrimaryKeysExceptFk.Select(x => "{0} {1}".AsFormat(re.TypeFor(x), re.NameFor(x))).Union(
                table.KeyManyToOneRelations.Select(x => "{0} {1}".AsFormat(re.TypeFor(x), re.NameFor(x)))).ToArray());

            using (var project = Default.ContractsProject.Writer())
                project.AddNewCompile(filename, template.Render());
        }
    }
}
