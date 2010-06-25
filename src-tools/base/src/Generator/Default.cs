using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;
using Simple.NVelocity;
using Simple.Metadata;

namespace Sample.Project.Generator
{
    public static class Default
    {
        public const string DefaultNamespace = "Sample.Project";
        public const bool LazyLoad = true;
        public const string ToolsProject = "src/Tools/??_Tools.csproj";
        public const string ContractsProject = "src/Contracts/??_Contracts.csproj";

        public static IEnumerable<string> TableNames
        {
            get
            {
                yield return "%";
                yield return "-SchemaInfo";
            }
        }

        public static Conventions Convention
        {
            get { return new Conventions(); }
        }

        public static ProjectFileWriter Writer(this string str)
        {
            var file = Directory.GetFiles(".", str).First();
            return new ProjectFileWriter(file).AutoCommit();
        }

        public static SimpleTemplate SetDefaults(this SimpleTemplate template, DbTable table)
        {
            var re = Convention;
            template["re"] = re;
            template["table"] = table;
            template["lazyload"] = Default.LazyLoad;
            template["namespace"] = Default.DefaultNamespace;
            template["classname"] = re.NameFor(table);
            return template;
        }

    }
}
