using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Tools.Migrations;
using Sample.Project.Tools.Data;
using log4net;
using Simple;
using System.Reflection;
using Sample.Project.Environment;

namespace Sample.Project.Tools.Macros
{
    public class PrepareMacro : ICommand
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());

        public void Execute()
        {
            logger.Info("Migrating...");
            if (!Configurator.IsProduction)
            {
                new MigrateTool() { Environment = Configurator.Development, Version = null }.Execute();
                new MigrateTool() { Environment = Configurator.Test, Version = 1 }.Execute();
                new MigrateTool() { Environment = Configurator.Test, Version = null }.Execute();
            }
            else
            {
                new MigrateTool().Execute();
            }

            logger.Info("Executing...");
            new InsertDataCommand().Execute();
        }
    }
}
