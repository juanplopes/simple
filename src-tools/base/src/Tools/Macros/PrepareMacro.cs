using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using log4net;
using Simple;
using System.Reflection;
using Example.Project.Environment;
using Example.Project.Tools.Database;

namespace Example.Project.Tools.Macros
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
