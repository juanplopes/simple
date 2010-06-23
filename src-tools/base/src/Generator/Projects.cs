using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;

namespace Sample.Project.Generator
{
    public static class Projects
    {
        public const string Tools = "src/Tools/??_Tools.csproj";
        public const string Contracts = "src/Contracts/??_Contracts.csproj";

        public static ProjectFileWriter Get(this string str)
        {
            var file = Directory.GetFiles(".", str).First();
            return new ProjectFileWriter(file);
        }
    }
}
