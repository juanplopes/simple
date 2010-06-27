using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;

namespace Sample.Project.Generator.Templates
{
    public class NewMigrationTemplate : IGenerator
    {
        public void Execute()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filename = string.Format("Migrations/{0}_Untitled.cs", timestamp);

            var template = this.ToTemplate();
            template["timestamp"] = timestamp;
            
            using (var project = Default.ToolsProject.Writer())
                project.AddNewCompile(filename, template.ToString());
        }
    }
}
