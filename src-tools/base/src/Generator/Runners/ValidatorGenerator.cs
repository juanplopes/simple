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
    public class ValidatorGenerator : BaseTableGenerator
    {
        public string FilePath(DbTable table)
        {
            return string.Format("Validators/{0}Validator.cs", Default.Convention.NameFor(table));
        }

        public override void Create(DbTable table)
        {
            var template = Templates.Validator.ToTemplate().SetDefaults(table);

            var filepath = FilePath(table);
            using (var project = Default.ContractsProject.Writer())
                if (!project.ExistsFile(filepath))
                    project.AddNewCompile(filepath, template.Render());
        }

        public override void Delete(DbTable table)
        {
            using (var project = Default.ContractsProject.Writer())
                project.RemoveAndDeleteFile(FilePath(table));
        }


    }
}
