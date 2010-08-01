using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using log4net;
using Simple;
using System.Reflection;
using Example.Project.Tools.Infra;
using Simple.Generator.Console;
using Example.Project.Tools.Templates;

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
