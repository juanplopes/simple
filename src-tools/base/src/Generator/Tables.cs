using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Project.Generator
{
    public static class Tables
    {
        public static IEnumerable<string> Default()
        {
            yield return "%";
            yield return "-SchemaInfo";
        }

    }
}
