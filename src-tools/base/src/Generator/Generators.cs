using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Runners;
using System.IO;
using NVelocity;
using NVelocity.App;
using Simple.NVelocity;

namespace Sample.Project.Generator
{
    public static class Generators
    {
        public static GeneratorResolver Define(this GeneratorResolver registry)
        {
            registry.Register<NewMigrationGenerator>("g migration");

            registry.Register<ScaffoldGenerator>("scaffold").AsTableGenerator();
            registry.Register<EntityGenerator>("g entity").AsTableGenerator();
            registry.Register<MappingGenerator>("g mapping").AsTableGenerator();

            return registry;
        }

        public static GeneratorOptions<T> AsTableGenerator<T>(this InitialGeneratorOptions<T> generator)
            where T : BaseTableGenerator
        {
            return generator
                .WithArgumentList("tables", x => x.TableNames)
                .WithOption("delete", x => x.DeleteFlag);
        }

        public static SimpleTemplate ToTemplate(this string template)
        {
            return new SimpleTemplate(template);
        }
    }
}
