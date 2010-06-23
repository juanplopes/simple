using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Runners;
using System.IO;
using Antlr.StringTemplate;

namespace Sample.Project.Generator
{
    public static class Generators
    {
        public static GeneratorResolver Define(this GeneratorResolver registry)
        {
            registry.Register<NewMigrationGenerator>("new migration");

            return registry;
        }

        public static StringTemplate ToTemplate(this string template)
        {
            return new StringTemplate(template);
        }
    }
}
