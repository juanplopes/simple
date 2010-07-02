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
    public class TestPrepareMacro : ICommand
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        
        public void Execute()
        {
            logger.Info("Migrating...");
            new MigrateTool() { Version = 1 }.Execute();
            new MigrateTool() { Version = null }.Execute();

            
            logger.Info("Executing...");
            new InsertDataCommand { ForceTestData = true }.Execute();
        }
    }
}
