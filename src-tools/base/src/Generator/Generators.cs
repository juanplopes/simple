using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Templates;
using System.IO;
using NVelocity;
using NVelocity.App;
using Simple.NVelocity;
using Sample.Project.Generator.Infra;
using Sample.Project.Generator.Migrations;
using Simple.Generator.Console;

namespace Sample.Project.Generator
{
    public static class Generators
    {
        public static GeneratorResolver Define(this GeneratorResolver registry, bool enableContextCommands)
        {
            //Migrations
            registry.Register<NewMigrationTemplate>("new migration")
                .WithArgument("name", x => x.Name);
            
            registry.Register<MigrateTool>("migrate")
                .WithOption("to", x => x.Version);

            //Table generators
            registry.Register<ScaffoldGenerator>("scaffold").AsTableGenerator();

            registry.Register<ServiceInterfaceTemplate>("g service interface").AsTableGenerator();
            registry.Register<ServiceImplTemplate>("g service impl").AsTableGenerator();
            registry.Register<EntityTemplate>("g entity").AsTableGenerator();
            registry.Register<ValidatorTemplate>("g validator").AsTableGenerator();
            registry.Register<MappingTemplate>("g mapping").AsTableGenerator();


            if (enableContextCommands)
            {
                registry.Register(() => new ExitCommand(Program.Manager), "@exit", "@quit");
                registry.Register(() => new SetContextCommand(Program.Manager), "@set")
                    .WithArgument("new context", x => x.NewContext);

                registry.Register(() => new ListContextsCommand(Program.Manager), "@list");
            }

            return registry;
        }

        public static GeneratorOptions<T> AsTableGenerator<T>(this InitialGeneratorOptions<T> generator)
            where T : TableTemplate
        {
            return generator
                .WithArgumentList("tables", x => x.TableNames)
                .WithOption("delete", x => x.DeleteFlag);
        }

        public static SimpleTemplate ToTemplate(this IGenerator generator)
        {
            var type = generator.GetType();
            var asm = type.Assembly;

            using (var stream = asm.GetManifestResourceStream(string.Format("{0}.txt", type.FullName)))
            {
                if (stream == null) throw new GeneratorException(string.Format("Couldn't find template for '{0}'...", type.FullName));
                return new SimpleTemplate(new StreamReader(stream).ReadToEnd());
            }
        }
    }
}
