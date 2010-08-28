using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using log4net;
using Simple;
using System.Reflection;
using Simple.Generator.Console;
using Example.Project.Tools.Templates;
using Example.Project.Tools.Templates.AutoContracts;

namespace Example.Project.Tools.Macros
{
    public class RefreshMacro : ICommand
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        
        public void Execute()
        {
            new AutoContractsTemplate().Execute();
        }
    }
}
