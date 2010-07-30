using Simple.Generator;
using Sample.Project.Tools.Templates;
using Sample.Project.Tools.Macros;
using Sample.Project.Tools.ResetDb;
using Sample.Project.Tools.Templates.Scaffold;
using Sample.Project.Tools.Templates.View;
using Sample.Project.Tools.Database;

namespace Sample.Project.Tools
{
    public static class Generators
    {
        public static CommandResolver Define(this CommandResolver registry, bool production)
        {
            registry.Register<MigrateTool>("migrate")
                .WithOption("to", x => x.Version)
                .WithOption("dry", x => x.DryRun)
                .WithOption("script", x => x.FilePath)
                .WithOption("env", x => x.Environment);

            registry.Register<InsertDataCommand>("data").WithOption("testdata", x => x.ForceTestData);
            registry.Register<PrepareMacro>("prepare");

            if (production)
            {
                registry.Register<TestPrepareMacro>("testprepare");
            }
            else
            {
                registry.Register<ResetDbCommand>("resetdb")
                    .WithOption("prepare", x => x.Prepare);

                registry.Register<NewMigrationTemplate>("new migration")
                    .WithArgument("name", x => x.Name)
                    .WithOption("donotopen", x => x.DoNotOpen);

                registry.Register<MagicMacro>("magic");

                registry.Register<ScaffoldGenerator>("scaffold")
                    .WithArgumentList("tables", x => x.TableNames);

                registry.Register<ScaffoldRemover>("d scaffold")
                   .WithArgumentList("class_names", x => x.ClassNames);

                registry.Register<ViewGenerator>("view")
                    .WithArgumentList("class_names", x => x.ClassNames);


                registry.Register<RefreshMacro>("refresh");
            }

            return registry;
        }

     
    }
}
