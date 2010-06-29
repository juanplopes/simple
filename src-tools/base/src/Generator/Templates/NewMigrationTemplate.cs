using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;

namespace Sample.Project.Generator.Templates
{
    public class NewMigrationTemplate : ICommand
    {
        public string Name { get; set; }

        public void Execute()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filename = string.Format("Migrations/{0}_{1}.cs", timestamp, Name ?? "Untitled");

            var template = this.ToTemplate();
            template["timestamp"] = timestamp;
            template["namespace"] = Default.DefaultNamespace;
            
            using (var project = Default.ToolsProject.Writer())
                project.AddNewCompile(filename, template.ToString());
        }
    }
}
