using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;
using System.Diagnostics;
using Simple.NVelocity;

namespace Sample.Project.Tools.Templates
{
    public class NewMigrationTemplate : ICommand
    {
        public string Name { get; set; }
        public bool OpenIt { get; set; }

        public void Execute()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filename = string.Format("{0}_{1}.cs", timestamp, Name ?? "Untitled");

            var template = new SimpleTemplate(Templates.NewMigration);
            template["timestamp"] = timestamp;
            template["opt"] = Options.Do;

            using (var project = Options.Do.DatabaseProject)
            {
                project.AddNewCompile(filename, template.ToString());

                if (OpenIt)
                    Process.Start(project.GetFullPath(filename));
            }
        }
    }
}
