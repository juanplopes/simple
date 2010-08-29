using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;
using Simple.NVelocity;
using Simple.Metadata;
using System.Collections;
using Example.Project.Config;
using Simple;
using Example.Project.Domain;
using Simple.Patterns;
using Simple.IO;

namespace Example.Project.Tools
{
    public class Options : Singleton<Options>
    {
        public ProjectDescription Model { get { return Project("Model"); } }
        public ProjectDescription Server { get { return Project("ModelServer"); } }
        public ProjectDescription Database { get { return Project("Database"); } }
        public ProjectDescription Web { get { return Project("Web"); } }

        public bool LazyLoad { get { return true; } }

        public string Namespace
        {
            get { return Configurator.DefaultNamespace; }
        }

        private static ProjectDescription Project(string name)
        {
            return new ProjectDescription("src/{0}", "{0}.csproj", Configurator.DefaultNamespace + ".{0}").WithName(name);
        }


        public ProjectDescription Solution
        {
            get
            {
                return new ProjectDescription("src", "{0}.sln", "{0}")
                    .WithName(Configurator.DefaultNamespace);
            }
        }

        public Conventions Conventions { get { return new Conventions(); } }

        public IEnumerable<string> TableNames
        {
            get
            {
                yield return "%";
                yield return "-" + Simply.Do.GetConfig<ApplicationConfig>().SchemaInfoTable;
            }
        }



    }
}
