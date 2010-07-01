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

namespace Sample.Project.Tools.Macros
{
    public class PrepareMacro : ICommand
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        
        public void Execute()
        {
            logger.Info("Migrating...");
            new MigrateTool() { WithDevelopment = true, WithTest = false, Version = null }.Execute();
            new MigrateTool() { WithDevelopment = false, WithTest = true, Version = 1 }.Execute();
            new MigrateTool() { WithDevelopment = false, WithTest = true, Version = null }.Execute();
            
            logger.Info("Executing...");
            new InsertDataCommand().Execute();
        }
    }
}
