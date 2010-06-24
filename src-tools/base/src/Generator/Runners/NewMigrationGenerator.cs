using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;

namespace Sample.Project.Generator.Runners
{
    public class NewMigrationGenerator : IGenerator
    {
        public void Execute()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filename = string.Format("Migrations/{0}_Untitled.cs", timestamp);

            var template = Templates.NewMigrationGenerator.ToTemplate();
            template["timestamp"] = timestamp;
            
            using (var project = Projects.Tools.Get().AutoCommit())
                project.AddNewCompile(filename, template.ToString());
        }
    }
}
