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
            var template = new StringTemplate(Templates.NewMigrationGenerator);
            template.SetAttribute("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            Console.WriteLine(template.ToString());
        }
    }
}
