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
        public string Namespace
        {
            get { return Configurator.DefaultNamespace; }
        }
        public string ContractsAssembly
        {
            get { return Namespace + ".Contracts"; }
        }
        public string ServerAssembly
        {
            get { return Namespace + ".Server"; }
        }
        public bool LazyLoad
        {
            get { return true; }
        }

        public Conventions Conventions
        {
            get { return new Conventions(); }
        }

        public ProjectFileWriter DatabaseProject
        {
            get { return new ProjectFileWriter("src/Database/??_Database.csproj"); }
        }
        public ProjectFileWriter ContractsProject
        {
            get { return new ProjectFileWriter("src/Contracts/??_Contracts.csproj"); }
        }

        public string SolutionFile
        {
            get { return FileLocator.ByPattern("src/*.sln"); }
        }

        public string ServerDirectory
        {
            get { return "src/Server"; }
        }

        public ProjectFileWriter ServerProject
        {
            get { return new ProjectFileWriter(Path.Combine(ServerDirectory, "/??_Server.csproj")); }
        }
        public ProjectFileWriter WebProject
        {
            get { return new ProjectFileWriter("src/Web/??_Web.csproj"); }
        }


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
