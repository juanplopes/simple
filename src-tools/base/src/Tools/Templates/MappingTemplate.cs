using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple.Metadata;
using Simple;
using Sample.Project.Config;
using Simple.NVelocity;
using Sample.Project.Tools.Infra;

namespace Sample.Project.Tools.Templates
{
    public class MappingTemplate : TableTemplate
    {
        public string FilePath(DbTable table)
        {
            return string.Format("Domain/Generated/{0}.hbm.xml", Default.Convention.NameFor(table));
        }

        public override void Create(DbTable table)
        {
            var template = this.ToTemplate().SetDefaults(table);

            using (var project = Default.ContractsProject.Writer())
                project.AddNewEmbeddedResource(FilePath(table), template.Render());
        }

        public override void Delete(DbTable table)
        {
            using (var project = Default.ContractsProject.Writer())
                project.RemoveAndDeleteFile(FilePath(table));
        }


    }
}
