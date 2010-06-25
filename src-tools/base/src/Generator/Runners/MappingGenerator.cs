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
    public class MappingGenerator : BaseTableGenerator
    {
        public override void ExecuteSingle(DbTable table)
        {
            var filename = string.Format("Domain/Generated/{0}.hbm.xml", Default.Convention.NameFor(table));

            var template = Templates.MappingGenerator.ToTemplate().SetDefaults(table);
            
            using (var project = Default.ContractsProject.Writer())
                project.AddNewEmbeddedResource(filename, template.Render());
        }
    }
}
