using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;
using Simple.NVelocity;
using Simple.Metadata;
using System.Collections;
using Sample.Project.Environment;
using Simple;
using Sample.Project.Config;

namespace Sample.Project.Tools
{
    public static class Default
    {
        public const string Namespace = Configurator.DefaultNamespace;
        public const string ContractsAssembly = Namespace + ".Contracts";
        public const string ServerAssembly = Namespace + ".Server";
        public const bool LazyLoad = true;

        public const string ToolsProject = "src/Tools/??_Tools.csproj";
        public const string ContractsProject = "src/Contracts/??_Contracts.csproj";
        public const string ServerProject = "src/Server/??_Server.csproj";

        public static IEnumerable<string> TableNames
        {
            get
            {
                yield return "%";
                yield return "-" + Simply.Do.GetConfig<ApplicationConfig>().SchemaInfoTable;
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
            return template.SetMany(new
            {
                re = re,
                table = table,
                lazyload = LazyLoad,
                @namespace = Namespace,
                contracts = ContractsAssembly,
                classname = re.NameFor(table),
                count = new Func<IEnumerable, int>(x => x.Cast<object>().Count()),
            });
        }

    }
}
