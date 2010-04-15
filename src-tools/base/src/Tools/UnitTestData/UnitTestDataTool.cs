using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Sample.Project.Environment;
using Simple.IO;
using Sample.Project.Tools;

namespace Sample.Project.Tools.UnitTestData
{
    public class UnitTestDataTool : DataToolRunner
    {
        protected override string DefaultEnvironment
        {
            get { return Configurator.Test; }
        }

        protected override void RunSamples(CommandLineReader cmd)
        {
            //insert here your sample data to populate unit test database
        }
    }
}
