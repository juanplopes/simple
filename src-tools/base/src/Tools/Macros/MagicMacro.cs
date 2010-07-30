using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Tools.Infra;
using Sample.Project.Tools.Templates.Scaffold;
using Microsoft.Build.BuildEngine;

namespace Sample.Project.Tools.Macros
{
    public class MagicMacro : ICommand
    {
        #region ICommand Members

        public void Execute()
        {
            new PrepareMacro().Execute();
            new ScaffoldGenerator().Execute();
        }

        #endregion
    }
}
