using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Antlr.StringTemplate;
using System.IO;

namespace Sample.Project.Generator.Runners
{
    public class NewMigrationGenerator : IGenerator
    {
        public void Execute()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var template = new StringTemplate(Templates.NewMigrationGenerator);
            template.SetAttribute("timestamp", timestamp);

            var project = new ProjectFileWriter("src/Tools/99_Tools.csproj");
            project.AddNewCompile("Migrations/" + timestamp + "_UntitledMigration.cs", template.ToString());
            project.WriteChanges();
        }
    }
}
