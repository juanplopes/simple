using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Infra;
using System.Diagnostics;

namespace Sample.Project.Generator.Contexts
{
    public class ExitCommand : IGenerator
    {
        #region IGenerator Members

        public void Execute()
        {
            if (Program.Manager.PromptContext == null)
                System.Environment.Exit(0);
            else
                Program.Manager.PromptContext = null;
          
        }

        #endregion
    }
}
