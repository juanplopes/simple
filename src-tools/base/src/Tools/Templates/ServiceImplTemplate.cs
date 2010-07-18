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
    public class ServiceImplTemplate : TableTemplate
    {
        public string FilePath(DbTable table)
        {
            return string.Format("Services/{0}Service.cs", Default.Convention.NameFor(table));
        }

        public override void Create(DbTable table)
        {
            var template = this.ToTemplate().SetDefaults(table);

            var filepath = FilePath(table);
            using (var project = Default.ServerProject.Writer())
                if (!project.ExistsFile(filepath))
                    project.AddNewCompile(filepath, template.Render());
        }

        public override void Delete(DbTable table)
        {
            using (var project = Default.ServerProject.Writer())
                project.RemoveAndDeleteFile(FilePath(table));
        }


    }
}
