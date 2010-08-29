using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;
using Example.Project.Tools.Templates;
using Simple;
using Example.Project.Services;
using Simple.Generator;
using Example.Project.Domain;
using Simple.NVelocity;
using System.Collections;

namespace Example.Project.Tools.Templates.Scaffold
{
    public class ScaffoldGenerator : ICommand
    {
        public IList<string> TableNames { get; set; }

        public void Execute()
        {
            using (var model = Options.Do.Model.Project)
            using (var server = Options.Do.Server.Project)
            {
                var list = MakeTemplateList(model, server);

                foreach (var table in GetTables())
                    foreach (var template in list)
                        template.Create(table);
            }
        }

        public static List<ITableTemplate> MakeTemplateList(ProjectFileWriter model, ProjectFileWriter server)
        {
            var list = new List<ITableTemplate>();
            list.Add(new TableTemplate(Templates.Entity)
                .Target(model, "Domain/Generated/{0}.cs"));

            list.Add(new TableTemplate(Templates.Mapping)
                .Target(model, "Domain/Generated/{0}.hbm.xml")
                .As(ProjectWriter.EmbeddedResource));

            list.Add(new TableTemplate(Templates.IService)
                .Target(model, "Services/I{0}Service.cs")
                .SetOverwrite(false));

            list.Add(new TableTemplate(Templates.Validator)
                .Target(model, "Validators/{0}Validator.cs")
                .SetOverwrite(false));

            list.Add(new TableTemplate(Templates.Service)
               .Target(server, "Services/{0}Service.cs")
               .SetOverwrite(false));
            return list;
        }

        private IList<DbTable> GetTables()
        {
            using (Context.Development)
            {
                var config = Simply.Do.GetConfig<ApplicationConfig>();
                var db = new DbSchema(config.ADOProvider, Simply.Do.GetConnectionString());
                var names = new TableNameTransformer(Options.Do.TableNames).Transform(TableNames);

                var ret = db.GetTables(names.Included, names.Excluded).ToList();

                Simply.Do.Log(this).InfoFormat("Found tables: {0}", string.Join(", ", ret.Select(x => x.Name).ToArray()));
                return ret;
            }
        }


    }
}
