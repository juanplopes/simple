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
    public class EntityGenerator : IGenerator
    {
        public IList<string> TableNames { get; set; }

        public IList<DbTable> GetTables()
        {
            var names = GetTransformedTableNames();

            var excluded = names.Where(x => x.StartsWith("-")).ToList();
            var included = names.Except(excluded).ToList();

            var db = new DbSchema(
                Simply.Do.GetConfig<ApplicationConfig>().ADOProvider, 
                Simply.Do.GetConnectionString());

            return db.GetTables(included, excluded.Select(x=>x.Substring(1)).ToList()).ToList();
        }

        private IList<string> GetTransformedTableNames()
        {
            var names = TableNames ?? Tables.Default().ToList();
            if (names.Count == 1 && names[0] == "$")
                names = Tables.Default().ToList();
            return names;
        }

        public void Execute()
        {
            var template = Templates.EntityGenerator.ToTemplate();
            template["re"] = new Conventions();
            template["tables"] = GetTables();
            Console.WriteLine(template);
        }
    }
}
