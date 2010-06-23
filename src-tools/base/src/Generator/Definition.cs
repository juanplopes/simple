using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Runners;

namespace Sample.Project.Generator
{
    public static class Definition
    {
        public static GeneratorResolver Define(this GeneratorResolver registry)
        {
            registry.Register(() => new NewMigrationGenerator(), "new migration");

            return registry;
        }
    }
}
